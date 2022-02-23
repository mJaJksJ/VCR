using Iris.Database;
using System.Security.Claims;

namespace Iris.Services.AuthService
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Авторизоваться
        /// </summary>
        /// <param name="operation">Операция запроса авторизации</param>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        (ClaimsIdentity, User) Authorize(
            AuthRequestOperation operation,
            string login,
            string password);
    }
}
