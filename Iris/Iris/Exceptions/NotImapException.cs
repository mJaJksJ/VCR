namespace Iris.Exceptions
{
    /// <summary>
    /// Не Ipam подключение
    /// </summary>
    public class NotImapException : IrisException
    {        /// <summary>
             /// .ctor
             /// </summary>
        public NotImapException(string russianMessage = null) : base(russianMessage ?? $"Не Ipam подключение") { }
    }
}
