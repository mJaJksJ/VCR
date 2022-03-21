namespace Iris.Api.Controllers.ConnectionsControllers
{
    public class MailServerContract
    {
        /// <summary>
        /// Ip
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Порт
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Доступен ли всем пользователям
        /// </summary>
        public bool IsPrivate { get; set; }
    }
}
