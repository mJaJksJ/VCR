using Iris.Database;

namespace Iris.Stores.AuthRequestStore
{
    /// <summary>
    /// Хранилище запросов авторизации
    /// </summary>
    public interface IAuthRequestsStore
    {
        /// <summary>
        /// Создать запрос
        /// </summary>
        AuthRequestOperation CreateRequest();

        /// <summary>
        /// Найти запрос
        /// </summary>
        /// <param name="id">Id запроса</param>
        AuthRequestOperation FindRequest(string id);
    }
}
