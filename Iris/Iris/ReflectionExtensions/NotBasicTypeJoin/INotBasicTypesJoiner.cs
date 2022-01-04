using Iris.Configuration;

namespace Iris.ReflectionExtensions.NotBasicTypeJoin
{
    public interface INotBasicTypesJoiner
    {
        public IJoinableConfig Join(IJoinableConfig config, IJoinableConfig joiningConfig);
    }
}
