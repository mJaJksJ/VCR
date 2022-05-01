using Iris.Services.LettersService.Contracts;
using MimeKit;
using NUnit.Framework;
using System.Text;

namespace UnitTests.Tests.Services
{
    [TestFixture]
    public class PersonContractTests
    {
        private const string Email = "mail@mail.ru";
        private const string Name = "name";

        [Test]
        public void PersonContract_NameAndEmail_PersonContract()
        {
            var person = new PersonContract(new MailboxAddress(Encoding.UTF8, Name, Email));

            Assert.IsTrue(person.Name == Name && person.Email == Email);
        }

        [Test]
        public void PersonContract_Email_PersonContract()
        {
            var person = new PersonContract(new MailboxAddress(Encoding.UTF8, null, Email));

            Assert.IsTrue(person.Name == null && person.Email == Email);
        }
    }
}
