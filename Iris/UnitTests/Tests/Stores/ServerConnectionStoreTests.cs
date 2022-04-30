using Iris.Stores.ServiceConnectionStore;
using NUnit.Framework;
using UnitTests.Database;

namespace UnitTests.Tests.Stores
{
    [TestFixture]
    public class ServerConnectionStoreTests
    {
        private IServerConnectionStore _serverConnectionStore;

        [SetUp]
        public void SetUp()
        {
            _serverConnectionStore = new ServerConnectionStore(TestDatabase.Instance);
        }
    }
}
