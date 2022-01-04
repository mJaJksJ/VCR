using Iris.Configuration;

namespace Iris.ReflectionExtensions.NotBasicTypeJoin
{
    public class ConfigJoiner : INotBasicTypesJoiner
    {
        public IJoinableConfig Join(IJoinableConfig config, IJoinableConfig joiningConfig)
        {
            var rightConfig = config as Config;
            var leftConfig = joiningConfig as Config;
            rightConfig.JoinWith(rightConfig.Database, leftConfig.Database, new DatabaseConfigJoiner());
            rightConfig.JoinWith(rightConfig.Logger, leftConfig.Logger, new LoggerConfigJoiner());

            // TODO: доработать
            // проработать варианты дозаписи существующих, добавления новых, пропуска тех что не обновляются
            //rightConfig.JoinWith(rightConfig.MailServers, leftConfig.MailServers, new MailServerConfigJoiner());

            return rightConfig;
        }
    }
}
