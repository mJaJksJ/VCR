using Iris.ReflectionExtensions;
using Iris.ReflectionExtensions.NotBasicTypeJoin;

namespace Iris.Configuration
{
    public interface IJoinableConfig { }

    public abstract class ConfigExtension
    {
        public IJoinableConfig JoinWith(IJoinableConfig config, IJoinableConfig joiningConfig, INotBasicTypesJoiner notBasicTypesJoiner)
        {
            var properties = joiningConfig.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.IsBasic())
                {
                    if (prop.IsNotEmpty(joiningConfig, out object val))
                    {
                        prop.SetValue(config, val);
                    }
                }
                else
                {
                    //TODO: времменное решение, если так ине получится полностью автоматизировать
                    notBasicTypesJoiner.Join(config, joiningConfig);                
                }

            }
            return config;
        }
    }
}
