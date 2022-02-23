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

namespace Iris.Controllers.AuthController
{
    [Authorize]
    public class AuthController: Controller
    {
        private readonly IAuthRequestsStore _authRequestsStore;
        private readonly IAuthService _authService;
        private readonly TokensStore _tokensStore;

        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<AuthController>();

        public AuthController(IAuthRequestsStore authRequestsStore, IAuthService authService, TokensStore tokensStore)
        {
            _authRequestsStore = authRequestsStore;
            _authService = authService;
            _tokensStore = tokensStore;
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

            var (token, expires) = GenerateToken(identity.Claims, user);

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

        private (string token, DateTime expires) GenerateToken(IEnumerable<Claim> claims, User user)
        {
            var dtNow = DateTime.Now;
            var issueTime = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, dtNow.Minute, dtNow.Second);
            var expires = user.IsAdmin ? issueTime.AddDays(7) : issueTime.AddSeconds(600000);
            var jwt = new JwtSecurityToken(
                null,
                null,
                claims,
                issueTime,
                expires,
                new SigningCredentials(
                    new SymmetricSecurityKey(null),
                    "HS256"
                )
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return (encodedJwt, expires);
        }
    }
}
