using Iris.Api.Controllers.ConnectionsControllers;
using Iris.Database;
using Iris.Services.ClaimsPrincipalHelperService;
using Iris.Services.MailServersService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests.Tests.Api
{
    [TestFixture]
    public class MailServersControllerTests
    {
        private const int UserId = 1;
        private Mock<IClaimsPrincipalHelperService> _claimsPrincipalHelperService;
        private MailServersController _controller;
        private Mock<IMailServersService> _mailServersService;
        private readonly MailServerContract _mailServerContract = new();

        [SetUp]
        public void SetUp()
        {
            _mailServersService = new Mock<IMailServersService>();
            _mailServersService.Setup(_ => _.GetMailServerAccounts(UserId))
                .Returns(new List<MailServerAccountContract>());
            _mailServersService.Setup(_ => _.GetAvailableMailServers(UserId))
                .Returns(new List<MailServer>());
            _mailServersService.Setup(_ => _.NewMailServer(_mailServerContract));

            _claimsPrincipalHelperService = new Mock<IClaimsPrincipalHelperService>();
            _claimsPrincipalHelperService.Setup(_ => _.GetUserId(null)).Returns(UserId);

            _controller = new MailServersController(_mailServersService.Object, _claimsPrincipalHelperService.Object);
        }

        [Test]
        public void GetMailServersAccounts_Data_OkResult()
        {
            var result = _controller.GetMailServersAccounts();

            Assert.AreEqual(((OkObjectResult)result).StatusCode, new OkResult().StatusCode);
        }

        [Test]
        public void GetAvailableMailServers_Data_OkResult()
        {
            var result = _controller.GetAvailableMailServers();

            Assert.AreEqual(((OkObjectResult)result).StatusCode, new OkResult().StatusCode);
        }

        [Test]
        public void AddUserMailService_Data_OkResult()
        {
            var result = _controller.AddUserMailService(_mailServerContract);

            Assert.AreEqual(((OkResult)result).StatusCode, new OkResult().StatusCode);
        }
    }
}
