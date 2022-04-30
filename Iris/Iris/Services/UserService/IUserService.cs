using Iris.Database;

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
        User EnsureUserExist(int userId);

        /// <summary>
        /// Получить юзера по его логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        User GetUserByLogin(string login);
    }
}
