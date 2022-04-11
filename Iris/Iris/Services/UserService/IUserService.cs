namespace Iris.Services.UserService
{
    /// <summary>
    /// Сервис работы с пользователем
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Убедиться что пользователь существует
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        void EnsureUserExist(int userId);
    }
}
