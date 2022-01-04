namespace Iris.Configuration
{
    public class LoggerConfig: ConfigExtension, IJoinableConfig
    {
        public string FileName { get; set; } 
        public int LimitFileSize { get; set; } 
    }
}
