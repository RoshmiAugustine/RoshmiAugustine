using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Opeeka.PICS.API.Filters.Policy;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Opeeka.PICS.API.Filters.Policy
{
    public class APIUserPermissionHandler : AuthorizationHandler<APIUserPermissionRequirement>
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="APIUserPermissionHandler"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        public APIUserPermissionHandler(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, APIUserPermissionRequirement requirement)
        {
            var isAPIUserRequest = GetIsAPIUserPermission();
            if (isAPIUserRequest && !context.HasFailed)
            {
                var requestReq = context.Requirements.OfType<APIUserPermissionRequirement>();
                if (requestReq.ToList().Count != 0)
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }

        public bool GetIsAPIUserPermission()
        {
            var claims = HttpContextAccessor.HttpContext.User.Claims;
            var result = claims.Where(x => x.Type == PCISEnum.B2cClaims.B2cRPOCTFP).FirstOrDefault()?.Value.ToString();
            if (result == null)
            {
                return false;
            }
            var role = claims.Where(x => x.Type == PCISEnum.B2cClaims.B2cRole).FirstOrDefault()?.Value.ToString();
            if (role != PCISEnum.Roles.APIUser)
            {
                return false;
            }
            return true;
        }

    }
}

