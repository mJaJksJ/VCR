namespace Iris.Configuration.NotBasicTypeJoin
{
    /// <inheritdoc cref="INotBasicTypesJoiner"/>
    public class MailServerConfigJoiner : INotBasicTypesJoiner
    {
        /// <inheritdoc/>
        public IJoinableConfig Join(IJoinableConfig config, IJoinableConfig joiningConfig)
        {
            return config;
        }
    }
}
