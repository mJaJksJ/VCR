namespace Iris.Exceptions
{
    /// <summary>
    /// Неизвестное поле
    /// </summary>
    public class UnknownFieldException : IrisException
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public UnknownFieldException(string name, string russianMessage = null) : base(russianMessage ?? $"Неизвестное поле {name}") { }

    }
}
