using Iris.Api.Controllers.AccountsControllers;
using Iris.Database;
using Iris.Exceptions;
using Iris.Services.AccountsService;
using Iris.Stores.ServiceConnectionStore;
using Moq;
using NUnit.Framework;
using System;
using UnitTests.Database;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    internal class AccountsServiceTests
    {
        private const string ExsistAccountName = "AccName";

        private readonly DatabaseContext _dbContext = TestDatabase.Instance;
        private Mock<IServerConnectionStore> _serverConnectionStore;
        private int _userId;
        private int _mailServerId;
        private int _accountId;

        private IAccountsService _accountsService;

        [SetUp]
        public void SetUp()
        {
            var user = _dbContext.Users.Add(new User
            {
                Name = "user"
            });
            _dbContext.SaveChanges();
            _userId = user.Entity.Id;

            var server = _dbContext.MailServers.Add(new MailServer
            {
                Host = "",
                Port = 1,
            });
            _dbContext.SaveChanges();
            _mailServerId = server.Entity.Id;

            var acc = _dbContext.Accounts.Add(new Account
            {
                Name = ExsistAccountName,
                UserId = _userId,
                Password = "",
                MailServerId = server.Entity.Id
            });
            _dbContext.SaveChanges();
            _accountId = acc.Entity.Id;

            _serverConnectionStore = new Mock<IServerConnectionStore>();
            _serverConnectionStore.Setup(_ => _.AddConnection(acc.Entity)).Returns(Guid.Empty);
            _serverConnectionStore.Setup(_ => _.RemoveConnection(_userId, _accountId));

            _accountsService = new AccountsService(_dbContext, _serverConnectionStore.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Accounts.RemoveRange(_dbContext.Accounts);
            _dbContext.MailServers.RemoveRange(_dbContext.MailServers);
            _dbContext.Users.RemoveRange(_dbContext.Users);

            _dbContext.SaveChanges();
        }

        [Test]
        public void AddNewAccount_ExistAccount_Exception()
        {
            Assert.Throws<AccountAlreadyExistException>(() => _accountsService.AddNewAccount(_userId, new AccountRequestContract { Name = ExsistAccountName }));
        }

        [Test]
        public void AddNewAccount_NotExistAccount_NotException()
        {
            Assert.DoesNotThrow(() => _accountsService.AddNewAccount(_userId, new AccountRequestContract { Name = ExsistAccountName + "1", MailServerId = _mailServerId, Password = "1" }));
        }

        [Test]
        public void RemoveAccount_AccData_RemovingAccount()
        {
            _accountsService.RemoveAccount(_userId, _accountId);
        }
    }
}
