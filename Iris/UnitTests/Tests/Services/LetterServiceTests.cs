using System.Collections.Generic;
using Iris.Api.Controllers.LettersControllers;
using Iris.Services.LettersService;
using Iris.Stores.ServiceConnectionStore;
using Moq;
using NUnit.Framework;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class LetterServiceTests
    {
        private const int UserId = 1;

        private ILetterService _letterService;
        private Mock<IServerConnectionStore> _connectionStore;

        [SetUp]
        public void SetUp()
        {
            _connectionStore = new Mock<IServerConnectionStore>();
            _connectionStore.Setup(_ => _.GetUserConnections(UserId, null)).Returns(new List<ServerConnection>());

            _letterService = new LetterService(_connectionStore.Object, null);
        }

        [Test]
        public void GetLetters_Non_EmptyList()
        {
            var letters = _letterService.GetLetters(1, new LettersRequest());

            Assert.IsEmpty(letters);
        }

    }
}
