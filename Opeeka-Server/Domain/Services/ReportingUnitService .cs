// -----------------------------------------------------------------------
// <copyright file="ReportingUnitService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using Opeeka.PICS.Domain.Interfaces.Common;
using System.Linq;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Services
{
    public class ReportingUnitService : BaseService, IReportingUnitService
    {
        private IReportingUnitRepository reportingUnitRepository;
        CRUDResponseDTO response = new CRUDResponseDTO();
        private IAgencySharingRepository agencySharingRepository;
        private IAgencySharingPolicyRepository agencySharingPolicyRepository;
        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<ReportingUnitService> logger;
        private ICollaborationSharingRepository collaborationSharingRepository;
        private ICollaborationSharingPolicyRepository collaborationSharingPolicyRepository;
        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;

        private readonly IUtility utility;

        /// Initializes a new instance of the <see cref="agencySharingHistroyRepository"/> class.
        private readonly IAgencySharingHistoryRepository agencySharingHistroyRepository;
        /// Initializes a new instance of the <see cref="collaborationSharingHistroyRepository"/> class.
        private readonly ICollaborationSharingHistoryRepository collaborationSharingHistroyRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingUnitService"/> class.
        /// </summary>
        /// <param name="reportingUnitRepository"></param>
        public ReportingUnitService(IReportingUnitRepository reportingUnitRepository, IAgencySharingRepository agencySharingRepository, IAgencySharingPolicyRepository agencySharingPolicyRepository, ILogger<ReportingUnitService> logger
                                        , ICollaborationSharingRepository collaborationSharingRepository, ICollaborationSharingPolicyRepository collaborationSharingPolicyRepository
            , LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, IUtility utility, IAgencySharingHistoryRepository agencySharingHistroyRepository, ICollaborationSharingHistoryRepository collaborationSharingHistroyRepository)
            : base(configRepo, httpContext)
        {
            this.reportingUnitRepository = reportingUnitRepository;
            this.agencySharingRepository = agencySharingRepository;
            this.agencySharingPolicyRepository = agencySharingPolicyRepository;
            this.logger = logger;
            this.collaborationSharingRepository = collaborationSharingRepository;
            this.collaborationSharingPolicyRepository = collaborationSharingPolicyRepository;
            this.localize = localizeService;
            this.utility = utility;
            this.agencySharingHistroyRepository = agencySharingHistroyRepository;
            this.collaborationSharingHistroyRepository = collaborationSharingHistroyRepository;
        }

        /// <summary>
        /// Add ReportingUnit
        /// </summary>
        /// <param name="reportingUnitData"></param>
        /// <returns>Object of CRUDResponseDTO</returns>
        public CRUDResponseDTO AddReportingUnit(ReportingUnitInputDTO reportingUnitData)
        {
            try
            {
                int ReportingUnitID;
                if (reportingUnitData != null)
                {
                    var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                    var reportingUnit = new ReportingUnitDTO
                    {
                        Name = reportingUnitData.Name,
                        Abbrev = reportingUnitData.Abbrev,
                        ParentAgencyID = reportingUnitData.ParentAgencyID,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = reportingUnitData.UpdateUserID,
                        StartDate = reportingUnitData.StartDate.Date,
                        EndDate = reportingUnitData.EndDate.HasValue ? reportingUnitData.EndDate.Value.Date : reportingUnitData.EndDate,
                        IsSharing = reportingUnitData.EndDate.HasValue ? false : true,
                        Description = reportingUnitData.Description

                    };

                    ReportingUnitID = this.reportingUnitRepository.AddReportingUnit(reportingUnit).ReportingUnitID;
                    if (ReportingUnitID != 0)
                    {

                        var agencySharing = new AgencySharingDTO
                        {
                            ReportingUnitID = ReportingUnitID,
                            AgencyID = reportingUnitData.ParentAgencyID,
                            AgencySharingPolicyID = this.agencySharingPolicyRepository.GetAgencySharingPolicy().Result.Where(x => x.Name.ToLower() == PCISEnum.SharingPolicies.ReadWrite.ToLower()).Select(y => y.AgencySharingPolicyID).FirstOrDefault(),
                            HistoricalView = false,
                            StartDate = reportingUnitData.StartDate.Date,
                            EndDate = reportingUnitData.EndDate.HasValue ? reportingUnitData.EndDate.Value.Date : reportingUnitData.EndDate,
                            IsSharing = reportingUnitData.EndDate.HasValue ? false : true
                        };


                        int AgencySharingID = this.agencySharingRepository.AddAgencySharing(agencySharing).AgencySharingID;

                        response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                    }
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddPartnerAgency
        /// </summary>
        /// <param name="partnerAgencyData"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO AddPartnerAgency(PartnerAgencyInputDTO partnerAgencyData)
        {
            try
            {
                int AgencySharingID;

                if (partnerAgencyData != null)
                {

                    var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                    var agencySharing = new AgencySharingDTO
                    {
                        ReportingUnitID = partnerAgencyData.ReportingUnitID,
                        AgencyID = partnerAgencyData.AgencyID,
                        AgencySharingPolicyID = partnerAgencyData.AgencySharingPolicyID,
                        HistoricalView = partnerAgencyData.HistoricalView ?? false,
                        StartDate = partnerAgencyData.StartDate.HasValue ? partnerAgencyData.StartDate.Value.Date : partnerAgencyData.StartDate,
                        EndDate = partnerAgencyData.EndDate.HasValue ? partnerAgencyData.EndDate.Value.Date : partnerAgencyData.EndDate,
                        IsSharing = partnerAgencyData.IsSharing
                    };

                    AgencySharingID = this.agencySharingRepository.AddAgencySharing(agencySharing).AgencySharingID;
                    if (AgencySharingID != 0)
                    {

                        response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                    }
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPartnerAgencyList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="reportingUnitID">reportingUnitID.</param>
        /// <returns>PartnerAgency List.</returns>
        public GetPartnerAgencyListDTO GetPartnerAgencyList(int reportingUnitID, int pageNumber, int pageSize)
        {
            try
            {
                GetPartnerAgencyListDTO getPartnerAgencyListDTO = new GetPartnerAgencyListDTO();
                List<PartnerAgencyDataDTO> response = new List<PartnerAgencyDataDTO>();
                if (pageNumber <= 0)
                {
                    getPartnerAgencyListDTO.PartnerAgencyList = null;
                    getPartnerAgencyListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getPartnerAgencyListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getPartnerAgencyListDTO;
                }
                else if (pageSize <= 0)
                {
                    getPartnerAgencyListDTO.PartnerAgencyList = null;
                    getPartnerAgencyListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getPartnerAgencyListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getPartnerAgencyListDTO;
                }
                else if (reportingUnitID <= 0)
                {
                    getPartnerAgencyListDTO.PartnerAgencyList = null;
                    getPartnerAgencyListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.ReportingUnitID));
                    getPartnerAgencyListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getPartnerAgencyListDTO;
                }
                else
                {
                    var partnerAgencyList = this.reportingUnitRepository.GetPartnerAgencyList(reportingUnitID, pageNumber, pageSize);
                    if (partnerAgencyList != null && partnerAgencyList.Count > 0)
                    {
                        getPartnerAgencyListDTO.PartnerAgencyList = partnerAgencyList;
                    }

                    getPartnerAgencyListDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    getPartnerAgencyListDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return getPartnerAgencyListDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddPartnerAgency
        /// </summary>
        /// <param name="partnerAgencyData"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO UpdatePartnerAgency(PartnerAgencyInputDTO partnerAgencyData, int userID)
        {
            try
            {
                if (partnerAgencyData != null)
                {
                    if (partnerAgencyData.AgencySharingIndex != Guid.Empty)
                    {
                        AgencySharing agencySharing = this.agencySharingRepository.GetAgencySharing(partnerAgencyData.AgencySharingIndex).Result;
                        if (agencySharing != null)
                        {
                            var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                            agencySharing.ReportingUnitID = partnerAgencyData.ReportingUnitID;
                            agencySharing.AgencySharingIndex = partnerAgencyData.AgencySharingIndex;
                            agencySharing.AgencyID = partnerAgencyData.AgencyID;
                            agencySharing.AgencySharingPolicyID = partnerAgencyData.AgencySharingPolicyID;
                            agencySharing.HistoricalView = partnerAgencyData.HistoricalView ?? false;
                            agencySharing.StartDate = partnerAgencyData.StartDate.HasValue ? partnerAgencyData.StartDate.Value.Date : partnerAgencyData.StartDate;
                            agencySharing.EndDate = partnerAgencyData.EndDate.HasValue ? partnerAgencyData.EndDate.Value.Date : partnerAgencyData.EndDate;
                            agencySharing.IsSharing = partnerAgencyData.IsSharing;
                            var agencySharingResult = this.agencySharingRepository.UpdateAgencySharing(agencySharing);

                            if (partnerAgencyData.EndDate != null)
                            {
                                AgencySharingHistoryDTO agencySharingHistoryDTO = new AgencySharingHistoryDTO();
                                agencySharingHistoryDTO.StartDate = partnerAgencyData.StartDate.HasValue ? partnerAgencyData.StartDate.Value.Date : partnerAgencyData.StartDate;
                                agencySharingHistoryDTO.EndDate = partnerAgencyData.EndDate.HasValue ? partnerAgencyData.EndDate.Value.Date : partnerAgencyData.EndDate;
                                agencySharingHistoryDTO.HistoricalView = partnerAgencyData.HistoricalView ?? false;
                                agencySharingHistoryDTO.AgencySharingID = agencySharing.AgencySharingID;
                                agencySharingHistoryDTO.RemovedUserID = userID;
                                AgencySharingHistoryDTO histroyresult = this.agencySharingHistroyRepository.AddAgencySharingHistroy(agencySharingHistoryDTO);
                            }

                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);

                        }
                    }
                }
                if (response.ResponseStatusCode != PCISEnum.StatusCodes.UpdationSuccess)
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetReportingUnitList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>ReportingUnit List.</returns>
        public GetReportingUnitListDTO GetReportingUnitList(long agencyID, int pageNumber, int pageSize)
        {
            try
            {
                GetReportingUnitListDTO getReportingUnitListDTO = new GetReportingUnitListDTO();
                List<ReportingUnitDataDTO> response = new List<ReportingUnitDataDTO>();
                int totalCount = 0;
                if (pageNumber <= 0)
                {
                    getReportingUnitListDTO.ReportingUnitList = null;
                    getReportingUnitListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getReportingUnitListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getReportingUnitListDTO;
                }
                else if (pageSize <= 0)
                {
                    getReportingUnitListDTO.ReportingUnitList = null;
                    getReportingUnitListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getReportingUnitListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getReportingUnitListDTO;
                }
                else if (agencyID <= 0)
                {
                    getReportingUnitListDTO.ReportingUnitList = null;
                    getReportingUnitListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.AgencyID);
                    getReportingUnitListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getReportingUnitListDTO;
                }
                else
                {
                    var reportingUnitList = this.reportingUnitRepository.GetReportingUnitList(agencyID, pageNumber, pageSize);
                    if (reportingUnitList != null && reportingUnitList.Count > 0)
                    {
                        getReportingUnitListDTO.ReportingUnitList = reportingUnitList;
                    }

                    totalCount = reportingUnitRepository.GetReportingUnitCount(agencyID);
                    getReportingUnitListDTO.TotalCount = totalCount;
                    getReportingUnitListDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    getReportingUnitListDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return getReportingUnitListDTO;
                }
            }
            catch (Exception)
            {
                throw;
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
        public GetRUCollaborationListDTO GetRUCollaborationList(long agencyID, int reportingUnitID, int pageNumber, int pageSize)
        {
            try
            {
                GetRUCollaborationListDTO getRUCollaborationListDTO = new GetRUCollaborationListDTO();
                List<RUCollaborationDataDTO> response = new List<RUCollaborationDataDTO>();
                if (pageNumber <= 0)
                {
                    getRUCollaborationListDTO.CollaborationList = null;
                    getRUCollaborationListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getRUCollaborationListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getRUCollaborationListDTO;
                }
                else if (pageSize <= 0)
                {
                    getRUCollaborationListDTO.CollaborationList = null;
                    getRUCollaborationListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getRUCollaborationListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getRUCollaborationListDTO;
                }
                else if (agencyID <= 0)
                {
                    getRUCollaborationListDTO.CollaborationList = null;
                    getRUCollaborationListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.AgencyID);
                    getRUCollaborationListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getRUCollaborationListDTO;
                }
                else if (reportingUnitID <= 0)
                {
                    getRUCollaborationListDTO.CollaborationList = null;
                    getRUCollaborationListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.ReportingUnitID));
                    getRUCollaborationListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getRUCollaborationListDTO;
                }
                else
                {
                    var collaborationList = this.reportingUnitRepository.GetRUCollaborationList(agencyID, reportingUnitID, pageNumber, pageSize);
                    if (collaborationList != null && collaborationList.Count > 0)
                    {
                        getRUCollaborationListDTO.CollaborationList = collaborationList;
                        foreach (RUCollaborationDataDTO item in collaborationList)
                        {
                            item.ListCollaborationQuestionaire = this.collaborationSharingRepository.GetCollaborationQuestionairesList(item.CollaborationID);
                        }
                    }

                    getRUCollaborationListDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    getRUCollaborationListDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return getRUCollaborationListDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddCollaborationSharing
        /// </summary>
        /// <param name="collaborationData"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO AddCollaborationSharing(CollaborationSharingInputDTO collaborationData)
        {
            try
            {
                int CollaborationSharingID;

                if (collaborationData != null)
                {
                    var collaborationSharing = new CollaborationSharingDTO
                    {
                        ReportingUnitID = collaborationData.ReportingUnitID,
                        AgencyID = collaborationData.AgencyID,
                        CollaborationID = collaborationData.CollaborationID,
                        CollaborationSharingPolicyID = collaborationData.CollaborationSharingPolicyID,
                        HistoricalView = collaborationData.HistoricalView ?? false,
                        StartDate = collaborationData.StartDate.HasValue ? collaborationData.StartDate.Value.Date : collaborationData.StartDate,
                        EndDate = collaborationData.EndDate.HasValue ? collaborationData.EndDate.Value.Date : collaborationData.EndDate,
                        IsSharing = collaborationData.IsSharing,
                    };

                    CollaborationSharingID = this.collaborationSharingRepository.AddCollaborationSharing(collaborationSharing).CollaborationSharingID;
                    if (CollaborationSharingID != 0)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                    }
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// EditCollaborationSharing.
        /// </summary>
        /// <param name="collaborationData">collaborationData.</param>
        /// <param name="userID">userID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO EditCollaborationSharing(CollaborationSharingInputDTO collaborationSharingInputDTO, int userID)
        {
            try
            {
                if (collaborationSharingInputDTO.CollaborationSharingIndex != Guid.Empty)
                {
                    CollaborationSharingDTO collaborationSharing = this.collaborationSharingRepository.GetCollaborationSharing(collaborationSharingInputDTO.CollaborationSharingIndex).Result;
                    if (collaborationSharing != null)
                    {
                        collaborationSharing.CollaborationID = collaborationSharingInputDTO.CollaborationID;
                        collaborationSharing.HistoricalView = collaborationSharingInputDTO.HistoricalView ?? false;
                        collaborationSharing.StartDate = collaborationSharingInputDTO.StartDate.HasValue ? collaborationSharingInputDTO.StartDate.Value.Date : collaborationSharingInputDTO.StartDate;
                        collaborationSharing.EndDate = collaborationSharingInputDTO.EndDate.HasValue ? collaborationSharingInputDTO.EndDate.Value.Date : collaborationSharingInputDTO.EndDate;
                        collaborationSharing.IsSharing = collaborationSharingInputDTO.IsSharing;

                        var CollaborationSharingResult = this.collaborationSharingRepository.UpdateCollaborationSharing(collaborationSharing);

                        if (collaborationSharingInputDTO.EndDate != null)
                        {
                            CollaborationSharingHistoryDTO collaborationHistoryDTO = new CollaborationSharingHistoryDTO();
                            collaborationHistoryDTO.StartDate = collaborationSharingInputDTO.StartDate.HasValue ? collaborationSharingInputDTO.StartDate.Value.Date : collaborationSharingInputDTO.StartDate;
                            collaborationHistoryDTO.EndDate = collaborationSharingInputDTO.EndDate.HasValue ? collaborationSharingInputDTO.EndDate.Value.Date : collaborationSharingInputDTO.EndDate;
                            collaborationHistoryDTO.HistoricalView = collaborationSharingInputDTO.HistoricalView ?? false;
                            collaborationHistoryDTO.CollaborationSharingID = collaborationSharing.CollaborationSharingID;
                            collaborationHistoryDTO.RemovedUserID = userID;
                            CollaborationSharingHistoryDTO histroyresult = this.collaborationSharingHistroyRepository.AddCollaborationSharingHistroy(collaborationHistoryDTO);
                        }

                        if (CollaborationSharingResult != null)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                        }
                    }
                }
                if (response.ResponseStatusCode != PCISEnum.StatusCodes.UpdationSuccess)
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Add ReportingUnit
        /// </summary>
        /// <param name="reportingUnitData"></param>
        /// <returns>Object of CRUDResponseDTO</returns>
        public CRUDResponseDTO EditReportingUnit(EditReportingUnitDTO editReportingUnitDTO)
        {
            try
            {
                if (editReportingUnitDTO.ReportingUnitIndex != Guid.Empty)
                {
                    var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                    ReportingUnit reportingUnit = this.reportingUnitRepository.GetReportingUnit(editReportingUnitDTO.ReportingUnitIndex).Result;
                    reportingUnit.Name = editReportingUnitDTO.Name;
                    reportingUnit.Abbrev = editReportingUnitDTO.Abbrev;
                    reportingUnit.IsRemoved = false;
                    reportingUnit.UpdateDate = DateTime.UtcNow;
                    reportingUnit.UpdateUserID = editReportingUnitDTO.UpdateUserID;
                    reportingUnit.StartDate = editReportingUnitDTO.StartDate;
                    reportingUnit.EndDate = editReportingUnitDTO.EndDate.HasValue ? editReportingUnitDTO.EndDate.Value.Date : editReportingUnitDTO.EndDate;
                    reportingUnit.Description = editReportingUnitDTO.Description;
                    reportingUnit.IsSharing = editReportingUnitDTO.EndDate.HasValue ? false : true;

                    var reportingResult = this.reportingUnitRepository.UpdateReportingUnit(reportingUnit);
                    if (reportingResult != null)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                    }
                }
                if (response.ResponseStatusCode != PCISEnum.StatusCodes.UpdationSuccess)
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
