using Iris.Services.ClaimsPrincipalHelperService;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class ClaimsPrincipalHelperServiceTests
    {
        private const string sId = "1";
        private const int Id = 1;
        private Mock<ClaimsPrincipal> _principal;

        private IClaimsPrincipalHelperService _service;

        [SetUp]
        public void SetUp()
        {
            _principal = new Mock<ClaimsPrincipal>();
            _principal.Setup(_ => _.Claims).Returns(new List<Claim>() { new Claim("sid", sId) });

            _service = new ClaimsPrincipalHelperService();
        }

        [Test]
        public void GetUserId_Principals_Id()
        {
            var id = _service.GetUserId(_principal.Object);

            Assert.AreEqual(Id, id);
        }
    }
}
