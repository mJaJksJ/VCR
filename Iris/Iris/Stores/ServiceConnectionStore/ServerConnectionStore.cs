using Iris.Database;
using Iris.Helpers;
using MailKit;

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
            _connectionsStorage = new Dictionary<int, List<ServerConnection>>();
            FillStorageFromDb();

            _databaseContext = databaseContext;
        }

        /// <inheritdoc/>
        public IEnumerable<ServerConnection> GetUserConnections(int userId)
        {
            return _connectionsStorage.EnsureUserHaveConnections(userId);
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
                var connections = GetUserConnections(account.UserId).ToList();

                var connectionProtocol = ConnectionProtocolHelper.ByString(account.ConnectionProtocol);
                using IMailService connection = connectionProtocol.GetConnection();

                connection.Connect(account.MailServer.Host, account.MailServer.Port, account.UseSsl);
                connection.Authenticate(account.Name, account.Password);

                var serverConnection = new ServerConnection(connection);
                connections.Add(serverConnection);

                return serverConnection.Id;
            }
        }

        private void FillStorageFromDb()
        {
            var users = _databaseContext.Users;

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
