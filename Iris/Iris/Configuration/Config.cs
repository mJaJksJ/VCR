using Iris.ReflectionExtensions;
using Iris.ReflectionExtensions.NotBasicTypeJoin;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Iris.Configuration
{
    public class Config: ConfigExtension, IJoinableConfig
    {
        public DatabaseConfig Database { get; set; }
        public LoggerConfig Logger { get; set; }
        public IEnumerable<MailServerConfig> MailServers { get; set; }

        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<Config>();

        public static Config BuildConfig()
        {
            var config = DefaultConfig()
                .AddConfig("config.json")
                .RecoverConfig("config.json")
                .AddConfig("config.local.json");

            return config;
        }

        public static Config DefaultConfig()
        {
            return new Config
            {
                Database = new DatabaseConfig(),
                Logger = new LoggerConfig
                {
                    FileName = "Iris",
                    LimitFileSize = 1024*1024*32
                },
                MailServers = Array.Empty<MailServerConfig>()
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
