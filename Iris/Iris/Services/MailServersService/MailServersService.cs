using Iris.Api.Controllers.ConnectionsControllers;
using Iris.Database;

namespace Iris.Services.MailServersService
{
    public class MailServersService : IMailServersService
    {
        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// .ctor
        /// </summary>
        public MailServersService(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public IEnumerable<MailServerAccountContract> GetMailServerAccounts(int userId)
        {
            var serverAccounts = _databaseContext.Accounts
                .Where(_ => _.UserId == userId)
                .Select(_ => new MailServerAccountContract
                {
                    Id = _.MailServer.Id,
                    Name = _.MailServer.Name,
                    AccountId = _.Id,
                    AccountName = _.Name
                })
                .ToArray();

            return serverAccounts;
        }

        public void NewMailServer(MailServerContract mailServerContract)
        {
            if(_databaseContext.MailServers.Any(_ => _.Host == mailServerContract.Host && _.Port == mailServerContract.Port))
            {
                throw new Exception();
            }
        }
    }
}
