using System.Collections.Concurrent;

namespace Iris.Stores
{
    /// <summary>
    /// Хранилище токенов
    /// </summary>
    public class TokensStore
    {
        /// <summary>
        /// Словарь с сопоставлением пользователя и последнего выданного токена для этого пользователя
        /// </summary>
        private readonly ConcurrentDictionary<string, string> _tokens = new();

        /// <summary>
        /// Добавить или обновить токен
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="token">Токен</param>
        public void AddOrUpdate(string userId, string token)
        {
            _tokens.AddOrUpdate(userId, token, (_, old) => token);
        }

        /// <summary>
        /// Удалить токен
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns>Было ли удаление успешно</returns>
        public bool Remove(string userId)
        {
            return _tokens.TryRemove(userId, out var _);
        }

        /// <summary>
        /// Сцществует ли токен
        /// </summary>
        /// <param name="token">Токен</param>
        public bool Exists(string token)
        {
            return _tokens.Any(t => t.Value == token);
        }
    }
}
