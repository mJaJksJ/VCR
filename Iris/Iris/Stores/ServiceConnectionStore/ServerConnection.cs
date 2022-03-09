using MailKit;

namespace Iris.Stores.ServiceConnectionStore
{
    /// <summary>
    /// Подключение к серверу
    /// </summary>
    public class ServerConnection
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Почтовый сервис
        /// </summary>
        public IMailService MailService { get; set; }

        /// <summary>
        /// .ctor
        /// </summary>
        public ServerConnection(IMailService mailService)
        {
            Id = Guid.NewGuid();
            MailService = mailService;
        }
    }
}
