// -----------------------------------------------------------------------
// <copyright file="InsightsController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Api.Controllers;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// Insights Controller.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class InsightsController : BaseController
    {
        /// Initializes a new instance of the <see cref="insightService"/> class.
        private readonly IInsightsService insightService;

        /// Initializes a new instance of the <see cref="Configuration"/> class.
        public IConfiguration Configuration { get; }


        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<InsightsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsightsController"/> class.
        /// </summary>
        /// <param name="insightService">insightService.</param>
        /// <param name="configuration">configuration.</param>
        public InsightsController(IInsightsService insightService, IConfiguration configuration, ILogger<InsightsController> logger)
        {
            this.insightService = insightService;
            this.Configuration = configuration;
            this.logger = logger;
        }

        /// <summary>
        /// GetSisenseUrl.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// Updated api route name as part of PCIS-3221.
        [HttpGet]
        [Route("get-insight-url/{agencyInsightDashboardId}")]
        public ActionResult GetSisenseUrl(int agencyInsightDashboardId)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                userTokenDetails.Role = this.GetRole().FirstOrDefault();
                return Ok(this.insightService.GetInsightUrlByDashboardID(userTokenDetails, agencyInsightDashboardId));
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetSisenseUrl/GET : Getting item : Exception  : Exception occurred while getting sisense url. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving sisense url. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetInsightDashboardDetailsForAgency.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route("insight-dashboard-details")]
        public ActionResult<InsightDashboardResponseDTO> GetInsightDashboardDetailsForAgency()
        {
            try
            {
                var agencyId = this.GetTenantID();
                var result = this.insightService.GetInsightDashboardDetailsForAgency(agencyId);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetInsightDashboardDetailsForAgency/GET : Getting item : Exception  : Exception occurred while getting InsightDashboard details for agency. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving InsightDashboard details for agency. Please try again later or contact support.");
            }
        }
    }
}