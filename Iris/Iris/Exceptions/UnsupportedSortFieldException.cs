namespace Iris.Exceptions
{
    /// <summary>
    /// Неподдерживаемое для сортировки поле
    /// </summary>
    public class UnsupportedSortFieldException : IrisException
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public UnsupportedSortFieldException(string name, string russianMessage = null) : base(russianMessage ?? $"Неизвестное поле {name}") { }

    }
}
