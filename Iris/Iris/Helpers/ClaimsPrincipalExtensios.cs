using System.Security.Claims;

namespace Iris.Helpers
{
    public static class ClaimsPrincipalExtensios
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var id = user.Claims.Single(_ => _.Type == "sid").Value;
            return int.Parse(id);
        }
    }
}
