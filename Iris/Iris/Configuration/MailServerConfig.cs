namespace Iris.Configuration
{
    public class MailServerConfig: ConfigExtension, IJoinableConfig
    {
        public string Name { get; set; }
        public string ServerName { get; set; }
        public int Port { get; set; }
    }
}
