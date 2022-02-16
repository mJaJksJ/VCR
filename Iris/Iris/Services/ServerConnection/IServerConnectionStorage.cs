using Iris.Common.Enums;
using Iris.Database;

namespace Iris.Services.ServerConnection
{
    public interface IServerConnectionStorage
    {
        IEnumerable<ServerConnection> GetUserConnections(int userId);
        ServerConnection GetUserConnection(int userId, Guid connectionId);
        void AddConnection(int userId, ConnectionProtocol connectionProtocol, Account account);
    }
}
