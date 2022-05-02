using Iris.Api.Controllers.LettersControllers;
using Iris.Services.Pop3ClientService;
using MailKit.Net.Pop3;
using MimeKit;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class Pop3ClientServiceTests
    {
        private IPop3ClientService _pop3ClientService;
        private Mock<Pop3Client> _mailStore;

        [SetUp]
        public void SetUp()
        {
            var letters = new List<MimeMessage>() { new MimeMessage() };

            _mailStore = new Mock<Pop3Client>();
            _mailStore.Setup(_ => _.Count).Returns(letters.Count);
            for (int i = 0; i < letters.Count; i++)
            {
                _mailStore.Setup(_ => _.GetMessage(i, CancellationToken.None, null)).Returns(letters[i]);
            }

            _pop3ClientService = new Pop3ClientService();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void GetLetters_Data_NotExeption(int needAttachments)
        {
            Assert.DoesNotThrow(() => _pop3ClientService.GetLetters(_mailStore.Object, (NeedAttachments)needAttachments, 1));
        }
    }
}
