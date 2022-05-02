using Iris.Api.Controllers.AuthControllers;
using Iris.Api.Results;
using Iris.Database;
using Iris.Services.AuthService;
using Iris.Services.ClaimsPrincipalHelperService;
using Iris.Stores.AuthRequestStore;
using Iris.Stores.TokensStore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UnitTests.Tests.Api
{
    [TestFixture]
    public class AuthControllerTests
    {
        private AuthController _controller;
        private Mock<IAuthRequestsStore> _authRequestsStore;
        private Mock<IAuthService> _authService;
        private Mock<ITokensStore> _tokensStore;
        private Mock<IClaimsPrincipalHelperService> _claimsPrincipalHelperService;
        private const string _requestId = "1";
        private readonly AuthRequestOperation _authRequestOperation = new() { Id = _requestId };
        private readonly AuthRequestContract _authRequestContract = new();
        private const string Token = "JNJAMK";
        private const string UserId = "1";

        [SetUp]
        public void SetUp()
        {
            _authRequestsStore = new Mock<IAuthRequestsStore>();
            _authRequestsStore.Setup(_ => _.CreateRequest()).Returns(_authRequestOperation);
            _authRequestsStore.Setup(_ => _.FindRequest(_requestId)).Returns(_authRequestOperation);

            _authService = new Mock<IAuthService>();
            var claims = new ClaimsIdentity();
            var user = new User();
            claims.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", ""));
            claims.AddClaim(new Claim(JwtRegisteredClaimNames.Sid, UserId));
            _authService.Setup(_ => _.Authorize(_authRequestOperation, _authRequestContract))
                .Returns((claims, user));
            _authService.Setup(_ => _.GenerateToken(claims.Claims, user)).Returns((Token, DateTime.Now));

            _tokensStore = new Mock<ITokensStore>();
            _tokensStore.Setup(_ => _.AddOrUpdate(UserId, Token));
            _tokensStore.Setup(_ => _.Remove(UserId));

            _claimsPrincipalHelperService = new Mock<IClaimsPrincipalHelperService>();
            _claimsPrincipalHelperService.Setup(_ => _.GetUserId(null)).Returns(int.Parse(UserId));

            _controller = new AuthController(_authRequestsStore.Object, _authService.Object, _tokensStore.Object, _claimsPrincipalHelperService.Object);

        }

        [Test]
        public void InitAuth_Data_OkResult()
        {
            var result = _controller.InitAuth();

            Assert.AreEqual(((CreatedResult)result).StatusCode, new CreatedResult("", null).StatusCode);
        }

        [Test]
        public void ExecuteAuthorization_BadRequest_OkResult()
        {
            var result = _controller.ExecuteAuthorization(_requestId + 1, new AuthRequestContract());

            Assert.AreEqual(((AuthErrorResult)result).Code, new AuthErrorResult().Code);
        }

        [Test]
        public void ExecuteAuthorization_Data_OkResult()
        {
            var result = _controller.ExecuteAuthorization(_requestId, _authRequestContract);

            Assert.AreEqual(((OkObjectResult)result).StatusCode, new OkResult().StatusCode);
        }

        [Test]
        public void ExecuteAuthorization_BadAuth_OkResult()
        {
            var result = _controller.ExecuteAuthorization(_requestId, new AuthRequestContract());

            Assert.AreEqual(((AuthErrorResult)result).Code, new AuthErrorResult().Code);
        }

        [Test]
        public void DeAuth_Data_OkResult()
        {
            var result = _controller.DeAuth();

            Assert.AreEqual(((OkResult)result).StatusCode, new OkResult().StatusCode);
        }

        [Test]
        public void IsAuth_Data_OkResult()
        {
            var result = _controller.IsAuth();

            Assert.AreEqual(((OkObjectResult)result).StatusCode, new OkResult().StatusCode);
        }
    }
}
