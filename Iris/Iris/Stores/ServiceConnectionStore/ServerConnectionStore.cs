using Iris.Database;
using Iris.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Iris.Stores.ServiceConnectionStore
{
    /// <inheritdoc cref="IServerConnectionStore"/>
    public class ServerConnectionStore : IServerConnectionStore
    {
        private readonly object _locker = new();
        private readonly Dictionary<int, List<ServerConnection>> _connectionsStorage;
        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// .ctor
        /// </summary>
        public ServerConnectionStore(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _connectionsStorage = new Dictionary<int, List<ServerConnection>>();
            FillStorageFromDb();
        }

        /// <inheritdoc/>
        public void AddUserConnection(int userId, ServerConnection serverConnection)
        {
            _connectionsStorage.EnsureUserHaveConnections(userId);
            _connectionsStorage[userId].Add(serverConnection);
        }

        /// <inheritdoc/>
        public IEnumerable<ServerConnection> GetUserConnections(int userId, IEnumerable<int> accIds)
        {
            var connections = _connectionsStorage.EnsureUserHaveConnections(userId);

            return accIds != null ? connections.Where(_ => accIds.Contains(_.Account.Id)) : connections;
        }

        /// <inheritdoc/>
        public ServerConnection GetUserConnection(int userId, Guid connectionId)
        {
            return _connectionsStorage.EnsureUserHaveConnection(userId, connectionId);
        }

        /// <inheritdoc/>
        public Guid AddConnection(Account account)
        {
            lock (_locker)
            {
                var connectionProtocol = ConnectionProtocolHelper.ByString(account.ConnectionProtocol);
                var connection = connectionProtocol.GetConnection();

                connection.Connect(account.MailServer.Host, account.MailServer.Port, account.UseSsl);
                connection.Authenticate(account.Name, account.Password);

                var serverConnection = new ServerConnection(connection)
                {
                    Account = account,
                };

                AddUserConnection(account.UserId, serverConnection);

                return serverConnection.Id;
            }
        }

        private void FillStorageFromDb()
        {
            var users = _databaseContext.Users
                .Include(_ => _.Accounts)
                .ThenInclude(_ => _.MailServer);

            foreach (var user in users)
            {
                _connectionsStorage.Add(user.Id, new List<ServerConnection>());
                foreach (var account in user.Accounts)
                {
                    AddConnection(account);
                }
            }
        }
    }
}
