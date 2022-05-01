using Iris.Database;
using Iris.Stores.AuthRequestStore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using UnitTests.Database;

namespace UnitTests.Tests.Stores
{
    [TestFixture]
    public class AuthRequestsStoreTests
    {
        private Mock<IServiceScopeFactory> _scopeFactory;
        private IAuthRequestsStore _authRequestsStore;

        [SetUp]
        public void SetUp()
        {
            _scopeFactory = new Mock<IServiceScopeFactory>();

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(_ => _.GetService(typeof(DatabaseContext))).Returns(TestDatabase.Instance);

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(_ => _.ServiceProvider).Returns(serviceProvider.Object);

            _scopeFactory.Setup(_ => _.CreateScope()).Returns(serviceScope.Object);

            _authRequestsStore = new AuthRequestsStore(_scopeFactory.Object);
        }

        [Test]
        public void CreateRequest_Non_Request()
        {
            var request = _authRequestsStore.CreateRequest();

            Assert.IsNotNull(request);
        }

        [Test]
        public void FindRequest_Non_Request()
        {
            var request = _authRequestsStore.CreateRequest();

            var foundRequest = _authRequestsStore.FindRequest(request.Id);

            var expected = request.Id;
            var actual = foundRequest.Id;

            Assert.AreEqual(expected, actual);
        }
    }
}
