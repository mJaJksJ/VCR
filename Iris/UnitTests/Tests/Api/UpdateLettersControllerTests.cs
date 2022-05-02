using Iris.Api.Controllers.LettersControllers;
using Iris.Services.ClaimsPrincipalHelperService;
using Iris.Services.LettersService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace UnitTests.Tests.Api
{
    [TestFixture]
    public class UpdateLettersControllerTests
    {
        private Mock<ILetterService> _letterService;
        private Mock<IClaimsPrincipalHelperService> _claimsPrincipalHelperService;

        private UpdateLettersController _controller;

        private const int UserId = 1;
        private const int AccId = 1;
        private const int LetterId = 1;
        private const int Flag = 1;

        [SetUp]
        public void SetUp()
        {
            _letterService = new Mock<ILetterService>();
            _letterService.Setup(_ => _.ChangeFlag(UserId, AccId, LetterId, Flag));
            _letterService.Setup(_ => _.RemoveLetter(UserId, AccId, LetterId));

            _claimsPrincipalHelperService = new Mock<IClaimsPrincipalHelperService>();
            _claimsPrincipalHelperService.Setup(_ => _.GetUserId(null)).Returns(UserId);

            _controller = new UpdateLettersController(_letterService.Object, _claimsPrincipalHelperService.Object);
        }

        [Test]
        public void ChangeFlag_Data_OkResult()
        {
            var result = _controller.ChangeFlag(AccId, LetterId, Flag);

            Assert.AreEqual(((OkResult)result).StatusCode, new OkResult().StatusCode);
        }

        [Test]
        public void RemoveLetter_Data_OkResult()
        {
            var result = _controller.RemoveLetter(AccId, LetterId);

            Assert.AreEqual(((OkResult)result).StatusCode, new OkResult().StatusCode);
        }
    }
}
