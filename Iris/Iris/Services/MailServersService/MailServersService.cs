using Iris.Api.Controllers.ConnectionsControllers;
using Iris.Database;
using Iris.Exceptions;

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
        public IEnumerable<MailServer> GetAvailableMailServers(int userId)
        {
            var userServerAccounts = _databaseContext.Accounts
                .Where(_ => _.UserId == userId)
                .Select(_ => _.MailServer)
                .ToArray();

            var userServersIds = userServerAccounts.Select(_ => _.Id);

            var publicServerAccounts = _databaseContext.MailServers
                .Where(_ => !_.IsPrivate && !userServersIds.Contains(_.Id))
                .ToArray();

            var serverAccounts = new List<MailServer>();
            serverAccounts.AddRange(userServerAccounts);
            serverAccounts.AddRange(publicServerAccounts);

            return serverAccounts.AsEnumerable();
        }

        /// <inheritdoc/>
        public void NewMailServer(MailServerContract mailServerContract)
        {
            if (_databaseContext.MailServers.Any(_ => _.Host == mailServerContract.Host && _.Port == mailServerContract.Port))
            {
                throw new ServerAlreadyExistException(mailServerContract.Host, mailServerContract.Port);
            }

            var mailServer = _databaseContext.MailServers.Add(new MailServer
            {
                Host = mailServerContract.Host,
                Port = mailServerContract.Port,
                Name = mailServerContract.Name,
                IsPrivate = mailServerContract.IsPrivate
            });

            _databaseContext.SaveChanges();


        }
    }
}
