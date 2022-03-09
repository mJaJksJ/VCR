using System.Reflection;

namespace Iris.Helpers.ReflectionExtensions
{
    /// <summary>
    /// Базовые типы данных
    /// TODO: дополнять при расширении конфига
    /// </summary>
    public enum BasicTypes
    {
#pragma warning disable 1591
        String,
        Int32
#pragma warning restore 1591
    }

    /// <summary>
    /// Расширение для PropertyInfo
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Не является ли свойство пустым
        /// </summary>
        /// <param name="propertyInfo">Свойство</param>
        /// <param name="checkingObj">Объект проверяемого свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool IsNotEmpty(this PropertyInfo propertyInfo, object checkingObj, out object value)
        {
            var type = propertyInfo.PropertyType.Name;
            value = propertyInfo.GetValue(checkingObj);

            if (BasicTypes.String.ToString() == type)
            {
                return !string.IsNullOrWhiteSpace(value as string);
            }

            if (BasicTypes.Int32.ToString() == type)
            {
                return (value as int?).Value != default;
            }

            //TODO: заменить на класс
            throw new Exception($"Unknown type {type} for reflection");
        }

        /// <summary>
        /// Простой ли тип свойства
        /// </summary>
        /// <param name="propertyInfo">Свойство</param>
        /// <returns></returns>
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
