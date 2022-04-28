using Iris.Configuration;
using Iris.Database;
using Iris.Exceptions;
using Iris.Services.AuthService;
using Iris.Services.UserService;
using Moq;
using NUnit.Framework;
using System.Text;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class AuthServiceTests
    {
        private const string UserName = "User1";
        private const string Password = "Password";

        private Config _config;
        private Mock<IUserService> _userService;

        private IAuthService _authService;

        [SetUp]
        public void SetUp()
        {
            _userService = new Mock<IUserService>();
            _userService.Setup(_ => _.GetUserByLogin(UserName)).Returns(new User { Name = "User1", Password = Password });

            _config = new Config
            {
                AuthConfig = new AuthConfig
                {
                    JwtSecurityKey = Encoding.ASCII.GetBytes("8u5j4WXfR74kDGE38k32zIBrLuDELjSTGzTx97OWwVY01-0uaayMdBlBWfZ55Fy8"),
                    JwtLifetime = 24 * 3600
                }
            };

            _authService = new AuthService(null, _config);
        }

        [TearDown]
        public void CleanUp()
        {
            // nothing
        }

        [TestCase(UserName, Password)]
        public void Authorize_RightData_GetClaims(string login, string password)
        {
            var (claims, user) = _authService.Authorize(new AuthRequestOperation(), login, password);

            Assert.IsNotNull(claims);
        }

        [TestCase(UserName + "1", Password)]
        [TestCase(UserName, "Password1")]
        [TestCase(UserName + "1", "Password1")]
        public void Authorize_BadData_GetAuthException(string login, string password)
        {
            TestDelegate testDelegate = () => _authService.Authorize(new AuthRequestOperation(), login, password);

            Assert.Throws<AuthException>(testDelegate);
        }

        [Test]
        public void GenerateToken_Data_TokenNotNull()
        {
            var (claims, user) = _authService.Authorize(new AuthRequestOperation(), UserName, Password);

            var (encodedJwt, expires) = _authService.GenerateToken(claims.Claims, user);

            Assert.IsFalse(string.IsNullOrEmpty(encodedJwt));
        }
    }
}
