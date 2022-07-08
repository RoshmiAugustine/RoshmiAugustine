// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireMetricsService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.Services
{
    public class PersonQuestionnaireMetricsService : BaseService, IPersonQuestionnaireMetricsService
    {
        private IPersonQuestionnaireMetricsRepository personQuestionnaireMetricsRepository;
        private IPersonAssessmentMetricsRepository PersonAssessmentMetricsRepository;
        CRUDResponseDTO response = new CRUDResponseDTO();
        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;

        private readonly IMapper mapper;
        /// <summary>
        /// Defines the queryBuilder.
        /// </summary>
        private readonly IQueryBuilder queryBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageService"/> class.
        /// </summary>
        /// <param name="personQuestionnaireMetricsRepository"></param>
        /// <param name="localizeService"></param>
        /// <param name="configRepo"></param>
        /// <param name="httpContext"></param>
        /// <param name="querybuild"></param>
        public PersonQuestionnaireMetricsService(IMapper mapper,IPersonQuestionnaireMetricsRepository personQuestionnaireMetricsRepository, LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, IQueryBuilder querybuild, IPersonAssessmentMetricsRepository personAssessmentMetricsRepository)
            : base(configRepo, httpContext)
        {
            this.personQuestionnaireMetricsRepository = personQuestionnaireMetricsRepository;
            this.localize = localizeService;
            this.queryBuilder = querybuild;
            this.mapper = mapper;
            this.PersonAssessmentMetricsRepository = personAssessmentMetricsRepository;
        }

        /// <summary>
        /// Get Dashboard Strength Metrics
        /// </summary>
        /// <param name="strengthMetricsSearchDTO"></param>
        /// <returns>DashboardStrengthMetricsListResponseDTO</returns>
        public DashboardStrengthMetricsListResponseDTO GetDashboardStrengthMetrics(StrengthMetricsSearchDTO strengthMetricsSearchDTO)
        {
            try
            {
                List<QueryFieldMappingDTO> fieldMappingList = GetStrengthMetricsConfiguration();
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQuery(strengthMetricsSearchDTO.SearchFilter, fieldMappingList);
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                DashboardStrengthMetricsListResponseDTO dashboardStrengthMetricsListResponseDTO = new DashboardStrengthMetricsListResponseDTO();
                List<DashboardStrengthMetricsDTO> response = new List<DashboardStrengthMetricsDTO>();
                int totalCount = 0;

                if (queryBuilderDTO.Page <= 0)
                {
                    dashboardStrengthMetricsListResponseDTO.DashboardStrengthMetricsList = null;
                    dashboardStrengthMetricsListResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    dashboardStrengthMetricsListResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return dashboardStrengthMetricsListResponseDTO;
                }
                else if (queryBuilderDTO.PageSize <= 0)
                {
                    dashboardStrengthMetricsListResponseDTO.DashboardStrengthMetricsList = null;
                    dashboardStrengthMetricsListResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    dashboardStrengthMetricsListResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return dashboardStrengthMetricsListResponseDTO;
                }
                else if (!strengthMetricsSearchDTO.userRole.Contains(PCISEnum.Roles.SuperAdmin) && strengthMetricsSearchDTO.helperID == 0)
                {
                    dashboardStrengthMetricsListResponseDTO.DashboardStrengthMetricsList = null;
                    dashboardStrengthMetricsListResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.HelperIndex));
                    dashboardStrengthMetricsListResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return dashboardStrengthMetricsListResponseDTO;
                }
                else
                {
                    strengthMetricsSearchDTO.role = this.GetRoleName(strengthMetricsSearchDTO.userRole);
                    var queryResponse = this.personQuestionnaireMetricsRepository.GetDashboardStrengthMetrics(strengthMetricsSearchDTO, queryBuilderDTO);
                    response = queryResponse.Item1;
                    totalCount = queryResponse.Item2;
                    dashboardStrengthMetricsListResponseDTO.DashboardStrengthMetricsList = response;
                    dashboardStrengthMetricsListResponseDTO.TotalCount = totalCount;
                    dashboardStrengthMetricsListResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    dashboardStrengthMetricsListResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return dashboardStrengthMetricsListResponseDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Strength Metrics Configuration
        /// </summary>
        /// <returns>List<QueryFieldMappingDTO></returns>
        private List<QueryFieldMappingDTO> GetStrengthMetricsConfiguration()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "[Top]",
                fieldAlias = "top",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ItemID",
                fieldAlias = "itemID",
                fieldTable = "CTE",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "InstrumentID",
                fieldAlias = "instrumentID",
                fieldTable = "CTE",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Label",
                fieldAlias = "item",
                fieldTable = "i",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Abbrev",
                fieldAlias = "instrument",
                fieldTable = "ins",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Helping",
                fieldAlias = "helping",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Improved",
                fieldAlias = "improved",
                fieldTable = "",
                fieldType = "number"
            });
            return fieldMappingList;
        }

        /// <summary>
        /// Get Dashboard Need Metrics
        /// </summary>
        /// <param name="needMetricsSearchDTO"></param>
        /// <returns>DashboardNeedMetricsListResponseDTO</returns>
        public DashboardNeedMetricsListResponseDTO GetDashboardNeedMetrics(NeedMetricsSearchDTO needMetricsSearchDTO)
        {
            try
            {
                List<QueryFieldMappingDTO> fieldMappingList = GetNeedMetricsConfiguration();
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQuery(needMetricsSearchDTO.SearchFilter, fieldMappingList);
                DashboardNeedMetricsListResponseDTO dashboardNeedMetricsListResponseDTO = new DashboardNeedMetricsListResponseDTO();
                List<DashboardNeedMetricsDTO> response = new List<DashboardNeedMetricsDTO>();
                if (queryBuilderDTO.Page <= 0)
                {
                    dashboardNeedMetricsListResponseDTO.DashboardNeedMetricsList = null;
                    dashboardNeedMetricsListResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    dashboardNeedMetricsListResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return dashboardNeedMetricsListResponseDTO;
                }
                else if (queryBuilderDTO.PageSize <= 0)
                {
                    dashboardNeedMetricsListResponseDTO.DashboardNeedMetricsList = null;
                    dashboardNeedMetricsListResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    dashboardNeedMetricsListResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return dashboardNeedMetricsListResponseDTO;
                }
                else if (!needMetricsSearchDTO.userRole.Contains(PCISEnum.Roles.SuperAdmin) && needMetricsSearchDTO.helperID == 0)
                {
                    dashboardNeedMetricsListResponseDTO.DashboardNeedMetricsList = null;
                    dashboardNeedMetricsListResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.HelperIndex));
                    dashboardNeedMetricsListResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return dashboardNeedMetricsListResponseDTO;
                }
                else
                {
                    needMetricsSearchDTO.role = this.GetRoleName(needMetricsSearchDTO.userRole);                    
                    var queryResponse = this.personQuestionnaireMetricsRepository.GetDashboardNeedMetrics(needMetricsSearchDTO, queryBuilderDTO);
                    dashboardNeedMetricsListResponseDTO.DashboardNeedMetricsList = queryResponse.Item1;
                    dashboardNeedMetricsListResponseDTO.TotalCount = queryResponse.Item2;
                    dashboardNeedMetricsListResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    dashboardNeedMetricsListResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return dashboardNeedMetricsListResponseDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Need Metrics Configuration
        /// </summary>
        /// <returns>List<QueryFieldMappingDTO></returns>
        private List<QueryFieldMappingDTO> GetNeedMetricsConfiguration()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "[Top]",
                fieldAlias = "top",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ItemID",
                fieldAlias = "itemID",
                fieldTable = "CTE",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "InstrumentID",
                fieldAlias = "instrumentID",
                fieldTable = "CTE",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Label",
                fieldAlias = "item",
                fieldTable = "i",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Abbrev",
                fieldAlias = "instrument",
                fieldTable = "ins",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Helping",
                fieldAlias = "helping",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Improved",
                fieldAlias = "improved",
                fieldTable = "",
                fieldType = "number"
            });
            return fieldMappingList;
        }

        /// <summary>
        /// Get data for Dashboard Strength pie chart
        /// </summary>
        /// <param name="role"></param>
        /// <param name="agencyID"></param>
        /// <param name="helperID"></param>
        /// <returns>DashboardStrengthPieChartResponseDTO</returns>
        public DashboardStrengthPieChartResponseDTO GetDashboardStrengthPiechartData(int? helperID, long agencyID, List<string> userRole, bool isSameAsLoggedInUser,int userID)
        {
            try
            {
                DashboardStrengthPieChartResponseDTO dashboardStrengthPieChartResponseDTO = new DashboardStrengthPieChartResponseDTO();
                DashboardStrengthPieChartDTO response = new DashboardStrengthPieChartDTO();

                if (!userRole.Contains(PCISEnum.Roles.SuperAdmin) && (helperID == null || helperID == 0))
                {
                    dashboardStrengthPieChartResponseDTO.DashboardStrengthPieChartData = null;
                    dashboardStrengthPieChartResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.HelperIndex));
                    dashboardStrengthPieChartResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return dashboardStrengthPieChartResponseDTO;
                }
                else
                {
                    var roleName = this.GetRoleName(userRole);
                    response = this.personQuestionnaireMetricsRepository.GetDashboardStrengthPiechartData(roleName, agencyID, helperID, isSameAsLoggedInUser, userID);
                    if (response != null)
                    {
                        dashboardStrengthPieChartResponseDTO.DashboardStrengthPieChartData = response;
                    }
                    dashboardStrengthPieChartResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    dashboardStrengthPieChartResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return dashboardStrengthPieChartResponseDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get data for Dashboard Need pie chart
        /// </summary>
        /// <param name="role"></param>
        /// <param name="agencyID"></param>
        /// <param name="helperID"></param>
        /// <returns>DashboardNeedPieChartResponseDTO</returns>
        public DashboardNeedPieChartResponseDTO GetDashboardNeedPiechartData(int? helperID, long agencyID, List<string> userRole, bool isSameAsLoggedInUser, int userID)
        {
            try
            {
                DashboardNeedPieChartResponseDTO dashboardNeedPieChartResponseDTO = new DashboardNeedPieChartResponseDTO();
                DashboardNeedPieChartDTO response = new DashboardNeedPieChartDTO();

                if (!userRole.Contains(PCISEnum.Roles.SuperAdmin) && (helperID == null || helperID == 0))
                {
                    dashboardNeedPieChartResponseDTO.DashboardNeedPieChartData = null;
                    dashboardNeedPieChartResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.HelperIndex));
                    dashboardNeedPieChartResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return dashboardNeedPieChartResponseDTO;
                }
                else
                {
                    var roleName = this.GetRoleName(userRole);
                    response = this.personQuestionnaireMetricsRepository.GetDashboardNeedPiechartData(roleName, agencyID, helperID, isSameAsLoggedInUser, userID);

                    if (response != null)
                    {
                        dashboardNeedPieChartResponseDTO.DashboardNeedPieChartData = response;
                    }
                    dashboardNeedPieChartResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    dashboardNeedPieChartResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return dashboardNeedPieChartResponseDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdatePersonQuestionnaireMetrics.
        /// </summary>
        /// <param name="personQuestionnaireMetrics">personQuestionnaireMetrics.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdatePersonQuestionnaireMetrics(List<PersonQuestionnaireMetricsDTO> personQuestionnaireMetrics)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                List<PersonQuestionnaireMetrics> PersonQuestionnaireMetricsEntity = new List<PersonQuestionnaireMetrics>();
                this.mapper.Map<List<PersonQuestionnaireMetricsDTO>, List<PersonQuestionnaireMetrics>>(personQuestionnaireMetrics, PersonQuestionnaireMetricsEntity);
                var Response = this.personQuestionnaireMetricsRepository.UpdateBulkPersonQuestionnaireMetrics(PersonQuestionnaireMetricsEntity);
                if (Response != null)
                {
                    result.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                    return result;
                }
                else
                {
                    result.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddPersonQuestionnaireMetrics.
        /// </summary>
        /// <param name="personQuestionnaireMetrics">personQuestionnaireMetrics.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AddPersonQuestionnaireMetrics(List<PersonQuestionnaireMetricsDTO> personQuestionnaireMetrics)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                List<PersonQuestionnaireMetrics> PersonQuestionnaireMetricsEntity = new List<PersonQuestionnaireMetrics>();
                this.mapper.Map<List<PersonQuestionnaireMetricsDTO>, List<PersonQuestionnaireMetrics>>(personQuestionnaireMetrics, PersonQuestionnaireMetricsEntity);
                var Response = this.personQuestionnaireMetricsRepository.AddBulkPersonQuestionnaireMetrics(PersonQuestionnaireMetricsEntity);
                if (Response != null)
                {
                    result.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                    return result;
                }
                else
                {
                    result.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireMetrics.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <param name="itemId">itemId.</param>
        /// <returns>PersonQuestionnaireMetricsDetailsDTO.</returns>
        public PersonQuestionnaireMetricsDetailsDTO GetPersonQuestionnaireMetrics(DashboardMetricsInputDTO metricsInput)
        {
            try
            {
                PersonQuestionnaireMetricsDetailsDTO result = new PersonQuestionnaireMetricsDetailsDTO();
                if (metricsInput?.personId > 0)
                {
                    List<PersonQuestionnaireMetricsDTO> personQuestionnaireMetrics = new List<PersonQuestionnaireMetricsDTO>();
                    var response = this.personQuestionnaireMetricsRepository.GetPersonQuestionnaireMetrics(metricsInput);
                    this.mapper.Map<List<PersonQuestionnaireMetrics>, List<PersonQuestionnaireMetricsDTO>>(response, personQuestionnaireMetrics);
                    result.PersonQuestionnaireMetrics = personQuestionnaireMetrics;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return result;
                }
                else
                {
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                    result.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonAssessmentMetricsInDetail
        /// </summary>
        /// <param name="metricsInput"></param>
        /// <returns></returns>
        public PersonAssessmentMetricsDetailsDTO GetPersonAssessmentMetricsInDetail(DashboardMetricsInputDTO metricsInput)
        {
            try
            {
                PersonAssessmentMetricsDetailsDTO result = new PersonAssessmentMetricsDetailsDTO();
                if (metricsInput?.personId > 0)
                {
                    List<PersonAssessmentMetricsDTO> personQuestionnaireMetrics = new List<PersonAssessmentMetricsDTO>();
                    var response = this.PersonAssessmentMetricsRepository.GetPersonAssessmentMetricsInDetail(metricsInput);
                    this.mapper.Map<List<PersonAssessmentMetrics>, List<PersonAssessmentMetricsDTO>>(response, personQuestionnaireMetrics);
                    result.PersonQuestionnaireMetrics = personQuestionnaireMetrics;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return result;
                }
                else
                {
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                    result.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CRUDResponseDTO AddBulkPersonAssessmentMetrics(List<PersonAssessmentMetricsDTO> personAssessmentMetrics)
        {

            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                List<PersonAssessmentMetrics> personAssessmentMetricsEntity = new List<PersonAssessmentMetrics>();
                this.mapper.Map<List<PersonAssessmentMetricsDTO>, List<PersonAssessmentMetrics>>(personAssessmentMetrics, personAssessmentMetricsEntity);
                var Response = this.PersonAssessmentMetricsRepository.AddBulkPersonAssessmentMetrics(personAssessmentMetricsEntity);
                if (Response != null)
                {
                    result.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                    return result;
                }
                else
                {
                    result.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CRUDResponseDTO UpdateBulkPersonAssessmentMetrics(List<PersonAssessmentMetricsDTO> personAssessmentMetrics)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                List<PersonAssessmentMetrics> personAssessmentMetricsEntity = new List<PersonAssessmentMetrics>();
                this.mapper.Map<List<PersonAssessmentMetricsDTO>, List<PersonAssessmentMetrics>>(personAssessmentMetrics, personAssessmentMetricsEntity);
                var Response = this.PersonAssessmentMetricsRepository.UpdateBulkPersonAssessmentMetrics(personAssessmentMetricsEntity);
                if (Response != null)
                {
                    result.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                    return result;
                }
                else
                {
                    result.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
