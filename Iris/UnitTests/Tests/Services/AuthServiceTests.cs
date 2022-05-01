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
        private const string Token = "GCAC4UPWTN6MS552";

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

            _authService = new AuthService(_userService.Object, _config);
        }

        [TearDown]
        public void CleanUp()
        {
            // nothing
        }

        [TestCase(UserName, Password)]
        public void Authorize_RightData_GetClaims(string login, string password)
        {
            var authenticator = new TwoStepsAuthenticator.TimeAuthenticator();
            var code = authenticator.GetCode(Token);
            var (claims, _) = _authService.Authorize(new AuthRequestOperation(), new AuthRequestContract { Password = password, KeyPassword = code, Login = login });

            Assert.IsNotNull(claims);
        }

        [TestCase(UserName, Password + "1")]
        [TestCase(UserName + "1", Password)]
        [TestCase(UserName + "1", Password + "1")]
        public void Authorize_BadPasswordOrUsername_GetAuthException(string login, string password)
        {
            var authenticator = new TwoStepsAuthenticator.TimeAuthenticator();
            var code = authenticator.GetCode(Token);
            void TestDelegate() => _authService.Authorize(new AuthRequestOperation(), new AuthRequestContract { Password = password, KeyPassword = code, Login = login });

            Assert.Throws<AuthException>(TestDelegate);
        }

        [TestCase(UserName, Password)]
        public void Authorize_BadCode_GetAuthException(string login, string password)
        {
            var authenticator = new TwoStepsAuthenticator.TimeAuthenticator();
            var code = "000000";
            void TestDelegate() => _authService.Authorize(new AuthRequestOperation(), new AuthRequestContract { Password = password, KeyPassword = code, Login = login });

            Assert.Throws<AuthException>(TestDelegate);
        }

        [Test]
        public void GenerateToken_Data_TokenNotNull()
        {
            var authenticator = new TwoStepsAuthenticator.TimeAuthenticator();
            var code = authenticator.GetCode(Token);
            var (claims, user) = _authService.Authorize(new AuthRequestOperation(), new AuthRequestContract { Password = Password, KeyPassword = code, Login = UserName });

            var (encodedJwt, _) = _authService.GenerateToken(claims.Claims, user);

            Assert.IsFalse(string.IsNullOrEmpty(encodedJwt));
        }
    }
}
