using Iris.Database;

namespace Iris.Stores.ServiceConnectionStore
{
    /// <summary>
    /// Хранилище подключений пользователей
    /// </summary>
    public interface IServerConnectionStore
    {
        /// <summary>
        /// Получить подключения пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        IEnumerable<ServerConnection> GetUserConnections(int userId);

        /// <summary>
        /// Получить подключение пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="connectionId">Id подключения</param>
        ServerConnection GetUserConnection(int userId, Guid connectionId);

        /// <summary>
        /// Добавить подключение
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <returns>Id подключения</returns>
        Guid AddConnection(Account account);
    }
}
