// -----------------------------------------------------------------------
// <copyright file="QuestionnaireService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Services
{
    public class QuestionnaireService : BaseService, IQuestionnaireService
    {
        /// Initializes a new instance of the personRepository/> class.
        private readonly IQuestionnaireRepository questionnaireRepository;
        private readonly IQuestionnaireItemRepository questionnaireItemRepository;
        private readonly IQuestionnaireWindowRepository questionnaireWindowRepository;
        private readonly IQuestionnaireReminderRuleRespository questionnaireReminderRuleRespository;
        private readonly IQuestionnaireNotifyRiskRuleRepository questionnaireNotifyRiskRuleRespository;
        private readonly IQuestionnaireNotifyRiskRuleConditionRepository questionnaireNotifyRiskRuleConditionRepository;
        private readonly IQuestionnaireNotifyRiskScheduleRepository questionnaireNotifyRiskScheduleRepository;
        private readonly IPersonRepository personRepository;
        private readonly INotifyReminderRepository notifyReminderRepository;
        private readonly INotificationLogRepository notificationLogRepository;
        private readonly INotificationTypeRepository notificationTypeRepository;
        private readonly INotifiationResolutionStatusRepository notifiationResolutionStatusRepository;
        private readonly IMapper mapper;
        private readonly IQuestionnaireNotifyRiskRuleRepository questionnaireNotifyRiskRuleRepository;
        private readonly INotifyRiskRepository notifyRiskRepository;
        private readonly INotifyRiskValueRepository notifyRiskValueRepository;
        private readonly IQuestionnaireSkipLogicRuleRepository questionnaireSkipLogicRuleRepository;
        private readonly IQuestionnaireSkipLogicRuleConditionRepository questionnaireSkipLogicRuleConditionRepository;
        private readonly IQuestionnaireSkipLogicRuleActionRepository questionnaireSkipLogicRuleActionRepository;

        private readonly IQuestionnaireDefaultResponseRuleRepository questionnaireDefaultResponseRuleRepository;
        private readonly IQuestionnaireDefaultResponseRuleConditionRepository questionnaireDefaultResponseRuleConditionRepository;
        private readonly IQuestionnaireDefaultResponseRuleActionRepository questionnaireDefaultResponseRuleActionRepository;
        private readonly IQuestionnaireRegularReminderRecurrenceRepository questionnaireRegularReminderRecurrenceRepository;
        private readonly IQuestionnaireRegularReminderTimeRuleRepository questionnaireRegularReminderTimeRuleRepository;
        private readonly ILookupRepository lookupRepository;

        CRUDResponseDTO response = new CRUDResponseDTO();
        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;

        /// Initializes a new instance of the Utility class.
        private readonly IUtility utility;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<QuestionnaireService> logger;
        private readonly IPersonQuestionnaireRepository personQuestionnaireRepository;
        //
        private readonly IQueryBuilder queryBuilder;

        public IQueue _queue { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personRepository"></param>
        /// <param name="logger"></param>
        public QuestionnaireService(IQuestionnaireDefaultResponseRuleActionRepository questionnaireDefaultResponseRuleActionRepository,IQuestionnaireDefaultResponseRuleConditionRepository questionnaireDefaultResponseRuleConditionRepository, IQuestionnaireDefaultResponseRuleRepository questionnaireDefaultResponseRuleRepository, IQuestionnaireSkipLogicRuleConditionRepository questionnaireSkipLogicRuleConditionRepository,
            IQuestionnaireSkipLogicRuleActionRepository questionnaireSkipLogicRuleActionRepository,
            IQuestionnaireSkipLogicRuleRepository questionnaireSkipLogicRuleRepository, INotifyRiskValueRepository notifyRiskValueRepository, INotifyRiskRepository notifyRiskRepository, IMapper mapper, IQuestionnaireNotifyRiskRuleRepository questionnaireNotifyRiskRuleRepository, INotifiationResolutionStatusRepository notifiationResolutionStatusRepository, INotificationTypeRepository notificationTypeRepository, INotificationLogRepository notificationLogRepository, INotifyReminderRepository notifyReminderRepository, IPersonRepository personRepository, IQuestionnaireNotifyRiskRuleConditionRepository questionnaireNotifyRiskRuleConditionRepository, IQuestionnaireNotifyRiskRuleRepository questionnaireNotifyRiskRuleRespository, IQuestionnaireReminderRuleRespository questionnaireReminderRuleRespository, IQuestionnaireWindowRepository questionnaireWindowRepository, IQuestionnaireNotifyRiskScheduleRepository questionnaireNotifyRiskScheduleRepository, IQuestionnaireItemRepository questionnaireItemRepository, IQuestionnaireRepository questionnaireRepository, ILogger<QuestionnaireService> logger, LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, IUtility utility, IPersonQuestionnaireRepository personQuestionnaireRepository, IQueue _queue, IQueryBuilder queryBuilder, IQuestionnaireRegularReminderRecurrenceRepository questionnaireRegularReminderRecurrenceRepository, IQuestionnaireRegularReminderTimeRuleRepository questionnaireRegularReminderTimeRuleRepository,ILookupRepository lookupRepository)
            : base(configRepo, httpContext)
        {
            this.personRepository = personRepository;
            this.questionnaireRepository = questionnaireRepository;
            this.questionnaireItemRepository = questionnaireItemRepository;
            this.questionnaireNotifyRiskScheduleRepository = questionnaireNotifyRiskScheduleRepository;
            this.questionnaireReminderRuleRespository = questionnaireReminderRuleRespository;
            this.questionnaireNotifyRiskRuleRespository = questionnaireNotifyRiskRuleRespository;
            this.questionnaireNotifyRiskRuleConditionRepository = questionnaireNotifyRiskRuleConditionRepository;
            this.questionnaireWindowRepository = questionnaireWindowRepository;
            this.logger = logger;
            this.localize = localizeService;
            this.utility = utility;
            this.personQuestionnaireRepository = personQuestionnaireRepository;
            this._queue = _queue;
            this.queryBuilder = queryBuilder;
            this.notifyReminderRepository = notifyReminderRepository;
            this.notificationLogRepository = notificationLogRepository;
            this.notificationTypeRepository = notificationTypeRepository;
            this.notifiationResolutionStatusRepository = notifiationResolutionStatusRepository;
            this.questionnaireNotifyRiskRuleRepository = questionnaireNotifyRiskRuleRepository;
            this.mapper = mapper;
            this.notifyRiskRepository = notifyRiskRepository;
            this.notifyRiskValueRepository = notifyRiskValueRepository;
            this.questionnaireSkipLogicRuleRepository = questionnaireSkipLogicRuleRepository;
            this.questionnaireSkipLogicRuleConditionRepository = questionnaireSkipLogicRuleConditionRepository;
            this.questionnaireSkipLogicRuleActionRepository = questionnaireSkipLogicRuleActionRepository;

            this.questionnaireDefaultResponseRuleRepository = questionnaireDefaultResponseRuleRepository;
            this.questionnaireDefaultResponseRuleConditionRepository = questionnaireDefaultResponseRuleConditionRepository;
            this.questionnaireDefaultResponseRuleActionRepository = questionnaireDefaultResponseRuleActionRepository;
            this.questionnaireRegularReminderTimeRuleRepository = questionnaireRegularReminderTimeRuleRepository;
            this.questionnaireRegularReminderRecurrenceRepository = questionnaireRegularReminderRecurrenceRepository;
            this.lookupRepository = lookupRepository;
        }

        /// <summary>
        /// GetQuestionnaireList.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>QuestionnaireListResponseDTO.</returns> 
        public QuestionnaireListResponseDTO GetQuestionnaireList(QuestionnaireSearchDTO questionnaireSearchDTO)
        {
            try
            {
                List<QueryFieldMappingDTO> fieldMappingList = GetQuestionnaireConfiguration();
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQuery(questionnaireSearchDTO.SearchFilter, fieldMappingList);
                QuestionnaireListResponseDTO getQuestionnaire = new QuestionnaireListResponseDTO();
                List<QuestionnaireDTO> response = new List<QuestionnaireDTO>();
                int totalCount = 0;
                if (queryBuilderDTO.Page <= 0)
                {
                    getQuestionnaire.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getQuestionnaire;
                }
                else if (queryBuilderDTO.PageSize <= 0)
                {
                    getQuestionnaire.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    getQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getQuestionnaire;
                }
                else
                {
                    queryBuilderDTO.OrderBy = queryBuilderDTO.OrderBy.Replace("order by", "order by Q.IsBaseQuestionnaire DESC, ");
                    response = this.questionnaireRepository.GetQuestionnaireList(questionnaireSearchDTO, queryBuilderDTO);
                    if (response.Count > 0)
                        totalCount = response.FirstOrDefault().TotalCount;

                    getQuestionnaire.QuestionnaireList = response;
                    getQuestionnaire.TotalCount = totalCount;
                    getQuestionnaire.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    getQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return getQuestionnaire;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// GetQuestionnaireDetails.
        /// </summary>
        /// <param name="questionnaireIndex">questionnaireIndex.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>QuestionnaireDetailsResponseDTO.</returns>
        public QuestionnaireDetailsResponseDTO GetQuestionnaireDetails(QuestionnaireDetailsSearchDTO detailsSearchDTO)
        {
            try
            {
                List<QueryFieldMappingDTO> fieldMappingList = GetQuestionnaireDetailsConfiguration();
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQuery(detailsSearchDTO.SearchFilter, fieldMappingList);
                QuestionnaireDetailsResponseDTO getQuestionnaire = new QuestionnaireDetailsResponseDTO();
                List<QuestionnaireDetailsDTO> response = new List<QuestionnaireDetailsDTO>();
                int totalCount = 0;
                if (detailsSearchDTO.pageNumber <= 0)
                {
                    getQuestionnaire.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getQuestionnaire;
                }
                else if (detailsSearchDTO.pageLimit <= 0)
                {
                    getQuestionnaire.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    getQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getQuestionnaire;
                }
                else if (detailsSearchDTO.questionId <= 0)
                {
                    getQuestionnaire.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.QuestionnaireID);
                    getQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getQuestionnaire;
                }
                else
                {

                    response = this.questionnaireItemRepository.GetQuestionnaireDetails(detailsSearchDTO, queryBuilderDTO);
                    if (response.Count > 0)
                    {
                        totalCount = response.FirstOrDefault().TotalCount; // this.questionnaireItemRepository.GetQuestionnaireDetailsCount(detailsSearchDTO.questionId);
                    }
                    else
                    {
                        totalCount = 0;
                    }

                    getQuestionnaire.QuestionnaireDetails = response;
                    getQuestionnaire.TotalCount = totalCount;
                    getQuestionnaire.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    getQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return getQuestionnaire;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotificationDetails.
        /// </summary>
        /// <param name="id">questionnaireid.</param>
        /// <returns>NotificationDetailsResponseDTO.</returns>
        public NotificationDetailsResponseDTO GetNotificationDetails(int id)
        {
            try
            {
                NotificationDetailsResponseDTO notification = new NotificationDetailsResponseDTO();
                if (id <= 0)
                {
                    notification.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.QuestionnaireID);
                    notification.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return notification;
                }
                else
                {
                    QuestionnaireNotifyRiskScheduleDTO QuestionnaireNotifyRiskSchedule = new QuestionnaireNotifyRiskScheduleDTO();
                    var response = this.questionnaireNotifyRiskScheduleRepository.GetNotificationDetails(id);
                    if (response != null && response.Count > 0)
                    {
                        List<QuestionnaireNotifyRiskRuleDTO> QuestionnaireNotifyRiskRuleList = new List<QuestionnaireNotifyRiskRuleDTO>();
                        QuestionnaireNotifyRiskSchedule.QuestionnaireNotifyRiskScheduleID = response[0].QuestionnaireNotifyRiskScheduleID;
                        QuestionnaireNotifyRiskSchedule.IsAlertsHelpersManagers = response[0].IsAlertsHelpersManagers;
                        QuestionnaireNotifyRiskSchedule.Name = response[0].NotificationName;
                        QuestionnaireNotifyRiskSchedule.QuestionnaireID = id;

                        var RiskRuleIds = response.Select(x => x.QuestionnaireNotifyRiskRuleID).Distinct();

                        foreach (int rule in RiskRuleIds)
                        {
                            var data = response.Where(x => x.QuestionnaireNotifyRiskRuleID == rule).ToList();

                            QuestionnaireNotifyRiskRuleDTO QuestionnaireNotifyRiskRuleDTO = new QuestionnaireNotifyRiskRuleDTO();

                            QuestionnaireNotifyRiskRuleDTO.Name = data[0].RuleName;
                            QuestionnaireNotifyRiskRuleDTO.NotificationLevelID = data[0].NotificationLevelID;
                            QuestionnaireNotifyRiskRuleDTO.QuestionnaireNotifyRiskRuleID = data[0].QuestionnaireNotifyRiskRuleID;
                            QuestionnaireNotifyRiskRuleDTO.QuestionnaireNotifyRiskScheduleID = data[0].QuestionnaireNotifyRiskScheduleID;

                            List<QuestionnaireNotifyRiskRuleConditionDTO> QuestionnaireNotifyRiskRuleConditionList = new List<QuestionnaireNotifyRiskRuleConditionDTO>();
                            foreach (var item in data)
                            {
                                QuestionnaireNotifyRiskRuleConditionDTO QuestionnaireNotifyRiskRuleCondition = new QuestionnaireNotifyRiskRuleConditionDTO();
                                QuestionnaireNotifyRiskRuleCondition.QuestionnaireNotifyRiskRuleConditionID = item.QuestionnaireNotifyRiskRuleConditionID;
                                QuestionnaireNotifyRiskRuleCondition.QuestionnaireItemId = item.QuestionnaireItemID;
                                QuestionnaireNotifyRiskRuleCondition.QuestionnaireNotifyRiskRuleID = item.QuestionnaireNotifyRiskRuleID;
                                QuestionnaireNotifyRiskRuleCondition.ComparisonOperator = item.ComparisonOperator;
                                QuestionnaireNotifyRiskRuleCondition.ComparisonValue = item.ComparisonValue;
                                QuestionnaireNotifyRiskRuleCondition.ListOrder = item.ListOrder;
                                QuestionnaireNotifyRiskRuleCondition.JoiningOperator = item.JoiningOperator;

                                QuestionnaireNotifyRiskRuleConditionList.Add(QuestionnaireNotifyRiskRuleCondition);
                            }
                            QuestionnaireNotifyRiskRuleDTO.QuestionnaireNotifyRiskRuleConditionDTO = QuestionnaireNotifyRiskRuleConditionList;
                            QuestionnaireNotifyRiskRuleList.Add(QuestionnaireNotifyRiskRuleDTO);
                        }

                        QuestionnaireNotifyRiskSchedule.QuestionnaireNotifyRiskRuleDTO = QuestionnaireNotifyRiskRuleList;
                    }
                    notification.NotificationDetails = QuestionnaireNotifyRiskSchedule;
                    notification.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    notification.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return notification;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateQuestionnaire
        /// </summary>
        /// <param name="questionnaireData"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO UpdateQuestionnaire(QuestionnaireInputDTO questionnaireData)
        {
            try
            {
                if (questionnaireData != null)
                {
                    var questionnaire = this.questionnaireItemRepository.GetQuestionnaireItems(questionnaireData.QuestionnaireItemID).Result;
                    if (questionnaire != null)
                    {
                        questionnaire.LowerItemResponseBehaviorID = questionnaireData.LowerItemResponseBehaviorID;
                        questionnaire.MedianItemResponseBehaviorID = questionnaireData.MedianItemResponseBehaviorID;
                        questionnaire.UpperItemResponseBehaviorID = questionnaireData.UpperItemResponseBehaviorID;
                        questionnaire.UpdateDate = DateTime.UtcNow;
                        questionnaire.UpdateUserID = questionnaireData.UpdateUserID;
                        questionnaire.LowerResponseValue = questionnaireData.LowerResponseValue;
                        questionnaire.UpperResponseValue = questionnaireData.UpperResponseValue;
                        questionnaire.CanOverrideLowerResponseBehavior = questionnaireData.MinOption;
                        questionnaire.CanOverrideUpperResponseBehavior = questionnaireData.MaxOption;
                        questionnaire.CanOverrideMedianResponseBehavior = questionnaireData.AltOption;
                        questionnaire.StartDate = questionnaireData.StartDate;
                        questionnaire.EndDate = questionnaireData.EndDate;


                        var addressResponse = this.questionnaireItemRepository.UpdateQuestionnaireItem(questionnaire);
                        if (addressResponse != null)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                        }
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
        /// GetQuestionnaireDefaultResponseRule.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnaireDefaultResponseRuleDetailsDTO.</returns>
        public QuestionnaireDefaultResponseRuleDetailsDTO GetQuestionnaireDefaultResponseRule(int questionnaireId)
        {
            try
            {

                QuestionnaireDefaultResponseRuleDetailsDTO result = new QuestionnaireDefaultResponseRuleDetailsDTO();
                var response = this.questionnaireDefaultResponseRuleRepository.GetQuestionnaireDefaultResponse(questionnaireId);
                if (response != null)
                {
                    foreach (var item in response)
                    {
                        item.QuestionnaireDefaultResponseRuleActions = this.questionnaireDefaultResponseRuleActionRepository.GetQuestionnaireDefaultResponseAction(item.QuestionnaireDefaultResponseRuleID);
                        item.QuestionnaireDefaultResponseRuleConditions = this.questionnaireDefaultResponseRuleConditionRepository.GetQuestionnaireDefaultResponseCondition(item.QuestionnaireDefaultResponseRuleID);
                    }
                    result.QuestionnaireDefaultResponseRules = response;
                    if (response.Count > 0)
                    {
                        result.QuestionnaireID = response[0]?.QuestionnaireID;
                        result.HasDefaultResponseRule = response[0]?.HasDefaultResponseRule;
                    }
                    else
                    {
                        result.QuestionnaireID = null;
                        result.HasDefaultResponseRule = null;
                    }
                    result.QuestionnaireDefaultResponseRules = response;
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
        /// UpdateSchedule.
        /// Updated for PCIS-3225 : Updating RegularReminderRecurrenceSettings, TimeRules update
        /// Updating InviteToCompleteReceiversFlag and InviteToCompleteReceiversIds too.
        /// </summary>
        /// <param name="scheduleParameter">ScheduleParameterDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateSchedule(ScheduleParameterDTO scheduleParameter)
        {
            try
            {
                CRUDResponseDTO response = new CRUDResponseDTO();


                UpdateQuestionnaireDetails(scheduleParameter);
                bool questionnaireAddedToQueue = false;
                UpdateQuestionnaireWindow(scheduleParameter, ref questionnaireAddedToQueue);                
                UpdateQuestionnaireReminderRule(scheduleParameter, ref questionnaireAddedToQueue);
                UpdateNotifyRiskSchedule(scheduleParameter);
                UpdateSkipLogic(scheduleParameter);
                UpdateDefaultRespose(scheduleParameter);
                UpdateReminderTimeRule(scheduleParameter, ref questionnaireAddedToQueue);
                if (questionnaireAddedToQueue)
                {
                    List<long> personQuestionnairesToBeAddedToQueue = this.personQuestionnaireRepository.GetAllPersonQuestionnaireIdsByQuestionnaireID(scheduleParameter.QuestionnaireID);
                    personQuestionnairesToBeAddedToQueue = personQuestionnairesToBeAddedToQueue?.Distinct().ToList();
                    personQuestionnairesToBeAddedToQueue.ForEach(x => StoreInQueue(x));
                    questionnaireAddedToQueue = true;
                }
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateReminderRecurrenceSettings.
        /// PCIS-3225 : Update ReminderRecurrenceSettings for RegularWindow settings.
        /// </summary>
        /// <param name="regularReminderReccurence"></param>
        /// <param name="questionnaireID"></param>
        /// <param name="questionnaireAddedToQueue"></param>
        private void UpdateReminderRecurrenceSettings(QuestionnaireRegularReminderRecurrenceDTO regularReminderReccurence, int questionnaireID, ref bool questionnaireAddedToQueue)
        {

            try
            {
                questionnaireAddedToQueue = true;
                QuestionnaireRegularReminderRecurrenceDTO ruleToAdd = new QuestionnaireRegularReminderRecurrenceDTO();
                var existingRule = this.questionnaireRegularReminderRecurrenceRepository.GetQuestionnaireRegularReminderRecurrence(questionnaireID);
                //if regularwindow is not selected then regularReminderReccurence will be null
                if (regularReminderReccurence == null )
                {
                    if (existingRule != null)
                    {
                        existingRule.IsRemoved = true;
                        existingRule.UpdateDate = DateTime.UtcNow;
                        this.questionnaireRegularReminderRecurrenceRepository.UpdateQuestionnaireReminderRecurrence(existingRule);
                    }
                    return;
                }
                if (regularReminderReccurence.QuestionnaireRegularReminderRecurrenceID > 0 || existingRule != null)
                {
                    regularReminderReccurence = ValidateRegularReminderSettings(regularReminderReccurence);
                    var existingRuleEdit = new QuestionnaireRegularReminderRecurrenceDTO()
                    {
                        IsRemoved = false,
                        QuestionnaireRegularReminderRecurrenceID = regularReminderReccurence.QuestionnaireRegularReminderRecurrenceID > 0 ? regularReminderReccurence.QuestionnaireRegularReminderRecurrenceID: existingRule.QuestionnaireRegularReminderRecurrenceID,
                        RecurrenceDayNameIDs = regularReminderReccurence.RecurrenceDayNameIDs,
                        QuestionnaireID = questionnaireID,
                        QuestionnaireWindowID = regularReminderReccurence.QuestionnaireWindowID,
                        RecurrencePatternID = regularReminderReccurence.RecurrencePatternID,
                        RecurrenceDayNoOfMonth = regularReminderReccurence.RecurrenceDayNoOfMonth,
                        RecurrenceInterval = regularReminderReccurence.RecurrenceInterval,
                        RecurrenceMonthIDs = regularReminderReccurence.RecurrenceMonthIDs,
                        RecurrenceOrdinalIDs = regularReminderReccurence.RecurrenceOrdinalIDs,
                        RecurrenceRangeStartDate = regularReminderReccurence.RecurrenceRangeStartDate,
                        RecurrenceRangeEndDate = regularReminderReccurence.RecurrenceRangeEndDate,
                        RecurrenceRangeEndInNumber = regularReminderReccurence.RecurrenceRangeEndInNumber,
                        RecurrenceRangeEndTypeID = regularReminderReccurence.RecurrenceRangeEndTypeID,
                        UpdateDate = DateTime.UtcNow
                    };
                    
                    this.questionnaireRegularReminderRecurrenceRepository.UpdateQuestionnaireReminderRecurrence(existingRuleEdit);
                }
                else
                {
                    var newRule = ValidateRegularReminderSettings(regularReminderReccurence);
                    newRule.QuestionnaireRegularReminderRecurrenceID = 0;
                    newRule.QuestionnaireID = questionnaireID;
                    newRule.IsRemoved = false;
                    newRule.UpdateDate = DateTime.UtcNow;
                    this.questionnaireRegularReminderRecurrenceRepository.AddQuestionnaireReminderRecurrence(newRule);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ValidateRegularReminderSettings.
        /// </summary>
        /// <param name="regularReminderReccurence"></param>
        /// <returns></returns>
        private QuestionnaireRegularReminderRecurrenceDTO ValidateRegularReminderSettings(QuestionnaireRegularReminderRecurrenceDTO regularReminderReccurence)
        {
            try
            {
                RecurrenceEndTypeDTO recurrenceEndTypeDTO = this.lookupRepository.GetAllRecurrenceEndType().Where(x => x.RecurrenceEndTypeID == regularReminderReccurence.RecurrenceRangeEndTypeID).FirstOrDefault();
                if(recurrenceEndTypeDTO.Name == PCISEnum.RecurrenceEndType.EndByNoEndate)
                {
                    regularReminderReccurence.RecurrenceRangeEndDate = null;
                    regularReminderReccurence.RecurrenceRangeEndInNumber = null;
                }

                RecurrencePatternDTO recurrencePatternDTO = this.lookupRepository.GetAllRecurrencePattern().Where(x => x.RecurrencePatternID == regularReminderReccurence.RecurrencePatternID).FirstOrDefault();
                if(recurrencePatternDTO.Name == PCISEnum.RecurrencePattern.DailyDays)
                {
                    regularReminderReccurence.RecurrenceOrdinalIDs = string.Empty;
                    regularReminderReccurence.RecurrenceDayNameIDs = string.Empty;
                    regularReminderReccurence.RecurrenceMonthIDs = string.Empty;
                    regularReminderReccurence.RecurrenceDayNoOfMonth = null;
                }
                if (recurrencePatternDTO.Name == PCISEnum.RecurrencePattern.DailyWeekdays)
                {
                    regularReminderReccurence.RecurrenceInterval = 0;
                    regularReminderReccurence.RecurrenceDayNoOfMonth = null;
                    regularReminderReccurence.RecurrenceOrdinalIDs = string.Empty;
                    regularReminderReccurence.RecurrenceMonthIDs = string.Empty;
                }
                if (recurrencePatternDTO.Name == PCISEnum.RecurrencePattern.Weekly)
                {
                    regularReminderReccurence.RecurrenceOrdinalIDs = string.Empty;
                    regularReminderReccurence.RecurrenceMonthIDs = string.Empty;
                    regularReminderReccurence.RecurrenceDayNoOfMonth = null;
                }
                if (recurrencePatternDTO.Name == PCISEnum.RecurrencePattern.MonthlyByDay)
                {
                    regularReminderReccurence.RecurrenceDayNameIDs = string.Empty;
                    regularReminderReccurence.RecurrenceOrdinalIDs = string.Empty;
                    regularReminderReccurence.RecurrenceMonthIDs = string.Empty;
                }
                if (recurrencePatternDTO.Name == PCISEnum.RecurrencePattern.MonthlyByOrdinalDay)
                {
                    regularReminderReccurence.RecurrenceMonthIDs = string.Empty;
                    regularReminderReccurence.RecurrenceDayNoOfMonth = null;
                }
                if (recurrencePatternDTO.Name == PCISEnum.RecurrencePattern.YearlyByMonth)
                {
                    regularReminderReccurence.RecurrenceOrdinalIDs = string.Empty;
                    regularReminderReccurence.RecurrenceDayNameIDs = string.Empty;
                }
                if (recurrencePatternDTO.Name == PCISEnum.RecurrencePattern.YearlyByOrdinal)
                {
                    regularReminderReccurence.RecurrenceDayNoOfMonth = null;
                }
                regularReminderReccurence.RecurrenceRangeStartDate = regularReminderReccurence.RecurrenceRangeStartDate.Date;
                if (regularReminderReccurence.RecurrenceRangeEndDate.HasValue)
                {
                    regularReminderReccurence.RecurrenceRangeEndDate = regularReminderReccurence.RecurrenceRangeEndDate.Value.Date;
                }
                     
                return regularReminderReccurence;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// PCIS-3225 : Update ReminderTimeRule
        /// </summary>
        /// <param name="scheduleParameter"></param>
        /// <param name="questionnaireAddedToQueue"></param>
        private void UpdateReminderTimeRule(ScheduleParameterDTO scheduleParameter, ref bool questionnaireAddedToQueue)
        {

            try
            {
                var addToQueue = false;
                List<QuestionnaireRegularReminderTimeRuleDTO> lst_TimeToAdd = new List<QuestionnaireRegularReminderTimeRuleDTO>();
                List<QuestionnaireRegularReminderTimeRuleDTO> lst_TimeToEdit = new List<QuestionnaireRegularReminderTimeRuleDTO>();
                var lst_ALLtimeRule = this.questionnaireRegularReminderTimeRuleRepository.GetQuestionnaireRegularReminderTimeRule(scheduleParameter.QuestionnaireID);
                if (scheduleParameter?.questionnaireRegularReminderTime == null || scheduleParameter?.questionnaireRegularReminderTime?.Count == 0)
                {
                    addToQueue = true;
                    lst_ALLtimeRule.ForEach(x => { x.IsRemoved = true; x.UpdateDate = DateTime.UtcNow; });
                    this.questionnaireRegularReminderTimeRuleRepository.UpdateReminderTimeRule(lst_ALLtimeRule);
                    return;
                }
                if (scheduleParameter.questionnaireRegularReminderTime.Count == 0) return;
                var removableTime = lst_ALLtimeRule.Where(x => !scheduleParameter.questionnaireRegularReminderTime.Select(y => y.QuestionnaireRegularReminderTimeRuleID).ToList().Contains(x.QuestionnaireRegularReminderTimeRuleID)).ToList();
                if (removableTime.Count > 0)
                {
                    removableTime?.ForEach(x => { x.IsRemoved = true; x.UpdateDate = DateTime.UtcNow; });
                    lst_TimeToEdit.AddRange(removableTime);
                }

                foreach (QuestionnaireRegularReminderTimeRuleDTO item in scheduleParameter?.questionnaireRegularReminderTime)
                {
                    if (item.QuestionnaireRegularReminderTimeRuleID > 0)
                    {
                        var timeRule = lst_ALLtimeRule.Where(x => x.QuestionnaireRegularReminderTimeRuleID == item.QuestionnaireRegularReminderTimeRuleID).FirstOrDefault();
                        timeRule.Hour = item.Hour;
                        timeRule.Minute = item.Minute;
                        timeRule.AMorPM = item.AMorPM;
                        timeRule.TimeZonesID = item.TimeZonesID;
                        timeRule.IsRemoved = false;
                        timeRule.UpdateDate = DateTime.UtcNow;
                        lst_TimeToEdit.Add(timeRule);
                    }
                    else
                    {
                        item.QuestionnaireRegularReminderTimeRuleID = 0;
                        item.IsRemoved = false;
                        item.UpdateDate = DateTime.UtcNow;
                        lst_TimeToAdd.Add(item);
                    }
                }
                if (lst_TimeToAdd.Count > 0)
                {
                    addToQueue = true;
                    this.questionnaireRegularReminderTimeRuleRepository.AddReminderTimeRule(lst_TimeToAdd);
                }
                if (lst_TimeToEdit.Count > 0)
                {
                    addToQueue = true;
                    this.questionnaireRegularReminderTimeRuleRepository.UpdateReminderTimeRule(lst_TimeToEdit);
                }
                questionnaireAddedToQueue = addToQueue;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> StoreInQueue(long personQuestionnaireID)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(personQuestionnaireID.ToString());
            return await this._queue.Push(PCISEnum.AzureQueues.Assessmentremindernotification, Convert.ToBase64String(plainTextBytes));
        }

        /// <summary>
        /// UpdateQuestionaireDetails
        /// </summary>
        /// <param name="id">questionnaireEditDetailsDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public bool UpdateQuestionnaireDetails(ScheduleParameterDTO scheduleParameter)
        {
            try
            {

                QuestionnairesDTO QuestionnaireDTO = this.questionnaireRepository.GetQuestionnaire(scheduleParameter.QuestionnaireID).Result;
                if (QuestionnaireDTO != null)
                {
                    QuestionnaireDTO.ReminderScheduleName = scheduleParameter.ReminderScheduleName;
                    QuestionnaireDTO.IsAlertsHelpersManagers = scheduleParameter.IsAlertsHelpersManagers;
                    QuestionnaireDTO.IsEmailRemindersHelpers = scheduleParameter.IsEmailRemindersHelpers;
                    QuestionnaireDTO.HasSkipLogic = scheduleParameter.HasSkipLogic;
                    QuestionnaireDTO.HasDefaultResponseRule = scheduleParameter.HasDefaultResponseRule;
                    QuestionnaireDTO.UpdateUserID = scheduleParameter.UpdatedUserID;
                    QuestionnaireDTO.UpdateDate = DateTime.UtcNow;
                    QuestionnaireDTO.InviteToCompleteReceiverIds = scheduleParameter.InviteToCompleteReceiverIds;
                    QuestionnaireDTO.IsEmailInviteToCompleteReminders = scheduleParameter.IsEmailInviteToCompleteReminders;
                    var Questionnaireresult = this.questionnaireRepository.UpdateQuestionnaire(QuestionnaireDTO);

                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateNotification.
        /// </summary>
        /// <param name="QuestionnaireReminderRuleList">QuestionnaireReminderRuleDTO</param>
        /// <returns>bool</returns>
        public bool UpdateNotifyRiskSchedule(ScheduleParameterDTO scheduleParameter)
        {
            try
            {
                if (scheduleParameter != null)
                {
                    var currentQuestionnaireNotifyRiskSchedule = this.questionnaireNotifyRiskScheduleRepository.GetQuestionnaireNotifyRiskScheduleByQuestionnaireID(scheduleParameter.QuestionnaireID).Result;

                    if (scheduleParameter.QuestionnaireNotifyRiskSchedule != null)
                    {
                        QuestionnaireNotifyRiskScheduleDTO QuestionnaireNotifyRiskScheduleDTO = new QuestionnaireNotifyRiskScheduleDTO();
                        QuestionnaireNotifyRiskScheduleDTO = scheduleParameter.QuestionnaireNotifyRiskSchedule;

                        if (QuestionnaireNotifyRiskScheduleDTO.QuestionnaireNotifyRiskScheduleID > 0)
                        {
                            var questionnaireNotifyRiskSchedule = this.questionnaireNotifyRiskScheduleRepository.GetQuestionnaireNotifyRiskSchedule(QuestionnaireNotifyRiskScheduleDTO.QuestionnaireNotifyRiskScheduleID).Result;

                            questionnaireNotifyRiskSchedule.Name = QuestionnaireNotifyRiskScheduleDTO.Name;
                            questionnaireNotifyRiskSchedule.UpdateUserID = scheduleParameter.UpdatedUserID;
                            questionnaireNotifyRiskSchedule.UpdateDate = DateTime.UtcNow;

                            var questionnaireNotifyRiskRuleList = this.questionnaireNotifyRiskRuleRespository.
                                GetQuestionnaireNotifyRiskRuleByScheduleID(QuestionnaireNotifyRiskScheduleDTO.QuestionnaireNotifyRiskScheduleID);
                            if (QuestionnaireNotifyRiskScheduleDTO.QuestionnaireNotifyRiskRuleDTO.Count > 0)
                            {
                                var removableRiskRuleList = questionnaireNotifyRiskRuleList.Where(x => !QuestionnaireNotifyRiskScheduleDTO.QuestionnaireNotifyRiskRuleDTO.Select(y => y.QuestionnaireNotifyRiskRuleID).ToList().Contains(x.QuestionnaireNotifyRiskRuleID)).ToList();

                                foreach (var RiskRuleList in removableRiskRuleList)
                                {
                                    RiskRuleList.IsRemoved = true;
                                    RiskRuleList.UpdateDate = DateTime.UtcNow;
                                    RiskRuleList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    this.questionnaireNotifyRiskRuleRespository.UpdateQuestionnaireNotifyRiskRule(RiskRuleList);
                                }

                                foreach (QuestionnaireNotifyRiskRuleDTO item in QuestionnaireNotifyRiskScheduleDTO.QuestionnaireNotifyRiskRuleDTO)
                                {
                                    if (item.QuestionnaireNotifyRiskRuleID > 0)
                                    {
                                        var questionnaireNotifyRiskRule = questionnaireNotifyRiskRuleList.Where(x=> x.QuestionnaireNotifyRiskRuleID == item.QuestionnaireNotifyRiskRuleID).FirstOrDefault();
                                        questionnaireNotifyRiskRule.Name = item.Name;
                                        questionnaireNotifyRiskRule.UpdateDate = DateTime.UtcNow;
                                        questionnaireNotifyRiskRule.UpdateUserID = scheduleParameter.UpdatedUserID;
                                        questionnaireNotifyRiskRule.NotificationLevelID = item.NotificationLevelID;

                                        var questionnaireNotifyRiskRuleConditionList = this.questionnaireNotifyRiskRuleConditionRepository.GetQuestionnaireNotifyRiskRuleConditionByRuleID(item.QuestionnaireNotifyRiskRuleID);

                                        if (item.QuestionnaireNotifyRiskRuleConditionDTO.Count > 0)
                                        {
                                            var removableRuleConditionList = questionnaireNotifyRiskRuleConditionList.Where(x => !item.QuestionnaireNotifyRiskRuleConditionDTO.Select(y => y.QuestionnaireNotifyRiskRuleConditionID).ToList().Contains(x.QuestionnaireNotifyRiskRuleConditionID)).ToList();
                                            foreach (var conditionList in removableRuleConditionList)
                                            {
                                                conditionList.IsRemoved = true;
                                                conditionList.UpdateDate = DateTime.UtcNow;
                                                conditionList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                                this.questionnaireNotifyRiskRuleConditionRepository.UpdateQuestionnaireNotifyRiskRuleCondition(conditionList);
                                            }

                                            foreach (QuestionnaireNotifyRiskRuleConditionDTO data in item.QuestionnaireNotifyRiskRuleConditionDTO)
                                            {
                                                if (data.QuestionnaireNotifyRiskRuleConditionID > 0)
                                                {
                                                    var questionnaireNotifyRiskRuleCondition = questionnaireNotifyRiskRuleConditionList.Where(x=> x.QuestionnaireNotifyRiskRuleConditionID == data.QuestionnaireNotifyRiskRuleConditionID).FirstOrDefault();

                                                    questionnaireNotifyRiskRuleCondition.QuestionnaireItemID = data.QuestionnaireItemId;
                                                    questionnaireNotifyRiskRuleCondition.ComparisonOperator = data.ComparisonOperator;
                                                    questionnaireNotifyRiskRuleCondition.ComparisonValue = data.ComparisonValue;
                                                    questionnaireNotifyRiskRuleCondition.ListOrder = data.ListOrder;
                                                    questionnaireNotifyRiskRuleCondition.JoiningOperator = data.JoiningOperator;
                                                    questionnaireNotifyRiskRuleCondition.UpdateUserID = scheduleParameter.UpdatedUserID;
                                                    questionnaireNotifyRiskRuleCondition.UpdateDate = DateTime.UtcNow;

                                                    this.questionnaireNotifyRiskRuleConditionRepository.UpdateQuestionnaireNotifyRiskRuleCondition(questionnaireNotifyRiskRuleCondition);
                                                }
                                                else
                                                {
                                                    data.UpdateUserID = scheduleParameter.UpdatedUserID;
                                                    this.questionnaireNotifyRiskRuleConditionRepository.AddQuestionnaireNotifyRiskRuleCondition(data);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            foreach (var conditionList in questionnaireNotifyRiskRuleConditionList)
                                            {
                                                conditionList.IsRemoved = true;
                                                conditionList.UpdateDate = DateTime.UtcNow;
                                                conditionList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                                this.questionnaireNotifyRiskRuleConditionRepository.UpdateQuestionnaireNotifyRiskRuleCondition(conditionList);
                                            }

                                        }
                                        this.questionnaireNotifyRiskRuleRespository.UpdateQuestionnaireNotifyRiskRule(questionnaireNotifyRiskRule);
                                    }
                                    else
                                    {
                                        item.UpdateUserID = scheduleParameter.UpdatedUserID;
                                        var questionnaireNotifyRiskRuleID = this.questionnaireNotifyRiskRuleRespository.AddQuestionnaireNotifyRiskRule(item);
                                        foreach (QuestionnaireNotifyRiskRuleConditionDTO data in item.QuestionnaireNotifyRiskRuleConditionDTO)
                                        {
                                            data.QuestionnaireNotifyRiskRuleID = questionnaireNotifyRiskRuleID;
                                            data.UpdateUserID = scheduleParameter.UpdatedUserID;
                                            this.questionnaireNotifyRiskRuleConditionRepository.AddQuestionnaireNotifyRiskRuleCondition(data);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var RiskRuleList in questionnaireNotifyRiskRuleList)
                                {
                                    RiskRuleList.IsRemoved = true;
                                    RiskRuleList.UpdateDate = DateTime.UtcNow;
                                    RiskRuleList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    this.questionnaireNotifyRiskRuleRespository.UpdateQuestionnaireNotifyRiskRule(RiskRuleList);
                                }

                            }
                            this.questionnaireNotifyRiskScheduleRepository.UpdateQuestionnaireNotifyRiskSchedule(questionnaireNotifyRiskSchedule);
                        }
                        else
                        {
                            if (currentQuestionnaireNotifyRiskSchedule != null)
                            {
                                currentQuestionnaireNotifyRiskSchedule.IsRemoved = true;
                                currentQuestionnaireNotifyRiskSchedule.UpdateDate = DateTime.UtcNow;
                                currentQuestionnaireNotifyRiskSchedule.UpdateUserID = scheduleParameter.UpdatedUserID;
                                this.questionnaireNotifyRiskScheduleRepository.UpdateQuestionnaireNotifyRiskSchedule(currentQuestionnaireNotifyRiskSchedule);

                                var removableRiskRuleList = this.questionnaireNotifyRiskRuleRespository.
                                       GetQuestionnaireNotifyRiskRuleByScheduleID(currentQuestionnaireNotifyRiskSchedule.QuestionnaireNotifyRiskScheduleID);
                                foreach (var RiskRuleList in removableRiskRuleList)
                                {
                                    RiskRuleList.IsRemoved = true;
                                    RiskRuleList.UpdateDate = DateTime.UtcNow;
                                    RiskRuleList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    this.questionnaireNotifyRiskRuleRespository.UpdateQuestionnaireNotifyRiskRule(RiskRuleList);

                                    var removableRuleConditionList = this.questionnaireNotifyRiskRuleConditionRepository.GetQuestionnaireNotifyRiskRuleConditionByRuleID(RiskRuleList.QuestionnaireNotifyRiskRuleID);

                                    foreach (var conditionList in removableRuleConditionList)
                                    {
                                        conditionList.IsRemoved = true;
                                        conditionList.UpdateDate = DateTime.UtcNow;
                                        conditionList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                        this.questionnaireNotifyRiskRuleConditionRepository.UpdateQuestionnaireNotifyRiskRuleCondition(conditionList);
                                    }
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(QuestionnaireNotifyRiskScheduleDTO.Name))
                            {
                                QuestionnaireNotifyRiskScheduleDTO questionnaireNotifyRiskSchedule = new QuestionnaireNotifyRiskScheduleDTO();

                                questionnaireNotifyRiskSchedule.Name = QuestionnaireNotifyRiskScheduleDTO.Name;
                                questionnaireNotifyRiskSchedule.UpdateUserID = scheduleParameter.UpdatedUserID;
                                questionnaireNotifyRiskSchedule.QuestionnaireID = scheduleParameter.QuestionnaireID;
                                var QuestionnaireRiskScheduleID = this.questionnaireNotifyRiskScheduleRepository.AddQuestionnaireNotifyRiskSchedule(questionnaireNotifyRiskSchedule);

                                foreach (QuestionnaireNotifyRiskRuleDTO item in QuestionnaireNotifyRiskScheduleDTO.QuestionnaireNotifyRiskRuleDTO)
                                {
                                    item.QuestionnaireNotifyRiskScheduleID = Convert.ToInt32(QuestionnaireRiskScheduleID);
                                    item.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    var questionnaireNotifyRiskRuleID = this.questionnaireNotifyRiskRuleRespository.AddQuestionnaireNotifyRiskRule(item);
                                    foreach (QuestionnaireNotifyRiskRuleConditionDTO data in item.QuestionnaireNotifyRiskRuleConditionDTO)
                                    {
                                        data.QuestionnaireNotifyRiskRuleID = questionnaireNotifyRiskRuleID;
                                        data.UpdateUserID = scheduleParameter.UpdatedUserID;
                                        this.questionnaireNotifyRiskRuleConditionRepository.AddQuestionnaireNotifyRiskRuleCondition(data);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (currentQuestionnaireNotifyRiskSchedule != null)
                        {
                            currentQuestionnaireNotifyRiskSchedule.IsRemoved = true;
                            currentQuestionnaireNotifyRiskSchedule.UpdateDate = DateTime.UtcNow;
                            currentQuestionnaireNotifyRiskSchedule.UpdateUserID = scheduleParameter.UpdatedUserID;
                            this.questionnaireNotifyRiskScheduleRepository.UpdateQuestionnaireNotifyRiskSchedule(currentQuestionnaireNotifyRiskSchedule);

                            var removableRiskRuleList = this.questionnaireNotifyRiskRuleRespository.
                                   GetQuestionnaireNotifyRiskRuleByScheduleID(currentQuestionnaireNotifyRiskSchedule.QuestionnaireNotifyRiskScheduleID);
                            foreach (var RiskRuleList in removableRiskRuleList)
                            {
                                RiskRuleList.IsRemoved = true;
                                RiskRuleList.UpdateDate = DateTime.UtcNow;
                                RiskRuleList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                this.questionnaireNotifyRiskRuleRespository.UpdateQuestionnaireNotifyRiskRule(RiskRuleList);

                                var removableRuleConditionList = this.questionnaireNotifyRiskRuleConditionRepository.GetQuestionnaireNotifyRiskRuleConditionByRuleID(RiskRuleList.QuestionnaireNotifyRiskRuleID);

                                foreach (var conditionList in removableRuleConditionList)
                                {
                                    conditionList.IsRemoved = true;
                                    conditionList.UpdateDate = DateTime.UtcNow;
                                    conditionList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    this.questionnaireNotifyRiskRuleConditionRepository.UpdateQuestionnaireNotifyRiskRuleCondition(conditionList);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateDefaultRespose.
        /// </summary>
        /// <param name="scheduleParameter">scheduleParameter</param>
        /// <returns>bool</returns>
        public bool UpdateDefaultRespose(ScheduleParameterDTO scheduleParameter)
        {
            try
            {
                if (scheduleParameter != null)
                {
                    var questionnaireDefaultResponseRuleList = this.questionnaireDefaultResponseRuleRepository.GetQuestionnaireDefaultResponseRule(scheduleParameter.QuestionnaireID);
                    if (scheduleParameter.QuestionnaireDefaultResponseRules?.Count > 0)
                    {
                        var removableDefaultResponseRuleList = questionnaireDefaultResponseRuleList.Where(x => !scheduleParameter.QuestionnaireDefaultResponseRules.Select(y => y.QuestionnaireDefaultResponseRuleID).ToList().Contains(x.QuestionnaireDefaultResponseRuleID)).ToList();

                        foreach (var DefaultResponseList in removableDefaultResponseRuleList)
                        {
                            DefaultResponseList.IsRemoved = true;
                            DefaultResponseList.UpdateDate = DateTime.UtcNow;
                            DefaultResponseList.UpdateUserID = scheduleParameter.UpdatedUserID;
                        }
                        List<QuestionnaireDefaultResponseRule> DefaultResponseRules = new List<QuestionnaireDefaultResponseRule>();
                        this.mapper.Map<List<QuestionnaireDefaultResponseRuleDTO>, List<QuestionnaireDefaultResponseRule>>(removableDefaultResponseRuleList, DefaultResponseRules);

                        this.questionnaireDefaultResponseRuleRepository.UpdateBulkQuestionnaireDefaultResponseRule(DefaultResponseRules);

                        foreach (QuestionnaireDefaultResponseRuleDTO item in scheduleParameter.QuestionnaireDefaultResponseRules)
                        {
                            if (item.QuestionnaireDefaultResponseRuleID > 0)
                            {
                                var questionnaireDefaultResponseRule = questionnaireDefaultResponseRuleList.Where(x => x.QuestionnaireDefaultResponseRuleID == item.QuestionnaireDefaultResponseRuleID).FirstOrDefault();
                                questionnaireDefaultResponseRule.Name = item.Name;
                                questionnaireDefaultResponseRule.UpdateDate = DateTime.UtcNow;
                                questionnaireDefaultResponseRule.UpdateUserID = scheduleParameter.UpdatedUserID;

                                //Condition Section
                                var questionnaireDefaultResponseRuleConditionList = this.questionnaireDefaultResponseRuleConditionRepository.GetQuestionnaireDefaultResponseCondition(item.QuestionnaireDefaultResponseRuleID);

                                if (item.QuestionnaireDefaultResponseRuleConditions?.Count > 0)
                                {
                                    var removableRuleConditionList = questionnaireDefaultResponseRuleConditionList.Where(x => !item.QuestionnaireDefaultResponseRuleConditions.Select(y => y.QuestionnaireDefaultResponseRuleConditionID)
                                                                      .ToList().Contains(x.QuestionnaireDefaultResponseRuleConditionID)).ToList();
                                    foreach (var conditionList in removableRuleConditionList)
                                    {
                                        conditionList.IsRemoved = true;
                                        conditionList.UpdateDate = DateTime.UtcNow;
                                        conditionList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    }
                                    List<QuestionnaireDefaultResponseRuleCondition> DefaultResponseRulesCondition = new List<QuestionnaireDefaultResponseRuleCondition>();
                                    this.mapper.Map<List<QuestionnaireDefaultResponseRuleConditionDTO>, List<QuestionnaireDefaultResponseRuleCondition>>(removableRuleConditionList, DefaultResponseRulesCondition);

                                    this.questionnaireDefaultResponseRuleConditionRepository.UpdateQuestionnaireDefaultResponseRuleCondition(DefaultResponseRulesCondition);
                                    List<QuestionnaireDefaultResponseRuleConditionDTO> conditionsToUpdate = new List<QuestionnaireDefaultResponseRuleConditionDTO>();
                                    List<QuestionnaireDefaultResponseRuleConditionDTO> conditionsToAdd = new List<QuestionnaireDefaultResponseRuleConditionDTO>();

                                    foreach (QuestionnaireDefaultResponseRuleConditionDTO data in item.QuestionnaireDefaultResponseRuleConditions)
                                    {
                                        if (data.QuestionnaireDefaultResponseRuleConditionID > 0)
                                        {
                                            var questionnaireDefaultResponseRuleCondition = questionnaireDefaultResponseRuleConditionList.Where(x => x.QuestionnaireDefaultResponseRuleConditionID == data.QuestionnaireDefaultResponseRuleConditionID).FirstOrDefault();

                                            questionnaireDefaultResponseRuleCondition.QuestionnaireItemID = data.QuestionnaireItemID;
                                            questionnaireDefaultResponseRuleCondition.QuestionnaireID = data.QuestionnaireID;
                                            questionnaireDefaultResponseRuleCondition.ComparisonOperator = data.ComparisonOperator;
                                            questionnaireDefaultResponseRuleCondition.ComparisonValue = data.ComparisonValue;
                                            questionnaireDefaultResponseRuleCondition.JoiningOperator = data.JoiningOperator;
                                            questionnaireDefaultResponseRuleCondition.UpdateUserID = scheduleParameter.UpdatedUserID;
                                            questionnaireDefaultResponseRuleCondition.ListOrder = data.ListOrder;
                                            questionnaireDefaultResponseRuleCondition.UpdateDate = DateTime.UtcNow;
                                            conditionsToUpdate.Add(questionnaireDefaultResponseRuleCondition);
                                        }
                                        else
                                        {
                                            data.UpdateUserID = scheduleParameter.UpdatedUserID;
                                            data.UpdateDate = DateTime.UtcNow;
                                            data.QuestionnaireDefaultResponseRuleID = data.QuestionnaireDefaultResponseRuleID == 0 ? item.QuestionnaireDefaultResponseRuleID : data.QuestionnaireDefaultResponseRuleID;
                                            conditionsToAdd.Add(data);
                                        }
                                    }
                                    List<QuestionnaireDefaultResponseRuleCondition> DefaultResponseRulesConditionToUpdate = new List<QuestionnaireDefaultResponseRuleCondition>();
                                    this.mapper.Map<List<QuestionnaireDefaultResponseRuleConditionDTO>, List<QuestionnaireDefaultResponseRuleCondition>>(conditionsToUpdate, DefaultResponseRulesConditionToUpdate);

                                    List<QuestionnaireDefaultResponseRuleCondition> DefaultResponseRulesConditionToAdd = new List<QuestionnaireDefaultResponseRuleCondition>();
                                    this.mapper.Map<List<QuestionnaireDefaultResponseRuleConditionDTO>, List<QuestionnaireDefaultResponseRuleCondition>>(conditionsToAdd, DefaultResponseRulesConditionToAdd);


                                    this.questionnaireDefaultResponseRuleConditionRepository.UpdateQuestionnaireDefaultResponseRuleCondition(DefaultResponseRulesConditionToUpdate);
                                    this.questionnaireDefaultResponseRuleConditionRepository.AddBulkQuestionnaireDefaultResponseRuleCondition(DefaultResponseRulesConditionToAdd);

                                }
                                else
                                {
                                    foreach (var conditionList in questionnaireDefaultResponseRuleConditionList)
                                    {
                                        conditionList.IsRemoved = true;
                                        conditionList.UpdateDate = DateTime.UtcNow;
                                        conditionList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    }

                                    List<QuestionnaireDefaultResponseRuleCondition> DefaultResponseRulesConditionToUpdate = new List<QuestionnaireDefaultResponseRuleCondition>();
                                    this.mapper.Map<List<QuestionnaireDefaultResponseRuleConditionDTO>, List<QuestionnaireDefaultResponseRuleCondition>>(questionnaireDefaultResponseRuleConditionList, DefaultResponseRulesConditionToUpdate);


                                    this.questionnaireDefaultResponseRuleConditionRepository.UpdateQuestionnaireDefaultResponseRuleCondition(DefaultResponseRulesConditionToUpdate);
                                }

                                //Action Section
                                var questionnaireDefaultResponseRuleActionList = this.questionnaireDefaultResponseRuleActionRepository.GetQuestionnaireDefaultResponseAction(item.QuestionnaireDefaultResponseRuleID);

                                if (item.QuestionnaireDefaultResponseRuleActions?.Count > 0)
                                {
                                    var removableRuleActionList = questionnaireDefaultResponseRuleActionList.Where(x => !item.QuestionnaireDefaultResponseRuleActions.Select(y => y.QuestionnaireDefaultResponseRuleActionID)
                                                                      .ToList().Contains(x.QuestionnaireDefaultResponseRuleActionID)).ToList();
                                    foreach (var actionList in removableRuleActionList)
                                    {
                                        actionList.IsRemoved = true;
                                        actionList.UpdateDate = DateTime.UtcNow;
                                        actionList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    }
                                    List<QuestionnaireDefaultResponseRuleAction> DefaultResponseRulesAction = new List<QuestionnaireDefaultResponseRuleAction>();
                                    this.mapper.Map<List<QuestionnaireDefaultResponseRuleActionDTO>, List<QuestionnaireDefaultResponseRuleAction>>(removableRuleActionList, DefaultResponseRulesAction);

                                    this.questionnaireDefaultResponseRuleActionRepository.UpdateQuestionnaireDefaultResponseRuleAction(DefaultResponseRulesAction);

                                    List<QuestionnaireDefaultResponseRuleActionDTO> actionsToUpdate = new List<QuestionnaireDefaultResponseRuleActionDTO>();
                                    List<QuestionnaireDefaultResponseRuleActionDTO> actionsToAdd = new List<QuestionnaireDefaultResponseRuleActionDTO>();

                                    foreach (QuestionnaireDefaultResponseRuleActionDTO data in item.QuestionnaireDefaultResponseRuleActions)
                                    {
                                        if (data.QuestionnaireDefaultResponseRuleActionID > 0)
                                        {
                                            var questionnaireDefaultResponseRuleCondition = questionnaireDefaultResponseRuleActionList.Where(x => x.QuestionnaireDefaultResponseRuleActionID == data.QuestionnaireDefaultResponseRuleActionID).FirstOrDefault();

                                            questionnaireDefaultResponseRuleCondition.QuestionnaireItemID = data.QuestionnaireItemID;
                                            questionnaireDefaultResponseRuleCondition.DefaultResponseID = data.DefaultResponseID;
                                            questionnaireDefaultResponseRuleCondition.UpdateUserID = scheduleParameter.UpdatedUserID;
                                            questionnaireDefaultResponseRuleCondition.ListOrder = data.ListOrder;
                                            questionnaireDefaultResponseRuleCondition.UpdateDate = DateTime.UtcNow;
                                            actionsToUpdate.Add(questionnaireDefaultResponseRuleCondition);
                                        }
                                        else
                                        {
                                            data.UpdateUserID = scheduleParameter.UpdatedUserID;
                                            data.UpdateDate = DateTime.UtcNow;
                                            data.QuestionnaireDefaultResponseRuleID = data.QuestionnaireDefaultResponseRuleID == 0 ? item.QuestionnaireDefaultResponseRuleID : data.QuestionnaireDefaultResponseRuleID;
                                            actionsToAdd.Add(data);
                                        }
                                    }
                                    List<QuestionnaireDefaultResponseRuleAction> DefaultResponseRulesActionToUpdate = new List<QuestionnaireDefaultResponseRuleAction>();
                                    this.mapper.Map<List<QuestionnaireDefaultResponseRuleActionDTO>, List<QuestionnaireDefaultResponseRuleAction>>(actionsToUpdate, DefaultResponseRulesActionToUpdate);

                                    List<QuestionnaireDefaultResponseRuleAction> DefaultResponseRulesActionsToAdd = new List<QuestionnaireDefaultResponseRuleAction>();
                                    this.mapper.Map<List<QuestionnaireDefaultResponseRuleActionDTO>, List<QuestionnaireDefaultResponseRuleAction>>(actionsToAdd, DefaultResponseRulesActionsToAdd);


                                    this.questionnaireDefaultResponseRuleActionRepository.UpdateQuestionnaireDefaultResponseRuleAction(DefaultResponseRulesActionToUpdate);
                                    this.questionnaireDefaultResponseRuleActionRepository.AddBulkQuestionnaireDefaultResponseRuleAction(DefaultResponseRulesActionsToAdd);

                                }
                                else
                                {
                                    foreach (var actionList in questionnaireDefaultResponseRuleActionList)
                                    {
                                        actionList.IsRemoved = true;
                                        actionList.UpdateDate = DateTime.UtcNow;
                                        actionList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    }

                                    List<QuestionnaireDefaultResponseRuleAction> DefaultResponseRulesActionsToUpdate = new List<QuestionnaireDefaultResponseRuleAction>();
                                    this.mapper.Map<List<QuestionnaireDefaultResponseRuleActionDTO>, List<QuestionnaireDefaultResponseRuleAction>>(questionnaireDefaultResponseRuleActionList, DefaultResponseRulesActionsToUpdate);


                                    this.questionnaireDefaultResponseRuleActionRepository.UpdateQuestionnaireDefaultResponseRuleAction(DefaultResponseRulesActionsToUpdate);
                                }

                                QuestionnaireDefaultResponseRule DefaultResponseRulesToUpdate = new QuestionnaireDefaultResponseRule();
                                this.mapper.Map<QuestionnaireDefaultResponseRuleDTO, QuestionnaireDefaultResponseRule>(questionnaireDefaultResponseRule, DefaultResponseRulesToUpdate);

                                this.questionnaireDefaultResponseRuleRepository.UpdateQuestionnaireDefaultResponseRule(DefaultResponseRulesToUpdate);
                            }
                            else
                            {
                                item.UpdateUserID = scheduleParameter.UpdatedUserID;

                                QuestionnaireDefaultResponseRule DefaultResponseRulesToAdd = new QuestionnaireDefaultResponseRule();
                                this.mapper.Map<QuestionnaireDefaultResponseRuleDTO, QuestionnaireDefaultResponseRule>(item, DefaultResponseRulesToAdd);
                                var DefaultResponseRule = this.questionnaireDefaultResponseRuleRepository.AddQuestionnaireDefaultResponseRule(DefaultResponseRulesToAdd);

                                //Condition Section
                                List<QuestionnaireDefaultResponseRuleConditionDTO> conditionsToAdd = new List<QuestionnaireDefaultResponseRuleConditionDTO>();
                                foreach (QuestionnaireDefaultResponseRuleConditionDTO data in item.QuestionnaireDefaultResponseRuleConditions)
                                {
                                    data.QuestionnaireDefaultResponseRuleID = DefaultResponseRule.QuestionnaireDefaultResponseRuleID;
                                    data.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    conditionsToAdd.Add(data);
                                }
                                List<QuestionnaireDefaultResponseRuleCondition> DefaultResponseRulesConditionToAdd = new List<QuestionnaireDefaultResponseRuleCondition>();
                                this.mapper.Map<List<QuestionnaireDefaultResponseRuleConditionDTO>, List<QuestionnaireDefaultResponseRuleCondition>>(conditionsToAdd, DefaultResponseRulesConditionToAdd);

                                this.questionnaireDefaultResponseRuleConditionRepository.AddBulkQuestionnaireDefaultResponseRuleCondition(DefaultResponseRulesConditionToAdd);

                                //Action Section
                                List<QuestionnaireDefaultResponseRuleActionDTO> actionsToAdd = new List<QuestionnaireDefaultResponseRuleActionDTO>();
                                foreach (QuestionnaireDefaultResponseRuleActionDTO data in item.QuestionnaireDefaultResponseRuleActions)
                                {
                                    data.QuestionnaireDefaultResponseRuleID = DefaultResponseRule.QuestionnaireDefaultResponseRuleID;
                                    data.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    actionsToAdd.Add(data);
                                }
                                List<QuestionnaireDefaultResponseRuleAction> DefaultResponseRulesActionsToAdd = new List<QuestionnaireDefaultResponseRuleAction>();
                                this.mapper.Map<List<QuestionnaireDefaultResponseRuleActionDTO>, List<QuestionnaireDefaultResponseRuleAction>>(actionsToAdd, DefaultResponseRulesActionsToAdd);

                                this.questionnaireDefaultResponseRuleActionRepository.AddBulkQuestionnaireDefaultResponseRuleAction(DefaultResponseRulesActionsToAdd);

                            }
                        }
                    }
                    else
                    {
                        foreach (var RiskRuleList in questionnaireDefaultResponseRuleList)
                        {
                            RiskRuleList.IsRemoved = true;
                            RiskRuleList.UpdateDate = DateTime.UtcNow;
                            RiskRuleList.UpdateUserID = scheduleParameter.UpdatedUserID;
                        }
                        List<QuestionnaireDefaultResponseRule> DefaultResponseRules = new List<QuestionnaireDefaultResponseRule>();
                        this.mapper.Map<List<QuestionnaireDefaultResponseRuleDTO>, List<QuestionnaireDefaultResponseRule>>(questionnaireDefaultResponseRuleList, DefaultResponseRules);

                        this.questionnaireDefaultResponseRuleRepository.UpdateBulkQuestionnaireDefaultResponseRule(DefaultResponseRules);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// UpdateNotification.
        /// </summary>
        /// <param name="QuestionnaireReminderRuleList">QuestionnaireReminderRuleDTO</param>
        /// <returns>bool</returns>
        public bool UpdateSkipLogic(ScheduleParameterDTO scheduleParameter)
        {
            try
            {
                if (scheduleParameter != null)
                {
                    var questionnaireSkipLogicRuleList = this.questionnaireSkipLogicRuleRepository.GetQuestionnaireSkipLogicRule(scheduleParameter.QuestionnaireID);
                    if (scheduleParameter.QuestionnaireSkipLogicRules?.Count > 0)
                    {
                        var removableSkipLogicRuleList = questionnaireSkipLogicRuleList.Where(x => !scheduleParameter.QuestionnaireSkipLogicRules.Select(y => y.QuestionnaireSkipLogicRuleID).ToList().Contains(x.QuestionnaireSkipLogicRuleID)).ToList();

                        foreach (var SkipLogicList in removableSkipLogicRuleList)
                        {
                            SkipLogicList.IsRemoved = true;
                            SkipLogicList.UpdateDate = DateTime.UtcNow;
                            SkipLogicList.UpdateUserID = scheduleParameter.UpdatedUserID;
                        }
                        if (removableSkipLogicRuleList.Count > 0)
                        {
                            List<QuestionnaireSkipLogicRule> SkipLogicRules = new List<QuestionnaireSkipLogicRule>();
                            this.mapper.Map<List<QuestionnaireSkipLogicRuleDTO>, List<QuestionnaireSkipLogicRule>>(removableSkipLogicRuleList, SkipLogicRules);

                            this.questionnaireSkipLogicRuleRepository.UpdateBulkQuestionnaireSkipLogicRule(SkipLogicRules);
                        }
                        foreach (QuestionnaireSkipLogicRuleDTO item in scheduleParameter.QuestionnaireSkipLogicRules)
                        {
                            if (item.QuestionnaireSkipLogicRuleID > 0)
                            {
                                var questionnaireSkipLogicRule = questionnaireSkipLogicRuleList.Where(x => x.QuestionnaireSkipLogicRuleID == item.QuestionnaireSkipLogicRuleID).FirstOrDefault();
                                questionnaireSkipLogicRule.Name = item.Name;
                                questionnaireSkipLogicRule.UpdateDate = DateTime.UtcNow;
                                questionnaireSkipLogicRule.UpdateUserID = scheduleParameter.UpdatedUserID;

                                //Condition Section
                                var questionnaireSkipLogicRuleConditionList = this.questionnaireSkipLogicRuleConditionRepository.GetQuestionnaireSkipLogicCondition(item.QuestionnaireSkipLogicRuleID);

                                if (item.QuestionnaireSkipLogicRuleConditions?.Count > 0)
                                {
                                    var removableRuleConditionList = questionnaireSkipLogicRuleConditionList.Where(x => !item.QuestionnaireSkipLogicRuleConditions.Select(y => y.QuestionnaireSkipLogicRuleConditionID)
                                                                      .ToList().Contains(x.QuestionnaireSkipLogicRuleConditionID)).ToList();
                                    foreach (var conditionList in removableRuleConditionList)
                                    {
                                        conditionList.IsRemoved = true;
                                        conditionList.UpdateDate = DateTime.UtcNow;
                                        conditionList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    }
                                    if (removableRuleConditionList.Count > 0)
                                    {
                                        List<QuestionnaireSkipLogicRuleCondition> SkipLogicRulesCondition = new List<QuestionnaireSkipLogicRuleCondition>();
                                        this.mapper.Map<List<QuestionnaireSkipLogicRuleConditionDTO>, List<QuestionnaireSkipLogicRuleCondition>>(removableRuleConditionList, SkipLogicRulesCondition);

                                        this.questionnaireSkipLogicRuleConditionRepository.UpdateQuestionnaireSkipLogicRuleCondition(SkipLogicRulesCondition);
                                    }
                                    List<QuestionnaireSkipLogicRuleConditionDTO> conditionsToUpdate = new List<QuestionnaireSkipLogicRuleConditionDTO>();
                                    List<QuestionnaireSkipLogicRuleConditionDTO> conditionsToAdd = new List<QuestionnaireSkipLogicRuleConditionDTO>();

                                    foreach (QuestionnaireSkipLogicRuleConditionDTO data in item.QuestionnaireSkipLogicRuleConditions)
                                    {
                                        if (data.QuestionnaireSkipLogicRuleConditionID > 0)
                                        {
                                            var questionnaireSkipLogicRuleCondition = questionnaireSkipLogicRuleConditionList.Where(x => x.QuestionnaireSkipLogicRuleConditionID == data.QuestionnaireSkipLogicRuleConditionID).FirstOrDefault();

                                            questionnaireSkipLogicRuleCondition.QuestionnaireItemID = data.QuestionnaireItemID;
                                            questionnaireSkipLogicRuleCondition.ComparisonOperator = data.ComparisonOperator;
                                            questionnaireSkipLogicRuleCondition.ComparisonValue = data.ComparisonValue;
                                            questionnaireSkipLogicRuleCondition.JoiningOperator = data.JoiningOperator;
                                            questionnaireSkipLogicRuleCondition.UpdateUserID = scheduleParameter.UpdatedUserID;
                                            questionnaireSkipLogicRuleCondition.ListOrder = data.ListOrder;
                                            questionnaireSkipLogicRuleCondition.UpdateDate = DateTime.UtcNow;
                                            conditionsToUpdate.Add(questionnaireSkipLogicRuleCondition);
                                        }
                                        else
                                        {
                                            data.UpdateUserID = scheduleParameter.UpdatedUserID;
                                            data.UpdateDate = DateTime.UtcNow;
                                            data.QuestionnaireSkipLogicRuleID = data.QuestionnaireSkipLogicRuleID == 0 ? item.QuestionnaireSkipLogicRuleID : data.QuestionnaireSkipLogicRuleID;
                                            conditionsToAdd.Add(data);
                                        }
                                    }
                                    List<QuestionnaireSkipLogicRuleCondition> SkipLogicRulesConditionToUpdate = new List<QuestionnaireSkipLogicRuleCondition>();
                                    this.mapper.Map<List<QuestionnaireSkipLogicRuleConditionDTO>, List<QuestionnaireSkipLogicRuleCondition>>(conditionsToUpdate, SkipLogicRulesConditionToUpdate);

                                    List<QuestionnaireSkipLogicRuleCondition> SkipLogicRulesConditionToAdd = new List<QuestionnaireSkipLogicRuleCondition>();
                                    this.mapper.Map<List<QuestionnaireSkipLogicRuleConditionDTO>, List<QuestionnaireSkipLogicRuleCondition>>(conditionsToAdd, SkipLogicRulesConditionToAdd);


                                    this.questionnaireSkipLogicRuleConditionRepository.UpdateQuestionnaireSkipLogicRuleCondition(SkipLogicRulesConditionToUpdate);
                                    this.questionnaireSkipLogicRuleConditionRepository.AddBulkQuestionnaireSkipLogicRuleCondition(SkipLogicRulesConditionToAdd);

                                }
                                else
                                {
                                    foreach (var conditionList in questionnaireSkipLogicRuleConditionList)
                                    {
                                        conditionList.IsRemoved = true;
                                        conditionList.UpdateDate = DateTime.UtcNow;
                                        conditionList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    }

                                    List<QuestionnaireSkipLogicRuleCondition> SkipLogicRulesConditionToUpdate = new List<QuestionnaireSkipLogicRuleCondition>();
                                    this.mapper.Map<List<QuestionnaireSkipLogicRuleConditionDTO>, List<QuestionnaireSkipLogicRuleCondition>>(questionnaireSkipLogicRuleConditionList, SkipLogicRulesConditionToUpdate);


                                    this.questionnaireSkipLogicRuleConditionRepository.UpdateQuestionnaireSkipLogicRuleCondition(SkipLogicRulesConditionToUpdate);
                                }

                                //Action Section
                                var questionnaireSkipLogicRuleActionList = this.questionnaireSkipLogicRuleActionRepository.GetQuestionnaireSkipLogicAction(item.QuestionnaireSkipLogicRuleID);

                                if (item.QuestionnaireSkipLogicRuleActions?.Count > 0)
                                {
                                    var removableRuleActionList = questionnaireSkipLogicRuleActionList.Where(x => !item.QuestionnaireSkipLogicRuleActions.Select(y => y.QuestionnaireSkipLogicRuleActionID)
                                                                      .ToList().Contains(x.QuestionnaireSkipLogicRuleActionID)).ToList();
                                    foreach (var actionList in removableRuleActionList)
                                    {
                                        actionList.IsRemoved = true;
                                        actionList.UpdateDate = DateTime.UtcNow;
                                        actionList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    }
                                    List<QuestionnaireSkipLogicRuleAction> SkipLogicRulesAction = new List<QuestionnaireSkipLogicRuleAction>();
                                    this.mapper.Map<List<QuestionnaireSkipLogicRuleActionDTO>, List<QuestionnaireSkipLogicRuleAction>>(removableRuleActionList, SkipLogicRulesAction);

                                    this.questionnaireSkipLogicRuleActionRepository.UpdateQuestionnaireSkipLogicRuleAction(SkipLogicRulesAction);

                                    List<QuestionnaireSkipLogicRuleActionDTO> actionsToUpdate = new List<QuestionnaireSkipLogicRuleActionDTO>();
                                    List<QuestionnaireSkipLogicRuleActionDTO> actionsToAdd = new List<QuestionnaireSkipLogicRuleActionDTO>();

                                    foreach (QuestionnaireSkipLogicRuleActionDTO data in item.QuestionnaireSkipLogicRuleActions)
                                    {
                                        if (data.QuestionnaireSkipLogicRuleActionID > 0)
                                        {
                                            var questionnaireSkipLogicRuleCondition = questionnaireSkipLogicRuleActionList.Where(x => x.QuestionnaireSkipLogicRuleActionID == data.QuestionnaireSkipLogicRuleActionID).FirstOrDefault();

                                            questionnaireSkipLogicRuleCondition.QuestionnaireItemID = data.QuestionnaireItemID;
                                            questionnaireSkipLogicRuleCondition.ActionLevelID = data.ActionLevelID;
                                            questionnaireSkipLogicRuleCondition.ActionTypeID = data.ActionTypeID;
                                            questionnaireSkipLogicRuleCondition.CategoryID = data.CategoryID;
                                            questionnaireSkipLogicRuleCondition.ChildItemID = data.ChildItemID;
                                            questionnaireSkipLogicRuleCondition.ParentItemID = data.ParentItemID;
                                            questionnaireSkipLogicRuleCondition.UpdateUserID = scheduleParameter.UpdatedUserID;
                                            questionnaireSkipLogicRuleCondition.ListOrder = data.ListOrder;
                                            questionnaireSkipLogicRuleCondition.UpdateDate = DateTime.UtcNow;
                                            actionsToUpdate.Add(questionnaireSkipLogicRuleCondition);
                                        }
                                        else
                                        {
                                            data.UpdateUserID = scheduleParameter.UpdatedUserID;
                                            data.UpdateDate = DateTime.UtcNow;
                                            data.QuestionnaireSkipLogicRuleID = data.QuestionnaireSkipLogicRuleID == 0 ? item.QuestionnaireSkipLogicRuleID : data.QuestionnaireSkipLogicRuleID;
                                            actionsToAdd.Add(data);
                                        }
                                    }
                                    List<QuestionnaireSkipLogicRuleAction> SkipLogicRulesActionToUpdate = new List<QuestionnaireSkipLogicRuleAction>();
                                    this.mapper.Map<List<QuestionnaireSkipLogicRuleActionDTO>, List<QuestionnaireSkipLogicRuleAction>>(actionsToUpdate, SkipLogicRulesActionToUpdate);

                                    List<QuestionnaireSkipLogicRuleAction> SkipLogicRulesActionsToAdd = new List<QuestionnaireSkipLogicRuleAction>();
                                    this.mapper.Map<List<QuestionnaireSkipLogicRuleActionDTO>, List<QuestionnaireSkipLogicRuleAction>>(actionsToAdd, SkipLogicRulesActionsToAdd);


                                    this.questionnaireSkipLogicRuleActionRepository.UpdateQuestionnaireSkipLogicRuleAction(SkipLogicRulesActionToUpdate);
                                    this.questionnaireSkipLogicRuleActionRepository.AddBulkQuestionnaireSkipLogicRuleAction(SkipLogicRulesActionsToAdd);

                                }
                                else
                                {
                                    foreach (var actionList in questionnaireSkipLogicRuleActionList)
                                    {
                                        actionList.IsRemoved = true;
                                        actionList.UpdateDate = DateTime.UtcNow;
                                        actionList.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    }

                                    List<QuestionnaireSkipLogicRuleAction> SkipLogicRulesActionsToUpdate = new List<QuestionnaireSkipLogicRuleAction>();
                                    this.mapper.Map<List<QuestionnaireSkipLogicRuleActionDTO>, List<QuestionnaireSkipLogicRuleAction>>(questionnaireSkipLogicRuleActionList, SkipLogicRulesActionsToUpdate);


                                    this.questionnaireSkipLogicRuleActionRepository.UpdateQuestionnaireSkipLogicRuleAction(SkipLogicRulesActionsToUpdate);
                                }

                                QuestionnaireSkipLogicRule SkipLogicRulesToUpdate = new QuestionnaireSkipLogicRule();
                                this.mapper.Map<QuestionnaireSkipLogicRuleDTO, QuestionnaireSkipLogicRule>(questionnaireSkipLogicRule, SkipLogicRulesToUpdate);

                                this.questionnaireSkipLogicRuleRepository.UpdateQuestionnaireSkipLogicRule(SkipLogicRulesToUpdate);
                            }
                            else
                            {
                                item.UpdateUserID = scheduleParameter.UpdatedUserID;

                                QuestionnaireSkipLogicRule SkipLogicRulesToAdd = new QuestionnaireSkipLogicRule();
                                this.mapper.Map<QuestionnaireSkipLogicRuleDTO, QuestionnaireSkipLogicRule>(item, SkipLogicRulesToAdd);
                                var skipLogicRule = this.questionnaireSkipLogicRuleRepository.AddQuestionnaireSkipLogicRule(SkipLogicRulesToAdd);

                                //Condition Section
                                List<QuestionnaireSkipLogicRuleConditionDTO> conditionsToAdd = new List<QuestionnaireSkipLogicRuleConditionDTO>();
                                foreach (QuestionnaireSkipLogicRuleConditionDTO data in item.QuestionnaireSkipLogicRuleConditions)
                                {
                                    data.QuestionnaireSkipLogicRuleID = skipLogicRule.QuestionnaireSkipLogicRuleID;
                                    data.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    conditionsToAdd.Add(data);
                                }
                                List<QuestionnaireSkipLogicRuleCondition> SkipLogicRulesConditionToAdd = new List<QuestionnaireSkipLogicRuleCondition>();
                                this.mapper.Map<List<QuestionnaireSkipLogicRuleConditionDTO>, List<QuestionnaireSkipLogicRuleCondition>>(conditionsToAdd, SkipLogicRulesConditionToAdd);

                                this.questionnaireSkipLogicRuleConditionRepository.AddBulkQuestionnaireSkipLogicRuleCondition(SkipLogicRulesConditionToAdd);

                                //Action Section
                                List<QuestionnaireSkipLogicRuleActionDTO> actionsToAdd = new List<QuestionnaireSkipLogicRuleActionDTO>();
                                foreach (QuestionnaireSkipLogicRuleActionDTO data in item.QuestionnaireSkipLogicRuleActions)
                                {
                                    data.QuestionnaireSkipLogicRuleID = skipLogicRule.QuestionnaireSkipLogicRuleID;
                                    data.UpdateUserID = scheduleParameter.UpdatedUserID;
                                    actionsToAdd.Add(data);
                                }
                                List<QuestionnaireSkipLogicRuleAction> SkipLogicRulesActionsToAdd = new List<QuestionnaireSkipLogicRuleAction>();
                                this.mapper.Map<List<QuestionnaireSkipLogicRuleActionDTO>, List<QuestionnaireSkipLogicRuleAction>>(actionsToAdd, SkipLogicRulesActionsToAdd);

                                this.questionnaireSkipLogicRuleActionRepository.AddBulkQuestionnaireSkipLogicRuleAction(SkipLogicRulesActionsToAdd);

                            }
                        }
                    }
                    else
                    {
                        foreach (var RiskRuleList in questionnaireSkipLogicRuleList)
                        {
                            RiskRuleList.IsRemoved = true;
                            RiskRuleList.UpdateDate = DateTime.UtcNow;
                            RiskRuleList.UpdateUserID = scheduleParameter.UpdatedUserID;
                        }
                        List<QuestionnaireSkipLogicRule> SkipLogicRules = new List<QuestionnaireSkipLogicRule>();
                        this.mapper.Map<List<QuestionnaireSkipLogicRuleDTO>, List<QuestionnaireSkipLogicRule>>(questionnaireSkipLogicRuleList, SkipLogicRules);

                        this.questionnaireSkipLogicRuleRepository.UpdateBulkQuestionnaireSkipLogicRule(SkipLogicRules);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateQuestionnaireReminderRule.
        /// </summary>
        /// <param name="QuestionnaireReminderRuleList">QuestionnaireReminderRuleDTO</param>
        /// <returns>bool</returns>
        public bool UpdateQuestionnaireReminderRule(ScheduleParameterDTO ScheduleParameterDTO, ref bool questionnaireAddedToQueue)
        {
            try
            {
                List<QuestionnaireReminderRuleDTO> lst_Add = new List<QuestionnaireReminderRuleDTO>(); 
                List<QuestionnaireReminderRule> lst_Edit = new List<QuestionnaireReminderRule>();
                if (ScheduleParameterDTO != null && ScheduleParameterDTO.QuestionnaireReminderRule != null && ScheduleParameterDTO.QuestionnaireReminderRule.Count > 0)
                {
                    var questionnaireReminderRuleList = this.questionnaireReminderRuleRespository.GetAllQuestionnaireReminderRules(ScheduleParameterDTO.QuestionnaireID);
                    bool addToQueue = false;
                    foreach (QuestionnaireReminderRuleDTO item in ScheduleParameterDTO.QuestionnaireReminderRule)
                    {
                        if (item.QuestionnaireReminderRuleID > 0)
                        {
                            var questionnaireWindow = questionnaireReminderRuleList.Where(x=> x.QuestionnaireReminderRuleID == item.QuestionnaireReminderRuleID).FirstOrDefault();
                            if (questionnaireWindow.ReminderOffsetDays != item.ReminderOffsetDays || questionnaireWindow.CanRepeat != item.CanRepeat || questionnaireWindow.RepeatInterval != item.RepeatInterval && questionnaireWindow.IsSelected != item.IsSelected ||
                            questionnaireWindow.ReminderOffsetTypeID != item.ReminderOffsetTypeID)
                            {
                                addToQueue = true;
                            }
                            questionnaireWindow.ReminderOffsetDays = item.ReminderOffsetDays;
                            questionnaireWindow.ReminderOffsetTypeID = item.ReminderOffsetTypeID ?? PCISEnum.OffsetType.Day;
                            questionnaireWindow.CanRepeat = item.CanRepeat;
                            questionnaireWindow.RepeatInterval = item.RepeatInterval;
                            questionnaireWindow.IsSelected = item.IsSelected;
                            lst_Edit.Add(questionnaireWindow);
                        }
                        else
                        {
                            if (questionnaireReminderRuleList != null && questionnaireReminderRuleList.Count > 0)
                            {
                                var questionnaireReminderRule = questionnaireReminderRuleList.Where(x => x.QuestionnaireReminderTypeID == item.QuestionnaireReminderTypeID).FirstOrDefault();
                                if (questionnaireReminderRule != null)
                                {
                                    var questionnaireWindow = questionnaireReminderRuleList.Where(x=> x.QuestionnaireReminderRuleID == questionnaireReminderRule.QuestionnaireReminderRuleID).FirstOrDefault();
                                    if (questionnaireWindow.ReminderOffsetDays != item.ReminderOffsetDays || questionnaireWindow.CanRepeat != item.CanRepeat || questionnaireWindow.RepeatInterval != item.RepeatInterval && questionnaireWindow.IsSelected != item.IsSelected ||
                            questionnaireWindow.ReminderOffsetTypeID != item.ReminderOffsetTypeID)
                                    {
                                        addToQueue = true;
                                    }
                                    questionnaireWindow.ReminderOffsetDays = item.ReminderOffsetDays;
                                    questionnaireWindow.ReminderOffsetTypeID = item.ReminderOffsetTypeID ?? PCISEnum.OffsetType.Day;
                                    questionnaireWindow.CanRepeat = item.CanRepeat;
                                    questionnaireWindow.RepeatInterval = item.RepeatInterval;
                                    questionnaireWindow.IsSelected = item.IsSelected;
                                    lst_Edit.Add(questionnaireWindow);
                                }
                                else
                                {
                                    QuestionnaireReminderRuleDTO questionnaireWindow = new QuestionnaireReminderRuleDTO();
                                    questionnaireWindow.ReminderOffsetDays = item.ReminderOffsetDays;
                                    questionnaireWindow.ReminderOffsetTypeID = item.ReminderOffsetTypeID ?? PCISEnum.OffsetType.Day;
                                    questionnaireWindow.QuestionnaireID = ScheduleParameterDTO.QuestionnaireID;
                                    questionnaireWindow.QuestionnaireReminderTypeID = item.QuestionnaireReminderTypeID;
                                    questionnaireWindow.CanRepeat = item.CanRepeat;
                                    questionnaireWindow.RepeatInterval = item.RepeatInterval;
                                    questionnaireWindow.IsSelected = item.IsSelected;
                                    lst_Add.Add(questionnaireWindow);
                                }
                            }
                            else
                            {
                                QuestionnaireReminderRuleDTO questionnaireWindow = new QuestionnaireReminderRuleDTO();
                                questionnaireWindow.ReminderOffsetDays = item.ReminderOffsetDays;
                                questionnaireWindow.ReminderOffsetTypeID = item.ReminderOffsetTypeID ?? PCISEnum.OffsetType.Day;
                                questionnaireWindow.QuestionnaireID = ScheduleParameterDTO.QuestionnaireID;
                                questionnaireWindow.QuestionnaireReminderTypeID = item.QuestionnaireReminderTypeID;
                                questionnaireWindow.CanRepeat = item.CanRepeat;
                                questionnaireWindow.RepeatInterval = item.RepeatInterval;
                                questionnaireWindow.IsSelected = item.IsSelected;
                                lst_Add.Add(questionnaireWindow);
                            }
                        }
                    }
                    if (lst_Add.Count > 0)
                    {
                        addToQueue = true;
                        this.questionnaireReminderRuleRespository.AddBulkQuestionnaireReminderRule(lst_Add);
                    }
                    if (lst_Edit.Count > 0)
                    {
                        addToQueue = true;
                        this.questionnaireReminderRuleRespository.UpdateBulkQuestionnaireReminderRule(lst_Edit);
                    }
                    questionnaireAddedToQueue = addToQueue;
                }
                else
                {
                    questionnaireAddedToQueue = false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateQuestionnaireWindow.
        /// PCIS-3225 : OffsetType included in window and update recurrence settings for regularwindow.
        /// </summary>
        /// <param name="QuestionnaireWindowList"></param>
        /// <returns>bool</returns>
        public bool UpdateQuestionnaireWindow(ScheduleParameterDTO ScheduleParameterDTO, ref bool questionnaireAddedToQueue)
        {
            try
            {
                QuestionnaireRegularReminderRecurrenceDTO regularReminderRecurrence = null;
                if (ScheduleParameterDTO.QuestionnaireWindow != null && ScheduleParameterDTO.QuestionnaireWindow.Count > 0)
                {
                    bool addToQueue = false;
                    List<QuestionnaireWindow> questionnaireWindowslist = this.questionnaireWindowRepository.GetAllQuestionnaireWindows(ScheduleParameterDTO.QuestionnaireID);

                    foreach (QuestionnaireWindowDTO item in ScheduleParameterDTO.QuestionnaireWindow)
                    {
                        if (item.QuestionnaireWindowID > 0)
                        {
                            var questionnaireWindow = questionnaireWindowslist.Where(x=>x.QuestionnaireWindowID == item.QuestionnaireWindowID).FirstOrDefault();
                            if (questionnaireWindow.DueDateOffsetDays != item.DueDateOffsetDays || questionnaireWindow.CanRepeat != item.CanRepeat || questionnaireWindow.WindowCloseOffsetDays != item.WindowCloseOffsetDays ||
                                questionnaireWindow.WindowOpenOffsetDays != item.WindowOpenOffsetDays || questionnaireWindow.RepeatIntervalDays != item.RepeatIntervalDays || questionnaireWindow.IsSelected != item.IsSelected)
                            {
                                addToQueue = true;
                            }
                            questionnaireWindow.DueDateOffsetDays = item.DueDateOffsetDays;
                            questionnaireWindow.CanRepeat = item.CanRepeat;
                            questionnaireWindow.WindowCloseOffsetDays = item.WindowCloseOffsetDays;
                            questionnaireWindow.WindowOpenOffsetDays = item.WindowOpenOffsetDays;
                            questionnaireWindow.RepeatIntervalDays = item.RepeatIntervalDays;
                            questionnaireWindow.IsSelected = item.IsSelected;
                            questionnaireWindow.CloseOffsetTypeID = item.CloseOffsetTypeID ?? PCISEnum.OffsetType.Day;
                            questionnaireWindow.OpenOffsetTypeID = item.OpenOffsetTypeID ?? PCISEnum.OffsetType.Day;
                            item.QuestionnaireWindowID = this.questionnaireWindowRepository.UpdateQuestionnaireWindow(questionnaireWindow).QuestionnaireWindowID;
                        }
                        else
                        {
                            if (questionnaireWindowslist != null && questionnaireWindowslist.Count > 0)
                            {
                                var questionnaireWindows = questionnaireWindowslist.Where(x => x.AssessmentReasonID == item.AssessmentReasonID).FirstOrDefault();
                                if (questionnaireWindows != null)
                                {
                                    var questionnaireWindow = questionnaireWindowslist.Where(x => x.QuestionnaireWindowID == questionnaireWindows.QuestionnaireWindowID).FirstOrDefault();
                                    if (questionnaireWindow.DueDateOffsetDays != item.DueDateOffsetDays || questionnaireWindow.CanRepeat != item.CanRepeat || questionnaireWindow.WindowCloseOffsetDays != item.WindowCloseOffsetDays ||
                                        questionnaireWindow.WindowOpenOffsetDays != item.WindowOpenOffsetDays || questionnaireWindow.RepeatIntervalDays != item.RepeatIntervalDays || questionnaireWindow.IsSelected != item.IsSelected)
                                    {
                                        addToQueue = true;
                                    }
                                    questionnaireWindow.DueDateOffsetDays = item.DueDateOffsetDays;
                                    questionnaireWindow.CanRepeat = item.CanRepeat;
                                    questionnaireWindow.WindowCloseOffsetDays = item.WindowCloseOffsetDays;
                                    questionnaireWindow.WindowOpenOffsetDays = item.WindowOpenOffsetDays;                                    
                                    questionnaireWindow.RepeatIntervalDays = item.RepeatIntervalDays;
                                    questionnaireWindow.IsSelected = item.IsSelected;
                                    questionnaireWindow.CloseOffsetTypeID = item.CloseOffsetTypeID ?? PCISEnum.OffsetType.Day;
                                    questionnaireWindow.OpenOffsetTypeID = item.OpenOffsetTypeID ?? PCISEnum.OffsetType.Day;
                                    item.QuestionnaireWindowID = this.questionnaireWindowRepository.UpdateQuestionnaireWindow(questionnaireWindow).QuestionnaireWindowID;
                                }
                                else
                                {
                                    QuestionnaireWindowDTO questionnaireWindow = new QuestionnaireWindowDTO();
                                    questionnaireWindow.DueDateOffsetDays = item.DueDateOffsetDays;
                                    questionnaireWindow.AssessmentReasonID = item.AssessmentReasonID;
                                    questionnaireWindow.QuestionnaireID = ScheduleParameterDTO.QuestionnaireID;
                                    questionnaireWindow.CanRepeat = item.CanRepeat;
                                    questionnaireWindow.WindowCloseOffsetDays = item.WindowCloseOffsetDays;
                                    questionnaireWindow.WindowOpenOffsetDays = item.WindowOpenOffsetDays;                                    
                                    questionnaireWindow.RepeatIntervalDays = item.RepeatIntervalDays;
                                    questionnaireWindow.IsSelected = item.IsSelected;
                                    addToQueue = true;
                                    questionnaireWindow.CloseOffsetTypeID = item.CloseOffsetTypeID ?? PCISEnum.OffsetType.Day;
                                    questionnaireWindow.OpenOffsetTypeID = item.OpenOffsetTypeID ?? PCISEnum.OffsetType.Day;
                                    item.QuestionnaireWindowID = this.questionnaireWindowRepository.AddQuestionnaireWindow(questionnaireWindow);
                                }
                            }
                            else
                            {
                                QuestionnaireWindowDTO questionnaireWindow = new QuestionnaireWindowDTO();
                                questionnaireWindow.DueDateOffsetDays = item.DueDateOffsetDays;
                                questionnaireWindow.AssessmentReasonID = item.AssessmentReasonID;
                                questionnaireWindow.QuestionnaireID = ScheduleParameterDTO.QuestionnaireID;
                                questionnaireWindow.CanRepeat = item.CanRepeat;
                                questionnaireWindow.WindowCloseOffsetDays = item.WindowCloseOffsetDays;
                                questionnaireWindow.WindowOpenOffsetDays = item.WindowOpenOffsetDays;
                                questionnaireWindow.RepeatIntervalDays = item.RepeatIntervalDays;
                                questionnaireWindow.IsSelected = item.IsSelected;
                                addToQueue = true;
                                questionnaireWindow.CloseOffsetTypeID = item.CloseOffsetTypeID ?? PCISEnum.OffsetType.Day;
                                questionnaireWindow.OpenOffsetTypeID = item.OpenOffsetTypeID ?? PCISEnum.OffsetType.Day;
                                item.QuestionnaireWindowID = this.questionnaireWindowRepository.AddQuestionnaireWindow(questionnaireWindow);
                            }
                        }
                        if(item?.questionnaireRegularReminderRecurrence != null && item.IsSelected)
                        {
                            item.questionnaireRegularReminderRecurrence.QuestionnaireWindowID = item.QuestionnaireWindowID;
                            item.questionnaireRegularReminderRecurrence.QuestionnaireID = ScheduleParameterDTO.QuestionnaireID;
                            regularReminderRecurrence = item.questionnaireRegularReminderRecurrence;
                        }
                    }
                    questionnaireAddedToQueue = addToQueue;
                }
                else
                {
                    questionnaireAddedToQueue = false;
                }
                UpdateReminderRecurrenceSettings(regularReminderRecurrence, ScheduleParameterDTO.QuestionnaireID, ref questionnaireAddedToQueue);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// UpdateQuestionaireDetails
        /// </summary>
        /// <param name="id">questionnaireEditDetailsDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateQuestionaireDetails(QuestionnaireEditDetailsDTO questionnaireEditDetailsDTO)
        {
            try
            {
                CRUDResponseDTO resultDTO = new CRUDResponseDTO();
                if (questionnaireEditDetailsDTO != null)
                {
                    QuestionnairesDTO QuestionnaireDTO = this.questionnaireRepository.GetQuestionnaire(questionnaireEditDetailsDTO.QuestionnaireID).Result;
                    if (QuestionnaireDTO != null)
                    {
                        QuestionnaireDTO.Name = questionnaireEditDetailsDTO.Name;
                        QuestionnaireDTO.UpdateUserID = questionnaireEditDetailsDTO.UpdateUserID;
                        QuestionnaireDTO.UpdateDate = DateTime.UtcNow;
                        QuestionnaireDTO.StartDate = questionnaireEditDetailsDTO.StartDate;
                        QuestionnaireDTO.EndDate = questionnaireEditDetailsDTO.EndDate;

                        QuestionnaireDTO.StartDate = questionnaireEditDetailsDTO.StartDate;
                        QuestionnaireDTO.EndDate = questionnaireEditDetailsDTO.EndDate;
                        QuestionnaireDTO.HasFormView = questionnaireEditDetailsDTO.HasFormView;
                        var Questionnaireresult = this.questionnaireRepository.UpdateQuestionnaire(QuestionnaireDTO);
                        if (Questionnaireresult != null)
                        {
                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                        }
                    }
                }
                return resultDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetReminderSchedule.
        /// </summary>
        /// <param name="id">questionnaireID.</param>
        /// <returns>NotificationDetailsResponseDTO.</returns>
        public ReminderScheduleResponseDTO GetReminderSchedule(int questionnaireID)
        {
            try
            {
                ReminderScheduleResponseDTO reminderScheduleResponse = new ReminderScheduleResponseDTO();
                if (questionnaireID > 0)
                {
                    QuestionnairesDTO questionnairesDTO = new QuestionnairesDTO();
                    questionnairesDTO = this.questionnaireRepository.GetQuestionnaire(questionnaireID).Result;
                    if (questionnairesDTO != null)
                    {
                        reminderScheduleResponse.Questionnaire = new ReminderScheduleDetailsDTO();
                        reminderScheduleResponse.Questionnaire.ReminderScheduleName = questionnairesDTO.ReminderScheduleName;
                        reminderScheduleResponse.Questionnaire.IsEmailRemindersHelpers = questionnairesDTO.IsEmailRemindersHelpers;
                        reminderScheduleResponse.Questionnaire.IsAlertsHelpersManagers = questionnairesDTO.IsAlertsHelpersManagers;
                        var questionnaireWindow = this.questionnaireRepository.GetQuestionnaireWindow(questionnaireID);
                        if (questionnaireWindow != null && questionnaireWindow.Count > 0)
                        {
                            //PCIS-3225 : Get ReminderRecurrence receivers only if windowid is selected
                            var window = questionnaireWindow.Where(x => x.AssessmentName == PCISEnum.AssessmentReason.Scheduled).FirstOrDefault();
                            if (window != null && window?.IsSelected == true)
                            {
                                var regularReminderRecurrence = this.questionnaireRegularReminderRecurrenceRepository.GetQuestionnaireRegularReminderRecurrence(questionnaireID);
                                window.questionnaireRegularReminderRecurrence = regularReminderRecurrence;
                            }
                            reminderScheduleResponse.Questionnaire.QuestionnaireWindow = questionnaireWindow;
                        }
                        var questionnaireReminderRule = this.questionnaireRepository.GetQuestionnaireReminderRule(questionnaireID);
                        if (questionnaireReminderRule != null && questionnaireReminderRule.Count > 0)
                        {
                            reminderScheduleResponse.Questionnaire.QuestionnaireReminderRule = questionnaireReminderRule;
                        }
                        //PCIS-3225 : Get InviteToCompleteReceiverIds
                        reminderScheduleResponse.Questionnaire.IsEmailInviteToCompleteReminders = questionnairesDTO.IsEmailInviteToCompleteReminders;
                        reminderScheduleResponse.Questionnaire.InviteToCompleteReceiverIds = questionnairesDTO.InviteToCompleteReceiverIds;

                        //PCIS-3225 : Get ReminderTimeRule receivers
                        List<QuestionnaireRegularReminderTimeRuleDTO> regularReminderRecurrenceTime = this.questionnaireRegularReminderTimeRuleRepository.GetQuestionnaireRegularReminderTimeRule(questionnaireID);
                        reminderScheduleResponse.Questionnaire.questionnaireRegularReminderTime = regularReminderRecurrenceTime;
                        reminderScheduleResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                        reminderScheduleResponse.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    }
                }
                return reminderScheduleResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireList
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>PersonQuestionnaireListResponseDTO</returns>
        public PersonQuestionnaireListResponseDTO GetPersonQuestionnaireList(Guid personIndex, int? questionnaireID, int pageNumber, int pageSize, UserTokenDetails userTokenDetails)
        {
            try
            {
                PersonQuestionnaireListResponseDTO PersonQuestionnaireListDTO = new PersonQuestionnaireListResponseDTO();
                if (pageNumber <= 0)
                {
                    PersonQuestionnaireListDTO.PersonQuestionnaireListDTO = null;
                    PersonQuestionnaireListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    PersonQuestionnaireListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return PersonQuestionnaireListDTO;
                }
                else if (pageSize <= 0)
                {
                    PersonQuestionnaireListDTO.PersonQuestionnaireListDTO = null;
                    PersonQuestionnaireListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    PersonQuestionnaireListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return PersonQuestionnaireListDTO;
                }
                else
                {
                    PeopleDTO person = this.personRepository.GetPersonalDetails(personIndex);
                    var sharedQuestionIDs = this.personRepository.GetSharedPersonQuestionnaireDetails(person.PersonID, userTokenDetails.AgencyID).SharedQuestionnaireIDs;
                    var helperColabQuestionIDs = string.Empty;
                    if (string.IsNullOrEmpty(sharedQuestionIDs))
                    {
                        helperColabQuestionIDs = this.personRepository.GetPersonHelperCollaborationDetails(person.PersonID, userTokenDetails.AgencyID, userTokenDetails.UserID).SharedQuestionnaireIDs;
                    }
                    var response = this.questionnaireRepository.GetPersonQuestionnaireList(personIndex, questionnaireID, pageNumber, pageSize, sharedQuestionIDs, helperColabQuestionIDs);
                    var count = response?.Count == 0 ? 0 : response[0].TotalCount;
                    PersonQuestionnaireListDTO.PersonQuestionnaireListDTO = response;
                    PersonQuestionnaireListDTO.TotalCount = count;
                    PersonQuestionnaireListDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    PersonQuestionnaireListDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return PersonQuestionnaireListDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// CloneQuestionnaire
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <param name="agencyID"></param>
        /// <param name="updateUserID"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO CloneQuestionnaire(int questionnaireID, long agencyID, int updateUserID)
        {
            try
            {
                CRUDResponseDTO resultDTO = new CRUDResponseDTO();
                List<QuestionnaireItemIdsDTO> QuestionnaireItemIds = new List<QuestionnaireItemIdsDTO>();

                if (questionnaireID != 0)
                {
                    QuestionnairesDTO QuestionnaireDTO = this.questionnaireRepository.GetQuestionnaire(questionnaireID).Result;
                    if (QuestionnaireDTO != null)
                    {
                        QuestionnaireDTO.AgencyID = agencyID;
                        QuestionnaireDTO.Name = "Clone of " + QuestionnaireDTO.Name;
                        QuestionnaireDTO.UpdateUserID = updateUserID;
                        QuestionnaireDTO.OwnerUserID = updateUserID;
                        QuestionnaireDTO.UpdateDate = DateTime.UtcNow;
                        QuestionnaireDTO.ParentQuestionnaireID = QuestionnaireDTO.QuestionnaireID;
                        QuestionnaireDTO.QuestionnaireID = 0;
                        QuestionnaireDTO.IsBaseQuestionnaire = false;
                        QuestionnaireDTO.EndDate = null;
                        QuestionnaireDTO.IsAlertsHelpersManagers = false;
                        QuestionnaireDTO.IsEmailRemindersHelpers = false;
                        QuestionnaireDTO.HasSkipLogic = QuestionnaireDTO.HasSkipLogic;
                        QuestionnaireDTO.HasDefaultResponseRule = QuestionnaireDTO.HasDefaultResponseRule;
                        QuestionnaireDTO.IsEmailInviteToCompleteReminders = QuestionnaireDTO.IsEmailInviteToCompleteReminders;
                        QuestionnaireDTO.InviteToCompleteReceiverIds = QuestionnaireDTO.InviteToCompleteReceiverIds;
                        var Questionnaireresult = this.questionnaireRepository.CloneQuestionnaire(QuestionnaireDTO);
                        if (Questionnaireresult != null && Questionnaireresult.QuestionnaireID > 0)
                        {
                            List<QuestionnaireItemsDTO> questionnaireItemsDTO = this.questionnaireItemRepository.GetQuestionnaireItemsByQuestionnaire(questionnaireID);
                            List<QuestionnaireItemsDTO> questionnaireItemsDTOtoClone = new List<QuestionnaireItemsDTO>();
                            if (questionnaireItemsDTO != null && questionnaireItemsDTO.Count > 0)
                            {
                                foreach (QuestionnaireItemsDTO QuestionnaireItemDTO in questionnaireItemsDTO)
                                {
                                    int OldQuestionnaireItemID = QuestionnaireItemDTO.QuestionnaireItemID;
                                    QuestionnaireItemDTO.QuestionnaireID = Questionnaireresult.QuestionnaireID;
                                    QuestionnaireItemDTO.UpdateUserID = updateUserID;
                                    QuestionnaireItemDTO.UpdateDate = DateTime.UtcNow;
                                    QuestionnaireItemDTO.QuestionnaireItemID = 0;
                                    QuestionnaireItemDTO.ClonedQuestionnaireItemId = OldQuestionnaireItemID;
                                    questionnaireItemsDTOtoClone.Add(QuestionnaireItemDTO);

                                    //QuestionnaireItemIds.Add(
                                    //    new QuestionnaireItemIdsDTO { OldQuestionnaireItemID = OldQuestionnaireItemID, NewQuestionnaireItemID = QuestionnaireItemresult.QuestionnaireItemID }
                                    //);
                                }
                                this.questionnaireItemRepository.CloneQuestionnaireItem(questionnaireItemsDTOtoClone);
                            }

                            List<QuestionnaireReminderRulesDTO> questionnaireReminderRulesDTO = this.questionnaireReminderRuleRespository.GetQuestionnaireReminderRulesByQuestionnaire(questionnaireID);
                            List<QuestionnaireReminderRulesDTO> questionnaireReminderRuleToClone = new List<QuestionnaireReminderRulesDTO>();

                            if (questionnaireReminderRulesDTO != null && questionnaireReminderRulesDTO.Count > 0)
                            {
                                foreach (QuestionnaireReminderRulesDTO QuestionnaireReminderRuleDTO in questionnaireReminderRulesDTO)
                                {
                                    QuestionnaireReminderRuleDTO.QuestionnaireID = Questionnaireresult.QuestionnaireID;
                                    QuestionnaireReminderRuleDTO.QuestionnaireReminderRuleID = 0;
                                    questionnaireReminderRuleToClone.Add(QuestionnaireReminderRuleDTO);
                                }
                                this.questionnaireReminderRuleRespository.CloneQuestionnaireReminderRule(questionnaireReminderRuleToClone);
                            }

                            List<QuestionnaireWindowsDTO> questionnaireWindowsDTO = this.questionnaireWindowRepository.GetQuestionnaireWindowsByQuestionnaire(questionnaireID);
                            List<QuestionnaireWindowsDTO> questionnaireWindowsDTOToClone = new List<QuestionnaireWindowsDTO>();
                            if (questionnaireWindowsDTO != null && questionnaireWindowsDTO.Count > 0)
                            {
                                foreach (QuestionnaireWindowsDTO QuestionnaireWindowDTO in questionnaireWindowsDTO)
                                {
                                    QuestionnaireWindowDTO.QuestionnaireID = Questionnaireresult.QuestionnaireID;
                                    QuestionnaireWindowDTO.QuestionnaireWindowID = 0;
                                    questionnaireWindowsDTOToClone.Add(QuestionnaireWindowDTO);
                                }
                                this.questionnaireWindowRepository.CloneQuestionnaireWindow(questionnaireWindowsDTOToClone);
                            }
                            QuestionnaireNotifyRiskSchedulesDTO questionnaireNotifyRiskSchedulesDTO = this.questionnaireNotifyRiskScheduleRepository.GetQuestionnaireNotifyRiskScheduleByQuestionnaire(questionnaireID).Result;
                            if (questionnaireNotifyRiskSchedulesDTO != null && questionnaireNotifyRiskSchedulesDTO.QuestionnaireNotifyRiskScheduleID > 0)
                            {
                                int OldQuestionnaireNotifyRiskScheduleID = questionnaireNotifyRiskSchedulesDTO.QuestionnaireNotifyRiskScheduleID;
                                questionnaireNotifyRiskSchedulesDTO.QuestionnaireID = Questionnaireresult.QuestionnaireID;
                                questionnaireNotifyRiskSchedulesDTO.UpdateUserID = updateUserID;
                                questionnaireNotifyRiskSchedulesDTO.UpdateDate = DateTime.UtcNow;
                                questionnaireNotifyRiskSchedulesDTO.QuestionnaireNotifyRiskScheduleID = 0;

                                var QuestionnaireNotifyRiskScheduleresult = this.questionnaireNotifyRiskScheduleRepository.CloneQuestionnaireNotifyRiskSchedule(questionnaireNotifyRiskSchedulesDTO);
                                if (QuestionnaireNotifyRiskScheduleresult != null && QuestionnaireNotifyRiskScheduleresult.QuestionnaireNotifyRiskScheduleID > 0)
                                {
                                    List<QuestionnaireNotifyRiskRulesDTO> questionnaireNotifyRiskRulesDTO = this.questionnaireNotifyRiskRuleRespository.GetQuestionnaireNotifyRiskRuleBySchedule(OldQuestionnaireNotifyRiskScheduleID);
                                    List<QuestionnaireNotifyRiskRulesDTO> questionnaireNotifyRiskRulesDTOToClone = new List<QuestionnaireNotifyRiskRulesDTO>();
                                    if (questionnaireNotifyRiskRulesDTO != null && questionnaireNotifyRiskRulesDTO.Count > 0)
                                    {
                                        foreach (QuestionnaireNotifyRiskRulesDTO QuestionnaireNotifyRiskRuleDTO in questionnaireNotifyRiskRulesDTO)
                                        {
                                            int OldQuestionnaireNotifyRiskRuleID = QuestionnaireNotifyRiskRuleDTO.QuestionnaireNotifyRiskRuleID;
                                            QuestionnaireNotifyRiskRuleDTO.QuestionnaireNotifyRiskScheduleID = QuestionnaireNotifyRiskScheduleresult.QuestionnaireNotifyRiskScheduleID;
                                            QuestionnaireNotifyRiskRuleDTO.UpdateUserID = updateUserID;
                                            QuestionnaireNotifyRiskRuleDTO.UpdateDate = DateTime.UtcNow;
                                            QuestionnaireNotifyRiskRuleDTO.QuestionnaireNotifyRiskRuleID = 0;
                                            QuestionnaireNotifyRiskRuleDTO.ClonedQuestionnaireNotifyRiskRuleID = OldQuestionnaireNotifyRiskRuleID;
                                            questionnaireNotifyRiskRulesDTOToClone.Add(QuestionnaireNotifyRiskRuleDTO);
                                        }
                                        this.questionnaireNotifyRiskRuleRespository.CloneQuestionnaireNotifyRiskRule(questionnaireNotifyRiskRulesDTOToClone);
                                        var QuestionnaireNotifyRiskRuleresult = this.questionnaireNotifyRiskRuleRespository.GetQuestionnaireNotifyRiskRuleBySchedule(QuestionnaireNotifyRiskScheduleresult.QuestionnaireNotifyRiskScheduleID);
                                        if (QuestionnaireNotifyRiskRuleresult != null)
                                        {
                                            foreach (var item in QuestionnaireNotifyRiskRuleresult)
                                            {
                                                int clonedQuestionnaireNotifyRiskRuleID = item.ClonedQuestionnaireNotifyRiskRuleID ?? default(int);
                                                List<QuestionnaireNotifyRiskRuleConditionsDTO> questionnaireNotifyRiskRuleConditionsDTO = this.questionnaireNotifyRiskRuleConditionRepository.GetQuestionnaireNotifyRiskRuleConditionByRiskRule(clonedQuestionnaireNotifyRiskRuleID);
                                                List<QuestionnaireNotifyRiskRuleConditionsDTO> questionnaireNotifyRiskRuleConditionsDTOToClone = new List<QuestionnaireNotifyRiskRuleConditionsDTO>();
                                                if (questionnaireNotifyRiskRuleConditionsDTO != null && questionnaireNotifyRiskRuleConditionsDTO.Count > 0)
                                                {
                                                    foreach (QuestionnaireNotifyRiskRuleConditionsDTO QuestionnaireNotifyRiskRuleConditionDTO in questionnaireNotifyRiskRuleConditionsDTO)
                                                    {
                                                        var questionnaireItems = this.questionnaireItemRepository.GetQuestionnaireItemsByQuestionnaire(Questionnaireresult.QuestionnaireID);

                                                        foreach (var questionnaireItem in questionnaireItems)
                                                        {
                                                            if (questionnaireItem.ClonedQuestionnaireItemId == QuestionnaireNotifyRiskRuleConditionDTO.QuestionnaireItemId)
                                                            {
                                                                QuestionnaireNotifyRiskRuleConditionDTO.QuestionnaireItemId = questionnaireItem.QuestionnaireItemID;
                                                            }
                                                        }
                                                        QuestionnaireNotifyRiskRuleConditionDTO.QuestionnaireNotifyRiskRuleID = item.QuestionnaireNotifyRiskRuleID;
                                                        QuestionnaireNotifyRiskRuleConditionDTO.UpdateUserID = updateUserID;
                                                        QuestionnaireNotifyRiskRuleConditionDTO.UpdateDate = DateTime.UtcNow;
                                                        QuestionnaireNotifyRiskRuleConditionDTO.QuestionnaireNotifyRiskRuleConditionID = 0;
                                                        questionnaireNotifyRiskRuleConditionsDTOToClone.Add(QuestionnaireNotifyRiskRuleConditionDTO);
                                                    }
                                                    this.questionnaireNotifyRiskRuleConditionRepository.CloneQuestionnaireNotifyRiskRuleCondition(questionnaireNotifyRiskRuleConditionsDTOToClone);
                                                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.CloneSuccess;
                                                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.CloneSuccess + Questionnaireresult.QuestionnaireID + ".");
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            var list_questionnaireSkipLogicRuleDTO = this.questionnaireSkipLogicRuleRepository.GetQuestionnaireSkipLogic(questionnaireID);
                            if (list_questionnaireSkipLogicRuleDTO != null && list_questionnaireSkipLogicRuleDTO.Count > 0)
                            {
                                foreach (QuestionnaireSkipLogicRuleDTO objQuestionnaireSkipLogicRuleDTO in list_questionnaireSkipLogicRuleDTO)
                                {
                                    int OldQuestionnaireSkipLogicRuleID = objQuestionnaireSkipLogicRuleDTO.QuestionnaireSkipLogicRuleID;
                                    objQuestionnaireSkipLogicRuleDTO.QuestionnaireID = Questionnaireresult.QuestionnaireID;
                                    objQuestionnaireSkipLogicRuleDTO.UpdateUserID = updateUserID;
                                    objQuestionnaireSkipLogicRuleDTO.UpdateDate = DateTime.UtcNow;
                                    objQuestionnaireSkipLogicRuleDTO.QuestionnaireSkipLogicRuleID = 0;
                                    objQuestionnaireSkipLogicRuleDTO.ClonedQuestionnaireSkipLogicRuleID = OldQuestionnaireSkipLogicRuleID;
                                    var newQuestionnaireSkipLogicRule = this.questionnaireSkipLogicRuleRepository.CloneQuestionnaireSkipLogicRule(objQuestionnaireSkipLogicRuleDTO);
                                    if (newQuestionnaireSkipLogicRule != null && newQuestionnaireSkipLogicRule.QuestionnaireSkipLogicRuleID > 0)
                                    {
                                        List<QuestionnaireSkipLogicRuleActionDTO> list_questionnaireSkipLogicRuleActionDTO = this.questionnaireSkipLogicRuleActionRepository.GetQuestionnaireSkipLogicAction(OldQuestionnaireSkipLogicRuleID);

                                        var clonedquestionnaireItems = this.questionnaireItemRepository.GetQuestionnaireItemsByQuestionnaire(Questionnaireresult.QuestionnaireID);
                                        List<QuestionnaireSkipLogicRuleActionDTO> questionnaireSkipLogicRuleActionDTOToClone = new List<QuestionnaireSkipLogicRuleActionDTO>();

                                        if (list_questionnaireSkipLogicRuleActionDTO != null && list_questionnaireSkipLogicRuleActionDTO.Count > 0)
                                        {
                                            foreach (QuestionnaireSkipLogicRuleActionDTO objQuestionnaireSkipLogicRuleActionDTO in list_questionnaireSkipLogicRuleActionDTO)
                                            {
                                                int OldQuestionnaireSkipLogicRuleActionId = objQuestionnaireSkipLogicRuleActionDTO.QuestionnaireSkipLogicRuleActionID;
                                                objQuestionnaireSkipLogicRuleActionDTO.QuestionnaireSkipLogicRuleID = newQuestionnaireSkipLogicRule.QuestionnaireSkipLogicRuleID;
                                                objQuestionnaireSkipLogicRuleActionDTO.UpdateUserID = updateUserID;
                                                objQuestionnaireSkipLogicRuleActionDTO.UpdateDate = DateTime.UtcNow;
                                                objQuestionnaireSkipLogicRuleActionDTO.QuestionnaireSkipLogicRuleActionID = 0;
                                                objQuestionnaireSkipLogicRuleActionDTO.QuestionnaireItemID = clonedquestionnaireItems.Where(x => x.ClonedQuestionnaireItemId == objQuestionnaireSkipLogicRuleActionDTO.QuestionnaireItemID).FirstOrDefault()?.QuestionnaireItemID;
                                                questionnaireSkipLogicRuleActionDTOToClone.Add(objQuestionnaireSkipLogicRuleActionDTO);
                                            }
                                            this.questionnaireSkipLogicRuleActionRepository.CloneQuestionnaireSkipLogicRuleAction(questionnaireSkipLogicRuleActionDTOToClone);
                                        }
                                        List<QuestionnaireSkipLogicRuleConditionDTO> list_questionnaireSkipLogicRuleConditionDTO = this.questionnaireSkipLogicRuleConditionRepository.GetQuestionnaireSkipLogicCondition(OldQuestionnaireSkipLogicRuleID);
                                        List<QuestionnaireSkipLogicRuleConditionDTO> questionnaireSkipLogicRuleConditionDTOToClone = new List<QuestionnaireSkipLogicRuleConditionDTO>();
                                        if (list_questionnaireSkipLogicRuleConditionDTO != null && list_questionnaireSkipLogicRuleConditionDTO.Count > 0)
                                        {
                                            foreach (QuestionnaireSkipLogicRuleConditionDTO objquestionnaireSkipLogicRuleConditionDTO in list_questionnaireSkipLogicRuleConditionDTO)
                                            {
                                                int OldQuestionnaireSkipLogicRuleConditionDTOId = objquestionnaireSkipLogicRuleConditionDTO.QuestionnaireSkipLogicRuleConditionID;
                                                objquestionnaireSkipLogicRuleConditionDTO.QuestionnaireSkipLogicRuleID = newQuestionnaireSkipLogicRule.QuestionnaireSkipLogicRuleID;
                                                objquestionnaireSkipLogicRuleConditionDTO.UpdateUserID = updateUserID;
                                                objquestionnaireSkipLogicRuleConditionDTO.UpdateDate = DateTime.UtcNow;
                                                objquestionnaireSkipLogicRuleConditionDTO.QuestionnaireSkipLogicRuleConditionID = 0;
                                                objquestionnaireSkipLogicRuleConditionDTO.QuestionnaireItemID = clonedquestionnaireItems.Where(x => x.ClonedQuestionnaireItemId == objquestionnaireSkipLogicRuleConditionDTO.QuestionnaireItemID).FirstOrDefault().QuestionnaireItemID;
                                                questionnaireSkipLogicRuleConditionDTOToClone.Add(objquestionnaireSkipLogicRuleConditionDTO);
                                            }
                                            this.questionnaireSkipLogicRuleConditionRepository.CloneQuestionnaireSkipLogicRuleCondition(questionnaireSkipLogicRuleConditionDTOToClone);
                                        }
                                    }
                                }
                            }

                            var listQuestionnaireDefaultResponseRuleDTO = this.questionnaireDefaultResponseRuleRepository.GetQuestionnaireDefaultResponse(questionnaireID);
                            if (listQuestionnaireDefaultResponseRuleDTO != null && listQuestionnaireDefaultResponseRuleDTO.Count > 0)
                            {
                                foreach (QuestionnaireDefaultResponseRuleDTO objQuestionnaireDefaultResponseRuleDTO in listQuestionnaireDefaultResponseRuleDTO)
                                {
                                    int OldQuestionnaireDefaultResponseRuleID = objQuestionnaireDefaultResponseRuleDTO.QuestionnaireDefaultResponseRuleID;
                                    objQuestionnaireDefaultResponseRuleDTO.QuestionnaireID = Questionnaireresult.QuestionnaireID;
                                    objQuestionnaireDefaultResponseRuleDTO.UpdateUserID = updateUserID;
                                    objQuestionnaireDefaultResponseRuleDTO.UpdateDate = DateTime.UtcNow;
                                    objQuestionnaireDefaultResponseRuleDTO.QuestionnaireDefaultResponseRuleID = 0;
                                    objQuestionnaireDefaultResponseRuleDTO.ClonedQuestionnaireDefaultResponseRuleID = OldQuestionnaireDefaultResponseRuleID;
                                    var newQuestionnaireDefaultResponseRule = this.questionnaireDefaultResponseRuleRepository.CloneQuestionnaireDefaultResponseRule(objQuestionnaireDefaultResponseRuleDTO);
                                    if (newQuestionnaireDefaultResponseRule != null && newQuestionnaireDefaultResponseRule.QuestionnaireDefaultResponseRuleID > 0)
                                    {
                                        List<QuestionnaireDefaultResponseRuleActionDTO> listQuestionnaireDefaultResponseRuleActionDTO = this.questionnaireDefaultResponseRuleActionRepository.GetQuestionnaireDefaultResponseAction(OldQuestionnaireDefaultResponseRuleID);

                                        var clonedquestionnaireItems = this.questionnaireItemRepository.GetQuestionnaireItemsByQuestionnaire(Questionnaireresult.QuestionnaireID);
                                        List<QuestionnaireDefaultResponseRuleActionDTO> questionnaireDefaultResponseRuleActionDTOToClone = new List<QuestionnaireDefaultResponseRuleActionDTO>();

                                        if (listQuestionnaireDefaultResponseRuleActionDTO != null && listQuestionnaireDefaultResponseRuleActionDTO.Count > 0)
                                        {
                                            foreach (QuestionnaireDefaultResponseRuleActionDTO objQuestionnaireDefaultResponseRuleActionDTO in listQuestionnaireDefaultResponseRuleActionDTO)
                                            {
                                                int OldQuestionnaireDefaultResponseRuleActionId = objQuestionnaireDefaultResponseRuleActionDTO.QuestionnaireDefaultResponseRuleActionID;
                                                objQuestionnaireDefaultResponseRuleActionDTO.QuestionnaireDefaultResponseRuleID = newQuestionnaireDefaultResponseRule.QuestionnaireDefaultResponseRuleID;
                                                objQuestionnaireDefaultResponseRuleActionDTO.UpdateUserID = updateUserID;
                                                objQuestionnaireDefaultResponseRuleActionDTO.UpdateDate = DateTime.UtcNow;
                                                objQuestionnaireDefaultResponseRuleActionDTO.QuestionnaireDefaultResponseRuleActionID = 0;
                                                objQuestionnaireDefaultResponseRuleActionDTO.QuestionnaireItemID = clonedquestionnaireItems.Where(x => x.ClonedQuestionnaireItemId == objQuestionnaireDefaultResponseRuleActionDTO.QuestionnaireItemID).FirstOrDefault()?.QuestionnaireItemID;
                                                questionnaireDefaultResponseRuleActionDTOToClone.Add(objQuestionnaireDefaultResponseRuleActionDTO);
                                            }
                                            this.questionnaireDefaultResponseRuleActionRepository.CloneQuestionnaireDefaultResponseRuleAction(questionnaireDefaultResponseRuleActionDTOToClone);
                                        }
                                        List<QuestionnaireDefaultResponseRuleConditionDTO> listQuestionnaireDefaultResponseRuleConditionDTO = this.questionnaireDefaultResponseRuleConditionRepository.GetQuestionnaireDefaultResponseCondition(OldQuestionnaireDefaultResponseRuleID);
                                        List<QuestionnaireDefaultResponseRuleConditionDTO> questionnaireDefaultResponseRuleConditionDTOToClone = new List<QuestionnaireDefaultResponseRuleConditionDTO>();
                                        if (listQuestionnaireDefaultResponseRuleConditionDTO != null && listQuestionnaireDefaultResponseRuleConditionDTO.Count > 0)
                                        {
                                            foreach (QuestionnaireDefaultResponseRuleConditionDTO objquestionnaireDefaultResponseRuleConditionDTO in listQuestionnaireDefaultResponseRuleConditionDTO)
                                            {
                                                int OldQuestionnaireDefaultResponseRuleConditionDTOId = objquestionnaireDefaultResponseRuleConditionDTO.QuestionnaireDefaultResponseRuleConditionID;
                                                objquestionnaireDefaultResponseRuleConditionDTO.QuestionnaireDefaultResponseRuleID = newQuestionnaireDefaultResponseRule.QuestionnaireDefaultResponseRuleID;
                                                objquestionnaireDefaultResponseRuleConditionDTO.UpdateUserID = updateUserID;
                                                objquestionnaireDefaultResponseRuleConditionDTO.UpdateDate = DateTime.UtcNow;
                                                objquestionnaireDefaultResponseRuleConditionDTO.QuestionnaireDefaultResponseRuleConditionID = 0;
                                                questionnaireDefaultResponseRuleConditionDTOToClone.Add(objquestionnaireDefaultResponseRuleConditionDTO);
                                            }
                                            this.questionnaireDefaultResponseRuleConditionRepository.CloneQuestionnaireDefaultResponseRuleCondition(questionnaireDefaultResponseRuleConditionDTOToClone);
                                        }
                                    }
                                }
                            }

                            //PCIS-3225 :  Clone the regular reminder recurence 
                            QuestionnaireRegularReminderRecurrenceDTO regularReminderRecurrence = this.questionnaireRegularReminderRecurrenceRepository.GetQuestionnaireRegularReminderRecurrence(questionnaireID);
                            if (regularReminderRecurrence != null && regularReminderRecurrence.QuestionnaireRegularReminderRecurrenceID > 0)
                            {
                                regularReminderRecurrence.QuestionnaireID = Questionnaireresult.QuestionnaireID;
                                regularReminderRecurrence.QuestionnaireRegularReminderRecurrenceID = 0;
                                regularReminderRecurrence.UpdateDate = DateTime.UtcNow;
                                this.questionnaireRegularReminderRecurrenceRepository.AddQuestionnaireReminderRecurrence(regularReminderRecurrence);
                            }

                            //PCIS-3225 : Clone the regular reminder Time Schedule 
                            List<QuestionnaireRegularReminderTimeRuleDTO> regularReminderTimeRuleDTO = this.questionnaireRegularReminderTimeRuleRepository.GetQuestionnaireRegularReminderTimeRule(questionnaireID);
                            if (regularReminderTimeRuleDTO != null && regularReminderTimeRuleDTO.Count > 0)
                            {
                                foreach (QuestionnaireRegularReminderTimeRuleDTO QuestionnaireTimeRuleDTO in regularReminderTimeRuleDTO)
                                {
                                    QuestionnaireTimeRuleDTO.QuestionnaireID = Questionnaireresult.QuestionnaireID;
                                    QuestionnaireTimeRuleDTO.QuestionnaireRegularReminderTimeRuleID = 0;
                                    QuestionnaireTimeRuleDTO.UpdateDate = DateTime.UtcNow;
                                }
                                this.questionnaireRegularReminderTimeRuleRepository.AddReminderTimeRule(regularReminderTimeRuleDTO);
                            }
                            
                        }
                        resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.CloneSuccess;
                        resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.CloneSuccess + Questionnaireresult.QuestionnaireID + ".");
                    }
                }

                return resultDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete a questionnaire.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <param name="userID">userID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO DeleteQuestionnaire(int questionnaireID, int userID)
        {
            try
            {
                var questionaire = this.questionnaireRepository.GetQuestionnaire(questionnaireID).Result;
                if (questionaire != null && questionaire.QuestionnaireID != 0)
                {
                    int usedQuestionaireCount = this.questionnaireRepository.GetQuestionnaireUsedCountByID(questionnaireID);
                    if (usedQuestionaireCount == 0)
                    {
                        questionaire.IsRemoved = true;
                        questionaire.UpdateDate = DateTime.UtcNow;
                        questionaire.UpdateUserID = userID;
                        var questionaireResponse = this.questionnaireRepository.UpdateQuestionnaire(questionaire);
                        if (questionaireResponse != null)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.DeleteRecordInUse;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeleteRecordInUse);
                    }
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidQuestionnaire;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidQuestionnaire);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<QueryFieldMappingDTO> GetQuestionnaireDetailsConfiguration()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Abbrev",
                fieldAlias = "catagoryAbbrev",
                fieldTable = "cat",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "itemName",
                fieldTable = "i",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "propertyValue",
                fieldTable = "p",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "StartDate",
                fieldAlias = "startDate",
                fieldTable = "v",
                fieldType = "date"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "EndDate",
                fieldAlias = "endDate",
                fieldTable = "v",
                fieldType = "date"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "minThresholdValue",
                fieldTable = "r",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "LowerItemResponseBehaviorId",
                fieldAlias = "minThreshold",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "minOption",
                fieldAlias = "minOption",
                fieldTable = "",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "maxThresholdValue",
                fieldTable = "r3",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "UpperItemResponseBehaviorId",
                fieldAlias = "maxThreshold",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "CanOverrideUpperResponseBehavior",
                fieldAlias = "maxOption",
                fieldTable = "",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "altDefaultValue",
                fieldTable = "r2",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "CanOverrideMedianResponseBehavior",
                fieldAlias = "altOption",
                fieldTable = "",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ListOrder",
                fieldAlias = "catagorylistOrder",
                fieldTable = "cat",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ListOrder",
                fieldAlias = "itemlistOrder",
                fieldTable = "i",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Value",
                fieldAlias = "minDefaultValue",
                fieldTable = "r4",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Value",
                fieldAlias = "maxDefaultValue",
                fieldTable = "r5",
                fieldType = "number"
            });
            return fieldMappingList;

        }

        private List<QueryFieldMappingDTO> GetQuestionnaireConfiguration()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "QuestionnaireID",
                fieldAlias = "questionnaireID",
                fieldTable = "Q",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Abbrev",
                fieldAlias = "instrumentAbbrev",
                fieldTable = "I",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "questionnaireName",
                fieldTable = "Q",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ReminderScheduleName",
                fieldAlias = "reminderScheduleName",
                fieldTable = "Q",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "notificationScheduleName",
                fieldTable = "QN",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "UpdateDate",
                fieldAlias = "updateDate",
                fieldTable = "Q",
                fieldType = "string"
            });
            return fieldMappingList;
        }

        /// <summary>
        /// GetQuestionnaireWindowsByQuestionnaire.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnaireWindowDetailsDTO.</returns>
        public QuestionnaireWindowDetailsDTO GetQuestionnaireWindowsByQuestionnaire(int questionnaireId)
        {
            try
            {
                QuestionnaireWindowDetailsDTO QuestionnaireWindowDetails = new QuestionnaireWindowDetailsDTO();
                if (questionnaireId > 0)
                {
                    QuestionnaireWindowDetails.QuestionnaireWindow = this.questionnaireWindowRepository.GetQuestionnaireWindowsByQuestionnaire(questionnaireId).Where(x => x.IsSelected).ToList();
                    QuestionnaireWindowDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    QuestionnaireWindowDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return QuestionnaireWindowDetails;
                }
                else
                {
                    QuestionnaireWindowDetails.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.QuestionnaireID);
                    QuestionnaireWindowDetails.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return QuestionnaireWindowDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireReminderRulesByQuestionnaire.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnaireReminderRuleDetailsDTO.</returns>
        public QuestionnaireReminderRuleDetailsDTO GetQuestionnaireReminderRulesByQuestionnaire(int questionnaireId)
        {
            try
            {
                QuestionnaireReminderRuleDetailsDTO QuestionnaireReminderRuleDetails = new QuestionnaireReminderRuleDetailsDTO();
                if (questionnaireId > 0)
                {
                    QuestionnaireReminderRuleDetails.QuestionnaireReminderRule = this.questionnaireReminderRuleRespository.GetQuestionnaireReminderRulesByQuestionnaire(questionnaireId);
                    QuestionnaireReminderRuleDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    QuestionnaireReminderRuleDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return QuestionnaireReminderRuleDetails;
                }
                else
                {
                    QuestionnaireReminderRuleDetails.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.QuestionnaireID);
                    QuestionnaireReminderRuleDetails.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return QuestionnaireReminderRuleDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddBulkNotifyReminder.
        /// </summary>
        /// <param name="notifyReminders">notifyReminders.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AddBulkNotifyReminder(List<NotifyReminderDTO> notifyReminders)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                var Response = this.notifyReminderRepository.AddBulkNotifyReminder(notifyReminders);
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
        /// UpdateBulkNotifyReminder.
        /// </summary>
        /// <param name="notifyReminders">notifyReminders.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateBulkNotifyReminder(List<NotifyReminderDTO> notifyReminders)
        {
            CRUDResponseDTO result = new CRUDResponseDTO();
            List<NotifyReminder> notifyReminder = new List<NotifyReminder>();
            this.mapper.Map<List<NotifyReminderDTO>, List<NotifyReminder>>(notifyReminders, notifyReminder);
            if (notifyReminder != null)
            {
                this.notifyReminderRepository.UpdateBulkNotifyReminder(notifyReminder);
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

        /// <summary>
        /// UpdateNotifications.
        /// </summary>
        /// <param name="GetNotifyReminderInput">GetNotifyReminderInput.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateNotifications(GetNotifyReminderInputDTO GetNotifyReminderInput)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                var response = this.notifyReminderRepository.GetNotifyReminders(GetNotifyReminderInput);
                if (response.Count > 0)
                {
                    var notificationType = this.notificationTypeRepository.GetNotificationType(PCISEnum.NotificationType.Reminder).Result;
                    var notificationLog = this.notificationLogRepository.GetNotifcationLogForReminder(response.Select(x => x.NotifyReminderID).ToList(), notificationType.NotificationTypeID);
                    if (notificationLog.Count > 0)
                    {
                        var notificationResolutionStatusID = this.notifiationResolutionStatusRepository.GetNotificationStatus(PCISEnum.NotificationStatus.Resolved).NotificationResolutionStatusID;

                        notificationLog.ForEach(x => x.NotificationResolutionStatusID = notificationResolutionStatusID);
                        this.notificationLogRepository.UpdateBulkNotificationLog(notificationLog);
                    }
                }
                if (response != null)
                {
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
        /// GetQuestionnaireRiskRule.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireNotifyRiskRulesDetailsDTO.</returns>
        public QuestionnaireNotifyRiskRulesDetailsDTO GetQuestionnaireRiskRule(int questionnaireID)
        {
            try
            {
                QuestionnaireNotifyRiskRulesDetailsDTO result = new QuestionnaireNotifyRiskRulesDetailsDTO();
                QuestionnaireNotifyRiskSchedulesDTO questionnaireNotifyRiskSchedulesDTO = this.questionnaireNotifyRiskScheduleRepository.GetQuestionnaireNotifyRiskScheduleByQuestionnaire(questionnaireID).Result;
                var response = this.questionnaireNotifyRiskRuleRepository.GetQuestionnaireNotifyRiskRuleBySchedule(questionnaireNotifyRiskSchedulesDTO.QuestionnaireNotifyRiskScheduleID);
                if (response != null)
                {
                    result.NotifyRiskRules = response;
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
        /// GetQuestionnaireNotifyRiskRuleConditionByRuleID.
        /// </summary>
        /// <param name="ruleId">ruleId.</param>
        /// <returns>QuestionnaireNotifyRiskRulesConditionDetailsDTO.</returns>
        public QuestionnaireNotifyRiskRulesConditionDetailsDTO GetQuestionnaireNotifyRiskRuleConditionByRuleID(int ruleId)
        {
            try
            {
                QuestionnaireNotifyRiskRulesConditionDetailsDTO result = new QuestionnaireNotifyRiskRulesConditionDetailsDTO();
                var response = this.questionnaireNotifyRiskRuleConditionRepository.GetQuestionnaireNotifyRiskRuleConditionByRuleID(ruleId);
                if (response != null)
                {
                    List<QuestionnaireNotifyRiskRuleConditionDTO> questionnaireNotifyRiskRuleCondition = new List<QuestionnaireNotifyRiskRuleConditionDTO>();
                    this.mapper.Map<List<QuestionnaireNotifyRiskRuleCondition>, List<QuestionnaireNotifyRiskRuleConditionDTO>>(response, questionnaireNotifyRiskRuleCondition);
                    result.NotifyRiskRuleConditions = questionnaireNotifyRiskRuleCondition;
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
        /// AddNotifyRisk.
        /// </summary>
        /// <param name="notifyRisk">notifyRisk.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AddNotifyRisk(NotifyRiskDTO notifyRisk)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                NotifyRisk NotifyRiskEntity = new NotifyRisk();
                this.mapper.Map<NotifyRiskDTO, NotifyRisk>(notifyRisk, NotifyRiskEntity);
                var Response = this.notifyRiskRepository.AddNotifyRisk(NotifyRiskEntity);
                if (Response != null)
                {
                    result.Id = Response.NotifyRiskID;
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
        /// AddNotifyRiskValues.
        /// </summary>
        /// <param name="notifyRiskValues">notifyRiskValues.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AddNotifyRiskValues(List<NotifyRiskValueDTO> notifyRiskValues)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                List<NotifyRiskValue> NotifyRiskValuesEntity = new List<NotifyRiskValue>();
                this.mapper.Map<List<NotifyRiskValueDTO>, List<NotifyRiskValue>>(notifyRiskValues, NotifyRiskValuesEntity);
                this.notifyRiskValueRepository.AddBulkNotifyRiskValue(NotifyRiskValuesEntity);
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
        /// GetNotificationStatus.
        /// </summary>
        /// <param name="status">status.</param>
        /// <returns>NotificationResolutionStatusDetailsDTO.</returns>
        public NotificationResolutionStatusDetailsDTO GetNotificationStatus(string status)
        {
            try
            {
                NotificationResolutionStatusDetailsDTO result = new NotificationResolutionStatusDetailsDTO();
                var response = this.notifiationResolutionStatusRepository.GetNotificationStatus(status);
                if (response != null)
                {
                    NotificationResolutionStatusDTO NotificationResolutionStatus = new NotificationResolutionStatusDTO();
                    this.mapper.Map<NotificationResolutionStatus, NotificationResolutionStatusDTO>(response, NotificationResolutionStatus);
                    result.NotificationStatus = NotificationResolutionStatus;
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
        /// GetNotificationStatus.
        /// </summary>
        /// <param name="notifyReminderIds">notifyReminderIds.</param>
        /// <returns>NotifyReminderDetailsDTO.</returns>
        public NotifyReminderDetailsDTO GetNotifyReminderByIds(List<int> notifyReminderIds)
        {
            NotifyReminderDetailsDTO result = new NotifyReminderDetailsDTO();
            var response = this.notifyReminderRepository.GetAsync(x => notifyReminderIds.Contains(x.NotifyReminderID)).Result.ToList();
            if (response != null)
            {
                List<NotifyReminderDTO> NotifyReminder = new List<NotifyReminderDTO>();
                this.mapper.Map<List<NotifyReminder>, List<NotifyReminderDTO>>(response, NotifyReminder);
                result.NotifyReminders = NotifyReminder;
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

        /// <summary>
        /// GetQuestionnaireItems.
        /// </summary>
        /// <param name="QuestionnaireId">QuestionnaireId.</param>
        /// <returns>QuestionnaireItemsResponseDTO.</returns>
        public QuestionnaireItemsResponseDTO GetQuestionnaireItems(int QuestionnaireId)
        {
            QuestionnaireItemsResponseDTO result = new QuestionnaireItemsResponseDTO();
            var response = this.questionnaireItemRepository.GetQuestionnaireItemsByQuestionnaire(QuestionnaireId);
            if (response != null)
            {
                result.QuestionnaireItemsForDashBoard = response;
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

        /// <summary>
        /// GetQuestionnaire.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnairesResponseDTO</returns>
        public QuestionnairesResponseDTO GetQuestionnaire(int questionnaireId)
        {
            QuestionnairesResponseDTO result = new QuestionnairesResponseDTO();
            var response = this.questionnaireRepository.GetQuestionnaire(questionnaireId).Result;
            if (response != null)
            {
                result.Questionnaire = response;
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

        /// <summary>
        /// GetQuestionnaireDetailsbyIds.
        /// </summary>
        /// <param name="questionnaireIds">questionnaireIds.</param>
        /// <returns>QuestionnairesResponseDTO.</returns>
        public QuestionnairesResponseDTO GetQuestionnaireDetailsbyIds(List<int> questionnaireIds)
        {
            QuestionnairesResponseDTO result = new QuestionnairesResponseDTO();
            var response = this.questionnaireRepository.GetQuestionnaireDetailsbyIds(questionnaireIds);
            if (response != null)
            {
                result.QuestionnaireList = response;
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

        /// <summary>
        /// GetQuestionnaireListForExternal.
        /// New function added for questionnaire-external api as part of PCIS-3180.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>QuestionnaireListResponseDTO.</returns> 
        public QuestionnaireListResponseDTO GetQuestionnaireListForExternal(QuestionnaireSearchInputDTO questionnaireSearchDTO, LoggedInUserDTO loggedInUserDTO)
        {
            try
            {
                List<QueryFieldMappingDTO> fieldMappingList = GetQuestionnaireConfigurationForExternal();
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQueryForExternalAPI(questionnaireSearchDTO, fieldMappingList);
                QuestionnaireListResponseDTO getQuestionnaire = new QuestionnaireListResponseDTO();
                List<QuestionnaireDTO> response = new List<QuestionnaireDTO>();
                int totalCount = 0;
                if (queryBuilderDTO.Page <= 0)
                {
                    getQuestionnaire.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getQuestionnaire;
                }
                else if (queryBuilderDTO.PageSize <= 0)
                {
                    getQuestionnaire.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    getQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getQuestionnaire;
                }
                else
                {
                    queryBuilderDTO.OrderBy = queryBuilderDTO.OrderBy.Replace("order by", "order by Q.IsBaseQuestionnaire DESC, ");
                    response = this.questionnaireRepository.GetQuestionnaireList(new QuestionnaireSearchDTO() {AgencyId = loggedInUserDTO.AgencyId }, queryBuilderDTO);
                    if (response.Count > 0)
                        totalCount = response.FirstOrDefault().TotalCount;

                    getQuestionnaire.QuestionnaireList = response;
                    getQuestionnaire.TotalCount = totalCount;
                    getQuestionnaire.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    getQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return getQuestionnaire;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireConfigurationForExternal.
        /// Always the First item to the list should be the column deatils for order by (With fieldTable as OrderBy for just user identification).
        /// And the next item should be the fieldMapping for order by Column specified above.
        /// </summary>
        /// <returns></returns>
        private List<QueryFieldMappingDTO> GetQuestionnaireConfigurationForExternal()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "QuestionnaireId",
                fieldAlias = "QuestionnaireId",
                fieldTable = "OrderBy",
                fieldType = "asc"
            }); 
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "QuestionnaireId",
                fieldAlias = "QuestionnaireId",
                fieldTable = "Q",
                fieldType = "int"
            });
            return fieldMappingList;
        }

        /// <summary>
        /// GetUnSelectedQuestionnaireWindowsByQuestionnaire.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnaireWindowDetailsDTO.</returns>
        public QuestionnaireWindowDetailsDTO GetUnSelectedQuestionnaireWindowsByQuestionnaire(int questionnaireId)
        {
            try
            {
                QuestionnaireWindowDetailsDTO QuestionnaireWindowDetails = new QuestionnaireWindowDetailsDTO();
                if (questionnaireId > 0)
                {
                    QuestionnaireWindowDetails.QuestionnaireWindow = this.questionnaireWindowRepository.GetQuestionnaireWindowsByQuestionnaire(questionnaireId).Where(x => x.IsSelected== false).ToList();
                    QuestionnaireWindowDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    QuestionnaireWindowDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return QuestionnaireWindowDetails;
                }
                else
                {
                    QuestionnaireWindowDetails.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.QuestionnaireID);
                    QuestionnaireWindowDetails.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return QuestionnaireWindowDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public QuestionnaireReminderRuleDetailsDTO GetReminderRulesByQuestionnaires(List<int> list_questionnaireIds)
        {
            try
            {
                QuestionnaireReminderRuleDetailsDTO QuestionnaireReminderRuleDetails = new QuestionnaireReminderRuleDetailsDTO();
                if (list_questionnaireIds.Count > 0)
                {
                    QuestionnaireReminderRuleDetails.QuestionnaireReminderRule = this.questionnaireReminderRuleRespository.GetAllReminderRulesByQuestionnaires(list_questionnaireIds);
                }
                QuestionnaireReminderRuleDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                QuestionnaireReminderRuleDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return QuestionnaireReminderRuleDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public QuestionnaireRegularReminderSettingsDTO GetRegularReminderSettingsForQuestionnaires(List<int> questionnaireIds)
        {
            try
            {
                QuestionnaireRegularReminderSettingsDTO regularReminderSettings = new QuestionnaireRegularReminderSettingsDTO();
                regularReminderSettings.Recurrence = this.questionnaireRegularReminderRecurrenceRepository.GetRegularReminderSettingsForQuestionnaires(questionnaireIds);
                regularReminderSettings.TimeRule = this.questionnaireRegularReminderTimeRuleRepository.GetRegularReminderTimeRuleForQuestionnaires(questionnaireIds);
                regularReminderSettings.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                regularReminderSettings.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return regularReminderSettings;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// GetReminderDetails For Trigger the InviteToComplete mail/sms. 
        /// Fetch all NotifyReminders with Pending mailstatus 
        /// </summary>
        /// <returns></returns>
        public RemindersForInviteToCompleteResponseDTO GetReminderDetailsForInviteToCompleteTrigger()
        {
            try
            {
                var remindersForInviteToComplete = this.notifyReminderRepository.GetReminderDetailsForInviteToCompleteTrigger();
                remindersForInviteToComplete = remindersForInviteToComplete.Where(x => !string.IsNullOrWhiteSpace(x.InviteToCompleteReceivers)).ToList();
                RemindersForInviteToCompleteResponseDTO response = new RemindersForInviteToCompleteResponseDTO();
                response.ReminderInviteDetails = remindersForInviteToComplete;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
