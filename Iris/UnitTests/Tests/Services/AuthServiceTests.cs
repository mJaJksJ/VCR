using Iris.Api.Controllers.AuthControllers;
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
        private const string Token = "QJD51dd4c1W12WEN";

        private Config _config;
        private Mock<IUserService> _userService;

        private IAuthService _authService;

        [SetUp]
        public void SetUp()
        {
            _userService = new Mock<IUserService>();
            _userService.Setup(_ => _.GetUserByLogin(UserName)).Returns(new User { Name = UserName, Password = Password, Token = Token });

            _config = new Config
            {
                AuthConfig = new AuthConfig
                {
                    JwtSecurityKey = Encoding.ASCII.GetBytes("8u5j4WXfR74kDGE38k32zIBrLuDELjSTGzTx97OWwVY01-0uaayMdBlBWfZ55Fy8"),
                    JwtLifetime = 24 * 3600
                }
            };

            _authService = new AuthService(null, _config, _userService.Object);
        }

        [TearDown]
        public void CleanUp()
        {
            // nothing
        }

        [TestCase(UserName, Password, Token)]
        public void Authorize_RightData_GetClaims(string login, string password, string token)
        {
            var (claims, _) = _authService.Authorize(new AuthRequestOperation(), new AuthRequestContract { Password = password, KeyPassword = token, Login = login });

            Assert.IsNotNull(claims);
        }

        [TestCase(UserName + "1", Password, Token)]
        [TestCase(UserName, Password + "1", Token)]
        [TestCase(UserName + "1", "Password1", Token)]
        [TestCase(UserName, Password, Token + "1")]
        public void Authorize_BadData_GetAuthException(string login, string password, string token)
        {
            void TestDelegate() => _authService.Authorize(new AuthRequestOperation(), new AuthRequestContract { Password = password, KeyPassword = token, Login = login });

            Assert.Throws<AuthException>(TestDelegate);
        }

        [Test]
        public void GenerateToken_Data_TokenNotNull()
        {
            var (claims, user) = _authService.Authorize(new AuthRequestOperation(), new AuthRequestContract { Password = Password, KeyPassword = Token, Login = UserName });

            var (encodedJwt, _) = _authService.GenerateToken(claims.Claims, user);

            Assert.IsFalse(string.IsNullOrEmpty(encodedJwt));
        }
    }
}
