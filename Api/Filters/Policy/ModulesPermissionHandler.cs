// -----------------------------------------------------------------------
// <copyright file="ModulesPermissionHandler.cs" company="Naico ITS">
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
    using Serilog;

    /// <summary>
    /// The authorization policy to validate web api permissions.
    /// </summary>
    public class ModulesPermissionHandler : AuthorizationHandler<CustomRequirement>
    {
        private static readonly ILogger Logger = Log.ForContext<WebAPIPermissionHandler>();

        private readonly IHttpContextAccessor HttpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModulesPermissionHandler"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        public ModulesPermissionHandler(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;

            // SystemUserOptions = systemUserOptions.Value;
        }

        /// <inheritdoc/>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
        {
            var path = HttpContextAccessor.HttpContext.Request.Path.Value;
            var routeValues = HttpContextAccessor.HttpContext.Request.RouteValues;
            var controllerName = routeValues.GetValueOrDefault("controller").ToString().ToLower();

            // var action = routeValues.GetValueOrDefault("action").ToString().ToLower();
            var permissions = GetModulesPermissions();
            var hasPermission = permissions.Any(p => p.Name.ToLower() == controllerName);

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Get the controller action permissions that apply to the logged in user.
        /// </summary>
        /// <returns>List of permissions or an empty list.</returns>
        public List<ApplicationObjectDTO> GetModulesPermissions()
        {
            var claimsIdentity = (ClaimsIdentity)HttpContextAccessor.HttpContext.User.Identity;
            var claims = claimsIdentity.FindAll("module_permission");

            var claimsList = claims as IList<Claim> ?? claims.ToList();
            if (!claimsList.Any())
            {
                return new List<ApplicationObjectDTO>();
            }

            var perms = claimsList.Select(claim => JsonConvert.DeserializeObject<ApplicationObjectDTO>(claim.Value)).ToList();
            return perms;
        }
    }
}
