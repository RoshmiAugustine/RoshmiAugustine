// -----------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Opeeka.PICS.Infrastructure.Enums;

    /// <summary>
    /// BaseController.
    /// </summary>
    [Authorize(Policy = "WebAPIPermission")]
    [ApiExplorerSettings(GroupName = "api-v1")]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Generic method to handle exceptions for HttpResponseMessage.
        /// </summary>
        /// <param name="exp">Exception.</param>
        /// <param name="userErrorMessage">Safe error message to display to the user.</param>
        /// <param name="httpStatus">error status to display to the user.</param>
        /// <returns>Result.</returns>
        protected ActionResult HandleException(Exception exp, string userErrorMessage, HttpStatusCode httpStatus = HttpStatusCode.InternalServerError)
        {
#if DEBUG
            return this.StatusCode((int)httpStatus, $"{userErrorMessage}: {exp.Message}");
#else
            return StatusCode((int)httpStatus, userErrorMessage);
#endif
        }

        /// <summary>
        /// Method for get user id.
        /// </summary>
        /// <returns>Int32.</returns>
        protected int GetUserID()
        {
            var result = ((ClaimsIdentity)this.User.Identity).FindFirst("UserId");
            return result != null ? Convert.ToInt32(result.Value) : 0;
        }

        /// <summary>
        /// GetTenantID.
        /// </summary>
        /// <returns>long.</returns>
        protected long GetTenantID()
        {
            var result = ((ClaimsIdentity)this.User.Identity).FindFirst("TenantId");
            return result != null ? Convert.ToInt64(result.Value) : 0;
        }

        /// <summary>
        /// Method to get token.
        /// </summary>
        /// <returns>string.</returns>
        protected string GetToken()
        {
            var result = ((ClaimsIdentity)User.Identity).FindFirst("token");
            return result != null ? result.Value : string.Empty;
        }

        /// <summary>
        /// Method to get b2cToken.
        /// </summary>
        /// <returns>string.</returns>
        protected string GetB2CToken()
        {
            var result = ((ClaimsIdentity)User.Identity).FindFirst("b2cToken");
            return result != null ? result.Value : string.Empty;
        }

        /// <summary>
        /// Method to get role.
        /// </summary>
        /// <returns>list string.</returns>
        protected List<string> GetRole()
        {
            var rolesClaim = ((ClaimsIdentity)User.Identity).FindFirst("Roles");
            return rolesClaim != null ? JsonConvert.DeserializeObject<List<string>>(rolesClaim.Value) : new List<string>();
        }

        /// <summary>
        /// Method to get role.
        /// </summary>
        /// <returns>int.</returns>
        protected int GetRoleId()
        {
            var result = ((ClaimsIdentity)this.User.Identity).FindFirst(PCISEnum.TokenClaims.RoleID);
            return result != null ? Convert.ToInt32(result.Value) : 0;
        }

        /// <summary>
        /// Method for get tenant abbrev.
        /// </summary>
        /// <returns>string.</returns>
        protected string GetTenantAbbreviation()
        {
            var result = ((ClaimsIdentity)User.Identity).FindFirst(PCISEnum.TokenClaims.AgencyAbbrev);
            return result != null ? result.Value : string.Empty;
        }

        /// <summary>
        /// Method for get email ID.
        /// </summary>
        /// <returns>string.</returns>
        protected string GetEmailID()
        {
            var result = ((ClaimsIdentity)User.Identity).FindFirst(PCISEnum.TokenClaims.Email);
            return result != null ? result.Value : string.Empty;
        }

        /// <summary>
        /// Method for get Instance URL.
        /// </summary>
        /// <returns>string.</returns>
        protected string GetInstanceURL()
        {
            var result = ((ClaimsIdentity)User.Identity).FindFirst(PCISEnum.TokenClaims.InstanceURL);
            return result != null ? result.Value : string.Empty;
        }
    }
}