using System.Security.Claims;

namespace Iris.Helpers
{
    /// <summary>
    /// Расширение для ClaimsPrincipal
    /// </summary>
    public static class ClaimsPrincipalExtensios
    {
        /// <summary>
        /// Получить Id пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var id = user.Claims.Single(_ => _.Type == "sid").Value;
            return int.Parse(id);
        }
    }
}
