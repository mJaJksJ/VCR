using Iris.Common.Enums;
using Iris.Exceptions;
using Iris.Services.ConnectionProtocolHelperService;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using NUnit.Framework;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class ConnectionProtocolHelperServiceTests
    {
        private IConnectionProtocolHelperService _connectionProtocolHelperService;

        [SetUp]
        public void SetUp()
        {
            _connectionProtocolHelperService = new ConnectionProtocolHelperService();
        }

        [Test]
        public void ByString_Imap_ConnectionProtocol()
        {
            var connectionProtocol = _connectionProtocolHelperService.ByString("Imap");

            Assert.AreEqual(ConnectionProtocol.Imap, connectionProtocol);
        }

        [Test]
        public void ByString_Pop3_ConnectionProtocol()
        {
            var connectionProtocol = _connectionProtocolHelperService.ByString("Pop3");

            Assert.AreEqual(ConnectionProtocol.Pop3, connectionProtocol);
        }

        [Test]
        public void ByString_BadData_Exception()
        {
            Assert.Throws<UnknownProtocolException>(() => _connectionProtocolHelperService.ByString(""));
        }

        [Test]
        public void GetConnection_Imap_ConnectionProtocol()
        {
            var connection = _connectionProtocolHelperService.GetConnection(ConnectionProtocol.Imap);

            Assert.IsTrue(connection is ImapClient);
        }

        [Test]
        public void GetConnection_Pop3_ConnectionProtocol()
        {
            var connection = _connectionProtocolHelperService.GetConnection(ConnectionProtocol.Pop3);

            Assert.IsTrue(connection is Pop3Client);
        }
    }
}
