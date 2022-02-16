using MailKit;

namespace Iris.Services.ServerConnection
{
    public class ServerConnection
    {
        public Guid Id { get; set; }
        public IMailService MailService { get; set; }

        public ServerConnection(IMailService mailService)
        {
            Id = Guid.NewGuid();
            MailService = mailService;
        }
    }
}
