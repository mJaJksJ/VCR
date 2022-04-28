using System.ComponentModel.DataAnnotations;

namespace Iris.Database
{
    /// <summary>
    /// Учетная запись пользователя
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Протокол подключения
        /// </summary>
        public string ConnectionProtocol { get; set; }

        /// <summary>
        /// Надо ли использовать SSL
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Id почтового сервера
        /// </summary>
        public int MailServerId { get; set; }

        /// <summary>
        /// Почтовый сервер
        /// </summary>
        public MailServer MailServer { get; set; }

        /// <summary>
        /// Письма
        /// </summary>
        public IEnumerable<Letter> Letters { get; set; }
    }
}
