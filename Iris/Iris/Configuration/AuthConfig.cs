namespace Iris.Configuration
{
    /// <summary>
    /// Конфигурация авторизации
    /// </summary>
    public class AuthConfig : ConfigExtension, IJoinableConfig
    {
        /// <summary>
        /// Секретный ключ токена
        /// </summary>
        public byte[] JwtSecurityKey { get; set; }

        /// <summary>
        /// Время жизни токена
        /// </summary>
        public int JwtLifetime { get; set; }

        /// <summary>
        /// Издатель токена
        /// </summary>
        public string JwtIssuer { get; set; }

        /// <summary>
        /// Пользователи токена
        /// </summary>
        public string JwtAudience { get; set; }
    }
}
