namespace Iris.Exceptions
{
    /// <summary>
    /// Ошибка авторизации
    /// </summary>
    public class AuthException : IrisException
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public AuthException(string russianMessage = null) : base(russianMessage ?? "Неверное имя пользователя или пароль") { }
    }
}
