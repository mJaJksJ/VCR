using Iris.Enums;
using Iris.Services.FormatLettersService;
using Iris.Services.LettersService.Contracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class FormatLettersServiceTests
    {
        private IEnumerable<LetterContract> _letterContracts;

        private IFormatLettersSevice _formatLettersSevice;

        [SetUp]
        public void SetUp()
        {
            _letterContracts = new List<LetterContract>
            {
                new LetterContract
                {
                    Id = "1",
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
                    }
                }
            };

            _formatLettersSevice = new FormatLettersSevice();
        }

        [Test]
        public void FormatLetter_Json_JsonFormattedLetter()
        {
            var expected = File.ReadAllText("Files/JsonLetter.json");

            var actual = _formatLettersSevice.FormatLetter(_letterContracts.First(), ResponseFormat.Json);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FormatLetter_Xml_XmlFormattedLetter()
        {
            var expected = File.ReadAllText("Files/XmlLetter.xml");

            var actual = _formatLettersSevice.FormatLetter(_letterContracts.First(), ResponseFormat.XML);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FormatLetters_Json_JsonFormattedLetters()
        {
            var expected = File.ReadAllText("Files/JsonLetters.json");

            var actual = _formatLettersSevice.FormatLetters(_letterContracts, ResponseFormat.Json);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FormatLetters_Xml_XmlFormattedLetters()
        {
            var expected = File.ReadAllText("Files/XmlLetters.xml");

            var actual = _formatLettersSevice.FormatLetters(_letterContracts, ResponseFormat.XML);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("Json")]
        [TestCase("JSON")]
        [TestCase("json")]
        [TestCase("JsOn")]
        [TestCase("jsON")]
        public void GetFormat_jsonFormatString_RightFormat(string format)
        {
            var expected = ResponseFormat.Json;

            var actual = _formatLettersSevice.GetFormat(format);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("Xml")]
        [TestCase("XML")]
        [TestCase("xml")]
        [TestCase("XmL")]
        [TestCase("xML")]
        public void GetFormat_xmlFormatString_RightFormat(string format)
        {
            var expected = ResponseFormat.XML;

            var actual = _formatLettersSevice.GetFormat(format);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("null")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("j son ")]
        [TestCase("x m  l ")]
        public void GetFormat_badString_Exception(string format)
        {
            Assert.Throws<Exception>(() => _formatLettersSevice.GetFormat(format));
        }
    }
}
