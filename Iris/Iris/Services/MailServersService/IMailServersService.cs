using Iris.Api.Controllers.ConnectionsControllers;

namespace Iris.Services.MailServersService
{
    public interface IMailServersService
    {
        public IEnumerable<MailServerAccountContract> GetMailServerAccounts(int userId);
    }
}
