using System.Security.Claims;

namespace Iris.Services.ClaimsPrincipalHelperService
{
    /// <inheritdoc cref="IClaimsPrincipalHelperService"/>
    public class ClaimsPrincipalHelperService : IClaimsPrincipalHelperService
    {
        /// <inheritdoc/>
        public int GetUserId(ClaimsPrincipal user)
        {
            var id = user.Claims.Single(_ => _.Type == "sid").Value;
            return int.Parse(id);
        }
    }
}