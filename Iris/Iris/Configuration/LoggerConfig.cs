namespace Iris.Configuration
{
    /// <summary>
    /// Конфиг логгера
    /// </summary>
    public class LoggerConfig : ConfigExtension, IJoinableConfig
    {
        /// <summary>
        /// Имя файла логгера
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Лимит на размер файла
        /// </summary>
        public int LimitFileSize { get; set; }
    }
}
