using Iris.Database;
using Iris.Exceptions.UserExceptions;
using Iris.Services.UserService;
using NUnit.Framework;
using System;
using UnitTests.Database;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private const string UserName = "user";

        private readonly DatabaseContext _dbContext = TestDatabase.Instance;
        private IUserService _userService;
        private int _userId;

        [SetUp]
        public void SetUp()
        {
            var user = _dbContext.Users.Add(new User
            {
                Name = UserName
            });
            _dbContext.SaveChanges();
            _userId = user.Entity.Id;

            _userService = new UserService(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Users.RemoveRange(_dbContext.Users);

            _dbContext.SaveChanges();
        }

        [Test]
        public void EnsureUserExist_ExistId_User()
        {
            var user = _userService.EnsureUserExist(_userId);

            Assert.AreEqual(_userId, user.Id);
        }

        [Test]
        public void EnsureUserExist_NotExistId_Exception()
        {
            Assert.Throws<UserNotExistException>(() => _userService.EnsureUserExist(_userId + 1));
        }

        [Test]
        public void GetUserByLogin_ExistLogin_User()
        {
            var user = _userService.GetUserByLogin(UserName);

            Assert.AreEqual(UserName, user.Name);
        }

        [Test]
        public void GetUserByLogin_NotExistLogin_Exception()
        {
            var user = _userService.GetUserByLogin(UserName + "1");

            Assert.IsNull(user);
        }
    }
}
