using Iris.Api.Results;
using Iris.Database;
using Iris.Services.AuthService;
using Iris.Services.ClaimsPrincipalHelperService;
using Iris.Stores;
using Iris.Stores.AuthRequestStore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Iris.Api.Controllers.AuthControllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IAuthRequestsStore _authRequestsStore;
        private readonly IAuthService _authService;
        private readonly TokensStore _tokensStore;
        private readonly IClaimsPrincipalHelperService _claimsPrincipalHelperService;

        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<AuthController>();

        /// <summary>
        /// .ctor
        /// </summary>
        public AuthController(IAuthRequestsStore authRequestsStore,
            IAuthService authService, TokensStore tokensStore,
            IClaimsPrincipalHelperService claimsPrincipalHelperService)
        {
            _authRequestsStore = authRequestsStore;
            _authService = authService;
            _tokensStore = tokensStore;
            _claimsPrincipalHelperService = claimsPrincipalHelperService;
        }

        /// <summary>
        /// Инициализировать авторизацию
        /// </summary>
        /// <returns>Id запроса авторизации</returns>
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

        /// <summary>
        /// Выполнить авторизацию
        /// </summary>
        /// <param name="id">Id запроса авторизации</param>
        /// <param name="authRequest">Контракт запроса авторизации</param>
        /// <returns>Контракт ответа авторизации</returns>
        [HttpPut("~/api/authorize/{id}"), AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponseContract), 200)]
        public IActionResult ExecuteAuthorization(string id, [FromBody] AuthRequestContract authRequest)
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
                (identity, user) = _authService.Authorize(operationRequest, authRequest);
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

            return Ok(new AuthResponseContract
            {
                UserId = userIdVal,
                Login = identity.Name,
                Token = token,
                Roles = roles,
                TokenType = JwtBearerDefaults.AuthenticationScheme,
            });
        }

        /// <summary>
        /// Де-авторизация
        /// </summary>
        [HttpPost("~/api/authorize/deauth")]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult DeAuth()
        {
            var userId = _claimsPrincipalHelperService.GetUserId(User);

            _tokensStore.Remove(userId.ToString());
            Log.Information($"User {userId} is de-auth");

            Response.Headers.Add("Clear-Site-Data", "\"cache\", \"cookies\", \"storage\"");

            return Ok();
        }

        /// <summary>
        /// Проверка авторизированности
        /// </summary>
        /// <returns></returns>
        [HttpGet("~/api/authorize/isauth"), AllowAnonymous]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult IsAuth()
        {
            return Ok(User.Identity.IsAuthenticated);
        }
    }
}
