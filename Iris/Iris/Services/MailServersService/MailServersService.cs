using Iris.Api.Controllers.ConnectionsControllers;
using Iris.Database;

namespace Iris.Services.MailServersService
{
    /// <inheritdoc cref="IMailServersService"/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public MailServerAccountContract NewMailServer(MailServerContract mailServerContract)
        {
            if (_databaseContext.MailServers.Any(_ => _.Host == mailServerContract.Host && _.Port == mailServerContract.Port))
            {
                throw new Exception();
            }
            throw new Exception();
        }
    }
}
