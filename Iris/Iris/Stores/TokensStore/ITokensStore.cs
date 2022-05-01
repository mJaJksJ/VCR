namespace Iris.Stores.TokensStore
{
    /// <summary>
    /// Хранилище токенов
    /// </summary>
    public interface ITokensStore
    {
        /// <summary>
        /// Добавить или обновить токен
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="token">Токен</param>
        void AddOrUpdate(string userId, string token);

        /// <summary>
        /// Удалить токен
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns>Было ли удаление успешно</returns>
        bool Remove(string userId);

        /// <summary>
        /// Сцществует ли токен
        /// </summary>
        /// <param name="token">Токен</param>
        bool Exists(string token);
    }
}
