// -----------------------------------------------------------------------
// <copyright file="FileUploadController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// FileUploadController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class FileUploadController : BaseController
    {
        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<FileUploadController> logger;

        /// Initializes a new instance of the <see cref="configuration"/> class.
        private readonly IConfiguration configuration;

        /// Initializes a new instance of the <see cref="fileService"/> class.
        private readonly IFileService fileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploadController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="configuration">configuration.</param>
        /// <param name="fileService">fileService.</param>
        public FileUploadController(ILogger<FileUploadController> logger, IConfiguration configuration, IFileService fileService)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.fileService = fileService;
        }

        /// <summary>
        /// SaveProfilePicAsync.
        /// </summary>
        /// <param name="uploadfile">uploadfile.</param>
        /// <returns>Task.<IActionResult>.</returns>
        [HttpPost]
        [Route("save-profile-pic")]
        public async Task<ActionResult<FileResponseDTO>> SaveProfilePicAsync(IFormFile uploadfile)
        {
            try
            {
                var role = this.GetRole();
                return await this.fileService.SaveProfilePicAsync(uploadfile, this.GetTenantID(), this.GetUserID(), role);
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"SaveProfilePicAsync/POST : Adding profile image : Exception  : Exception occurred Adding profile image. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding profile image. Please try again later or contact support.");
            }
        }
    }
}
