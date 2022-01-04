namespace Iris.Configuration
{
    /// <summary>
    /// Конфиг почтового сервера
    /// </summary>
    public class MailServerConfig: ConfigExtension, IJoinableConfig
    {
        /// <summary>
        /// Имя для сервера
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Имя вызова сервера
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// Порт сервера
        /// </summary>
        public int Port { get; set; }
    }
}
