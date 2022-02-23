using Iris.Configuration;
using Iris.Database;
using Iris.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Iris.Services.AuthService
{
    /// <inheritdoc cref="IAuthService"/>
    public class AuthService : IAuthService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Config _config;

        /// <summary>
        /// .ctor
        /// </summary>
        public AuthService(DatabaseContext context, Config config)
        {
            _databaseContext = context;
            _config = config;
        }

        /// <inheritdoc/>
        public (ClaimsIdentity, User) Authorize(AuthRequestOperation operation, string login, string password)
        {
            var user = _databaseContext.Users.SingleOrDefault(u => u.Name == login);

            if (user == null || password != user.Password)
            {
                throw new AuthException();
            }

            return (GenerateIdentity(user), user);
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
