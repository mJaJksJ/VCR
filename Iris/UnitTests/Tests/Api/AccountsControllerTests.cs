using Iris.Api.Controllers.AccountsControllers;
using Iris.Services.AccountsService;
using Iris.Services.ClaimsPrincipalHelperService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace UnitTests.Tests.Api
{
    [TestFixture]
    public class AccountsControllerTests
    {
        private const int UserId = 1;
        private const int AccId = 1;
        private Mock<IClaimsPrincipalHelperService> _claimsPrincipalHelperService;
        private AccountsController _controller;
        private Mock<IAccountsService> _accountsService;
        private readonly AccountRequestContract _requestContract = new();

        [SetUp]
        public void SetUp()
        {
            _accountsService = new Mock<IAccountsService>();
            _accountsService.Setup(_ => _.AddNewAccount(UserId, _requestContract));
            _accountsService.Setup(_ => _.RemoveAccount(UserId, AccId));

            _claimsPrincipalHelperService = new Mock<IClaimsPrincipalHelperService>();
            _claimsPrincipalHelperService.Setup(_ => _.GetUserId(null)).Returns(UserId);

            _controller = new AccountsController(_accountsService.Object, _claimsPrincipalHelperService.Object);
        }

        [Test]
        public void AddNewAccount_Data_OkResult()
        {
            var result = _controller.AddNewAccount(_requestContract);

            Assert.AreEqual(((OkResult)result).StatusCode, new OkResult().StatusCode);
        }

        [Test]
        public void RemoveAccount_Data_OkResult()
        {
            var result = _controller.RemoveAccount(AccId);

            Assert.AreEqual(((OkResult)result).StatusCode, new OkResult().StatusCode);
        }
    }
}
