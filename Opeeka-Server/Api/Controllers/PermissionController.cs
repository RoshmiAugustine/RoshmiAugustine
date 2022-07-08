// -----------------------------------------------------------------------
// <copyright file="PermissionController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Enums;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// ConsumerController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class PermissionController : BaseController
    {
        /// Initializes a new instance of the <see cref="permissionService"/> class.
        /// <summary>
        /// Defines the agencyService.
        /// </summary>
        private readonly IPermissionService permissionService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        /// <summary>
        /// Defines the logger.
        /// </summary>
        private readonly ILogger<PermissionController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="permissionService">agencyService.</param>
        public PermissionController(ILogger<PermissionController> logger, IPermissionService permissionService)
        {
            this.permissionService = permissionService;
            this.logger = logger;
        }

        /// <summary>
        /// GetPermissions.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("permissions")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetPermissions()
        {
            try
            {
                int roleID = Convert.ToInt32(base.GetRoleId());
                List<string> applicationObjectTypes = new List<string>();
                applicationObjectTypes.Add(PCISEnum.ApplicationObjectTypes.UIComponentButton);
                applicationObjectTypes.Add(PCISEnum.ApplicationObjectTypes.UIComponentMenu);
                var result = this.permissionService.GetUserPermissionsByRoleID(roleID, applicationObjectTypes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetPermissions/GET : Listing UI Permissions : Exception  : Exception occurred getting UI permissions. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving UI permissions. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPermissions.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("shared-permissions/{agencySharingIndex}/{collaborationSharingIndex}/{isActiveForSharing}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetSharedPermissions(string agencySharingIndex, string collaborationSharingIndex, bool isActiveForSharing)
        {
            try
            {
                int roleID = Convert.ToInt32(base.GetRoleId());
                List<string> applicationObjectTypes = new List<string>();
                applicationObjectTypes.Add(PCISEnum.ApplicationObjectTypes.UIComponentButton);
                applicationObjectTypes.Add(PCISEnum.ApplicationObjectTypes.UIComponentMenu);
                var result = this.permissionService.GetSharedPersonPermissionsByRoleID(roleID, applicationObjectTypes, agencySharingIndex, collaborationSharingIndex, isActiveForSharing);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetSharedPermissions/GET : Listing shared Permissions : Exception  : Exception occurred getting shared permissions. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving shared permissions. Please try again later or contact support.");
            }
        }
    }
}
