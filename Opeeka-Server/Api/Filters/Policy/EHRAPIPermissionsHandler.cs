// -----------------------------------------------------------------------
// <copyright file="WebAPIPermissionsHandler.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.API.Filters.Policy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Infrastructure.Enums;
    using Serilog;

    /// <summary>
    /// The authorization policy to validate web api permissions.
    /// </summary>
    public class EHRAPIPermissionHandler : AuthorizationHandler<EHRAPIPermissionRequirement>
    {
        private static readonly ILogger Logger = Log.ForContext<EHRAPIPermissionHandler>();

        private readonly IHttpContextAccessor HttpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="EHRAPIPermissionHandler"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        public EHRAPIPermissionHandler(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EHRAPIPermissionRequirement requirement)
        {
            var isEHRRequest = this.GetIsEHRPermission();
            if (isEHRRequest == "true")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
        
        public string GetIsEHRPermission()
        {
            var claimsIdentity = (ClaimsIdentity)HttpContextAccessor.HttpContext.User.Identity;
            var claim = claimsIdentity.FindFirst(PCISEnum.TokenClaims.EHRRequest);

            if (claim == null)
            {
                return string.Empty;
            }
            var isEHRRequest = JsonConvert.DeserializeObject<string>(claim.Value);
            return isEHRRequest;
        }
    }
}
