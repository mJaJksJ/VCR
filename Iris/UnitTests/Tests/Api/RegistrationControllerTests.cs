using Iris.Api.Controllers.RegistrationController;
using Iris.Api.Controllers.RegistrationControllers;
using Iris.Services.RegistrationService.cs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace UnitTests.Tests.Api
{
    [TestFixture]
    public class RegistrationControllerTests
    {
        private RegistrationController _registrationController;
        private Mock<IRegistrationService> _registrationService;
        private readonly RegistrationRequestContract _registrationRequestContract = new();

        [SetUp]
        public void SetUp()
        {
            _registrationService = new Mock<IRegistrationService>();
            _registrationService.Setup(_ => _.RegisterUser(_registrationRequestContract))
                .Returns(new RegistrationResponseContract());

            _registrationController = new RegistrationController(_registrationService.Object);
        }

        [Test]
        public void RegisterUser_Data_OkResult()
        {
            var result = _registrationController.RegisterUser(_registrationRequestContract);

            Assert.AreEqual((result as OkObjectResult).StatusCode, new OkResult().StatusCode);
        }
    }
}
