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
        /// <param name="accIds">Id учетных записей</param>
        IEnumerable<ServerConnection> GetUserConnections(int userId, IEnumerable<int> accIds = null);

        /// <summary>
        /// Добавить подключения пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="serverConnection">Подключение</param>
        void AddUserConnection(int userId, ServerConnection serverConnection);

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

        /// <summary>
        /// Удалить подключение пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="accountId">Id учетной записи</param>
        public void RemoveConnection(int userId, int accountId);
    }
}
