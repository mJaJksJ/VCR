using Iris.Api.Controllers.LettersControllers;
using Iris.Database;
using Iris.Exceptions;
using Iris.Services.ImapClientService;
using Iris.Services.LettersService;
using Iris.Services.LettersService.Contracts;
using Iris.Stores.ServiceConnectionStore;
using MailKit.Net.Imap;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnitTests.Database;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class LetterServiceTests
    {
        private ILetterService _letterService;
        private Mock<IServerConnectionStore> _connectionStore;
        private readonly DatabaseContext _dbContext = TestDatabase.Instance;
        private int _userId;
        private int _accountId;
        private readonly Guid _guid = Guid.NewGuid();
        private Mock<IImapClientService> _imapClientService;

        [SetUp]
        public void SetUp()
        {
            var user = _dbContext.Users.Add(new User
            {
                Name = "user"
            });
            _dbContext.SaveChanges();
            _userId = user.Entity.Id;

            var server = _dbContext.MailServers.Add(new MailServer
            {
                Host = "",
                Port = 1,
            });
            _dbContext.SaveChanges();

            var acc = _dbContext.Accounts.Add(new Account
            {
                Name = "accName",
                UserId = _userId,
                Password = "",
                MailServerId = server.Entity.Id
            });
            _dbContext.SaveChanges();
            _accountId = acc.Entity.Id;

            var mailServer = new ImapClient();
            var letterContract = new LetterContract
            {
                Id = 1,
                Sender = new PersonContract
                {
                    Name = "testSender",
                    Email = "testSender@iris.ru",
                    Id = 1,
                },
                Receivers = new List<PersonContract>
                {
                    new PersonContract
                    {
                        Name = "testReceiver1",
                        Email = "testReceiver1@iris.ru",
                        Id = 2,
                    },
                    new PersonContract
                    {
                        Name = "testReceiver2",
                        Email = "testReceiver2@iris.ru",
                        Id = 3,
                    },
                },
                Subject = "TestFormatting",
                Date = new System.DateTime(2022, 04, 17, 17, 5, 30),
                Text = "Test letter for FormatLettersServiceTests",
                Attachments = new List<AttachmentContract>
                {
                    new AttachmentContract
                    {
                        Id = 1,
                        Name = "attach_1.txt",
                    },
                    new AttachmentContract
                    {
                        Id = 2,
                        Name = "attach_2.7z",
                    },
                },
                AccountId = _accountId
            };

            var person = _dbContext.Persons.Add(new Person());
            _dbContext.SaveChanges();

            _dbContext.Letters.Add(new Letter
            {
                Subject = "TestFormatting",
                Date = new DateTime(2022, 04, 17, 17, 5, 30),
                Text = "Test letter for FormatLettersServiceTests",
                AccountId = _accountId,
                SenderId = person.Entity.Id,
                Receivers = new List<Person> { person.Entity },
                Attachments = new List<Attachment>(){
                    new Attachment
                    {
                    Name = "",
                    Blob = Array.Empty<byte>()
                }}
            });
            _dbContext.SaveChanges();

            _imapClientService = new Mock<IImapClientService>();
            _imapClientService.Setup(_ => _.GetLetters(mailServer, NeedAttachments.WithoutAttachments, _accountId)).Returns(new List<LetterContract>()
            {
                letterContract
            });
            _imapClientService.Setup(_ => _.GetLetters(mailServer, NeedAttachments.OnlyName, _accountId)).Returns(new List<LetterContract>()
            {
                letterContract
            });
            _imapClientService.Setup(_ => _.GetLetters(mailServer, NeedAttachments.WithAttachmentsBlob, _accountId)).Returns(new List<LetterContract>()
            {
                letterContract
            });

            _connectionStore = new Mock<IServerConnectionStore>();
            _connectionStore.Setup(_ => _.GetUserConnections(_userId, new List<int>() { _accountId })).Returns(new List<ServerConnection>()
            {
                new ServerConnection(mailServer)
                {
                    Account = acc.Entity,
                    Id = _guid
                }
            });

            _connectionStore.Setup(_ => _.GetUserConnections(_userId, null)).Returns(new List<ServerConnection>()
            {
                new ServerConnection(mailServer)
                {
                    Account = acc.Entity,
                    Id = _guid
                }
            });

            _letterService = new LetterService(_connectionStore.Object, _dbContext, _imapClientService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Attachments.RemoveRange(_dbContext.Attachments);
            _dbContext.Letters.RemoveRange(_dbContext.Letters);
            _dbContext.Persons.RemoveRange(_dbContext.Persons);
            _dbContext.Accounts.RemoveRange(_dbContext.Accounts);
            _dbContext.MailServers.RemoveRange(_dbContext.MailServers);
            _dbContext.Users.RemoveRange(_dbContext.Users);

            _dbContext.SaveChanges();
        }

        [Test]
        public void GetLetters_WithoutAccountsSettings_LettersList()
        {
            var letters = _letterService.GetLetters(_userId, new LettersRequest());

            Assert.IsNotEmpty(letters);
        }

        [Test]
        public void GetLetters_Filters_LettersList()
        {
            var letters = _letterService.GetLetters(_userId, new LettersRequest
            {
                AccountsSettings = new Dictionary<int, Settings>() { { _accountId, new Settings() } },
                Filters = new List<FilterLetter>()
                {
                    new FilterLetter {Field = LetterField.Attacments, IsRegex = true, Templates = new List<string>() {""}},
                    new FilterLetter {Field = LetterField.Receivers, IsRegex = true, Templates = new List<string>() {""}},
                    new FilterLetter {Field = LetterField.Subject, IsRegex = true, Template = ""},
                    new FilterLetter {Field = LetterField.Date, IsRegex = true, Template = ""},
                    new FilterLetter {Field = LetterField.Text, IsRegex = true, Template = ""},
                    new FilterLetter {Field = LetterField.Sender, IsRegex = true, Template = ""},
                }
            });

            Assert.IsNotEmpty(letters);
        }

        [TestCase(3, 0)]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 1)]
        [TestCase(0, 1)]
        [TestCase(2, 1)]
        public void GetLetters_Sort_LettersList(int letterField, int sortBy)
        {
            var letters = _letterService.GetLetters(_userId, new LettersRequest
            {
                AccountsSettings = new Dictionary<int, Settings>() { { _accountId, new Settings() } },
                Sort = new SortLetter { Field = (LetterField)letterField, SortBy = (SortBy)sortBy }
            });

            Assert.IsNotEmpty(letters);
        }

        [Test]
        public void GetLetters_BadSort_LettersList()
        {
            Assert.Throws<UnsupportedSortFieldException>(() => _letterService.GetLetters(_userId, new LettersRequest
            {
                AccountsSettings = new Dictionary<int, Settings>() { { _accountId, new Settings() } },
                Sort = new SortLetter { Field = LetterField.Receivers, SortBy = SortBy.Asc }
            }));
        }

        [Test]
        public void GetLetters_OnlyNameFromDb_LettersList()
        {
            var letters = _letterService.GetLetters(_userId, new LettersRequest
            {
                AccountsSettings = new Dictionary<int, Settings>() { { _accountId, new Settings { LettersStorage = LettersFrom.LocalDb, NeedAttachments = NeedAttachments.OnlyName } } }
            });

            Assert.IsNotEmpty(letters);
        }

        [Test]
        public void GetLetters_WithoutAttachmentsFromDb_LettersList()
        {
            var letters = _letterService.GetLetters(_userId, new LettersRequest
            {
                AccountsSettings = new Dictionary<int, Settings>() { { _accountId, new Settings { LettersStorage = LettersFrom.LocalDb, NeedAttachments = NeedAttachments.WithoutAttachments } } }
            });

            Assert.IsNotEmpty(letters);
        }

        [Test]
        public void GetLetters_WithAttachmentsBlobFromDb_LettersList()
        {
            var letters = _letterService.GetLetters(_userId, new LettersRequest
            {
                AccountsSettings = new Dictionary<int, Settings>() { { _accountId, new Settings { LettersStorage = LettersFrom.LocalDb, NeedAttachments = NeedAttachments.WithAttachmentsBlob } } }
            });

            Assert.IsNotEmpty(letters);
        }

        [Test]
        public void GetLetters_ServerAndFromDb_LettersList()
        {
            var letters = _letterService.GetLetters(_userId, new LettersRequest
            {
                AccountsSettings = new Dictionary<int, Settings>() { { _accountId, new Settings { LettersStorage = LettersFrom.LocalAndRemote } } }
            });

            Assert.IsNotEmpty(letters);
        }

        [Test]
        public void GetLetters_SaveToDb_LettersList()
        {
            var letters = _letterService.GetLetters(_userId, new LettersRequest
            {
                AccountsSettings = new Dictionary<int, Settings>() { { _accountId, new Settings() } },
                SaveLettersToLocalBd = true
            });

            Assert.IsNotEmpty(letters);
        }
    }
}
