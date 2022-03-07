namespace Iris.Configuration
{
    public class AuthConfig : ConfigExtension, IJoinableConfig
    {
        public byte[] SymmetricSecurityKey { get; set; }
    }
}
