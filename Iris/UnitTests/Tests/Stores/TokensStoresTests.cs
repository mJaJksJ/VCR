using Iris.Stores.TokensStore;
using NUnit.Framework;

namespace UnitTests.Tests.Stores
{
    [TestFixture]
    public class TokensStoresTests
    {
        private ITokensStore _store;

        [SetUp]
        public void SetUp()
        {
            _store = new TokensStore();
        }

        [Test]
        public void AddOrUpdate_Add_AddedToken()
        {
            var userId = 1;
            var token = "1";
            _store.AddOrUpdate(userId.ToString(), token);

            Assert.IsTrue(_store.Exists(token));
        }

        [Test]
        public void AddOrUpdate_Update_UpdatedToken()
        {
            var userId = 1;
            var token = "1";
            _store.AddOrUpdate(userId.ToString(), token);
            var newToken = "2";
            _store.AddOrUpdate(userId.ToString(), newToken);

            Assert.IsTrue(!_store.Exists(token) && _store.Exists(newToken));
        }

        [Test]
        public void Remove_Remove_NoToken()
        {
            var userId = 1;
            var token = "1";
            _store.Remove(userId.ToString());

            Assert.IsTrue(!_store.Exists(token));
        }
    }
}
