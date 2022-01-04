using Iris.ReflectionExtensions;

namespace Iris.Configuration
{
    public class DatabaseConfig: ConfigExtension, IJoinableConfig
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string DbName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
