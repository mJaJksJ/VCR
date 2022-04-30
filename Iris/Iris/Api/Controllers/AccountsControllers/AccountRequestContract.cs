namespace Iris.Api.Controllers.AccountsControllers
{
    /// <summary>
    /// Запрос добавления новой учетной записи
    /// </summary>
    public class AccountRequestContract
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

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
    }
}
