// -----------------------------------------------------------------------
// <copyright file="ErrorLogController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Input;
    using Opeeka.PICS.Domain.Resources;
    using Opeeka.PICS.Infrastructure.Enums;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// ErrorLogController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class ErrorLogController : BaseController
    {
        /// <summary>
        /// logger.
        /// </summary>
        private readonly ILogger<ErrorLogController> logger;

        /// <summary>
        /// LocalizeService.
        /// </summary>
        private readonly LocalizeService localize;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLogController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="localizeService">localizeService.</param>
        public ErrorLogController(ILogger<ErrorLogController> logger, LocalizeService localizeService)
        {
            this.logger = logger;
            this.localize = localizeService;
        }

        /// <summary>
        /// AddExceptionLogDetails.
        /// </summary>
        /// <param name="errorLogDTO">errorLogDTO.</param>
        /// <returns>ErrorLogResponseDTO.</returns>
        [HttpPost]
        [Route("error-log")]
        public ActionResult<ErrorLogResponseDTO> AddExceptionLogDetails([FromBody] ErrorLogDTO errorLogDTO)
        {
            try
            {
                ErrorLogResponseDTO resultDTO = new ErrorLogResponseDTO();
                long agencyID = this.GetTenantID();

                this.logger.LogError(MyLogEvents.ErrorLog, $"Agency : {agencyID}. Client Side Exception : {errorLogDTO.ErrorMessage}");

                resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.ErrorLog;
                resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.ErrorLog);
                return resultDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ErrorLog, $"AddExceptionLogDetails/POST : Adding client side exception Log  : Exception occurred adding Exception Log Details.{ex.Message}");
                return this.HandleException(ex, "An error occurred while adding Exception Log Details. Please try again later or contact support.");
            }
        }
    }
}
