using Iris.Exceptions.UserExceptions;
using Iris.Stores.ServiceConnectionStore;

namespace Iris.Helpers
{
    /// <summary>
    /// Расширения для ServerConnection
    /// </summary>
    public static class ServerConnectionHelper
    {
        /// <summary>
        /// Есть ли у пользователя подключения
        /// </summary>
        /// <param name="storage">Хранилище подключений</param>
        /// <param name="userId">Id пользователя</param>
        /// <returns>Набор подключение</returns>
        /// <exception cref="UserHasNoConnectionsException"></exception>
        public static IEnumerable<ServerConnection> EnsureUserHaveConnections(this Dictionary<int, List<ServerConnection>> storage, int userId)
        {
            if (storage.ContainsKey(userId))
            {
                return storage[userId];
            }

            throw new UserHasNoConnectionsException();
        }

        /// <summary>
        /// Есть ли у пользователя подключение
        /// </summary>
        /// <param name="storage">Хранилище подключений</param>
        /// <param name="userId">Id пользователя</param>
        /// <param name="accountId">Id учетной записи</param>
        /// <returns>Подключение</returns>
        /// <exception cref="UserHasNoConnectionsException"></exception>
        public static ServerConnection EnsureUserHaveConnection(this Dictionary<int, List<ServerConnection>> storage, int userId, int accountId)
        {
            var connections = storage.EnsureUserHaveConnections(userId);

            return connections.FirstOrDefault(_ => _.Account.Id == accountId) ?? throw new UserHasNoConnectionsException($"Пользователь не имеет подключение учетной записи с id = {accountId}");
        }
    }
}
