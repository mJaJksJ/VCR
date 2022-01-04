using Iris.Configuration;

namespace Iris.ReflectionExtensions.NotBasicTypeJoin
{
    public class LoggerConfigJoiner : INotBasicTypesJoiner
    {
        public IJoinableConfig Join(IJoinableConfig config, IJoinableConfig joiningConfig)
        {
            return config;
        }
    }
}
