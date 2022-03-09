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
        public string Ip { get; set; }

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
        public List<Account> Accounts { get; set; }
    }
}
