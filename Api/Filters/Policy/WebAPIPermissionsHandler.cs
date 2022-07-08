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
    public class WebAPIPermissionHandler : AuthorizationHandler<CustomRequirement>
    {
        private static readonly ILogger Logger = Log.ForContext<WebAPIPermissionHandler>();

        private readonly IHttpContextAccessor HttpContextAccessor;
        /// <summary>
        /// Initializes a new instance of the <see cref="WebAPIPermissionHandler"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        public WebAPIPermissionHandler(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
        {
            bool hasPermission = false;
            var isEHRRequest = GetIsEHRPermission();
            if (isEHRRequest == "true")
            {
                foreach (var req in context.Requirements.OfType<EHRAPIPermissionRequirement>())
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            var path = HttpContextAccessor.HttpContext.Request.Path.Value;
            var routeValues = HttpContextAccessor.HttpContext.Request.RouteValues.ToList();

            var parameterRouteValues = routeValues.Where(rv => rv.Key != "controller" && rv.Key != "action").ToList();
            path = ReplaceRouteValues(path, parameterRouteValues);
            var requestType = HttpContextAccessor.HttpContext.Request.Method;
            var APIpermissions = this.GetAPIPermissions();
            var isSharedPerms = this.GetIsSharedPermission();
            if (isSharedPerms == "true")
            {
                var sharedAPIPermissions = this.GetSharedAPIPermissions();
                hasPermission = sharedAPIPermissions.Any(p => p.Name.ToLower().Trim('/') == path.ToLower().Trim('/') && p.OperationTypes.Contains(requestType.ToLower()));
            }
            else
            {
                hasPermission = APIpermissions.Any(p => p.Name.ToLower().Trim('/') == path.ToLower().Trim('/') && p.OperationTypes.Contains(requestType.ToLower()));
            }

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

        public string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public string ReplaceRouteValues(string path, List<KeyValuePair<string, object>> parameterRouteValues)
        {
            var segments = path.Split('/').ToList();
            var routeSegments = segments.SkipLast(parameterRouteValues.Count).ToList();
            var parameterSegments = segments.Skip(routeSegments.Count).ToList();
            foreach (var item in parameterRouteValues)
            {
                string oldValue = item.Value.ToString();
                string newValue = "{" + item.Key + "}";
                int index = parameterSegments.IndexOf(oldValue);
                if (index != -1)
                {
                    parameterSegments[index] = newValue;
                }
            }

            routeSegments.AddRange(parameterSegments);
            var result = string.Join('/', routeSegments.ToArray());
            return result;
        }

        /// <summary>
        /// Get the controller action permissions that apply to the logged in user.
        /// </summary>
        /// <returns>List of permissions or an empty list.</returns>
        public List<ApplicationObjectDTO> GetAPIPermissions()
        {
            var claimsIdentity = (ClaimsIdentity)HttpContextAccessor.HttpContext.User.Identity;
            var claims = claimsIdentity.FindAll(PCISEnum.PermissionsClaim.APIEndPointPermission);

            var claimsList = claims as IList<Claim> ?? claims.ToList();
            if (!claimsList.Any())
            {
                return new List<ApplicationObjectDTO>();
            }

            var perms = claimsList.Select(claim => JsonConvert.DeserializeObject<ApplicationObjectDTO>(claim.Value)).ToList();
            return perms;
        }

        /// <summary>
        /// Get the controller action permissions that apply to the logged in user
        /// </summary>
        /// <returns>List of permissions or an empty list</returns>
        public string GetIsSharedPermission()
        {
            var claimsIdentity = (ClaimsIdentity)HttpContextAccessor.HttpContext.User.Identity;
            var claim = claimsIdentity.FindFirst(PCISEnum.Parameters.IsSharedPermission);

            if (claim == null)
            {
                return string.Empty;
            }
            var isSharedPerms = JsonConvert.DeserializeObject<string>(claim.Value);
            return isSharedPerms;
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

        /// <summary>
        /// Get the controller action permissions that apply to the logged in user
        /// </summary>
        /// <returns>List of permissions or an empty list</returns>
        public List<ApplicationObjectDTO> GetSharedAPIPermissions()
        {
            var claimsIdentity = (ClaimsIdentity)HttpContextAccessor.HttpContext.User.Identity;
            var claims = claimsIdentity.FindAll(PCISEnum.SharedPermissionsClaim.APIEndPointSharedPermission);

            var claimsList = claims as IList<Claim> ?? claims.ToList();
            if (!claimsList.Any()) return new List<ApplicationObjectDTO>();

            var perms = claimsList.Select(claim => JsonConvert.DeserializeObject<ApplicationObjectDTO>(claim.Value)).ToList();
            return perms;
        }

        /// <summary>
        /// Get the namespace permissions that apply to the logged in user
        /// </summary>
        /// <returns>List of permissions or an empty list.</returns>
        public List<ApplicationObjectDTO> GetNamespacePermissions()
        {
            var claimsIdentity = (ClaimsIdentity)HttpContextAccessor.HttpContext.User.Identity;
            var claims = claimsIdentity.FindAll(PCISEnum.PermissionsClaim.NamespacePermission);

            var claimsList = claims as IList<Claim> ?? claims.ToList();
            if (!claimsList.Any())
            {
                return new List<ApplicationObjectDTO>();
            }

            var perms = claimsList.Select(claim => JsonConvert.DeserializeObject<ApplicationObjectDTO>(claim.Value)).ToList();
            return perms;
        }

        public List<ApplicationObjectDTO> GetModulePermissions()
        {
            var claimsIdentity = (ClaimsIdentity)HttpContextAccessor.HttpContext.User.Identity;
            var claims = claimsIdentity.FindAll(PCISEnum.PermissionsClaim.ModulePermission);

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
