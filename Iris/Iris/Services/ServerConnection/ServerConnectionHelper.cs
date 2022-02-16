using Iris.Services.ServerConnection.Exceptions;

namespace Iris.Services.ServerConnection
{
    public static class ServerConnectionHelper
    {
        public static List<ServerConnection> EnsureUserHaveConnections(this Dictionary<int, List<ServerConnection>> storage, int userId)
        {
            if (storage.ContainsKey(userId))
            {
                return storage[userId];
            }

            throw new UserHasNoConnectionsException();
        }

        public static ServerConnection EnsureUserHaveConnection(this Dictionary<int, List<ServerConnection>> storage, int userId, Guid connectionId)
        {
            var connections = storage.EnsureUserHaveConnections(userId);

            return connections.FirstOrDefault(_ => _.Id == connectionId) ?? throw new UserHasNoConnectionsException($"Пользователь не имеет подключение с id = {connectionId}");
        }
    }
}
