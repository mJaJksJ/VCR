using Iris.Configuration;

namespace Iris.Configuration.NotBasicTypeJoin
{
    /// <inheritdoc cref="INotBasicTypesJoiner"/>
    public class LoggerConfigJoiner : INotBasicTypesJoiner
    {
        /// <inheritdoc/>
        public IJoinableConfig Join(IJoinableConfig config, IJoinableConfig joiningConfig)
        {
            return config;
        }
    }
}
