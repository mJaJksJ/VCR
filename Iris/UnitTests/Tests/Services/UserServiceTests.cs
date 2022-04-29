using System;
using System.Reflection;
using Iris.Attributes;
using Iris.Database;
using Iris.Services.UserService;
using Moq;
using NUnit.Framework;
using UnitTests.Helpers;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private static readonly Type ClassType = typeof(UserService);

        private Mock<DatabaseContext> _databaseContext;
        private IUserService _userService;

        [SetUp]
        public void SetUp()
        {
            _databaseContext = new Mock<DatabaseContext>();
            _userService = new UserService(_databaseContext.Object);
        }
        [Test]
        public void EnsureUserExist_Non_Succes()
        {
            try
            {
                _userService.EnsureUserExist(new int());
            }
            finally
            {
                if (CheckDbMethods.HasDbGetterDataAttribute(ClassType, "EnsureUserExist"))
                {
                    Assert.Pass();
                }
                Assert.Fail();
            }
        }

        [Test]
        public void GetUserByLogin_Non_Succes()
        {
            try
            {
                _userService.GetUserByLogin(string.Empty);
            }
            finally
            {
                if (CheckDbMethods.HasDbGetterDataAttribute(ClassType, "GetUserByLogin"))
                {
                    Assert.Pass();
                }
                Assert.Fail();
            }
        }
    }
}
