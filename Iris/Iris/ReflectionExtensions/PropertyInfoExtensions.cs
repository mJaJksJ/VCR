using System.Reflection;
using Serilog;

namespace Iris.ReflectionExtensions
{
    public enum BasicTypes
    {
        String,
        Int32
    }

    public static class PropertyInfoExtensions
    {
        public static bool IsNotEmpty(this PropertyInfo propertyInfo, object checkingObj, out object value)
        {
            var type = propertyInfo.PropertyType.Name;           
            value = propertyInfo.GetValue(checkingObj);

            if(BasicTypes.String.ToString() == type)
            {
                return !string.IsNullOrWhiteSpace(value as string);
            }

            if (BasicTypes.Int32.ToString() == type)
            {
                return (value as int?).Value != default;
            }

            throw new Exception($"Unknown type {type} for reflection");
        }

        public static bool IsBasic(this PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType.Name;

            var basicTypes = Enum
                .GetValues<BasicTypes>()
                .Select(_ => _.ToString());

            return basicTypes.Contains(type);
        }
    }
}
