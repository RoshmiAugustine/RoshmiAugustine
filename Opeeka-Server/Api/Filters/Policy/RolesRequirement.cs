using Microsoft.AspNetCore.Authorization;

namespace Opeeka.PICS.API.Filters.Policy
{
    /// <summary>
    /// The authorization requirement for base VPT authorization.
    /// </summary>
    public class CustomRequirement : IAuthorizationRequirement
    {
        public CustomRequirement()
        {
        }
    }
}
