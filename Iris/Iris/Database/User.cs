using System.ComponentModel.DataAnnotations;

namespace Iris.Database
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
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
        public string Password { get; set; }

        /// <summary>
        /// Является ли администратором
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Аккаунты
        /// </summary>
        public IEnumerable<Account> Accounts { get; set; }

        /// <summary>
        /// Кем создан
        /// </summary>
        public int CreatedBy { get; set; }
    }
}
