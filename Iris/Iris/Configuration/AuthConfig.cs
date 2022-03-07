namespace Iris.Configuration
{
    public class AuthConfig : ConfigExtension, IJoinableConfig
    {
        public byte[] JwtSecurityKey { get; set; }
        public int JwtLifetime { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
    }
}
