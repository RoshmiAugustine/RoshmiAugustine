// -----------------------------------------------------------------------
// <copyright file="NotificationLogService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;
namespace Opeeka.PICS.Domain.Services
{
    public class NotificationLogService : BaseService, INotificationLogService
    {

        /// Initializes a new instance of the notificationLogRepository/> class.
        private readonly INotificationLogRepository notificationLogRepository;
        private IUserRepository userRepository;
        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;
        private readonly IQueryBuilder queryBuilder;
        private readonly IMapper mapper;

        /// <summary>
        /// Notification Log Service
        /// </summary>
        /// <param name="notificationLogRepository"></param>
        /// <param name="localizeService"></param>
        /// <param name="configRepo"></param>
        /// <param name="httpContext"></param>
        /// <param name="querybuild"></param>
        public NotificationLogService(IMapper mapper,INotificationLogRepository notificationLogRepository, LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext,
            IQueryBuilder querybuild, IUserRepository userRepository)
            : base(configRepo, httpContext)
        {
            this.notificationLogRepository = notificationLogRepository;
            this.userRepository = userRepository;
            this.localize = localizeService;
            this.queryBuilder = querybuild;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get Notification Log List
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <returns>NotificationLogResponseDTO</returns>
        public NotificationLogResponseDTO GetNotificationLogList(NotificationLogSearchDTO notificationLogSearchDTO)
        {
            try
            {
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                List<QueryFieldMappingDTO> fieldMappingList = GetNotificationLogConfiguration(offset);
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQuery(notificationLogSearchDTO.SearchFilter, fieldMappingList);
                NotificationLogResponseDTO notificationLogDTO = new NotificationLogResponseDTO();
                List<NotificationLogDTO> response = new List<NotificationLogDTO>();
                int totalCount = 0;

                if (queryBuilderDTO.Page <= 0)
                {
                    notificationLogDTO.NotificationLog = null;
                    notificationLogDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return notificationLogDTO;
                }
                else if (queryBuilderDTO.PageSize <= 0)
                {
                    notificationLogDTO.NotificationLog = null;
                    notificationLogDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return notificationLogDTO;
                }
                else
                {
                    notificationLogSearchDTO.role = this.GetRoleName(notificationLogSearchDTO.userRole);                    
                    Tuple<List<NotificationLogDTO>, int> queryResponse = Tuple.Create(notificationLogDTO.NotificationLog, 0);
                    if (notificationLogSearchDTO.CallingType == PCISEnum.CallingType.Dashboard)
                    {
                        queryResponse = this.notificationLogRepository.GetDashboardNotificationLogList(notificationLogSearchDTO, queryBuilderDTO);
                    }
                    else
                    {
                        queryResponse = this.notificationLogRepository.GetNotificationLogList(notificationLogSearchDTO, queryBuilderDTO);
                    }
                    response = queryResponse.Item1;
                    totalCount = queryResponse.Item2;
                    notificationLogDTO.NotificationLog = response;
                    notificationLogDTO.TotalCount = totalCount;
                    notificationLogDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return notificationLogDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Notification Log Configuration
        /// </summary>
        /// <returns></returns>
        private List<QueryFieldMappingDTO> GetNotificationLogConfiguration(int offset = 0)
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "PersonName",
                fieldAlias = "personName",
                fieldTable = "NH",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "NotificationLogID",
                fieldAlias = "notificationLogID",
                fieldTable = "NH",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "NotificationType",
                fieldAlias = "notificationType",
                fieldTable = "NH",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Details",
                fieldAlias = "details",
                fieldTable = "NH",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = @$"CAST(DateAdd(MINUTE,{0 - offset},NotificationDate) AS DATE)",
                fieldAlias = "notificationDate",
                fieldTable = "",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "PersonID",
                fieldAlias = "personID",
                fieldTable = "NH",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "NotificationTypeID",
                fieldAlias = "notificationTypeID",
                fieldTable = "NH",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "NotificationDate",
                fieldAlias = "notificationDate",
                fieldTable = "NH",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "NotificationResolutionStatusID",
                fieldAlias = "notificationResolutionStatusID",
                fieldTable = "NH",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "NH.StatusDate",
                fieldAlias = "statusDate",
                fieldTable = "NH",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "NotificationType",
                fieldAlias = "notificationType",
                fieldTable = "",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "PersonIndex",
                fieldAlias = "personIndex",
                fieldTable = "P",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "QuestionnaireID",
                fieldAlias = "questionnaireID",
                fieldTable = "Q",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "AssessmentID",
                fieldAlias = "assessmentID",
                fieldTable = "A",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "HelperName",
                fieldAlias = "helperName",
                fieldTable = "NH",
                fieldType = "string"
            });
            return fieldMappingList;
        }

        /// <summary>
        /// Get Notification Log List
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <returns>NotificationLogResponseDTO</returns>
        public NotificationLogResponseDTO GetPastNotificationLogList(NotificationLogSearchDTO notificationLogSearchDTO)
        {
            try
            {
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                List<QueryFieldMappingDTO> fieldMappingList = GetNotificationLogConfiguration(offset);
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQuery(notificationLogSearchDTO.SearchFilter, fieldMappingList);
                NotificationLogResponseDTO notificationLogDTO = new NotificationLogResponseDTO();
                List<NotificationLogDTO> response = new List<NotificationLogDTO>();
                int totalCount = 0;

                if (queryBuilderDTO.Page <= 0)
                {
                    notificationLogDTO.NotificationLog = null;
                    notificationLogDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return notificationLogDTO;
                }
                else if (queryBuilderDTO.PageSize <= 0)
                {
                    notificationLogDTO.NotificationLog = null;
                    notificationLogDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return notificationLogDTO;
                }
                else
                {
                    notificationLogSearchDTO.role = this.GetRoleName(notificationLogSearchDTO.userRole);                    
                    var queryResponse = this.notificationLogRepository.GetPastNotificationLogList(notificationLogSearchDTO, queryBuilderDTO);
                    response = queryResponse.Item1;
                    totalCount = queryResponse.Item2;
                    notificationLogDTO.NotificationLog = response;
                    notificationLogDTO.TotalCount = totalCount;
                    notificationLogDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return notificationLogDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddBulkNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AddBulkNotificationLog(List<NotificationLogDTO> notificationLog)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                List<NotificationLog> NotificationLogEntity = new List<NotificationLog>();
                this.mapper.Map<List<NotificationLogDTO>, List<NotificationLog>>(notificationLog, NotificationLogEntity);
                this.notificationLogRepository.AddBulkNotificationLog(NotificationLogEntity);
                result.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotificationLogForNotificationResolveAlert.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>NotificationLogResponseDTO.</returns>
        public NotificationLogResponseDTO GetNotificationLogForNotificationResolveAlert(long personId)
        {
            try
            {
                NotificationLogResponseDTO result = new NotificationLogResponseDTO();
                var response = this.notificationLogRepository.GetNotificationLogForNotificationResolveAlert(personId);
                if (response != null)
                {
                    List<NotificationLogDTO> Notificationlog = new List<NotificationLogDTO>();
                    this.mapper.Map<List<NotificationLog>, List<NotificationLogDTO>>(response, Notificationlog);
                    result.NotificationLog = Notificationlog;
                    result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    return result;
                }
                else
                {
                    result.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateBulkNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateBulkNotificationLog(List<NotificationLogDTO> notificationLog)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                List<NotificationLog> NotificationLogEntity = new List<NotificationLog>();
                this.mapper.Map<List<NotificationLogDTO>, List<NotificationLog>>(notificationLog, NotificationLogEntity);
                this.notificationLogRepository.UpdateNotificationLog(NotificationLogEntity);
                result.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetUnResolvedNotificationLogForNotificationResolveAlert.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>RiskNotificationResponseDTO.</returns>
        public RiskNotificationResponseDTO GetUnResolvedNotificationLogForNotificationResolveAlert(long personId)
        {
            try
            {
                RiskNotificationResponseDTO result = new RiskNotificationResponseDTO();
                var response = this.notificationLogRepository.GetUnResolvedNotificationLogForNotificationResolveAlert(personId);
                if (response != null)
                {
                    result.NotificationLog = response;
                    result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    return result;
                }
                else
                {
                    result.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// NotificationCountIndication
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <param name="lastLoginTime"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public NotificationLogResponseDTO NotificationCountIndication(NotificationLogSearchDTO notificationLogSearchDTO, int userId)
        {
            try
            {
                var notificationViewedOn = DateTime.UtcNow;
                NotificationLogResponseDTO notificationLogDTO = new NotificationLogResponseDTO();
                List<NotificationLogDTO> response = new List<NotificationLogDTO>();
                int totalCount = 0;
                var userData = this.userRepository.GetByIdAsync(userId).Result;
                if (userData != null)
                {
                    notificationViewedOn = userData.NotificationViewedOn ?? DateTime.UtcNow;
                    var result = this.userRepository.UpdateAsync(userData).Result;
                }
                notificationLogSearchDTO.role = this.GetRoleName(notificationLogSearchDTO.userRole);
                var queryResponse = this.notificationLogRepository.GetNotificationCount(notificationLogSearchDTO, notificationViewedOn);
                response = null;
                totalCount = queryResponse;
                notificationLogDTO.NotificationLog = response;
                notificationLogDTO.TotalCount = totalCount;
                notificationLogDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return notificationLogDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
                
    }
}