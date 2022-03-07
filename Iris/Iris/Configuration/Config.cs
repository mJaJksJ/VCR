using Iris.Configuration.NotBasicTypeJoin;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace Iris.Configuration
{
    /// <summary>
    /// Конфигурация
    /// </summary>
    public class Config: ConfigExtension, IJoinableConfig
    {
        /// <summary>
        /// Конфигурация базы данных
        /// </summary>
        public DatabaseConfig Database { get; set; }

        /// <summary>
        /// Конфигурация логгера
        /// </summary>
        public LoggerConfig Logger { get; set; }

        /// <summary>
        /// конфигурация почтовых серверов
        /// </summary>
        public IEnumerable<MailServerConfig> MailServers { get; set; }

        public AuthConfig AuthConfig { get; set; }

        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<Config>();

        /// <summary>
        /// .ctor
        /// </summary>
        public static Config BuildConfig()
        {
            var config = DefaultConfig()
                .AddConfig("config.json")
                .RecoverConfig("config.json")
                .AddConfig("config.local.json");

            return config;
        }

        /// <summary>
        /// Дефолтная конфигурация
        /// </summary>
        /// <returns></returns>
        public static Config DefaultConfig()
        {
            return new Config
            {
                Database = new DatabaseConfig(),
                Logger = new LoggerConfig
                {
                    FileName = "Iris.log",
                    LimitFileSize = 1024 * 1024 * 32
                },
                MailServers = Array.Empty<MailServerConfig>(),
                AuthConfig = new AuthConfig
                {
                    JwtSecurityKey = Encoding.ASCII.GetBytes("8u5j4WXfR74kDGE38k32zIBrLuDELjSTGzTx97OWwVY01-0uaayMdBlBWfZ55Fy8"),
                    JwtLifetime = 24*3600,
                    JwtAudience = "iris",
                    JwtIssuer = "iris"
                }
            };
        }

        private Config RecoverConfig(string path)
        {
            var json = JObject.FromObject(this).ToString();
            File.WriteAllText(path, json);

            return this;
        }

        private Config AddConfig(Config config)
        {
            var newConfig = JoinWith(this, config, new ConfigJoiner());
            return newConfig as Config ?? throw new Exception("Cast error");
        }

        private Config AddConfig(string path)
        {
            Log.Information($"Try load config {path}");
            if (!File.Exists(path))
            {
                Log.Warning($"Config {path} is not exist");
                return this;
            }

            var config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path));

            if(config == null)
            {
                Log.Warning($"Config {path} load badly");
                return this;
            }

            Log.Information($"Config {path} load succesfully");
            var newConfig = AddConfig(config);
            return newConfig;
        }

    }
}
