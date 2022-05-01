using Iris.Api.Controllers.ConnectionsControllers;
using Iris.Database;
using Iris.Exceptions;
using Iris.Services.MailServersService;
using NUnit.Framework;
using System.Linq;
using UnitTests.Database;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class MailServersServiceTests
    {
        private const string ExsistAccountName = "AccName";
        private const string Host = "iris";
        private const int Port = 1;

        private readonly DatabaseContext _dbContext = TestDatabase.Instance;
        private int _userId;

        private IMailServersService _mailServersService;

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
                Host = Host,
                Port = Port,
            });
            _dbContext.SaveChanges();

            _dbContext.Accounts.Add(new Account
            {
                Name = ExsistAccountName,
                UserId = _userId,
                Password = "",
                MailServerId = server.Entity.Id
            });
            _dbContext.SaveChanges();

            _mailServersService = new MailServersService(_dbContext);
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
        public void GetMailServerAccounts_UserId_Accounts()
        {
            var servers = _mailServersService.GetMailServerAccounts(_userId);

            Assert.IsTrue(servers.Count() == 1);
        }

        [Test]
        public void GetAvailableMailServers_UserId_Accounts()
        {
            var servers = _mailServersService.GetAvailableMailServers(_userId);

            Assert.IsTrue(servers.Count() == 1);
        }

        [TestCase(Host + "1", Port + 1)]
        [TestCase(Host + "1", Port)]
        [TestCase(Host, Port + 1)]
        public void NewMailServer_NotExist_AddServer(string host, int port)
        {
            _mailServersService.NewMailServer(new MailServerContract
            {
                Host = host,
                Port = port
            });

            Assert.IsTrue(_dbContext.MailServers.Count() == 2);
        }

        [Test]
        public void NewMailServer_Exist_Exception()
        {

            Assert.Throws<ServerAlreadyExistException>(() => _mailServersService.NewMailServer(new MailServerContract
            {
                Host = Host,
                Port = Port
            }));
        }
    }
}
