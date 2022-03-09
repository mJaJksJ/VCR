namespace Iris.Configuration
{
    /// <summary>
    /// Конфигурации базы данных
    /// </summary>
    public class DatabaseConfig : ConfigExtension, IJoinableConfig
    {
        /// <summary>
        /// Сервер расположения бд
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// Порт
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Имя бд
        /// </summary>
        public string DbName { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }
    }
}
