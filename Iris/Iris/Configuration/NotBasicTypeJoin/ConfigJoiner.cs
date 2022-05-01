namespace Iris.Configuration.NotBasicTypeJoin
{
    /// <inheritdoc cref="INotBasicTypesJoiner"/>
    public class ConfigJoiner : INotBasicTypesJoiner
    {
        /// <inheritdoc/>
        public IJoinableConfig Join(IJoinableConfig config, IJoinableConfig joiningConfig)
        {
            var rightConfig = config as Config;
            var leftConfig = joiningConfig as Config;
            rightConfig.JoinWith(rightConfig.Database, leftConfig.Database, null);
            rightConfig.JoinWith(rightConfig.Logger, leftConfig.Logger, null);
            rightConfig.JoinWith(rightConfig.AuthConfig, leftConfig.AuthConfig, new AuthConfigJoiner());
            return rightConfig;
        }
    }
}
