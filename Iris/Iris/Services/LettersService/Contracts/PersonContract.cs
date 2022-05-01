using Iris.Exceptions;
using MimeKit;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Iris.Services.LettersService.Contracts
{
    /// <summary>
    /// Контракт персоны
    /// </summary>
    [Serializable]
    public class PersonContract
    {
        /// <summary>
        /// Id персоны
        /// </summary>
        [JsonProperty("Id")]
        [XmlElement("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Имя персоны
        /// </summary>
        [JsonProperty("Name")]
        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Почтовый адрес персоны
        /// </summary>
        [JsonProperty("Email")]
        [XmlAttribute("Email")]
        public string Email { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Name} <{Email}>";
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="internetAddress">Адресс персоны</param>
        /// <exception cref="ParsePersonInternetAddressException">Ошибка парсинга имени и email персоны</exception>
        public PersonContract(InternetAddress internetAddress)
        {
            var person = internetAddress.ToString().Split(new[] { '\"', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);

            switch (person.Length)
            {
                case 1:
                    Email = person[0];
                    break;

                case 3:
                    Name = person[0];
                    Email = person[2];
                    break;

                default:
                    throw new ParsePersonInternetAddressException(person.ToString());
            }
        }

        /// <summary>
        /// .ctor
        /// </summary>
        public PersonContract()
        {

        }
    }
}
