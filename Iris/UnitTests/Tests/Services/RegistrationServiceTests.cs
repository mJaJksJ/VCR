using Iris.Api.Controllers.RegistrationControllers;
using Iris.Database;
using Iris.Services.RegistrationService.cs;
using Iris.Services.UserService;
using Moq;
using NUnit.Framework;
using UnitTests.Database;

namespace UnitTests.Tests.Services
{
    public class RegistrationServiceTests
    {
        private const string UserName = "user";
        private const string NotUserName = "nonuser";

        private readonly DatabaseContext _dbContext = TestDatabase.Instance;
        private IRegistrationService _registrationService;
        private Mock<IUserService> _userService;

        [SetUp]
        public void SetUp()
        {
            _userService = new Mock<IUserService>();
            _userService.Setup(_ => _.GetUserByLogin(NotUserName)).Returns((User)null);
            _userService.Setup(_ => _.GetUserByLogin(UserName)).Returns(new User { Name = UserName });

            _registrationService = new RegistrationService(_userService.Object, _dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Users.RemoveRange(_dbContext.Users);

            _dbContext.SaveChanges();
        }

        [Test]
        public void RegisterUser_UserName_NotRegistred()
        {
            var response = _registrationService.RegisterUser(new RegistrationRequestContract
            {
                Name = UserName
            });

            Assert.IsFalse(response.IsSucces);
        }

        [Test]
        public void RegisterUser_NotUserName_Registred()
        {
            var response = _registrationService.RegisterUser(new RegistrationRequestContract
            {
                Name = NotUserName
            });

            Assert.IsTrue(response.IsSucces);
        }
    }
}
