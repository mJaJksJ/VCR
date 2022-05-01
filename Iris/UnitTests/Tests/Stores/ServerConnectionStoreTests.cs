using Iris.Common.Enums;
using Iris.Database;
using Iris.Services.ConnectionProtocolHelperService;
using Iris.Stores.ServiceConnectionStore;
using MailKit;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnitTests.Database;

namespace UnitTests.Tests.Stores
{
    [TestFixture]
    public class ServerConnectionStoreTests
    {
        private const string UserName = "user";
        private const string Imap = "Imap";
        private const string Host = "host";
        private const bool Ssl = true;
        private const string Password = "password";
        private const int Port = 1;
        private IServerConnectionStore _serverConnectionStore;
        private readonly DatabaseContext _dbContext = TestDatabase.Instance;
        private int _userId;
        private int _accountId;
        private const string ExsistAccountName = "AccName";
        private Mock<IConnectionProtocolHelperService> _connectionProtocolHelperService;
        private Mock<IMailService> _mailService;

        [SetUp]
        public void SetUp()
        {
            var user = _dbContext.Users.Add(new User
            {
                Name = UserName,
            });
            _dbContext.SaveChanges();
            _userId = user.Entity.Id;

            var server = _dbContext.MailServers.Add(new MailServer
            {
                Host = Host,
                Port = Port,
            });
            _dbContext.SaveChanges();

            var acc = _dbContext.Accounts.Add(new Account
            {
                Name = ExsistAccountName,
                UserId = _userId,
                Password = Password,
                MailServerId = server.Entity.Id,
                UseSsl = Ssl,
                ConnectionProtocol = Imap
            });
            _dbContext.SaveChanges();
            _accountId = acc.Entity.Id;

            _mailService = new Mock<IMailService>();
            _mailService.Setup(_ => _.Connect(Host, Port, Ssl, CancellationToken.None));
            _mailService.Setup(_ => _.Authenticate(ExsistAccountName, Password, CancellationToken.None));

            _connectionProtocolHelperService = new Mock<IConnectionProtocolHelperService>();
            _connectionProtocolHelperService.Setup(_ => _.ByString(Imap)).Returns(ConnectionProtocol.Imap);
            _connectionProtocolHelperService.Setup(_ => _.GetConnection(ConnectionProtocol.Imap))
                .Returns(_mailService.Object);


            _serverConnectionStore = new ServerConnectionStore(_dbContext, _connectionProtocolHelperService.Object);
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
        public void AddUserConnection_NewConnection_AddingConnections()
        {
            _serverConnectionStore.AddUserConnection(_userId, new ServerConnection(_mailService.Object)
            {
                Account = new Account()
            });

            var count = _serverConnectionStore.GetUserConnections(_userId).Count();

            Assert.AreEqual(2, count);
        }

        [Test]
        public void GetUserConnections_Non_Connections()
        {
            var count = _serverConnectionStore.GetUserConnections(_userId).Count();

            Assert.AreEqual(1, count);
        }

        [Test]
        public void GetUserConnections_AccIds_Connections()
        {
            var count = _serverConnectionStore.GetUserConnections(_userId, new List<int> { _accountId }).Count();

            Assert.AreEqual(1, count);
        }

        [Test]
        public void GetUserConnection_Data_COnnection()
        {
            var connection = _serverConnectionStore.GetUserConnection(_userId, _accountId);

            Assert.IsTrue(connection.Account.Id == _accountId && connection.Account.UserId == _userId);
        }

        [Test]
        public void RemoveConnection_Data_RemovingConnections()
        {
            _serverConnectionStore.RemoveConnection(_userId, _accountId);
            var count = _serverConnectionStore.GetUserConnections(_userId).Count();

            Assert.AreEqual(0, count);
        }
    }
}
