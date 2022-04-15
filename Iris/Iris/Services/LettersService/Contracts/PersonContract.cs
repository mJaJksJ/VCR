using Iris.Exceptions;
using MimeKit;

namespace Iris.Services.LettersService.Contracts
{
    /// <summary>
    /// Контракт персоны
    /// </summary>
    public class PersonContract
    {
        /// <summary>
        /// Id персоны
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя персоны
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Почтовый адрес персоны
        /// </summary>
        public string Email { get; set; }

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
    }
}
