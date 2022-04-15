namespace Iris.Exceptions.UserExceptions
{
    /// <summary>
    /// Ошибка: пользователь не авторизован
    /// </summary>
    public class UserNotAuthorizedException : IrisException
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public UserNotAuthorizedException(int userId, string russianMessage = null) : base(russianMessage ?? $"Пользователь c id {userId} не авторизован") { }
    }
}
