// -----------------------------------------------------------------------
// <copyright file="DashboardController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Input;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Enums;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// Dashboard Controller.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class DashboardController : BaseController
    {
        /// Initializes a new instance of the <see cref="personQuestionnaireMetricsService"/> class.
        private readonly IPersonQuestionnaireMetricsService personQuestionnaireMetricsService;

        /// Initializes a new instance of the <see cref="helperService"/> class.
        private readonly IHelperService helperService;

        /// Initializes a new instance of the <see cref="searchService"/> class.
        private readonly ISearchService searchService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<DashboardController> logger;

        /// <summary>
        ///  Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="personQuestionnaireMetricsService">personQuestionnaireMetricsService.</param>
        /// <param name="helperService">helperService.</param>
        /// <param name="searchService">searchService.</param>
        public DashboardController(ILogger<DashboardController> logger, IPersonQuestionnaireMetricsService personQuestionnaireMetricsService, IHelperService helperService, ISearchService searchService)
        {
            this.personQuestionnaireMetricsService = personQuestionnaireMetricsService;
            this.helperService = helperService;
            this.searchService = searchService;
            this.logger = logger;
        }

        /// <summary>
        /// Get Strength metrics data for Dashboard.
        /// </summary>
        /// <param name="strengthMetricsSearchDTO">strengthMetricsSearchDTO.</param>
        /// <returns>DashboardStrengthMetricsListResponseDTO.</returns>
        [HttpPost]
        [Route("strength-metrics")]
        public ActionResult<DashboardStrengthMetricsListResponseDTO> GetStrengthMetricsList([FromBody] StrengthMetricsSearchDTO strengthMetricsSearchDTO)
        {
            try
            {
                int? helperID = null;
                bool isSameAsLoggedInUser = false;
                var userID = this.GetUserID();
                var userRole = this.GetRole();
                var agencyID = this.GetTenantID();

                strengthMetricsSearchDTO.userID = userID;
                if (strengthMetricsSearchDTO.helperIndex == null)
                {
                    var helperData = this.helperService.GetUserDetails(Convert.ToInt32(userID));
                    if (helperData != null && helperData.UserDetails != null)
                    {
                        helperID = helperData.UserDetails.HelperID;
                    }
                }
                else
                {
                    var helperInfo = this.helperService.GetHelperInfo((Guid)strengthMetricsSearchDTO.helperIndex,agencyID);
                    if (helperInfo != null && helperInfo.HelperDetails != null)
                    {
                        strengthMetricsSearchDTO.userID = helperInfo.HelperDetails.UserId;
                        helperID = helperInfo.HelperDetails.HelperID;
                        userRole = new List<string>() { helperInfo.HelperDetails.Role };
                        if (helperInfo.HelperDetails.UserId == userID)
                        {
                            isSameAsLoggedInUser = true;
                        }
                    }
                    else
                    {
                        DashboardStrengthMetricsListResponseDTO emptyResponseDTO = new DashboardStrengthMetricsListResponseDTO();
                        emptyResponseDTO.DashboardStrengthMetricsList = null;
                        emptyResponseDTO.TotalCount = 0;
                        emptyResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                        emptyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return emptyResponseDTO;
                    }
                }

                strengthMetricsSearchDTO.isSameAsLoggedInUser = isSameAsLoggedInUser;
                strengthMetricsSearchDTO.helperID = helperID.HasValue ? helperID.Value : 0;
                strengthMetricsSearchDTO.userRole = userRole;
                strengthMetricsSearchDTO.agencyID = agencyID;
                var response = this.personQuestionnaireMetricsService.GetDashboardStrengthMetrics(strengthMetricsSearchDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetStrengthMetricsList/POST : Listing Dashboard Strength metrics : Exception  : Exception occurred getting Strength metrics. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving strength metrics. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get Needs metrics data for Dashboard.
        /// </summary>
        /// <param name="needMetricsSearchDTO">needMetricsSearchDTO.</param>
        /// <returns>DashboardNeedMetricsListResponseDTO.</returns>
        [HttpPost]
        [Route("need-metrics")]
        public ActionResult<DashboardNeedMetricsListResponseDTO> GetNeedMetricsList([FromBody] NeedMetricsSearchDTO needMetricsSearchDTO)
        {
            try
            {
                int? helperID = null;
                bool isSameAsLoggedInUser = false;
                var userID = this.GetUserID();
                var userRole = this.GetRole();
                var agencyID = this.GetTenantID();

                needMetricsSearchDTO.userID = userID;
                if (needMetricsSearchDTO.helperIndex == null)
                {
                    var helperData = this.helperService.GetUserDetails(Convert.ToInt32(userID));
                    if (helperData != null && helperData.UserDetails != null)
                    {
                        helperID = helperData.UserDetails.HelperID;
                    }
                }
                else
                {
                    var helperInfo = this.helperService.GetHelperInfo((Guid)needMetricsSearchDTO.helperIndex, agencyID);
                    if (helperInfo != null && helperInfo.HelperDetails != null)
                    {
                        needMetricsSearchDTO.userID = helperInfo.HelperDetails.UserId;
                        helperID = helperInfo.HelperDetails.HelperID;
                        userRole = new List<string>() { helperInfo.HelperDetails.Role }; 
                        if (helperInfo.HelperDetails.UserId == userID)
                        {
                            isSameAsLoggedInUser = true;
                        }
                    }
                    else
                    {
                        DashboardNeedMetricsListResponseDTO emptyResponseDTO = new DashboardNeedMetricsListResponseDTO();
                        emptyResponseDTO.DashboardNeedMetricsList = null;
                        emptyResponseDTO.TotalCount = 0;
                        emptyResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                        emptyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return emptyResponseDTO;
                    }
                }

                needMetricsSearchDTO.isSameAsLoggedInUser = isSameAsLoggedInUser;
                needMetricsSearchDTO.helperID = helperID.HasValue ? helperID.Value : 0;
                needMetricsSearchDTO.userRole = userRole;
                needMetricsSearchDTO.agencyID = agencyID;
                var response = this.personQuestionnaireMetricsService.GetDashboardNeedMetrics(needMetricsSearchDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetNeedMetricsList/POST : Listing Dashboard Need metrics : Exception  : Exception occurred getting Need metrics. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving need metrics. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get Strength pie chart data for Dashboard.
        /// </summary>
        /// <param name="helperIndex">helperIndex.</param>
        /// <returns>DashboardStrengthPieChartResponseDTO.</returns>
        [HttpGet]
        [Route("strength-pie-chart/{helperIndex?}")]
        public ActionResult<DashboardStrengthPieChartResponseDTO> GetStrengthPieChartData(Guid? helperIndex = null)
        {
            try
            {
                int? helperID = null;
                bool isSameAsLoggedInUser = false;
                var userID = this.GetUserID();
                var userRole = this.GetRole();
                var agencyID = this.GetTenantID();

                if (helperIndex == null)
                {
                    var helperData = this.helperService.GetUserDetails(Convert.ToInt32(userID));
                    if (helperData != null && helperData.UserDetails != null)
                    {
                        helperID = helperData.UserDetails.HelperID;
                    }
                }
                else
                {
                    var helperInfo = this.helperService.GetHelperInfo((Guid)helperIndex, agencyID);
                    if (helperInfo != null && helperInfo.HelperDetails != null)
                    {
                        userID = helperInfo.HelperDetails.UserId;
                        helperID = helperInfo.HelperDetails.HelperID;
                        userRole = new List<string>() { helperInfo.HelperDetails.Role };
                        if (helperInfo.HelperDetails.UserId == userID)
                        {
                            isSameAsLoggedInUser = true;
                        }
                    }
                    else
                    {
                        DashboardStrengthPieChartResponseDTO emptyResponseDTO = new DashboardStrengthPieChartResponseDTO();
                        emptyResponseDTO.DashboardStrengthPieChartData = null;
                        emptyResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                        emptyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return emptyResponseDTO;
                    }
                }

                var response = this.personQuestionnaireMetricsService.GetDashboardStrengthPiechartData(helperID, agencyID, userRole, isSameAsLoggedInUser, userID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetStrengthPieChartData/Get : Listing Dashboard Strength pie chart : Exception  : Exception occurred getting Strength metrics pie chart. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving strength pie chart. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get Needs pie chart data for Dashboard.
        /// </summary>
        /// <param name="helperIndex">helperIndex.</param>
        /// <returns>DashboardNeedPieChartResponseDTO.</returns>
        [HttpGet]
        [Route("need-pie-chart/{helperIndex?}")]
        public ActionResult<DashboardNeedPieChartResponseDTO> GetNeedPieChartData(Guid? helperIndex = null)
        {
            try
            {
                int? helperID = null;
                bool isSameAsLoggedInUser = false;
                var userID = this.GetUserID();
                var userRole = this.GetRole();
                var agencyID = this.GetTenantID();

                if (helperIndex == null)
                {
                    var helperData = this.helperService.GetUserDetails(Convert.ToInt32(userID));
                    if (helperData != null && helperData.UserDetails != null)
                    {
                        helperID = helperData.UserDetails.HelperID;
                    }
                }
                else
                {
                    var helperInfo = this.helperService.GetHelperInfo((Guid)helperIndex, agencyID);
                    if (helperInfo != null && helperInfo.HelperDetails != null)
                    {
                        userID = helperInfo.HelperDetails.UserId;
                        helperID = helperInfo.HelperDetails.HelperID;
                        userRole = new List<string>() { helperInfo.HelperDetails.Role };
                        if (helperInfo.HelperDetails.UserId == userID)
                        {
                            isSameAsLoggedInUser = true;
                        }
                    }
                    else
                    {
                        DashboardNeedPieChartResponseDTO emptyResponseDTO = new DashboardNeedPieChartResponseDTO();
                        emptyResponseDTO.DashboardNeedPieChartData = null;
                        emptyResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                        emptyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return emptyResponseDTO;
                    }
                }

                var response = this.personQuestionnaireMetricsService.GetDashboardNeedPiechartData(helperID, agencyID, userRole, isSameAsLoggedInUser, userID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetNeedPieChartData/Get : Listing Dashboard Need pie chart : Exception  : Exception occurred getting Need metrics. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving need pie chart. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpperPaneSearch.
        /// </summary>
        /// <param name="upperpaneSearchKeyDTO">search.</param>
        /// <returns>UpperPaneSearchResponseDTO.</returns>
        [HttpPost]
        [Route("search")]
        public ActionResult<UpperpaneSearchResponseDTO> GetUpperpaneSearchResults([FromBody] UpperpaneSearchKeyDTO upperpaneSearchKeyDTO)
        {
            try
            {
                int helperID = 0;
                var userID = this.GetUserID();
                var agencyID = this.GetTenantID();
                var userRole = this.GetRole();
                var helperData = this.helperService.GetUserDetails(Convert.ToInt64(userID));
                if (helperData != null && helperData.UserDetails != null)
                {
                    helperID = helperData.UserDetails.HelperID;
                }

                UpperpaneSearchResponseDTO response = this.searchService.GetUpperpaneSearchResults(upperpaneSearchKeyDTO, agencyID, userRole, helperID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetUpperpaneSearchResults/POST : Upper Pane Search : Exception  : Exception occurred getting Upper Pane Search. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Upper Pane Search. Please try again later or contact support.");
            }
        }
    }
}
