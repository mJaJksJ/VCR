using System.ComponentModel.DataAnnotations;

namespace Iris.Database
{
    /// <summary>
    /// Почтовый сервер
    /// </summary>
    public class MailServer
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ip
        /// </summary>
        [Required]
        public string Host { get; set; }

        /// <summary>
        /// Порт
        /// </summary>
        [Required]
        public int Port { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Аккаунты
        /// </summary>
        public IEnumerable<Account> Accounts { get; set; }
    }
}
