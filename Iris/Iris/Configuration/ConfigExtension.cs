﻿using Iris.Configuration.NotBasicTypeJoin;
using Iris.Helpers.ReflectionExtensions;

namespace Iris.Configuration
{
    /// <summary>
    /// Интерфейс объединяемых конфигов
    /// </summary>
    public interface IJoinableConfig { }

    /// <summary>
    /// Расширение для классов конфигов
    /// </summary>
    public abstract class ConfigExtension
    {
        /// <summary>
        /// Объединить конфиги
        /// </summary>
        /// <param name="config">Основной конфиг</param>
        /// <param name="joiningConfig">Добавляемый конфиг</param>
        /// <param name="notBasicTypesJoiner">Объединитель конфигов с полями не простых типов</param>
        /// <returns>Объединенный конфиг</returns>
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
                    switch (notBasicTypesJoiner)
                    {
                        case AuthConfigJoiner authConfigJoiner:
                            authConfigJoiner.Join(config, joiningConfig);
                            break;

                        default:
                            notBasicTypesJoiner.Join(config, joiningConfig);
                            break;
                    }
                }

            }
            return config;
        }
    }
}
