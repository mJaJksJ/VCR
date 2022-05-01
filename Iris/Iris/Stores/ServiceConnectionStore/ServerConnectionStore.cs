using Iris.Database;
using Iris.Helpers;
using Iris.Services.ConnectionProtocolHelperService;
using Microsoft.EntityFrameworkCore;

namespace Iris.Stores.ServiceConnectionStore
{
    /// <inheritdoc cref="IServerConnectionStore"/>
    public class ServerConnectionStore : IServerConnectionStore
    {
        private readonly object _locker = new();
        private readonly Dictionary<int, List<ServerConnection>> _connectionsStorage;
        private readonly DatabaseContext _databaseContext;
        private readonly IConnectionProtocolHelperService _connectionProtocolHelperService;

        /// <summary>
        /// .ctor
        /// </summary>
        public ServerConnectionStore(DatabaseContext databaseContext, IConnectionProtocolHelperService connectionProtocolHelper)
        {
            _databaseContext = databaseContext;
            _connectionProtocolHelperService = connectionProtocolHelper;
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
        public ServerConnection GetUserConnection(int userId, int accountId)
        {
            return _connectionsStorage.EnsureUserHaveConnection(userId, accountId);
        }

        /// <inheritdoc/>
        public Guid AddConnection(Account account)
        {
            lock (_locker)
            {
                var connectionProtocol = _connectionProtocolHelperService.ByString(account.ConnectionProtocol);
                var connection = _connectionProtocolHelperService.GetConnection(connectionProtocol);

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

        /// <inheritdoc/>
        public void RemoveConnection(int userId, int accountId)
        {
            lock (_locker)
            {
                _connectionsStorage.EnsureUserHaveConnections(userId);
                var connection = GetUserConnection(userId, accountId);
                _connectionsStorage[userId].Remove(connection);
            }
        }
    }
}
