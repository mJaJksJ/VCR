using Iris.Api.Controllers.LettersControllers;
using Iris.Services.ImapClientService;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class ImapClientServiceTests
    {
        private Mock<IMailFolder> _mailFolder;
        private IImapClientService _imapClientService;
        private Mock<ImapClient> _mailStore;


        [SetUp]
        public void SetUp()
        {
            _mailFolder = new Mock<IMailFolder>();
            _mailFolder.Setup(_ => _.Open(FolderAccess.ReadOnly, CancellationToken.None)).Returns(FolderAccess.ReadOnly);
            _mailFolder.Setup(_ => _.GetEnumerator()).Returns(new List<MimeMessage>() { new MimeMessage() }.AsEnumerable().GetEnumerator);

            _mailStore = new Mock<ImapClient>();
            _mailStore.Setup(_ => _.Inbox).Returns(_mailFolder.Object);

            _imapClientService = new ImapClientService();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void GetLetters_Data_NotExeption(int needAttachments)
        {
            Assert.DoesNotThrow(() => _imapClientService.GetLetters(_mailStore.Object, (NeedAttachments)needAttachments, 1));
        }
    }
}
