namespace Iris.Exceptions.UserExceptions
{
    /// <summary>
    /// Ошибка несуществующего пользователя
    /// </summary>
    public class UserNotExistException : IrisException
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public UserNotExistException(int userId, string russianMessage = null) : base(russianMessage ?? $"Пользователь c id {userId} не существует") { }

        /// <summary>
        /// .ctor
        /// </summary>
        public UserNotExistException(string login, string russianMessage = null) : base(russianMessage ?? $"Пользователь {login} не существует") { }
    }
}
