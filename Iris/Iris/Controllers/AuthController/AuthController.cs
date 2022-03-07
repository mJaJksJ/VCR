using Iris.Api;
using Iris.Database;
using Iris.Services.AuthService;
using Iris.Stores.AuthRequestStore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Serilog;
using Iris.Api.Results;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Iris.Stores;
using Iris.Configuration;
using Iris.Helpers;

namespace Iris.Controllers.AuthController
{
    [Authorize]
    public class AuthController: Controller
    {
        private readonly IAuthRequestsStore _authRequestsStore;
        private readonly IAuthService _authService;
        private readonly TokensStore _tokensStore;
        private readonly Config _config;

        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<AuthController>();

        public AuthController(IAuthRequestsStore authRequestsStore, IAuthService authService, TokensStore tokensStore, Config config)
        {
            _authRequestsStore = authRequestsStore;
            _authService = authService;
            _tokensStore = tokensStore;
            _config = config;
        }


        [HttpPost("~/api/authorize"), AllowAnonymous]
        [ProducesResponseType(typeof(int), 201)]
        public IActionResult InitAuth()
        {
            var request = _authRequestsStore.CreateRequest();
            return Created(
                $"/api/authorize/{Uri.EscapeDataString(request.Id)}",
                request.Id
            );
        }

        [HttpPut("~/api/authorize/{id}"), AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponse), 200)]
        public IActionResult ExecuteAuthorization(string id, [FromBody] AuthRequest authRequest)
        {
            var operationRequest = _authRequestsStore.FindRequest(id);

            if (operationRequest == null)
            {
                return new AuthErrorResult(message: "Ошибка авторизации");
            }

            ClaimsIdentity identity;
            User user;

            try
            {
                (identity, user) = _authService.Authorize(operationRequest, authRequest.Login, authRequest.Password);
            }
            catch
            {
                Log.Error($"Ошибка авторизации пользователя {authRequest.Login}");
                throw;
            }

            if (identity == null)
            {
                Log.Error($"Ошибка авторизации пользователя {authRequest.Login}");
                return new AuthErrorResult();
            }

            var (token, expires) = _authService.GenerateToken(identity.Claims, user);

            var roles = identity.Claims.Where(_ => _.Type == identity.RoleClaimType)
                .Select(_ => _.Value).ToList();

            var userIdVal = identity.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sid).Value;
            var userId = int.Parse(userIdVal);

            _tokensStore.AddOrUpdate(userId.ToString(), token);

            Log.Information($"Пользователь {user.Name} авторизован");

            return Ok(new AuthResponse
            {
                UserId = userIdVal,
                Login = identity.Name,
                Token = token,
                Roles = roles,
                TokenType = JwtBearerDefaults.AuthenticationScheme,
            });
        }

        [HttpPost("~/api/authorize/deauth")]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult DeAuth()
        {
            var userId = User.GetUserId();

            _tokensStore.Remove(userId.ToString());
            Log.Information($"User {userId} is de-auth");

            Response.Headers.Add("Clear-Site-Data", "\"cache\", \"cookies\", \"storage\"");

            return Ok();
        }

        [HttpGet("~/api/authorize/isauth"), AllowAnonymous]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult IsAuth()
        {
            return Ok(User.Identity.IsAuthenticated);
        }
    }
}
