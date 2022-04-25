using Iris.Api.Controllers.AuthControllers;
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
        /// <param name="authRequest">Контракт запроса авторизации</param>
        (ClaimsIdentity, User) Authorize(AuthRequestOperation operation, AuthRequestContract authRequest);

        /// <summary>
        /// Создать токен
        /// </summary>
        /// <param name="claims">Клеймсы</param>
        /// <param name="user">Пользователь</param>
        (string token, DateTime expires) GenerateToken(IEnumerable<Claim> claims, User user);
    }
}
