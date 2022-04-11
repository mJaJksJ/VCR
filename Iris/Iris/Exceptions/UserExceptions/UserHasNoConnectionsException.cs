namespace Iris.Exceptions.UserExceptions
{
    /// <summary>
    /// Ошибка отсутствияподключения к почтовому серверу
    /// </summary>
    public class UserHasNoConnectionsException : IrisException
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public UserHasNoConnectionsException(string russianMessage = null) : base(russianMessage ?? "Пользователь не имеет подключений к серверам") { }
    }
}
