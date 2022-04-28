using Iris.Database;
using Iris.Exceptions.UserExceptions;
using Iris.Helpers.DatabaseExtensions;
using Iris.Services.UserService;
using Moq;
using NUnit.Framework;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private const int ExistedUserId = 1;
        private const int NotExistedUserId = 2;
        private const string ExistedUserLogin = "admin";
        private const string NotExistedUserLogin = "notUser";

        private Mock<DatabaseContext> _databaseContext;
        private IUserService _userService;

        [SetUp]
        public void SetUp()
        {
            _databaseContext = new Mock<DatabaseContext>();
            _databaseContext.Setup(_ => _.GetUserById(ExistedUserId)).Returns(new User {Id = ExistedUserId });
            _databaseContext.Setup(_ => _.GetUserById(NotExistedUserId)).Returns((User)null);
            _databaseContext.Setup(_ => _.GetUserByLogin(ExistedUserLogin)).Returns(new User { Name = ExistedUserLogin });
            _databaseContext.Setup(_ => _.GetUserByLogin(NotExistedUserLogin)).Returns((User)null);

            _userService = new UserService(_databaseContext.Object);
        }

        [TestCase(ExistedUserId)]
        public void EnsureUserExist_ExistedId_NotException(int id)
        {
            Assert.DoesNotThrow(() => _userService.EnsureUserExist(id));
        }

        [TestCase(NotExistedUserId)]
        public void EnsureUserExist_NotExistedId_Exception(int id)
        {
            Assert.Throws<UserNotExistException>(() => _userService.EnsureUserExist(id));
        }

        [TestCase(ExistedUserLogin)]
        public void GetUserByLogin_Existedlogin_NotException(string login)
        {
            Assert.DoesNotThrow(() => _userService.GetUserByLogin(login));
        }

        [TestCase(NotExistedUserLogin)]
        public void GetUserByLogin_NotExistedLogin_Exception(string login)
        {
            Assert.Throws<UserNotExistException>(() => _userService.GetUserByLogin(login));
        }
    }
}
