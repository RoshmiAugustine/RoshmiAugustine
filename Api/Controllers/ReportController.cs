// -----------------------------------------------------------------------
// <copyright file="ReportController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Input;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;

    /// <summary>
    /// ReportController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class ReportController : BaseController
    {
        /// Initializes a new instance of the <see cref="reportService"/> class.
        private readonly IReportService reportService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<ReportController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportController"/> class.
        /// ReportController.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="reportService">reportService.</param>
        /// <param name="converter">converter. </param>
        public ReportController(ILogger<ReportController> logger,
                                IReportService reportService)
        {
            this.reportService = reportService;
            this.logger = logger;
        }

        /// <summary>
        /// GetItemReportData.
        /// </summary>
        /// <param name="itemDetailsDTO">itemDetailsDTO.</param>
        /// <returns>ItemDetailsResponseDTO.</returns>
        [HttpPost]
        [Route("report/item-detail")]
        public ActionResult<ItemDetailsResponseDTO> GetItemReportData([FromBody] ReportInputDTO itemDetailsDTO)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                ItemDetailsResponseDTO response = this.reportService.GetItemReportData(itemDetailsDTO, userTokenDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetItemReportData/Post : Getting item : Exception  : Exception occurred while recieving report data. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving report data. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetStoryMapReportData.
        /// </summary>
        /// <param name="reportInputDTO">reportInputDTO.</param>
        /// <returns>StoryMapResponseDTO.</returns>
        [HttpPost]
        [Route("report/story-map")]
        public ActionResult<StoryMapResponseDTO> GetStoryMapReportData([FromBody] ReportInputDTO reportInputDTO)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                StoryMapResponseDTO response = this.reportService.GetStoryMapReportData(reportInputDTO, userTokenDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetStoryMapReportData/Post : Post item : Exception  : Exception occurred while recieving story map report data. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving story map report data. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get PersonStrength FamilyReport Data.
        /// </summary>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <returns>PersonStrengthReportResponseDTO.</returns>
        [HttpPost]
        [Route("report/family-person-strength-report")]
        public ActionResult<PersonStrengthReportResponseDTO> GetPersonStrengthFamilyReportData([FromBody] FamilyReportInputDTO familyReportInputDTO)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                PersonStrengthReportResponseDTO response = this.reportService.GetPersonStrengthFamilyReportData(familyReportInputDTO, userTokenDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetPersonStrengthFamilyReportData/Post : Post item : Exception  : Exception occurred while recieving person strength report data. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving person strength report data. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get PersonNeeds FamilyReport Data.
        /// </summary>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <returns>PersonNeedReportResponseDTO.</returns>
        [HttpPost]
        [Route("report/family-person-needs-report")]
        public ActionResult<PersonNeedReportResponseDTO> GetPersonNeedsFamilyReportData([FromBody] FamilyReportInputDTO familyReportInputDTO)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                PersonNeedReportResponseDTO response = this.reportService.GetPersonNeedsFamilyReportData(familyReportInputDTO, userTokenDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetPersonNeedsFamilyReportData/Post : Post item : Exception  : Exception occurred while recieving person need report data. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving person need report data. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get SupportResources FamilyReport Data.
        /// </summary>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <returns>SupportResourceReportResponseDTO.</returns>
        [HttpPost]
        [Route("report/family-support-resources-report")]
        public ActionResult<SupportResourceReportResponseDTO> GetSupportResourcesFamilyReportData([FromBody] FamilyReportInputDTO familyReportInputDTO)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                SupportResourceReportResponseDTO response = this.reportService.GetSupportResourcesFamilyReportData(familyReportInputDTO, userTokenDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetSupportResourcesFamilyReportData/Post : Post item : Exception  : Exception occurred while recieving support resources family report data. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving support resources family report data. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get SupportNeeds FamilyReport Data.
        /// </summary>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <returns>SupportNeedsReportResponseDTO.</returns>
        [HttpPost]
        [Route("report/family-support-needs-report")]
        public ActionResult<SupportNeedsReportResponseDTO> GetSupportNeedsFamilyReportData([FromBody] FamilyReportInputDTO familyReportInputDTO)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                SupportNeedsReportResponseDTO response = this.reportService.GetSupportNeedsFamilyReportData(familyReportInputDTO, userTokenDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetSupportNeedsFamilyReportData/Post : Post item : Exception  : Exception occurred while recieving support needs family report data. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving support needs family report data. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get FamilyReport Status.
        /// </summary>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <returns>FamilyReportStatusResponseDTO.</returns>
        [HttpPost]
        [Route("report/family-report-status")]
        public ActionResult<FamilyReportStatusResponseDTO> GetFamilyReportStatus([FromBody] FamilyReportInputDTO familyReportInputDTO)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                FamilyReportStatusResponseDTO response = this.reportService.GetFamilyReportStatus(familyReportInputDTO, userTokenDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetFamilyReportStatus/Post : Post item : Exception  : Exception occurred while recieving family report status. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving family report status. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// CreatePDF Byte Array.
        /// </summary>
        /// <param name="htmlData">htmlData.</param>
        /// <returns>HTMLToPDFResponseDTO.</returns>
        [HttpPost]
        [Route("report/family-report-downloadPDF")]
        public ActionResult<HTMLToPDFResponseDTO> CreatePDF([FromBody] PdfReportDTO htmlData)
        {
            try
            {
                HTMLToPDFResponseDTO response = this.reportService.HTMLToPDFByteArrayConversion(htmlData);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"CreatePDF/Post : Post item : Exception  : Exception occurred while creating pdf. {ex.Message}");
                return this.HandleException(ex, "An error occurred while creating pdf. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllQuestionnairesForSuperStoryMap.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="personCollaborationID">personCollaborationID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>AssessedQuestionnairesForPersonDTO.</returns>
        [HttpGet]
        [Route("report/superstory-questionnaires/{personIndex}/{personCollaborationID}/{pageNumber}/{pageSize}")]
        public ActionResult<AssessedQuestionnairesForPersonDTO> GetAllQuestionnairesForSuperStoryMap(Guid personIndex, int personCollaborationID, int pageNumber, int pageSize)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                AssessedQuestionnairesForPersonDTO response = this.reportService.GetAllQuestionnairesForSuperStoryMap(userTokenDetails, personIndex, personCollaborationID, pageNumber, pageSize);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetAllQuestionnairesForSuperStoryMap/Get : Get item : Exception  : Exception occurred while getting all questionnaires for superstorymap. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting all questionnaires for superstorymap. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllDetailsForSuperStoryMap.
        /// </summary>
        /// <param name="superStoryInputDTO">superStoryInputDTO.</param>
        /// <returns>AssessedQuestionnairesForPersonDTO.</returns>
        [HttpPost]
        [Route("report/superstory-details")]
        public ActionResult<SuperStoryResponseDTO> GetAllDetailsForSuperStoryMap(SuperStoryInputDTO superStoryInputDTO)
        {
            try
            {
                long agencyID = this.GetTenantID();
                SuperStoryResponseDTO response = this.reportService.GetAllDetailsForSuperStoryMap(superStoryInputDTO, agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetAllDetailsForSuperStoryMap/Post : Post item : Exception  : Exception occurred while getting all details for superstorymap. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting all details for superstorymap. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllPowerBIReports.
        /// </summary>
        /// <param name="instrumentId">instrumentId.</param>
        /// <returns>AgencyPowerBIReportResponseDTO.</returns>
        [HttpGet]
        [Route("report/powerbi/{instrumentId}")]
        public ActionResult<AgencyPowerBIReportResponseDTO> GetAllPowerBIReports(int instrumentId)
        {
            try
            {
                long agencyId = this.GetTenantID();
                AgencyPowerBIReportResponseDTO response = this.reportService.GetAllPowerBIReportsForAgency(agencyId, instrumentId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetAllPowerBIReports/Get : Get item : Exception  : Exception occurred while getting all details for powerBi Reports. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting all details for powerBi Reports. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPowerBIReportURL.
        /// </summary>
        /// <param name="powerBiInputDTO">powerBiInputDTO.</param>
        /// <returns>PowerBIReportURLResponseDTO.</returns>
        [HttpPost]
        [Route("report/powerbi-token-url")]
        public ActionResult<PowerBIReportURLResponseDTO> GetPowerBIReportURL(PowerBiInputDTO powerBiInputDTO)
        {
            try
            {
                long agencyId = this.GetTenantID();
                int userId = this.GetUserID();
                PowerBIReportURLResponseDTO response = this.reportService.GetPowerBIReportURLByReportID(powerBiInputDTO, agencyId, userId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetPowerBIReportURL/Post : Get item : Exception  : Exception occurred while getting token-url for powerBi Reports. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting token-url for powerBi Reports. Please try again later or contact support.");
            }
        }
    }
}
