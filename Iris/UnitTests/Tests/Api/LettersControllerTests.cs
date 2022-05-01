using Iris.Api.Controllers.LettersControllers;
using Iris.Enums;
using Iris.Services.ClaimsPrincipalHelperService;
using Iris.Services.FormatLettersService;
using Iris.Services.LettersService;
using Iris.Services.LettersService.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests.Tests.Api
{
    public class LettersControllerTests
    {
        private const int UserId = 1;
        private const string Format = "json";
        private LettersController _lettersController;
        private Mock<ILetterService> _letterService;
        private Mock<IFormatLettersSevice> _formatLettersSevice;
        private readonly LettersRequest _lettersRequest = new();
        private readonly IEnumerable<LetterContract> _contracts = new List<LetterContract>();
        private readonly ResponseFormat _format = ResponseFormat.Json;
        private Mock<IClaimsPrincipalHelperService> _claimsPrincipalHelperService;

        [SetUp]
        public void SetUp()
        {
            _letterService = new Mock<ILetterService>();
            _letterService.Setup(_ => _.GetLetters(UserId, _lettersRequest))
                .Returns(new List<LetterContract>());

            _formatLettersSevice = new Mock<IFormatLettersSevice>();
            _formatLettersSevice.Setup(_ => _.FormatLetters(_contracts, _format)).Returns(string.Empty);
            _formatLettersSevice.Setup(_ => _.GetFormat(Format)).Returns(_format);

            _claimsPrincipalHelperService = new Mock<IClaimsPrincipalHelperService>();
            _claimsPrincipalHelperService.Setup(_ => _.GetUserId(null)).Returns(UserId);

            _lettersController = new LettersController(_letterService.Object, _formatLettersSevice.Object, _claimsPrincipalHelperService.Object);
        }

        [Test]
        public void GetLetters_Data_OkResult()
        {
            var result = _lettersController.GetLetters(_lettersRequest);

            Assert.AreEqual(((OkObjectResult)result).StatusCode, new OkResult().StatusCode);
        }

        [Test]
        public void GetLetters_Format_OkResult()
        {
            var result = _lettersController.GetLetters(Format, _lettersRequest);

            Assert.AreEqual(((OkObjectResult)result).StatusCode, new OkResult().StatusCode);
        }
    }
}
