namespace Iris.Configuration.NotBasicTypeJoin
{
    /// <inheritdoc cref="INotBasicTypesJoiner"/>
    public class DatabaseConfigJoiner : INotBasicTypesJoiner
    {
        /// <inheritdoc/>
        public IJoinableConfig Join(IJoinableConfig config, IJoinableConfig joiningConfig)
        {
            return config;
        }
    }
}
