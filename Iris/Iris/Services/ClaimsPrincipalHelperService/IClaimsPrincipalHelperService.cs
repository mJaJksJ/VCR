using System.Security.Claims;

namespace Iris.Services.ClaimsPrincipalHelperService
{
    /// <summary>
    /// Сервис работы с ClaimsPrincipal
    /// </summary>
    public interface IClaimsPrincipalHelperService
    {
        /// <summary>
        /// Получить Id пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        int GetUserId(ClaimsPrincipal user);
    }
}
