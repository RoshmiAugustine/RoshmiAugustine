// -----------------------------------------------------------------------
// <copyright file="SystemRoleController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// SystemRole Controller.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class SystemRoleController : BaseController
    {
        private readonly ISystemRoleService systemRoleService;
        private readonly ILogger<SystemRoleController> logger;

        /// <summary>
        ///  Initializes a new instance of the <see cref="SystemRoleController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="systemRoleService">systemRoleService.</param>
        public SystemRoleController(ILogger<SystemRoleController> logger, ISystemRoleService systemRoleService)
        {
            this.systemRoleService = systemRoleService;
            this.logger = logger;
        }

        /// <summary>
        /// GetSystemRoleList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>SystemRoleListResponseDTO.</returns>
        [HttpGet]
        [Route("system-role/{pageNumber}/{pageSize}")]
        public ActionResult<SystemRoleListResponseDTO> GetSystemRoleList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.systemRoleService.GetSystemRoleList(pageNumber, pageSize);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetSystemRoleList/Get : GET item : Exception  : Exception occurred getting system role list. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving system role list. Please try again later or contact support.");
            }
        }
    }
}
