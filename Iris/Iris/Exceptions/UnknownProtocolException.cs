namespace Iris.Exceptions
{
    /// <summary>
    /// Ошибка неизвестного протокола
    /// </summary>
    public class UnknownProtocolException : IrisException
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public UnknownProtocolException(string russianMessage = null, string protocol = null) : base(russianMessage ?? $"Неизвестный протокол {protocol}") { }

    }
}
