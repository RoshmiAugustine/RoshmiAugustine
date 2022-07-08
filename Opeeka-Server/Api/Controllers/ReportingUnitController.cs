// -----------------------------------------------------------------------
// <copyright file="ReportingUnitController.cs" company="Naico ITS">
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
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// ReportingUnitController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class ReportingUnitController : BaseController
    {
        /// Initializes a new instance of the <see cref="reportingUnitService"/> class.
        private readonly IReportingUnitService reportingUnitService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<ReportingUnitController> logger;

        /// <summary>
        ///  Initializes a new instance of the <see cref="ReportingUnitController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="reportingUnitService">reportingUnitService.</param>
        public ReportingUnitController(ILogger<ReportingUnitController> logger, IReportingUnitService reportingUnitService)
        {
            this.reportingUnitService = reportingUnitService;
            this.logger = logger;
        }

        /// <summary>
        /// POST.
        /// </summary>
        /// <param name="reportingUnitData">reportingUnitData.</param>
        /// <returns>ActionResult.<CRUDResponseDTO></returns>
        [HttpPost]
        [Route("reportingunit")]
        public ActionResult<CRUDResponseDTO> AddReportingUnit([FromBody] ReportingUnitInputDTO reportingUnitData)
        {
            try
            {
                if (reportingUnitData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        reportingUnitData.ParentAgencyID = this.GetTenantID();
                        reportingUnitData.UpdateUserID = this.GetUserID();
                        var response = this.reportingUnitService.AddReportingUnit(reportingUnitData);
                        return response;
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddReportingUnit/Post : Adding reportingUnit : Exception  : Exception occurred adding report unit. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding report unit. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddPartnerAgency.
        /// </summary>
        /// <param name="partnerAgencyData">partnerAgencyData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("partneragency")]
        public ActionResult<CRUDResponseDTO> AddPartnerAgency([FromBody] PartnerAgencyInputDTO partnerAgencyData)
        {
            try
            {
                if (partnerAgencyData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.reportingUnitService.AddPartnerAgency(partnerAgencyData);
                        return response;
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddPartnerAgency/Post : Adding Partner Agency : Exception  : Exception occurred adding partner agency. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding partner agency. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPartnerAgencyList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="reportingUnitID">reportingUnitID.</param>
        /// <returns>PartnerAgency List.</returns>
        [HttpGet]
        [Route("partner-agencies/{reportingUnitID}/{pageNumber}/{pageSize}")]
        public ActionResult<GetPartnerAgencyListDTO> GetPartnerAgencyList(int reportingUnitID, int pageNumber, int pageSize)
        {
            try
            {
                var response = this.reportingUnitService.GetPartnerAgencyList(reportingUnitID, pageNumber, pageSize);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPartnerAgencyList/GET : Getting item : Exception  : Exception occurred Getting Partner Agency List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving partner agency list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddPartnerAgency.
        /// </summary>
        /// <param name="partnerAgencyData">partnerAgencyData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPut]
        [Route("partneragency")]
        public ActionResult<CRUDResponseDTO> UpdatePartnerAgency([FromBody] PartnerAgencyInputDTO partnerAgencyData)
        {
            try
            {
                if (partnerAgencyData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.reportingUnitService.UpdatePartnerAgency(partnerAgencyData, this.GetUserID());
                        return response;
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdatePartnerAgency/Put : Update Partner Agency : Exception  : Exception occurred updating partner agency. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating partner agency. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetReportingUnitList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>ReportingUnit List.</returns>
        [HttpGet]
        [Route("reportingunits/{pageNumber}/{pageSize}")]
        public ActionResult<GetReportingUnitListDTO> GetReportingUnitList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.reportingUnitService.GetReportingUnitList(this.GetTenantID(), pageNumber, pageSize);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetReportingUnitList/GET : Getting item : Exception  : Exception occurred Getting Reporting unit List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving reporting unit list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetRUCollaborationList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">collaborationID.</param>
        /// <param name="reportingUnitID">reportingUnitID.</param>
        /// <returns>Collaboration List.</returns>
        [HttpGet]
        [Route("collaborationsharing/{agencyID}/{reportingUnitID}/{pageNumber}/{pageSize}")]
        public ActionResult<GetRUCollaborationListDTO> GetRUCollaborationList(long agencyID, int reportingUnitID, int pageNumber, int pageSize)
        {
            try
            {
                var response = this.reportingUnitService.GetRUCollaborationList(agencyID, reportingUnitID, pageNumber, pageSize);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetRUCollaborationList/GET : Getting item : Exception  : Exception occurred Getting collaboration List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving collaboration list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// PUT.Updating ReportingUnit.
        /// </summary>
        /// <param name="reportingUnitData">reportingUnitData.</param>
        /// <returns>ActionResult.<CRUDResponseDTO></returns>
        [HttpPut]
        [Route("reportingUnit")]
        public ActionResult<CRUDResponseDTO> Update([FromBody] EditReportingUnitDTO editReportingUnitDTO)
        {
            try
            {
                if (editReportingUnitDTO != null)
                {
                    if (this.ModelState.IsValid)
                    {

                        editReportingUnitDTO.UpdateUserID = this.GetUserID();
                        var response = this.reportingUnitService.EditReportingUnit(editReportingUnitDTO);
                        return response;
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"Update/Put : Updating item : Exception  : Exception occurred updating item. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating item. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddCollaborationSharing.
        /// </summary>
        /// <param name="collaborationData">collaborationData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("collaborationsharing")]
        public ActionResult<CRUDResponseDTO> AddCollaborationSharing([FromBody] CollaborationSharingInputDTO collaborationData)
        {
            try
            {
                if (collaborationData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.reportingUnitService.AddCollaborationSharing(collaborationData);
                        return response;
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddCollaborationSharing/Post : Adding Collaboration : Exception  : Exception occurred adding collaboration sharing. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding collaboration sharing. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Edit CollaborationSharing.
        /// </summary>
        /// <param name="collaborationData">collaborationData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPut]
        [Route("collaborationsharing")]
        public ActionResult<CRUDResponseDTO> update([FromBody] CollaborationSharingInputDTO collaborationData)
        {
            try
            {
                if (collaborationData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.reportingUnitService.EditCollaborationSharing(collaborationData, this.GetUserID());
                        return response;
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"update/Put :  Edit  Collaboration sharing : Exception  : Exception occurred while updating collaboration sharing. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating collaboration shating. Please try again later or contact support.");
            }
        }
    }
}