using Iris.Common.Enums;
using Iris.Database;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using Microsoft.EntityFrameworkCore;

namespace Iris.Services.ServerConnection
{
    public class ServerConnectionStorage: IServerConnectionStorage
    {
        private readonly object _locker = new object();
        private Dictionary<int, List<ServerConnection>> _connectionsStorage;

        public ServerConnectionStorage(DatabaseContext databaseContext)
        {
            _connectionsStorage = new Dictionary<int, List<ServerConnection>>();
            
        }

        public IEnumerable<ServerConnection> GetUserConnections(int userId)
        {
            return _connectionsStorage.EnsureUserHaveConnections(userId);
        }

        public ServerConnection GetUserConnection(int userId, Guid connectionId)
        {
            return _connectionsStorage.EnsureUserHaveConnection(userId, connectionId);
        }

        public void AddConnection(int userId, Account account)
        {
            EnsureUserHasAccount();

            var connections = GetUserConnections(userId).ToList();
            var connectionProtocol = account.ConnectionProtocol == "Pop3" ? ConnectionProtocol.Pop3 : ConnectionProtocol.Imap;
            using IMailService connection = connectionProtocol == ConnectionProtocol.Pop3 ? new Pop3Client() : new ImapClient();
            connection.Connect(account.MailServer.Host, account.MailServer.Port, account.UseSsl);
            connection.Authenticate(account.Name, account.Password);

            connections.Add(new ServerConnection(connection));
        }

        private void FillStorageFromDb(DatabaseContext databaseContext)
        {
            var users = databaseContext.Accounts
                .Include(_ => _.MailServer);
                
            foreach (var user in users)
            {
                _connectionsStorage.Add(user.Id, new List<ServerConnection>());
                foreach(var account in user.Accounts)
                {
                    AddConnection(user.Id, account);
                }
            }
        }
    }
}
