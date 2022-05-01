
using Iris.Configuration;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Text;

namespace UnitTests.Tests.Configuration
{
    [TestFixture]
    public class ConfigTests
    {
        [Test]
        public void DefaultConfig_Non_RightConfig()
        {
            var expected = JObject.FromObject(new Config
            {
                Database = new DatabaseConfig(),
                Logger = new LoggerConfig
                {
                    FileName = "Iris.log",
                    LimitFileSize = 1024 * 1024 * 32,
                    FilePath = "../logs"
                },
                AuthConfig = new AuthConfig
                {
                    JwtSecurityKey = Encoding.ASCII.GetBytes("8u5j4WXfR74kDGE38k32zIBrLuDELjSTGzTx97OWwVY01-0uaayMdBlBWfZ55Fy8"),
                    JwtLifetime = 24 * 3600,
                    JwtAudience = "iris",
                    JwtIssuer = "iris"
                }
            });

            var actual = JObject.FromObject(Config.DefaultConfig());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BuildConfig_Non_RightConfig()
        {
            var expected = JObject.FromObject(new Config
            {
                Database = new DatabaseConfig(),
                Logger = new LoggerConfig
                {
                    FileName = "Iris.log",
                    LimitFileSize = 1024 * 1024 * 32,
                    FilePath = "../logs"
                },
                AuthConfig = new AuthConfig
                {
                    JwtSecurityKey = Encoding.ASCII.GetBytes("8u5j4WXfR74kDGE38k32zIBrLuDELjSTGzTx97OWwVY01-0uaayMdBlBWfZ55Fy8"),
                    JwtLifetime = 24 * 3600,
                    JwtAudience = "iris",
                    JwtIssuer = "iris"
                }
            });

            var actual = JObject.FromObject(Config.BuildConfig());

            Assert.AreEqual(expected, actual);
        }
    }
}
