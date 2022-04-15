namespace Iris.Exceptions
{
    /// <summary>
    /// Ошибка парсинга имени и email персоны
    /// </summary>
    public class ParsePersonInternetAddressException : IrisException
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public ParsePersonInternetAddressException(string person, string russianMessage = null) : base(russianMessage ?? $"Не удалось разобрать имя и email {person}") { }
    }
}
