namespace Iris.Configuration.NotBasicTypeJoin
{
    /// <summary>
    /// Объединитель конфигов с полями не простых типов
    /// </summary>
    public interface INotBasicTypesJoiner
    {
        /// <summary>
        /// Объединить конфиги
        /// </summary>
        /// <param name="config">Основной конфиг</param>
        /// <param name="joiningConfig">Добавляемый конфиг</param>
        /// <returns>Объединенный конфиг</returns>
        public IJoinableConfig Join(IJoinableConfig config, IJoinableConfig joiningConfig);
    }
}
