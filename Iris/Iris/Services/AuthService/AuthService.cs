using Iris.Api.Controllers.AuthControllers;
using Iris.Configuration;
using Iris.Database;
using Iris.Exceptions;
using Iris.Services.UserService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Iris.Services.AuthService
{
    /// <inheritdoc cref="IAuthService"/>
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly Config _config;

        /// <summary>
        /// .ctor
        /// </summary>
        public AuthService(IUserService userService, Config config)
        {
            _userService = userService;
            _config = config;
        }

        /// <inheritdoc/>
        public (ClaimsIdentity, User) Authorize(AuthRequestOperation operation, AuthRequestContract authRequest)
        {
            var user = _userService.GetUserByLogin(authRequest.Login);

            var key = TwoStepsAuthenticator.Authenticator.GenerateKey();
            var secret = user.Token;
            var authenticator = new TwoStepsAuthenticator.TimeAuthenticator();
            bool isOk = authenticator.CheckCode(secret, authRequest.KeyPassword, user);

            if (user == null || authRequest.Password != user.Password || !isOk)
            {
                throw new AuthException();
            }

            return (GenerateIdentity(user), user);
        }

        /// <inheritdoc/>
        public (string token, DateTime expires) GenerateToken(IEnumerable<Claim> claims, User user)
        {
            var dtNow = DateTime.Now;
            var issueTime = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, dtNow.Minute, dtNow.Second);
            var expires = user.IsAdmin ? issueTime.AddDays(7) : issueTime.AddSeconds(_config.AuthConfig.JwtLifetime);
            var jwt = new JwtSecurityToken(
                _config.AuthConfig.JwtIssuer,
                _config.AuthConfig.JwtAudience,
                claims,
                issueTime,
                expires,
                new SigningCredentials(
                    new SymmetricSecurityKey(_config.AuthConfig.JwtSecurityKey),
                    "HS256"
                )
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return (encodedJwt, expires);
        }

        private static ClaimsIdentity GenerateIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(""))
            };

            return new ClaimsIdentity(claims);
        }
    }
}
