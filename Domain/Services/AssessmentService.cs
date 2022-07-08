// -----------------------------------------------------------------------
// <copyright file="AssessmentService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using Opeeka.PICS.Domain.Interfaces.Common;
using Microsoft.Extensions.Configuration;
using System.Web;
using System.Net;
using System.Linq;
using Microsoft.Graph;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Newtonsoft.Json;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.Text.RegularExpressions;

namespace Opeeka.PICS.Domain.Services
{
    public class AssessmentService : BaseService, IAssessmentService
    {
        /// <summary>
        /// questionnaireRepository.
        /// </summary>
        private readonly IQuestionnaireRepository questionnaireRepository;

        private readonly IAssessmentRepository assessmentRepository;
        private readonly IAssessmentHistoryRepository assessmentHistoryRepository;
        private readonly IAssessmentResponseRepository assessmentResponseRepository;
        private readonly IAssessmentNoteRepository assessmentNoteRepository;
        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;
        private readonly IMapper mapper;
        CRUDResponseDTO response = new CRUDResponseDTO();
        private readonly INoteRepository noteRepository;
        private readonly IAssessmentResponseNoteRepository assessmentResponseNoteRepository;

        private readonly IAssessmentEmailLinkRepository _assessmentEmailLinkRepository;
        private readonly IPersonSupportRepository _personSupportRepository;
        private readonly IEmailSender _emailSender;
        private readonly ISMSSender smsSender;
        private readonly IConfiguration _config;
        /// Initializes a new instance of the DataProtection.
        private readonly IDataProtection _dataProtector;
        private readonly IAssessmentStatusRepository assessmentStatusRepository;
        private readonly IResponseRepository responseRepository;
        private readonly IQuestionnaireNotifyRiskRuleConditionRepository questionnaireNotifyRiskRuleConditionRepository;
        private readonly IPersonRepository personRepository;
        private readonly INotificationTypeRepository notificationTypeRepository;
        private readonly INotifiationResolutionStatusRepository notifiationResolutionStatusRepository;
        private readonly INotificationLogRepository notificationLogRepository;
        private readonly INotifyRiskRepository notifyRiskRepository;
        private readonly IHttpContextAccessor httpContext;
        private readonly IAssessmentReasonRepository assessmentReasonRepository;
        /// Initializes a new instance of the Utility class.
        private readonly IUtility utility;
        private readonly IVoiceTypeRepository voiceTypeRepository;
        private readonly IQueryBuilder queryBuilder;
        private readonly ILookupRepository lookupRepository;
        /// <summary>
        /// Defines the logger.
        /// </summary>
        private readonly ILogger<AssessmentService> logger;
        public IQueue _queue { get; }
        private readonly IConfigurationRepository configurationRepository;
        private readonly IAssessmentEmailOtpRepository assessmentEmailOtpRepository;
        private readonly IAgencyRepository agencyRepository;
        private readonly IPersonQuestionnaireRepository personQuestionnaireRepository;
        private readonly IQuestionnaireSkipLogicRuleRepository questionnaireSkipLogicRuleRepository;
        private readonly IQuestionnaireSkipLogicRuleConditionRepository questionnaireSkipLogicRuleConditionRepository;
        private readonly IQuestionnaireSkipLogicRuleActionRepository questionnaireSkipLogicRuleActionRepository;
        private readonly IHelperRepository helperRepository;

        private readonly IQuestionnaireDefaultResponseRuleRepository questionnaireDefaultResponseRuleRepository;
        private readonly IQuestionnaireDefaultResponseRuleConditionRepository questionnaireDefaultResponseRuleConditionRepository;
        private readonly IQuestionnaireDefaultResponseRuleActionRepository questionnaireDefaultResponseRuleActionRepository;
        private readonly IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository;
        private readonly IPersonQuestionnaireScheduleService personQuestionnaireScheduleService;
        private readonly IAssessmentResponseAttachmentRepository assessmentResponseFileRepository;

        public AssessmentService(IQuestionnaireDefaultResponseRuleActionRepository questionnaireDefaultResponseRuleActionRepository, IQuestionnaireDefaultResponseRuleConditionRepository questionnaireDefaultResponseRuleConditionRepository, IQuestionnaireDefaultResponseRuleRepository questionnaireDefaultResponseRuleRepository,
            ISMSSender smsSender,
            IQuestionnaireSkipLogicRuleConditionRepository questionnaireSkipLogicRuleConditionRepository,
            IQuestionnaireSkipLogicRuleActionRepository questionnaireSkipLogicRuleActionRepository,
            IQuestionnaireSkipLogicRuleRepository questionnaireSkipLogicRuleRepository,
            IAssessmentResponseRepository assessmentResponseRepository,
            IAssessmentRepository assessmentRepository, IQuestionnaireRepository questionnaireRepository,
            ILogger<AssessmentService> logger, LocalizeService localizeService,
            IConfigurationRepository configRepo, IHttpContextAccessor httpContext,
            IMapper mapper, INoteRepository noteRepository,
            IAssessmentResponseNoteRepository assessmentResponseNoteRepository,
            IAssessmentStatusRepository assessmentStatusRepository,
            IAssessmentEmailLinkRepository assessmentEmailLinkRepository,
            IPersonSupportRepository personSupportRepository,
            IEmailSender emailSender, IConfiguration configuration,
            IDataProtection dataProtectorWrapper,
            IResponseRepository responseRepository,
            IQuestionnaireNotifyRiskRuleConditionRepository questionnaireNotifyRiskRuleConditionRepository,
            IPersonRepository personRepository,
            INotificationTypeRepository notificationTypeRepository,
            INotificationLogRepository notificationLogRepository,
            INotifiationResolutionStatusRepository notifiationResolutionStatusRepository,
            INotifyRiskRepository notifyRiskRepository,
            IAssessmentHistoryRepository assessmentHistoryRepository,
            IAssessmentNoteRepository assessmentNoteRepository,
            IAssessmentReasonRepository assessmentReasonRepository, IUtility utility,
            IQueue queue, IVoiceTypeRepository voiceTypeRepository,
            IAssessmentEmailOtpRepository assessmentEmailOtpRepository,
            IAgencyRepository agencyRepository, IPersonQuestionnaireRepository personQuestionnaireRepository, IHelperRepository helperRepository,
            IQueryBuilder querybuild,
            IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository,
            IPersonQuestionnaireScheduleService personQuestionnaireScheduleService,ILookupRepository lookupRepository,
            IAssessmentResponseAttachmentRepository assessmentResponseFileRepository
            )
            : base(configRepo, httpContext)
        {
            this.questionnaireRepository = questionnaireRepository;
            this.assessmentRepository = assessmentRepository;
            this.assessmentResponseRepository = assessmentResponseRepository;
            this.logger = logger;
            this.localize = localizeService;
            this.httpContext = httpContext;
            this.mapper = mapper;
            this.noteRepository = noteRepository;
            this.assessmentResponseNoteRepository = assessmentResponseNoteRepository;
            this.assessmentStatusRepository = assessmentStatusRepository;
            this._config = configuration;
            this._dataProtector = dataProtectorWrapper;
            this._emailSender = emailSender;
            this._assessmentEmailLinkRepository = assessmentEmailLinkRepository;
            this._personSupportRepository = personSupportRepository;
            this.responseRepository = responseRepository;
            this.questionnaireNotifyRiskRuleConditionRepository = questionnaireNotifyRiskRuleConditionRepository;
            this.personRepository = personRepository;
            this.notificationTypeRepository = notificationTypeRepository;
            this.notifiationResolutionStatusRepository = notifiationResolutionStatusRepository;
            this.notificationLogRepository = notificationLogRepository;
            this.notifyRiskRepository = notifyRiskRepository;
            this.assessmentHistoryRepository = assessmentHistoryRepository;
            this.assessmentNoteRepository = assessmentNoteRepository;
            this.assessmentReasonRepository = assessmentReasonRepository;
            this.utility = utility;
            this._queue = queue;
            this.configurationRepository = configRepo;
            this.voiceTypeRepository = voiceTypeRepository;
            this.assessmentEmailOtpRepository = assessmentEmailOtpRepository;
            this.agencyRepository = agencyRepository;
            this.personQuestionnaireRepository = personQuestionnaireRepository;
            this.questionnaireSkipLogicRuleRepository = questionnaireSkipLogicRuleRepository;
            this.questionnaireSkipLogicRuleConditionRepository = questionnaireSkipLogicRuleConditionRepository;
            this.questionnaireSkipLogicRuleActionRepository = questionnaireSkipLogicRuleActionRepository;
            this.helperRepository = helperRepository;
            this.queryBuilder = querybuild;
            this.smsSender = smsSender;
            this.questionnaireDefaultResponseRuleRepository = questionnaireDefaultResponseRuleRepository;
            this.questionnaireDefaultResponseRuleConditionRepository = questionnaireDefaultResponseRuleConditionRepository;
            this.questionnaireDefaultResponseRuleActionRepository = questionnaireDefaultResponseRuleActionRepository;
            this.personQuestionnaireScheduleRepository = personQuestionnaireScheduleRepository;
            this.personQuestionnaireScheduleService = personQuestionnaireScheduleService;
            this.lookupRepository = lookupRepository;
            this.assessmentResponseFileRepository = assessmentResponseFileRepository;
        }

        /// <summary>
        /// GetQuestions.
        /// </summary>
        /// <param name="id">Questionnaire ID</param>
        /// <returns>QuestionsResponseDTO.</returns>
        public QuestionsResponseDTO GetQuestions(int id)
        {
            try
            {
                QuestionsResponseDTO objQuestionsResponse = new QuestionsResponseDTO();

                var response = this.questionnaireRepository.GetQuestions(id);
                var SkippedDetails = this.questionnaireRepository.GetQuestionsSkippedActionDetails(id);
                objQuestionsResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                objQuestionsResponse.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                objQuestionsResponse.Questions = response;
                objQuestionsResponse.QuestionsSkippedActionDetails = SkippedDetails;
                return objQuestionsResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAssessmentDetails.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">ID of a Questionnaire.</param>
        /// <param name="date">date.</param>
        /// <param name="pageNumber"></param>
        /// <param name="totalCount"></param>
        /// <returns>AssessmentDetailsResponseDTO.</returns>
        public AssessmentDetailsResponseDTO GetAssessmentDetails(Guid personIndex, int questionnaireId, DateTime? date, UserTokenDetails userTokenDetails, int pageNumber, long totalCount, int assessmentId)
        {
            try
            {

                int pageSize = Convert.ToInt32(this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.AssesmentPageSize, userTokenDetails.AgencyID)?.Value);

                AssessmentDetailsResponseDTO objAssessmentDetailsResponseDTO = new AssessmentDetailsResponseDTO();
                #region calculating PageNumber for Reverse order
                if (totalCount != 0 && pageNumber != 0 && assessmentId == 0)
                {
                    objAssessmentDetailsResponseDTO.PageNumber = pageNumber;//just returning the received pageNumber itself.
                    var totalPages = totalCount / pageSize;  //calculating totalPages
                    var remainder = totalCount % pageSize;//calculating remainders 
                    totalPages = remainder > 0 ? totalPages + 1 : totalPages;//if reminder greater than 0 then add 1 to totalPages 
                    pageNumber = Convert.ToInt32(totalPages - pageNumber + 1);//calculating pageNumber 
                }
                else
                {
                    #region DeeplinkWithAssessmentID
                    if (assessmentId > 0)
                    {
                        var responseResult = this.assessmentRepository.GetAssessmentPageNumberByAssessmentID(personIndex, questionnaireId, assessmentId, userTokenDetails.AgencyID);
                        if (responseResult != null && responseResult?.TotalCount > 0)
                        {
                            objAssessmentDetailsResponseDTO.TotalCount = responseResult.TotalCount;
                            var totalPages = responseResult.TotalCount / pageSize;  //calculating totalPages
                            var remainder = responseResult.TotalCount % pageSize;//calculating remainders 
                            totalPages = remainder > 0 ? totalPages + 1 : totalPages;//if reminder greater than 0 then add 1 to totalPages 

                            var pagenumber = responseResult.RowNumber / pageSize;//calculating pagenumber 
                            objAssessmentDetailsResponseDTO.PageNumber = Convert.ToInt32(totalPages - pagenumber);//calculating Reverse pageNumber 
                            pageNumber = pagenumber + 1;//increment the actual pageNumber since the first pageNo starts from 0 from above pagenumber calculation.                            
                        }
                    }
                    #endregion
                    //On initial load both pageNumber and totalCount will be 0, set pageNumber to 1
                    pageNumber = pageNumber == 0 ? 1 : pageNumber;
                }
                #endregion calculating PageNumber for Reverse order

                var sharedAssessmentIDs = this.GetSharedAssessmentIDs(personIndex, questionnaireId, userTokenDetails.AgencyID);
                var helpersAssessmentIDs = string.Empty;
                if (string.IsNullOrEmpty(sharedAssessmentIDs))
                {
                    helpersAssessmentIDs = this.GetHelperAssessmentIDs(personIndex, questionnaireId, userTokenDetails);
                }

                var response = this.assessmentRepository.GetAssessmentDetails(personIndex, questionnaireId, date, sharedAssessmentIDs, userTokenDetails.AgencyID, helpersAssessmentIDs, pageNumber, pageSize);
                var count = response != null && response?.Count != 0 ? response[0].TotalCount : 0;
                objAssessmentDetailsResponseDTO.TotalCount = count;
                objAssessmentDetailsResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                objAssessmentDetailsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                objAssessmentDetailsResponseDTO.AssessmentDetails = response;
                return objAssessmentDetailsResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAssessmentValues.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">Id of a Questionnaire.</param>
        /// <param name="assessmentIDs">Ids of a Assesments.</param>
        /// <returns>AssessmentResponseDTO.</returns>
        public AssessmentResponseDTO GetAssessmentValues(Guid personIndex, int questionnaireId, UserTokenDetails userTokenDetails, string assessmentIDs)
        {
            try
            {
                AssessmentResponseDTO objAssessmentDetailsResponseDTO = new AssessmentResponseDTO();
                var sharedAssessmentIDs = this.GetSharedAssessmentIDs(personIndex, questionnaireId, userTokenDetails.AgencyID);
                var helpersAssessmentIDs = string.Empty;
                if(string.IsNullOrEmpty(assessmentIDs))
                {
                    objAssessmentDetailsResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    objAssessmentDetailsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    objAssessmentDetailsResponseDTO.AssessmentValues = new List<AssessmentValuesDTO>(); 
                    return objAssessmentDetailsResponseDTO;
                }
                if (string.IsNullOrEmpty(sharedAssessmentIDs))
                {
                    helpersAssessmentIDs = this.GetHelperAssessmentIDs(personIndex, questionnaireId, userTokenDetails);
                }

                var response = this.assessmentResponseRepository.GetAssessmentValues(personIndex, questionnaireId, sharedAssessmentIDs, userTokenDetails.AgencyID, helpersAssessmentIDs, assessmentIDs);


                if (response?.Count > 0)
                {
                    if (response?.Where(x => x.Isconfidential).Count() > 0)
                    {
                        var isRequiredConfidential = response.Where(x => x.IsRequiredConfidential == true).FirstOrDefault();
                        var isOtherConfidential = response.Where(x => x.IsOtherConfidential == true).FirstOrDefault();
                        var isPersonRequestedConfidential = response.Where(x => x.IsPersonRequestedConfidential == true).FirstOrDefault();
                        if (isRequiredConfidential != null)
                        {
                            response = response.Select(x => { if (x.QuestionnaireItemID + "-" + x.PersonSupportID == isRequiredConfidential.QuestionnaireItemID + "-" + isRequiredConfidential.PersonSupportID) x.IsRequiredConfidential = true; return x; }).ToList();
                        }
                        if (isOtherConfidential != null)
                        {
                            response = response.Select(x => { if (x.QuestionnaireItemID + "-" + x.PersonSupportID == isOtherConfidential.QuestionnaireItemID + "-" + isOtherConfidential.PersonSupportID) x.IsOtherConfidential = true; return x; }).ToList();
                        }
                        if (isPersonRequestedConfidential != null)
                        {
                            response = response.Select(x => { if (x.QuestionnaireItemID + "-" + x.PersonSupportID == isPersonRequestedConfidential.QuestionnaireItemID + "-" + isPersonRequestedConfidential.PersonSupportID) x.IsPersonRequestedConfidential = true; return x; }).ToList();
                        }
                    }
                    else
                    {
                        response = response.Select(x => { x.IsRequiredConfidential = false; x.IsOtherConfidential = false; x.IsPersonRequestedConfidential = false; return x; }).ToList();
                    }
                }

                objAssessmentDetailsResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                objAssessmentDetailsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                objAssessmentDetailsResponseDTO.AssessmentValues = response;
                return objAssessmentDetailsResponseDTO;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireDefaultResponseValues.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">Id of a Questionnaire.</param>
        /// <param name="agencyID">Id of a agency.</param>
        /// <returns>QuestionnaireDefaultResponseValuesDTO.</returns>
        public QuestionnaireDefaultResponseValuesDTO GetQuestionnaireDefaultResponseValues(Guid personIndex, int questionnaireId, long agencyID)
        {
            try
            {
                QuestionnaireDefaultResponseValuesDTO objQuestionsResponse = new QuestionnaireDefaultResponseValuesDTO();
                List<QuestionnaireDefaultResponseDTO> QuestionnaireDefaultResponseList = new List<QuestionnaireDefaultResponseDTO>();
                var response = this.questionnaireDefaultResponseRuleRepository.GetQuestionnaireDefaultResponse(questionnaireId);
                if (response != null && response.Count > 0 && response[0].HasDefaultResponseRule)
                {
                    foreach (var rule in response)
                    {
                        rule.QuestionnaireDefaultResponseRuleActions = this.questionnaireDefaultResponseRuleActionRepository.GetQuestionnaireDefaultResponseAction(rule.QuestionnaireDefaultResponseRuleID);
                        rule.QuestionnaireDefaultResponseRuleConditions = this.questionnaireDefaultResponseRuleConditionRepository.GetQuestionnaireDefaultResponseCondition(rule.QuestionnaireDefaultResponseRuleID);

                        if (rule.QuestionnaireDefaultResponseRuleConditions.Count > 0)
                        {
                            var assessmentResponses = this.assessmentResponseRepository.GetAssessmentResponseForDefualtResponseValue(rule.QuestionnaireDefaultResponseRuleConditions.FirstOrDefault().QuestionnaireID,
                                personIndex, rule.QuestionnaireDefaultResponseRuleConditions.Select(x => x.QuestionnaireItemID).ToList());
                            bool? notify = null;
                            foreach (var item in rule.QuestionnaireDefaultResponseRuleConditions)
                            {
                                var responses = assessmentResponses.Where(x => x.QuestionnaireItemID == item.QuestionnaireItemID).ToList();
                                if (responses != null && responses.Count > 0)
                                {
                                    foreach (var newitem in responses)
                                    {
                                        var responseDto = this.responseRepository.GetResponse(newitem.ResponseID).Result;
                                        switch (item.ComparisonOperator)
                                        {
                                            case PCISEnum.ComparisonOperator.Equal:
                                                if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                                {
                                                    notify = (notify ?? true) && responseDto?.Value == item?.ComparisonValue && responseDto?.ListOrder == item?.ListOrder;
                                                }
                                                else
                                                {
                                                    notify = (notify ?? false) || (responseDto?.Value == item?.ComparisonValue && responseDto?.ListOrder == item?.ListOrder);
                                                }
                                                break;
                                            case PCISEnum.ComparisonOperator.EqualEqual:
                                                if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                                {
                                                    notify = (notify ?? true) && responseDto?.Value == item?.ComparisonValue && responseDto?.ListOrder == item?.ListOrder;
                                                }
                                                else

                                                {
                                                    notify = (notify ?? false) || (responseDto?.Value == item?.ComparisonValue && responseDto?.ListOrder == item?.ListOrder);
                                                }
                                                break;
                                            case PCISEnum.ComparisonOperator.GreaterThan:
                                                if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                                {
                                                    notify = (notify ?? true) && responseDto?.Value > item?.ComparisonValue;
                                                }
                                                else
                                                {
                                                    notify = (notify ?? false) || responseDto?.Value > item?.ComparisonValue;
                                                }
                                                break;
                                            case PCISEnum.ComparisonOperator.GreaterThanOrEqual:
                                                if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                                {
                                                    notify = (notify ?? true) && responseDto?.Value >= item?.ComparisonValue;
                                                }
                                                else
                                                {
                                                    notify = (notify ?? false) || responseDto?.Value >= item?.ComparisonValue;
                                                }
                                                break;
                                            case PCISEnum.ComparisonOperator.LessThan:
                                                if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                                {
                                                    notify = (notify ?? true) && responseDto?.Value < item?.ComparisonValue;
                                                }
                                                else
                                                {
                                                    notify = (notify ?? false) || responseDto?.Value < item?.ComparisonValue;
                                                }
                                                break;
                                            case PCISEnum.ComparisonOperator.LessThanOrEqual:
                                                if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                                {
                                                    notify = (notify ?? true) && responseDto?.Value <= item?.ComparisonValue;
                                                }
                                                else
                                                {
                                                    notify = (notify ?? false) || responseDto?.Value <= item?.ComparisonValue;
                                                }
                                                break;
                                            case PCISEnum.ComparisonOperator.NotEqual:
                                                if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                                {
                                                    notify = (notify ?? true) && responseDto?.Value != item?.ComparisonValue;
                                                }
                                                else
                                                {
                                                    notify = (notify ?? false) || responseDto?.Value != item?.ComparisonValue;
                                                }
                                                break;
                                            default:
                                                notify = null;
                                                break;
                                        }
                                    }
                                }
                            }
                            if (notify == true)
                            {
                                foreach (var item in rule.QuestionnaireDefaultResponseRuleActions)
                                {
                                    QuestionnaireDefaultResponseDTO QuestionnaireDefaultResponse = new QuestionnaireDefaultResponseDTO();
                                    QuestionnaireDefaultResponse.QuestionnaireItemID = item.QuestionnaireItemID.Value;
                                    QuestionnaireDefaultResponse.DefaultResponseID = item.DefaultResponseID;
                                    QuestionnaireDefaultResponse.DefaultValue = item.DefaultValue;
                                    QuestionnaireDefaultResponse.ItemID = item.ItemID;
                                    QuestionnaireDefaultResponse.QuestionnaireID = rule.QuestionnaireID;
                                    QuestionnaireDefaultResponseList.Add(QuestionnaireDefaultResponse);
                                }

                            }
                        }
                    }
                }

                objQuestionsResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                objQuestionsResponse.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                objQuestionsResponse.QuestionnaireDefaultResponses = QuestionnaireDefaultResponseList;
                return objQuestionsResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddAssessmentProgress
        /// </summary>
        /// <param name="assessmentProgressData"></param>
        /// <param name="updateUserID"></param>
        /// <returns>CRUDResponseDTO</returns>
        public AddAssessmentResponseDTO AddAssessmentProgress(List<AssessmentProgressInputDTO> assessmentProgressData, int updateUserID, bool IsEmailAssessment, long loggedInAgencyID)
        {
            try
            {
                int assessmentStatusID = 0;
                long personQuestionnaireID = 0;
                Assessment assessmentResult = new Assessment();
                AssessmentResponse assessmentResponseResult = new AssessmentResponse();
                AddAssessmentResponseDTO response = new AddAssessmentResponseDTO();
                List<AssessmentResponse> listResponse = new List<AssessmentResponse>();
                AssessmentReason assessmentReason = new AssessmentReason();
                var assessmentStatus = this.assessmentStatusRepository.GetAllAssessmentStatus();
                var saveStatus = assessmentStatus.Where(e => e.Name == PCISEnum.AssessmentStatus.InProgress).FirstOrDefault().AssessmentStatusID;
                var submitStatus = assessmentStatus.Where(e => e.Name == PCISEnum.AssessmentStatus.Submitted).FirstOrDefault().AssessmentStatusID;
                if (assessmentProgressData != null && assessmentProgressData.Count != 0)
                {
                    personQuestionnaireID = this.assessmentRepository.GetPersonQuestionnaireID(assessmentProgressData.FirstOrDefault().PersonIndex, assessmentProgressData.FirstOrDefault().QuestionnaireID);
                    var personIndex = assessmentProgressData.FirstOrDefault().PersonIndex;
                    var person = personRepository.GetPerson(personIndex);
                    var assessmentReasons = this.assessmentReasonRepository.GetAllAssessmentReason();
                    //foreach (AssessmentProgressInputDTO assessmentProgressInputDTO in assessmentProgressData)
                    //{
                    // var person = personRepository.GetPerson(assessmentProgressInputDTO.PersonIndex);
                    var assessmentProgressInputDTO = assessmentProgressData.FirstOrDefault();
                    if (!this.personRepository.IsValidPersonInAgencyForQuestionnaire(person.PersonID, person.AgencyID, assessmentProgressInputDTO.QuestionnaireID, loggedInAgencyID, IsEmailAssessment))
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                        response.AssessmentID = 0;
                        return response;
                    }
                    int? eventNoteUpdatedBy = updateUserID;
                    int? submittedBy = null;


                    assessmentReason = assessmentReasons.Find(e => e.AssessmentReasonID == assessmentProgressInputDTO.AssessmentReasonID);

                    if (assessmentProgressInputDTO.AssessmentStatus.ToLower() == PCISEnum.Constants.Save.ToLower())
                    {
                        assessmentStatusID = saveStatus;
                    }
                    if (assessmentProgressInputDTO.AssessmentStatus.ToLower() == PCISEnum.Constants.Submit.ToLower())
                    {
                        assessmentStatusID = submitStatus;
                        submittedBy = updateUserID;
                    }
                    if (assessmentReason.Name.ToLower() != PCISEnum.AssessmentReason.Trigger.ToLower())
                    {
                        assessmentProgressInputDTO.EventDate = null;
                        assessmentProgressInputDTO.EventNotes = null;
                        eventNoteUpdatedBy = null;
                    }
                    var voicetype = this.voiceTypeRepository.GetVoiceType(assessmentProgressInputDTO.VoiceTypeID)?.Name;
                    if ((voicetype == PCISEnum.VoiceType.Communimetric || voicetype == PCISEnum.VoiceType.Consumer) && assessmentProgressInputDTO?.VoiceTypeFKID > 0)
                        assessmentProgressInputDTO.VoiceTypeFKID = null;
                    var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                    var assessmentDTO = new AssessmentDTO
                    {
                        AssessmentID = (assessmentProgressInputDTO.AssessmentID == null) ? 0 : (int)assessmentProgressInputDTO.AssessmentID,
                        PersonQuestionnaireID = personQuestionnaireID,
                        VoiceTypeID = assessmentProgressInputDTO.VoiceTypeID,
                        DateTaken = assessmentProgressInputDTO.DateTaken,
                        ReasoningText = assessmentProgressInputDTO.ReasoningText,
                        AssessmentReasonID = assessmentProgressInputDTO.AssessmentReasonID,
                        AssessmentStatusID = assessmentStatusID,
                        PersonQuestionnaireScheduleID = null,
                        IsUpdate = true,
                        Approved = null,
                        CloseDate = assessmentProgressInputDTO.CloseDate,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = updateUserID,
                        NoteUpdateDate = DateTime.UtcNow,
                        NoteUpdateUserID = updateUserID,
                        EventDate = assessmentProgressInputDTO.EventDate.HasValue ? assessmentProgressInputDTO.EventDate.Value : assessmentProgressInputDTO.EventDate,
                        EventNotes = assessmentProgressInputDTO.EventNotes,
                        VoiceTypeFKID = assessmentProgressInputDTO.VoiceTypeFKID,
                        EventNoteUpdatedBy = eventNoteUpdatedBy,
                    };

                    if (assessmentDTO.AssessmentID == 0)
                    {
                        assessmentDTO.SubmittedUserID = submittedBy;
                        Assessment assessment = new Assessment();
                        this.mapper.Map<AssessmentDTO, Assessment>(assessmentDTO, assessment);
                        assessmentResult = this.assessmentRepository.AddAssessment(assessment);
                    }
                    else
                    {
                        var assessment = this.assessmentRepository.GetAssessment(assessmentDTO.AssessmentID).Result;
                        assessmentDTO.SubmittedUserID = submittedBy.HasValue ? submittedBy : assessment.SubmittedUserID;
                        assessmentDTO.NotifyReminderID = assessment.NotifyReminderID.HasValue ? assessment.NotifyReminderID : null;
                        if (assessment.AssessmentStatusID == assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.EmailSent).FirstOrDefault().AssessmentStatusID && !IsEmailAssessment)
                        {
                            assessmentDTO.AssessmentStatusID = assessment.AssessmentStatusID;
                        }
                        if (assessment != null && assessment.IsRemoved)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.AssessmentDeleted;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.AssessmentDeleted);
                        }
                        else
                        {
                            if (assessment.ReasoningText != assessmentDTO.ReasoningText)
                            {
                                assessmentDTO = this.SetNoteUpdateUser(assessmentDTO, updateUserID);
                            }
                            if (assessment.EventNotes != assessmentDTO.EventNotes)
                            {
                                assessmentDTO.EventNoteUpdatedBy = eventNoteUpdatedBy;
                            }
                            this.mapper.Map<AssessmentDTO, Assessment>(assessmentDTO, assessment);
                            assessmentResult = this.assessmentRepository.UpdateAssessment(assessment);
                        }

                    }

                    if (assessmentResult != null && assessmentResult.AssessmentID > 0)
                    {
                        response.AssessmentID = assessmentResult.AssessmentID;
                        var responseValueTypes = this.lookupRepository?.GetAllResponseValueTypes();
                        var imageValueTypeIds = responseValueTypes?.Where(x => x.Name == PCISEnum.ResponseValueType.Doodle
                        || x.Name == PCISEnum.ResponseValueType.Signature).Select(x => x.ResponseValueTypeID).ToList();
                        var fileValueTypeIds = responseValueTypes?.Where(x => x.Name == PCISEnum.ResponseValueType.Attachment)
                            .Select(x => x.ResponseValueTypeID).ToList();
                        var oldResponse = assessmentResponseRepository.GetAssessmentResponse(assessmentResult.AssessmentID).Result;

                        var removedAssessment = oldResponse.Where(x => !assessmentProgressInputDTO.AssessmentResponses.Select(y => y.AssessmentResponseID).ToList().Contains(x.AssessmentResponseID)).ToList();
                        if (removedAssessment != null && removedAssessment.Count > 0)
                        {
                            List<AssessmentResponse> updatedResponseList = new List<AssessmentResponse>();
                            foreach (var item in removedAssessment)
                            {
                                AssessmentResponse newResponse = new AssessmentResponse();
                                this.mapper.Map<AssessmentResponsesDTO, AssessmentResponse>(item, newResponse);
                                newResponse.IsRemoved = true;
                                newResponse.ItemID = item.ItemId == 0 ? null : (int?)item.ItemId;
                                newResponse.UpdateDate = DateTime.UtcNow;
                                updatedResponseList.Add(newResponse);
                            }
                            var result = this.assessmentResponseRepository.UpdateBulkAssessmentResponse(updatedResponseList);
                        }
                        List<Note> removableNotesList = new List<Note>();
                        List<Note> noteListToUpdate = new List<Note>();

                        List<AssessmentResponseAttachment> fileListToAdd = new List<AssessmentResponseAttachment>();
                        List<AssessmentResponseAttachment> fileListToUpdate = new List<AssessmentResponseAttachment>();

                        List<AssessmentResponse> assessmentResponseListToAdd = new List<AssessmentResponse>();
                        List<AssessmentResponse> assessmentResponseListToUpdate = new List<AssessmentResponse>();
                        List<AssessmentResponse> assessmentResponseList = new List<AssessmentResponse>();
                        List<AssessmentResponse> assessmentResponseChildItemsToInsert = new List<AssessmentResponse>();
                        List<AssessmentResponse> assessmentResponseChildItemsToUpdate = new List<AssessmentResponse>();

                        foreach (AssessmentResponseInputDTO assessmentResponseInputDTO in assessmentProgressInputDTO.AssessmentResponses)
                        {                            
                            var assessmentResponseDTO = new AssessmentResponsesDTO
                            {
                                AssessmentResponseID = (assessmentResponseInputDTO.AssessmentResponseID == null) ? 0 : (int)assessmentResponseInputDTO.AssessmentResponseID,
                                AssessmentID = assessmentResult.AssessmentID,
                                PersonSupportID = assessmentResponseInputDTO.PersonSupportID,
                                ResponseID = assessmentResponseInputDTO.ResponseID,
                                ItemResponseBehaviorID = assessmentResponseInputDTO.ItemResponseBehaviorID == 0 ? null : (int?)assessmentResponseInputDTO.ItemResponseBehaviorID,
                                IsRequiredConfidential = assessmentResponseInputDTO.IsRequiredConfidential,
                                IsPersonRequestedConfidential = assessmentResponseInputDTO.IsPersonRequestedConfidential,
                                IsOtherConfidential = assessmentResponseInputDTO.IsOtherConfidential,
                                IsRemoved = false,
                                UpdateDate = DateTime.UtcNow,
                                UpdateUserID = updateUserID,
                                QuestionnaireItemID = assessmentResponseInputDTO.QuestionnaireItemID,
                                IsCloned = assessmentResponseInputDTO.IsCloned,
                                CaregiverCategory = assessmentResponseInputDTO.CaregiverCategory,
                                Priority = null,
                                Value = assessmentResponseInputDTO.Value,
                            };
                                                        
                            AssessmentResponse assessmentResponse = new AssessmentResponse();
                            this.mapper.Map<AssessmentResponsesDTO, AssessmentResponse>(assessmentResponseDTO, assessmentResponse);
                            assessmentResponse.ItemID = assessmentResponseInputDTO.ItemID == 0 ? null : (int?)assessmentResponseInputDTO.ItemID;
                            assessmentResponse.ParentAssessmentResponse = null;
                            if (assessmentResponseDTO.AssessmentResponseID == 0)
                            {
                                assessmentResponse.AssessmentResponseGuid = Guid.NewGuid();
                                assessmentResponseInputDTO.AssessmentResponseGUID = assessmentResponse.AssessmentResponseGuid;
                                assessmentResponseListToAdd.Add(assessmentResponse);
                            }
                            else
                            {
                                assessmentResponseListToUpdate.Add(assessmentResponse);
                            }
                            //Processing imageTypeResponses - doodle and signatures
                            if (imageValueTypeIds != null && imageValueTypeIds.Contains(assessmentResponseInputDTO.ResponseValueTypeId))
                            {
                                if (assessmentResponseDTO?.AssessmentResponseID > 0)
                                {
                                    var existingAssessmentResponse = oldResponse.Where(x => x.AssessmentResponseID == assessmentResponseDTO.AssessmentResponseID).FirstOrDefault();
                                    assessmentResponse.AssessmentResponseGuid = existingAssessmentResponse.AssessmentResponseGuid;
                                    assessmentResponse.Value = existingAssessmentResponse.Value;
                                }
                                string imageblobFileName = string.Format(PCISEnum.AssessmentImageResponses.FileName, assessmentResponse.AssessmentResponseGuid.ToString());
                                var resultURL = UploadAndGetImageURLFromAzureBlob(person.AgencyID, assessmentResponseInputDTO.ImageValue, assessmentResponse, imageblobFileName).Result;
                                assessmentResponse.Value = resultURL;
                            }
                        }
                        if (assessmentResponseListToUpdate.Count > 0)
                        {
                            var result = this.assessmentResponseRepository.UpdateBulkAssessmentResponse(assessmentResponseListToUpdate);
                        }
                        if (assessmentResponseListToAdd.Count > 0)
                        {
                            var result = this.assessmentResponseRepository.AddBulkAssessmentResponse(assessmentResponseListToAdd);
                        }
                        List<int> assessmentResponseIDs = new List<int>();
                        List<Guid> assessmentResponseGuids = new List<Guid>();
                        List<AssessmentResponse> assessmentResponsesWithImage= new List<AssessmentResponse>();
                        assessmentResponseIDs = assessmentProgressInputDTO.AssessmentResponses.Where(x => x.AssessmentResponseID != 0 && x.AssessmentResponseID != null).Select(x => x.AssessmentResponseID ?? 0).ToList();
                        assessmentResponseGuids = assessmentProgressInputDTO.AssessmentResponses.Where(x => x.AssessmentResponseGUID != Guid.Empty && x.AssessmentResponseGUID != null).Select(x => x.AssessmentResponseGUID ?? Guid.Empty).ToList();
                        var assessmentResponseListByID = assessmentResponseRepository.GetAssessmentResponseList(assessmentResponseIDs).Result;
                        var assessmentResponseListByGuid = assessmentResponseRepository.GetAssessmentResponseListByGUID(assessmentResponseGuids).Result;

                        UpdateConfidentialityFlags(assessmentResult.AssessmentID, person.PersonID, assessmentProgressInputDTO.QuestionnaireID, IsEmailAssessment);
                        //File Extension for Attachments
                        var fileExtensionPattern = string.Empty;
                        var fileTypeConfig = this.GetConfigurationByName(PCISEnum.ConfigurationParameters.AssessmentFileTypes);
                        if (!string.IsNullOrEmpty(fileTypeConfig?.Value))
                        {
                            var fileTypes = fileTypeConfig.Value.ToUpper().Replace(".", "").Replace(",", "|");
                            fileExtensionPattern = string.Format(PCISEnum.AssessmentImageResponses.FileTypeExtensionPattern, fileTypes);
                        }
                        foreach (AssessmentResponseInputDTO assessmentResponseInputDTO in assessmentProgressInputDTO.AssessmentResponses)
                        {
                            AssessmentResponse assessmentResponse = new AssessmentResponse();
                            if (assessmentResponseInputDTO.AssessmentResponseID != 0 && assessmentResponseInputDTO.AssessmentResponseID != null)
                            {
                                assessmentResponse = assessmentResponseListByID.Where(x => x.AssessmentResponseID == assessmentResponseInputDTO.AssessmentResponseID).FirstOrDefault();
                            }
                            else
                            {
                                assessmentResponse = assessmentResponseListByGuid.Where(x => x.AssessmentResponseGuid == assessmentResponseInputDTO.AssessmentResponseGUID).FirstOrDefault();
                            }

                            //Processing Child ItemResponses
                            ProccessAssessmentResponseChilds(updateUserID, assessmentResponseChildItemsToInsert, assessmentResponseChildItemsToUpdate, assessmentResponseInputDTO, assessmentResponse);

                            listResponse.Add(assessmentResponse);
                            if (assessmentResponse != null && assessmentResponse.AssessmentResponseID > 0)
                            {
                                #region AssessmentNotes
                                List<Note> notes = new List<Note>();
                                notes = this.noteRepository.GetNotesByAssessmentResponseID(assessmentResponse.AssessmentResponseID);
                                //}
                                if (assessmentResponseInputDTO.AssessmentResponseNotes.Count > 0)
                                {
                                    var removableNotes = notes.Where(x => !assessmentResponseInputDTO.AssessmentResponseNotes.Select(y => y.NoteID).ToList().Contains(x.NoteID)).ToList();

                                    foreach (Note note in removableNotes)
                                    {
                                        note.IsRemoved = true;
                                        note.UpdateDate = DateTime.UtcNow;
                                        note.UpdateUserID = updateUserID;
                                    }

                                    if (removableNotes.Count > 0)
                                    {
                                        noteListToUpdate.AddRange(removableNotes);
                                    }
                                    List<Note> noteListToAdd = new List<Note>();
                                    foreach (AssessmentResponseNoteInputDTO assessmentResponseNoteInputDTO in assessmentResponseInputDTO.AssessmentResponseNotes)
                                    {
                                        Note note = new Note();
                                        if (assessmentResponseNoteInputDTO.NoteID == null || assessmentResponseNoteInputDTO.NoteID == 0)
                                        {
                                            var noteDTO = new NoteDTO
                                            {
                                                NoteID = 0,
                                                NoteText = assessmentResponseNoteInputDTO.NoteText,
                                                IsConfidential = assessmentResponseNoteInputDTO.IsConfidential,
                                                IsRemoved = false,
                                                UpdateDate = DateTime.UtcNow,
                                                UpdateUserID = updateUserID,
                                                VoiceTypeFKID = IsEmailAssessment ? (assessmentProgressInputDTO.VoiceTypeFKID == null ? person.PersonID : assessmentProgressInputDTO.VoiceTypeFKID) : null,
                                                AddedByVoiceTypeID = assessmentProgressInputDTO.VoiceTypeID,
                                                NoteGUID = Guid.NewGuid()
                                            };
                                            assessmentResponseInputDTO.canAddResponseNote = true;
                                            this.mapper.Map<NoteDTO, Note>(noteDTO, note);
                                            noteListToAdd.Add(note);
                                        }
                                        else
                                        {
                                            var oldNote = notes.Where(x => x.NoteID == assessmentResponseNoteInputDTO.NoteID).FirstOrDefault();
                                            if (oldNote != null)
                                            {
                                                if (oldNote.NoteText != assessmentResponseNoteInputDTO.NoteText || oldNote.IsConfidential != assessmentResponseNoteInputDTO.IsConfidential)
                                                {
                                                    var noteDTO = new NoteDTO
                                                    {
                                                        NoteID = assessmentResponseNoteInputDTO.NoteID.HasValue ? assessmentResponseNoteInputDTO.NoteID.Value : 0,
                                                        NoteText = assessmentResponseNoteInputDTO.NoteText,
                                                        IsConfidential = assessmentResponseNoteInputDTO.IsConfidential,
                                                        IsRemoved = false,
                                                        UpdateDate = DateTime.UtcNow,
                                                        UpdateUserID = updateUserID,
                                                        AddedByVoiceTypeID = oldNote.AddedByVoiceTypeID,
                                                        VoiceTypeFKID = oldNote.VoiceTypeFKID
                                                    };
                                                    this.mapper.Map<NoteDTO, Note>(noteDTO, note);
                                                    noteListToUpdate.Add(note);
                                                }
                                            }
                                            else
                                            {
                                                var noteDTO = new NoteDTO
                                                {
                                                    NoteID = 0,
                                                    NoteText = assessmentResponseNoteInputDTO.NoteText,
                                                    IsConfidential = assessmentResponseNoteInputDTO.IsConfidential,
                                                    IsRemoved = false,
                                                    UpdateDate = DateTime.UtcNow,
                                                    UpdateUserID = updateUserID,
                                                    NoteGUID = Guid.NewGuid()
                                                };
                                                assessmentResponseInputDTO.canAddResponseNote = true;
                                                this.mapper.Map<NoteDTO, Note>(noteDTO, note);
                                                noteListToAdd.Add(note);
                                            }
                                        }
                                    }
                                    this.noteRepository.AddBulkAsync(noteListToAdd);
                                    List<AssessmentResponseNote> assessmentResponseNoteList = new List<AssessmentResponseNote>();
                                    List<Guid> noteListByGuid = noteListToAdd.Select(x => x.NoteGUID).ToList();
                                    var noteList = this.noteRepository.GetNotesAsync(noteListByGuid).ToList();
                                    foreach (var note in noteListToAdd)
                                    {
                                        var noteData = noteList.Where(x => x.NoteGUID == note.NoteGUID).FirstOrDefault();
                                        if (noteData != null && noteData.NoteID > 0 && assessmentResponseInputDTO.canAddResponseNote)
                                        {
                                            var assessmentResponseNoteDTO = new AssessmentResponseNoteDTO
                                            {
                                                AssessmentResponseID = assessmentResponse.AssessmentResponseID,
                                                NoteID = noteData.NoteID
                                            };
                                            AssessmentResponseNote assessmentResponseNote = new AssessmentResponseNote();
                                            this.mapper.Map<AssessmentResponseNoteDTO, AssessmentResponseNote>(assessmentResponseNoteDTO, assessmentResponseNote);
                                            assessmentResponseNoteList.Add(assessmentResponseNote);
                                        }
                                    }
                                    this.assessmentResponseNoteRepository.AddBulkAssessmentResponseNote(assessmentResponseNoteList);
                                }
                                else
                                {
                                    foreach (Note note in notes)
                                    {
                                        note.IsRemoved = true;
                                        note.UpdateDate = DateTime.UtcNow;
                                        note.UpdateUserID = updateUserID;
                                    }

                                    if (notes.Count > 0)
                                    {
                                        noteListToUpdate.AddRange(notes);
                                    }
                                }
                                #endregion

                                #region DoodleOrSignOrAttachments-To ResponseAttachmentTable                             
                                if (fileValueTypeIds != null && fileValueTypeIds.Contains(assessmentResponseInputDTO.ResponseValueTypeId) || 
                                    imageValueTypeIds != null && imageValueTypeIds.Contains(assessmentResponseInputDTO.ResponseValueTypeId))
                                {
                                    var voiceTypeFKId = IsEmailAssessment ? (assessmentProgressInputDTO.VoiceTypeFKID == null
                                               ? person.PersonID : assessmentProgressInputDTO.VoiceTypeFKID) : null;
                                    var oldResponseFiles = this.assessmentResponseFileRepository.GetAllFileByAssessmentResponseId(assessmentResponse.AssessmentResponseID);
                                    //Processing Attachments - only insertion/soft-deletion, No updation will happen.
                                    if (assessmentResponseInputDTO.AssessmentResponseFiles?.Count > 0 && 
                                        fileValueTypeIds.Contains(assessmentResponseInputDTO.ResponseValueTypeId))
                                    {
                                        var removableFiles = oldResponseFiles?.Where(x => !assessmentResponseInputDTO.AssessmentResponseFiles.Select(y => y.AssessmentResponseAttachmentID).ToList().Contains(x.AssessmentResponseAttachmentID)).ToList();
                                        foreach (AssessmentResponseAttachment file in removableFiles)
                                        {
                                            file.IsRemoved = true;
                                            file.UpdateDate = DateTime.UtcNow;
                                            file.UpdateUserID = updateUserID;
                                            fileListToUpdate.Add(file);
                                        }
                                        foreach (var item in assessmentResponseInputDTO.AssessmentResponseFiles)
                                        {
                                            if (!string.IsNullOrWhiteSpace(fileExtensionPattern) && item?.ByteArrayValue?.Length > 0)
                                            {
                                                Guid fileGuid = Guid.NewGuid();
                                                var blobFileName = string.Empty;
                                                Match match = Regex.Match(item.FileName.ToUpper(), fileExtensionPattern);
                                                assessmentResponse.Value = string.Empty;
                                                if (match.Success)
                                                {
                                                    blobFileName = string.Format(PCISEnum.AssessmentImageResponses.AttachmentFileName, assessmentResponse.AssessmentResponseID, fileGuid, match.Groups[1].Value.ToLower());
                                                    var resultFileURL = UploadAndGetImageURLFromAzureBlob(person.AgencyID, item.ByteArrayValue, assessmentResponse, blobFileName, PCISEnum.ResponseValueType.Attachment).Result;
                                                    var fileResponse = new AssessmentResponseAttachment()
                                                    {
                                                        AssessmentResponseAttachmentID = item.AssessmentResponseAttachmentID.HasValue ? item.AssessmentResponseAttachmentID.Value : 0,
                                                        AssessmentResponseID = assessmentResponse.AssessmentResponseID,
                                                        FileName = item.FileName,
                                                        FileURL = resultFileURL,
                                                        UpdateDate = DateTime.UtcNow,
                                                        UpdateUserID = updateUserID,
                                                        VoiceTypeFKID = voiceTypeFKId,
                                                        IsRemoved = false,
                                                        AddedByVoiceTypeID = assessmentProgressInputDTO.VoiceTypeID,
                                                        AssessmentResponseFileGUID = fileGuid
                                                    };
                                                    fileListToAdd.Add(fileResponse);
                                                }
                                            }                                                
                                        }   
                                    }
                                    //Processing doodleOrSignature - To Handle the created/last updated by/date.
                                    else if (imageValueTypeIds.Contains(assessmentResponseInputDTO.ResponseValueTypeId) && 
                                        !string.IsNullOrWhiteSpace(assessmentResponseInputDTO.ImageValue.ToString()))
                                    {
                                        var valueType = responseValueTypes.Where(x => x.ResponseValueTypeID == assessmentResponseInputDTO.ResponseValueTypeId).FirstOrDefault();
                                        var existingImageFile = oldResponseFiles.Where(x => x.AssessmentResponseID == assessmentResponse.AssessmentResponseID).FirstOrDefault();
                                        if (existingImageFile != null)
                                        {
                                            existingImageFile.FileName = valueType.Name;
                                            existingImageFile.FileURL = assessmentResponse.Value;
                                            existingImageFile.UpdateDate = DateTime.UtcNow;
                                            existingImageFile.UpdateUserID = updateUserID;
                                            existingImageFile.VoiceTypeFKID = voiceTypeFKId;
                                            existingImageFile.AddedByVoiceTypeID = assessmentProgressInputDTO.VoiceTypeID;
                                            fileListToUpdate.Add(existingImageFile);
                                        }
                                        else
                                        {
                                            var fileResponse = new AssessmentResponseAttachment()
                                            {
                                                AssessmentResponseAttachmentID = 0,
                                                AssessmentResponseID = assessmentResponse.AssessmentResponseID,
                                                FileName = valueType.Name,
                                                FileURL = assessmentResponse.Value,
                                                UpdateDate = DateTime.UtcNow,
                                                UpdateUserID = updateUserID,
                                                VoiceTypeFKID = voiceTypeFKId,
                                                IsRemoved = false,
                                                AddedByVoiceTypeID = assessmentProgressInputDTO.VoiceTypeID,
                                                AssessmentResponseFileGUID = Guid.NewGuid()
                                            };
                                            fileListToAdd.Add(fileResponse);
                                        }

                                    }
                                }
                                #endregion
                            }
                        }

                        if (fileListToAdd.Count > 0)
                        {
                            this.assessmentResponseFileRepository.AddBulkAssessmentResponseFile(fileListToAdd);
                        }

                        if (fileListToUpdate.Count > 0)
                        {
                            this.assessmentResponseFileRepository.UpdateBulkAssessmentResponseFile(fileListToUpdate);
                        }
                        if (noteListToUpdate.Count > 0)
                        {
                            var result = this.noteRepository.UpdateBulkNotes(noteListToUpdate);
                        }

                        if (assessmentResponseChildItemsToInsert.Count > 0)
                        {
                            var result = this.assessmentResponseRepository.AddBulkAssessmentResponse(assessmentResponseChildItemsToInsert);
                        }

                        if (assessmentResponseChildItemsToUpdate.Count > 0)
                        {
                            var result = this.assessmentResponseRepository.UpdateBulkAssessmentResponse(assessmentResponseChildItemsToUpdate);
                        }
                        if (assessmentResponsesWithImage.Count > 0)
                        {
                            var result = this.assessmentResponseRepository.UpdateBulkAssessmentResponse(assessmentResponsesWithImage);
                        }
                        if (assessmentProgressInputDTO.AssessmentStatus.ToLower() == PCISEnum.Constants.Save.ToLower())
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.SaveSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.SaveSuccess);
                        }
                        if (assessmentProgressInputDTO.AssessmentStatus.ToLower() == PCISEnum.Constants.Submit.ToLower())
                        {
                            var notifiationResolutionStatus = this.notifiationResolutionStatusRepository.GetNotificationStatusForUnResolved();
                            var resolvedstatus = this.notifiationResolutionStatusRepository.GetNotificationStatusForResolved();

                            var notification = this.notificationTypeRepository.GetAllNotificationType();
                            int assessmentRejectTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Reject).ToList()[0].NotificationTypeID;
                            var notificationLog = notificationLogRepository.GetRowAsync(x => x.NotificationResolutionStatusID == notifiationResolutionStatus.NotificationResolutionStatusID && x.NotificationTypeID == assessmentRejectTypeID && x.FKeyValue == assessmentProgressInputDTO.AssessmentID && x.IsRemoved == false).Result;
                            if (notificationLog != null)
                            {
                                notificationLog.NotificationResolutionStatusID = resolvedstatus.NotificationResolutionStatusID;
                                this.notificationLogRepository.UpdateNotificationLog(notificationLog);
                            }
                            StoreInQueue(assessmentResult.AssessmentID);

                            response.ResponseStatusCode = PCISEnum.StatusCodes.SubmitSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.SubmitSuccess);

                            //listResponse = listResponse.Where(x => x.ItemResponseBehaviorID == 1).ToList();

                            var data = this.assessmentResponseRepository.GetNeedforFocusValues(listResponse.Select(y => y.AssessmentResponseID.ToString()).ToList());
                            listResponse = listResponse.Where(x => data.Select(y => y.AssessmentResponseID).ToList().Contains(x.AssessmentResponseID)).ToList();
                            int priority = 1;
                            foreach (var item in data)
                            {
                                foreach (var assessmentResponse in listResponse)
                                {
                                    if (assessmentResponse.AssessmentResponseID == item.AssessmentResponseID)
                                    {
                                        assessmentResponse.Priority = priority;
                                        priority++;
                                        break;
                                    }
                                }
                            }

                            var result = this.assessmentResponseRepository.UpdatePriority(listResponse);

                            var assesmentNotificationInputDTO = new AssesmentNotificationInputDTO()
                            {
                                AssesmentNotificationType = (!IsEmailAssessment ?
                               PCISEnum.AssessmentNotificationType.Submit : PCISEnum.AssessmentNotificationType
                               .EmailSubmit),
                                AssessmentID = assessmentResult.AssessmentID,
                                NotificationDate = assessmentResult.UpdateDate,
                                UpdateUserId = updateUserID,
                                SubmittedUserID = assessmentResult.SubmittedUserID
                            };
                            response = this.AddNotificationOnAssessment(assesmentNotificationInputDTO, null);
                            if (response.ResponseStatusCode == PCISEnum.StatusCodes.NotificationSuccess)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.SubmitSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.SubmitSuccess);
                            }

                        }
                    }
                }
                //}
                response.AssessmentID = assessmentResult == null ? 0 : assessmentResult.AssessmentID;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> UploadAndGetImageURLFromAzureBlob(long agencyID, byte[] imageValue, AssessmentResponse assessmentResponse, string blobFileName = "", string type = "")
        {
            try
            {
                string imageURL = assessmentResponse.Value;
                string agencyFolderName = string.Format(PCISEnum.AssessmentImageResponses.AgencyFolder, agencyID.ToString());
                string assessmentFolderName = string.Format(PCISEnum.AssessmentImageResponses.AssessmentFolder, assessmentResponse.AssessmentID);
                string imageFileName = blobFileName;
                var storageConnectionString = this._config.GetValue<string>("AssessmentImageStorageConnectionString");
                Match match = Regex.Match(storageConnectionString, PCISEnum.AssessmentImageResponses.StorageAccountPattern);
                assessmentResponse.Value = string.Empty;
                if (match.Success && imageValue?.Length > 0)
                {
                    var storageAccountName = match.Groups[1].Value;
                    if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
                    {
                        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                        CloudBlobContainer container = blobClient.GetContainerReference(PCISEnum.AssessmentImageResponses.Container);
                        await container.CreateIfNotExistsAsync();
                        CloudBlobDirectory agencyFolder = container.GetDirectoryReference(agencyFolderName);
                        CloudBlobDirectory assessmentFolder = agencyFolder.GetDirectoryReference(assessmentFolderName);
                        CloudBlockBlob blockblob = assessmentFolder.GetBlockBlobReference(imageFileName);
                        if (type != PCISEnum.ResponseValueType.Attachment)
                        {
                            blockblob.Properties.ContentType = PCISEnum.AssessmentImageResponses.ContentType;
                        }
                        await blockblob.UploadFromByteArrayAsync(imageValue, 0, imageValue.Length);
                    }
                    CRUDResponseDTO responseDTO = new CRUDResponseDTO();
                    imageURL = string.Format(PCISEnum.AssessmentImageResponses.ImageURL, storageAccountName, PCISEnum.AssessmentImageResponses.Container, agencyFolderName, assessmentFolderName, imageFileName);
                    //assessmentResponse.Value = imageURL;
                }
                return imageURL;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Function to process the assessment response child groups
        private Tuple<List<AssessmentResponse>, List<AssessmentResponse>> ProccessAssessmentResponseChilds(int updateUserID, List<AssessmentResponse> assessmentResponseChildItemsToInsert, List<AssessmentResponse> assessmentResponseChildItemsToUpdate, AssessmentResponseInputDTO assessmentResponseInputDTO, AssessmentResponse assessmentResponse)
        {
            try
            {
                if (assessmentResponseInputDTO.AssessmentResponseChildItems?.Count > 0)
                {
                    int groupNumber = 0;

                    var existingAssesmentChildAssesments = this.assessmentResponseRepository.GetAssessmentResponseListByParentId(assessmentResponse.AssessmentResponseID).Result;

                    foreach (var assessmentResponseChildGroups in assessmentResponseInputDTO.AssessmentResponseChildItems)//Process Groups
                    {
                        groupNumber++;

                        foreach (AssessmentResponseChildItemsDTO assessmentResponseChildItem in assessmentResponseChildGroups)//Processing each individual items in groups
                        {
                            if (assessmentResponseChildItem.AssessmentResponseID == 0)
                            {
                                var assessmentResponseDTO = new AssessmentResponsesDTO
                                {
                                    AssessmentResponseID = (assessmentResponseChildItem.AssessmentResponseID == null) ? 0 : (int)assessmentResponseChildItem.AssessmentResponseID,
                                    PersonSupportID = assessmentResponse.PersonSupportID,
                                    ItemResponseBehaviorID = null,
                                    IsRequiredConfidential = assessmentResponse.IsRequiredConfidential,
                                    IsPersonRequestedConfidential = assessmentResponse.IsPersonRequestedConfidential,
                                    IsOtherConfidential = assessmentResponse.IsOtherConfidential,
                                    IsRemoved = false,
                                    UpdateDate = DateTime.UtcNow,
                                    UpdateUserID = updateUserID,
                                    IsCloned = assessmentResponse.IsCloned,
                                    CaregiverCategory = assessmentResponse.CaregiverCategory,
                                    Priority = null,
                                    Value = assessmentResponseChildItem.Value,
                                };

                                AssessmentResponse assessmentChildResponse = new AssessmentResponse();
                                this.mapper.Map<AssessmentResponsesDTO, AssessmentResponse>(assessmentResponseDTO, assessmentChildResponse);
                                assessmentChildResponse.AssessmentID = null;
                                assessmentChildResponse.ResponseID = assessmentResponseChildItem.ResponseID.HasValue ? assessmentResponseChildItem.ResponseID : null;
                                assessmentChildResponse.QuestionnaireItemID = null;
                                assessmentChildResponse.ItemID = assessmentResponseChildItem.ChildItemID;
                                assessmentChildResponse.GroupNumber = groupNumber;
                                assessmentChildResponse.ParentAssessmentResponseID = assessmentResponse.AssessmentResponseID;
                                assessmentResponseChildItemsToInsert.Add(assessmentChildResponse);
                            }
                            else
                            {
                                var assesmentToUpdate = existingAssesmentChildAssesments.Where(x => x.AssessmentResponseID == assessmentResponseChildItem.AssessmentResponseID).FirstOrDefault();
                                if (assesmentToUpdate != null)
                                {
                                    assesmentToUpdate.UpdateDate = DateTime.UtcNow;
                                    assesmentToUpdate.UpdateUserID = updateUserID;
                                    assesmentToUpdate.Value = assessmentResponseChildItem.Value;
                                    if (assessmentResponseChildItem.ResponseID.HasValue)
                                        assesmentToUpdate.ResponseID = assessmentResponseChildItem.ResponseID;
                                    assesmentToUpdate.GroupNumber = groupNumber;
                                    assesmentToUpdate.ParentAssessmentResponseID = assessmentResponse.AssessmentResponseID;
                                    assessmentResponseChildItemsToUpdate.Add(assesmentToUpdate);//modify the existing data

                                }
                            }
                        }
                    }
                    if (assessmentResponseChildItemsToUpdate.Count() > 0)
                    {
                        var updatingChildResponses = assessmentResponseChildItemsToUpdate.Select(x => x.AssessmentResponseID).ToList();
                        var removedChildResponses = existingAssesmentChildAssesments.Where(x => !updatingChildResponses.Contains(x.AssessmentResponseID)).ToList();
                        foreach (var childResponse in removedChildResponses)
                        {
                            childResponse.IsRemoved = true;
                            childResponse.UpdateDate = DateTime.UtcNow;
                            assessmentResponseChildItemsToUpdate.Add(childResponse);
                        }
                    }
                }
                return Tuple.Create(assessmentResponseChildItemsToInsert, assessmentResponseChildItemsToUpdate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateConfidentialityFlags(int assessmentID, long personID, int questionnaireID, bool isEmailAssessment)
        {
            try
            {
                if (assessmentID != 0 && !isEmailAssessment)
                {
                    var assessmentResponses = this.assessmentResponseRepository.GetAssessmentResponse(assessmentID).Result.ToList();
                    if (assessmentResponses.Count > 0)
                    {
                        var confidentialItemsInDB = this.assessmentResponseRepository.GetConfidentialQuestionnaireItemID(personID, questionnaireID, assessmentID);
                        var noncofidentialItems = assessmentResponses.Where(x => x.Isconfidential == false).Select(x => x.QuestionnaireItemID).ToList();
                        if (noncofidentialItems.Count > 0 && confidentialItemsInDB.Count > 0)
                        {
                            var responsesToBeUpdated = confidentialItemsInDB.Where(x => noncofidentialItems.Contains(x.QuestionnaireItemID)).Select(x => x.AssessmentResponseID).ToList();
                            if (responsesToBeUpdated.Count > 0)
                            {
                                List<AssessmentResponse> list_responses = new List<AssessmentResponse>();
                                foreach (var response in responsesToBeUpdated)
                                {
                                    var responses = this.assessmentResponseRepository.GetAssessmentResponses(response).Result;
                                    responses.IsOtherConfidential = false;
                                    responses.IsRequiredConfidential = false;
                                    responses.IsPersonRequestedConfidential = false;
                                    list_responses.Add(responses);
                                }
                                var result = this.assessmentResponseRepository.UpdateBulkAssessmentResponse(list_responses);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// SetNoteUpdateUser.
        /// </summary>
        /// <param name="assessmentDTO"></param>
        /// <param name="updateUserID"></param>
        /// <returns></returns>
        public AssessmentDTO SetNoteUpdateUser(AssessmentDTO assessmentDTO, int updateUserID)
        {
            assessmentDTO.NoteUpdateDate = DateTime.UtcNow;
            assessmentDTO.NoteUpdateUserID = updateUserID;
            return assessmentDTO;
        }

        //public void HandleRiskNotification(AssessmentNotificationRiskDTO assessmentNotificationRiskDTO)
        //{
        //    var responseDto = responseRepository.GetResponse(assessmentNotificationRiskDTO.ResponseID).Result;
        //    var questionnaireNotifyRiskRuleConditionDto = questionnaireNotifyRiskRuleConditionRepository.GetQuestionnaireNotifyRiskRuleConditionByQuestionnaireItemID(assessmentNotificationRiskDTO.QuestionnaireItemID).Result;

        //    bool notify = false;
        //    switch (questionnaireNotifyRiskRuleConditionDto.ComparisonOperator)
        //    {
        //        case PCISEnum.ComparisonOperator.Equal:
        //            if (responseDto.Value == questionnaireNotifyRiskRuleConditionDto.ComparisonValue)
        //            {
        //                notify = true;
        //            }
        //            break;
        //        case PCISEnum.ComparisonOperator.GreaterThan:
        //            if (responseDto.Value > questionnaireNotifyRiskRuleConditionDto.ComparisonValue)
        //            {
        //                notify = true;
        //            }
        //            break;
        //        case PCISEnum.ComparisonOperator.GreaterThanOrEqual:
        //            if (responseDto.Value >= questionnaireNotifyRiskRuleConditionDto.ComparisonValue)
        //            {
        //                notify = true;
        //            }
        //            break;
        //        case PCISEnum.ComparisonOperator.LessThan:
        //            if (responseDto.Value < questionnaireNotifyRiskRuleConditionDto.ComparisonValue)
        //            {
        //                notify = true;
        //            }
        //            break;
        //        case PCISEnum.ComparisonOperator.LessThanOrEqual:
        //            if (responseDto.Value <= questionnaireNotifyRiskRuleConditionDto.ComparisonValue)
        //            {
        //                notify = true;
        //            }
        //            break;
        //        case PCISEnum.ComparisonOperator.NotEqual:
        //            if (responseDto.Value != questionnaireNotifyRiskRuleConditionDto.ComparisonValue)
        //            {
        //                notify = true;
        //            }
        //            break;
        //        default:
        //            notify = false;
        //            break;
        //    }
        //    if (notify)
        //    {
        //        //NotificationLog
        //        var person = personRepository.GetPerson(assessmentNotificationRiskDTO.PersonIndex);
        //        int statusID = this.notifiationResolutionStatusRepository.GetNotificationStatus(PCISEnum.NotificationStatus.Unresolved).Result.NotificationResolutionStatusID;
        //        var notificationType = notificationTypeRepository.GetNotificationType(PCISEnum.NotificationType.Danger).Result;

        //        NotificationLog notificationLog = new NotificationLog
        //        {
        //            NotificationDate = DateTime.UtcNow,
        //            PersonID = person.PersonID,
        //            NotificationTypeID = notificationType.NotificationTypeID,
        //            NotificationResolutionStatusID = statusID,
        //            UpdateDate = DateTime.UtcNow,
        //            UpdateUserID = assessmentNotificationRiskDTO.UpdateUserID
        //        };
        //        notificationLogRepository.AddNotificationLog(notificationLog);

        //        //NotifyRisk
        //        NotifyRisk notifyRisk = new NotifyRisk
        //        {
        //            QuestionnaireNotifyRiskRuleID = questionnaireNotifyRiskRuleConditionDto.QuestionnaireNotifyRiskRuleID,
        //            PersonID = person.PersonID,
        //            AssessmentID = assessmentNotificationRiskDTO.AssessmentID,
        //            NotifyDate = DateTime.UtcNow,
        //            UpdateDate = DateTime.UtcNow,
        //            UpdateUserID = assessmentNotificationRiskDTO.UpdateUserID
        //        };
        //        notifyRiskRepository.AddNotifyRisk(notifyRisk);
        //    }
        //}

        /// <summary>
        /// SendAssessmentEmailLink
        /// </summary>
        /// <param name="EmailLinkParameterDetails"></param>
        /// <returns></returns>       
        public CRUDResponseDTO SendAssessmentEmailLink(AssessmentEmailLinkInputDTO EmailLinkParameterDetails, long agencyID, string agencyAbbrev, bool isResend)
        {
            try
            {
                HttpStatusCode emailresponse = HttpStatusCode.BadRequest;
                bool textPermission = false;
                bool emailPermission = false;
                string cellNumber = string.Empty;
                if (EmailLinkParameterDetails != null)
                {
                    var voiceType = this.voiceTypeRepository.GetVoiceType(EmailLinkParameterDetails.VoiceTypeID);
                    if (voiceType?.Name != PCISEnum.VoiceType.Communimetric && voiceType?.Name != PCISEnum.VoiceType.Helper)
                    {
                        var toDisplayName = string.Empty;
                        var toEmailID = string.Empty;
                        string personName = string.Empty;
                        if (voiceType.Name == PCISEnum.VoiceType.Consumer)
                        {
                            var personDetails = this.personRepository.GetRowAsync(x => x.PersonIndex == EmailLinkParameterDetails.PersonIndex).Result;
                            toDisplayName = string.Format("{0} {1} {2}", personDetails.FirstName, string.IsNullOrEmpty(personDetails.MiddleName) ? "" : personDetails.MiddleName, personDetails.LastName);
                            toDisplayName = toDisplayName.Replace("  ", " ");
                            toEmailID = personDetails.Email;
                            personName = personDetails.FirstName;
                            textPermission = personDetails.TextPermission;
                            emailPermission = personDetails.EmailPermission;
                            cellNumber = string.IsNullOrWhiteSpace(personDetails.Phone1) ? personDetails.Phone2Code + personDetails.Phone2 : personDetails.Phone1Code + personDetails.Phone1;
                        }
                        else if (voiceType.Name == PCISEnum.VoiceType.Support && EmailLinkParameterDetails?.PersonSupportID != 0)
                        {
                            var supportDetails = this._personSupportRepository.GetRowAsync(x => x.PersonSupportID == EmailLinkParameterDetails.PersonSupportID).Result;
                            toDisplayName = string.Format("{0} {1} {2}", supportDetails.FirstName, string.IsNullOrEmpty(supportDetails.MiddleName) ? "" : supportDetails.MiddleName, supportDetails.LastName);
                            toDisplayName = toDisplayName.Replace("  ", " ");
                            toEmailID = supportDetails.Email;
                            personName = supportDetails.FirstName;
                            textPermission = supportDetails.TextPermission;
                            emailPermission = supportDetails.EmailPermission;
                            cellNumber = supportDetails.PhoneCode + supportDetails.Phone;
                        }
                        else if (voiceType.Name == PCISEnum.VoiceType.Helper && EmailLinkParameterDetails?.HelperID != 0)
                        {
                            var helperDetails = this.helperRepository.GetRowAsync(x => x.HelperID == EmailLinkParameterDetails.HelperID).Result;
                            agencyID = helperDetails.AgencyID;
                            toDisplayName = string.Format("{0} {1} {2}", helperDetails.FirstName, string.IsNullOrEmpty(helperDetails.MiddleName) ? "" : helperDetails.MiddleName, helperDetails.LastName);
                            toDisplayName = toDisplayName.Replace("  ", " ");
                            toEmailID = helperDetails.Email;
                            emailPermission = helperDetails.IsEmailReminderAlerts;
                        }
                        if (!string.IsNullOrEmpty(toEmailID) && !string.IsNullOrEmpty(toDisplayName))
                        {
                            var assessmentEmailLinkDTO = new AssessmentEmailLinkDetailsDTO()
                            {
                                PersonIndex = EmailLinkParameterDetails.PersonIndex,
                                AssessmentID = EmailLinkParameterDetails.AssessmentID,
                                PersonSupportID = (EmailLinkParameterDetails.PersonSupportID == 0 ? null : EmailLinkParameterDetails.PersonSupportID),
                                QuestionnaireID = EmailLinkParameterDetails.QuestionnaireID,
                                HelperID = (EmailLinkParameterDetails.HelperID == 0 ? null : EmailLinkParameterDetails.HelperID),
                                UpdateDate = DateTime.UtcNow,
                                PersonORSupportEmail = toEmailID,
                                UpdateUserID = EmailLinkParameterDetails.UpdateUserID,
                                VoiceTypeID = EmailLinkParameterDetails.VoiceTypeID,
                                PhoneNumber = cellNumber
                            };
                            Guid EmailLinkDetailsId = Guid.Empty;
                            if (textPermission || emailPermission)
                            {
                                if (!isResend)
                                {
                                    EmailLinkDetailsId = InsertEmailLinkParameters(assessmentEmailLinkDTO);
                                }
                                else
                                {
                                    var assessmentEmailLinkDetails = this._assessmentEmailLinkRepository.GetEmailLinkDataByPersonIndex(EmailLinkParameterDetails.PersonIndex, EmailLinkParameterDetails.AssessmentID);
                                    assessmentEmailLinkDetails.UpdateDate = DateTime.UtcNow;
                                    EmailLinkDetailsId = UpdateEmailLinkParameters(assessmentEmailLinkDetails);
                                }

                                bool emailResult = false;
                                bool smsResult = false;

                                if (emailPermission)
                                {
                                    string assessmentURLForEmail = CreateAssessmentLink(EmailLinkDetailsId, agencyAbbrev, PCISEnum.Invitation.Email);
                                    if (!string.IsNullOrEmpty(assessmentURLForEmail))
                                    {
                                        var assessmentDetails = this.assessmentRepository.GetAssessment(EmailLinkParameterDetails.AssessmentID).Result;
                                        emailresponse = DraftMailForAssessmentEmailUrl(assessmentURLForEmail, agencyID, toDisplayName, toEmailID, personName, assessmentDetails?.ReasoningText);
                                        if (emailresponse == HttpStatusCode.Accepted)
                                        {
                                            emailResult = true;
                                        }
                                        else
                                        {
                                            response.ResponseStatusCode = PCISEnum.StatusCodes.EmailSendFailed;
                                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                                            return response;
                                        }
                                    }
                                    else
                                    {
                                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidEmailAssessmentURL;
                                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidEmailAssessmentURL);
                                    }
                                }
                                //else
                                //{
                                //    response.ResponseStatusCode = PCISEnum.StatusCodes.missingEmailPermission;
                                //    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.missingEmailPermission);
                                //}

                                if (textPermission)
                                {
                                    string assessmentURLForSMS = CreateAssessmentLink(EmailLinkDetailsId, agencyAbbrev, PCISEnum.Invitation.SMS);
                                    if (!string.IsNullOrEmpty(assessmentURLForSMS))
                                    {
                                        var body = GenerateBodyForSMS(assessmentURLForSMS, agencyID);
                                        var fromNumber = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentSMS.ConfigKeyToFromNo, agencyID);
                                        smsResult = this.smsSender.SendSMS(body, cellNumber, fromNumber?.Value);
                                        if (!smsResult)
                                        {
                                            response.ResponseStatusCode = PCISEnum.StatusCodes.TextSendFailed;
                                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                                            return response;
                                        }
                                    }
                                    else
                                    {
                                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidEmailAssessmentURL;
                                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidEmailAssessmentURL);
                                    }
                                }
                                //else
                                //{
                                //    response.ResponseStatusCode = PCISEnum.StatusCodes.missingTextPermission;
                                //    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.missingTextPermission);
                                //}

                                if (smsResult && emailResult && response != null)
                                {
                                    response.ResponseStatusCode = PCISEnum.StatusCodes.TextAndEmailSendSuccess;
                                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.TextAndEmailSendSuccess);
                                }
                                else if (smsResult && response != null)
                                {
                                    response.ResponseStatusCode = PCISEnum.StatusCodes.TextSendSuccess;
                                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.TextSendSuccess);
                                }
                                else if (emailResult && response != null)
                                {
                                    response.ResponseStatusCode = PCISEnum.StatusCodes.EmailSendSuccess;
                                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailSendSuccess);
                                }
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.missingEmailAndTextPermission;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.missingEmailAndTextPermission);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.MissingEmailID;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.MissingEmailID);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidVoiceType;
                        response.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidVoiceType), voiceType?.Name);
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
        /// Draft Mail For Assessment Email Url
        /// </summary>
        /// <param name="assessmentURL"></param>
        /// <param name="agencyID"></param>
        /// <param name="toDisplayName"></param>
        /// <param name="toEmailID"></param>
        /// <returns></returns>
        private HttpStatusCode DraftMailForAssessmentEmailUrl(string assessmentURL, long agencyID, string toDisplayName, string toEmailID, string personName, string reasoningText)
        {

            var fromemail = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.FromEmail, agencyID);
            var fromemailDisplayName = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.FromDisplayName, agencyID);
            var emailText = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.EmailText, agencyID);
            var emailSubject = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.EmailSubject, agencyID);
            string assessmentEmailText = this.localize.GetLocalizedHtmlString(emailText.Value);
            reasoningText = string.IsNullOrWhiteSpace(reasoningText) ? "" : string.Format("{0}. ", reasoningText).Replace("..", ".");
            var agencyUri = new Uri(assessmentURL);
            var baseUri = agencyUri.GetLeftPart(System.UriPartial.Authority);

            //replace email url code in email text
            if (assessmentEmailText != null)
            {
                assessmentEmailText = assessmentEmailText.Replace(PCISEnum.AssessmentEmail.applicationUrlReplaceText, baseUri);
                assessmentEmailText = assessmentEmailText.Replace(PCISEnum.AssessmentEmail.emailUrlCodeReplaceText, assessmentURL);
                var emailUrlExpiry = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.EmailLinkExpiry, agencyID);
                assessmentEmailText = assessmentEmailText.Replace(PCISEnum.AssessmentEmail.emailexpiryCodeReplaceText, emailUrlExpiry.Value);
                assessmentEmailText = assessmentEmailText.Replace(PCISEnum.AssessmentEmail.personNameReplaceText, personName);
                assessmentEmailText = assessmentEmailText.Replace(PCISEnum.AssessmentEmail.AssessmentNotes, reasoningText);
            }

            SendEmail sendEmail = new SendEmail()
            {
                Body = assessmentEmailText,
                IsHtmlEmail = true,
                Subject = this.localize.GetLocalizedHtmlString(emailSubject.Value),
                FromDisplayName = fromemailDisplayName.Value,
                FromEmail = fromemail.Value,
                ToDisplayName = toDisplayName,
                ToEmail = toEmailID
            };
            return _emailSender.SendEmailAsync(sendEmail);
        }

        public string CreateAssessmentLink(Guid emailParametersIndexId, string agencyAbbrev, string from)
        {
            try
            {
                if (!emailParametersIndexId.Equals(Guid.Empty))
                {
                    var purposeStringKey = this._config["AssessmentEmailLink-Key"];
                    string assessmentEncryptedURL = string.Empty;
                    if (!string.IsNullOrEmpty(purposeStringKey))
                    {
                        string baseUrl = CreateBaseURL(agencyAbbrev);
                        string assessmentUrl = baseUrl + "?id={0}";
                        string queryParameterId = this._dataProtector.Encrypt(emailParametersIndexId.ToString() + $"|{PCISEnum.Invitation.Email}", purposeStringKey);
                        if (from == PCISEnum.Invitation.SMS)
                            queryParameterId = this._dataProtector.Encrypt(emailParametersIndexId.ToString() + $"|{PCISEnum.Invitation.SMS}", purposeStringKey);
                        assessmentEncryptedURL = string.Format(assessmentUrl, HttpUtility.UrlEncode(queryParameterId));
                    }
                    var Isvalid = Uri.IsWellFormedUriString(assessmentEncryptedURL, UriKind.Absolute);
                    if (Isvalid)
                    {
                        return assessmentEncryptedURL;
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string CreateBaseURL(string agencyAbbrev)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            ConfigurationParameterDTO domain = new ConfigurationParameterDTO();
            if (environment.ToLower() == "development")
            {
                domain = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.ConfigKeyToURL + "_Dev");
            }
            else
            {
                domain = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.ConfigKeyToURL + "_" + environment);
            }
            if (!string.IsNullOrEmpty(domain?.Value))
            {
                var agencyURL = domain?.Value.Replace("www", agencyAbbrev);
                return agencyURL;
            }
            return string.Empty;
        }

        /// <summary>
        /// Insert Email Link Parameters
        /// </summary>
        /// <param name="assessmentLinkParameterDTO"></param>
        /// <returns>Guid</returns>
        public Guid InsertEmailLinkParameters(AssessmentEmailLinkDetailsDTO assessmentLinkParameterDTO)
        {
            try
            {
                if (!String.IsNullOrEmpty(assessmentLinkParameterDTO.PersonORSupportEmail) || !String.IsNullOrEmpty(assessmentLinkParameterDTO.PhoneNumber))
                {
                    AssessmentEmailLinkDetails assessmentEmailLink = new AssessmentEmailLinkDetails();
                    this.mapper.Map<AssessmentEmailLinkDetailsDTO, AssessmentEmailLinkDetails>(assessmentLinkParameterDTO, assessmentEmailLink);
                    var assessmentEmailLinkEntity = this._assessmentEmailLinkRepository.AddEmailLinkData(assessmentEmailLink);
                    if (assessmentEmailLinkEntity != null)
                    {
                        return assessmentEmailLinkEntity.EmailLinkDetailsIndex;
                    }
                }
                return Guid.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateEmailLinkParameters.
        /// </summary>
        /// <param name="assessmentEmailLink">assessmentEmailLink.</param>
        /// <returns>Guid.</returns>
        public string GenerateBodyForSMS(string url, long agencyID)
        {
            try
            {
                var smsBodyConfig = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentSMS.ConfigKeyToBody, agencyID);
                //string body = "New Assessment Request: " + url;
                string body = smsBodyConfig.Value.Replace(PCISEnum.AssessmentSMS.emailUrlReplaceCode, url);
                return body;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateEmailLinkParameters.
        /// </summary>
        /// <param name="assessmentEmailLink">assessmentEmailLink.</param>
        /// <returns>Guid.</returns>
        public Guid UpdateEmailLinkParameters(AssessmentEmailLinkDetails assessmentEmailLink)
        {
            try
            {
                if (!String.IsNullOrEmpty(assessmentEmailLink.PersonOrSupportEmail))
                {
                    var assessmentEmailLinkEntity = this._assessmentEmailLinkRepository.UpdateEmailLinkData(assessmentEmailLink);
                    if (assessmentEmailLinkEntity != null)
                    {
                        return assessmentEmailLinkEntity.EmailLinkDetailsIndex;
                    }
                }
                return Guid.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// RemoveAssessment.
        /// </summary>
        /// <param name="assessmentID">The assessmentID<see cref="int"/>.</param>
        /// <param name="roles">The roles<see cref="List<string>"/>.</param>
        /// <returns>CRUDResponseDTO.<ResultDTO>.</returns>
        public CRUDResponseDTO RemoveAssessment(int assessmentID, List<string> roles, long loggedInAgencyID)
        {
            try
            {
                CRUDResponseDTO response = new CRUDResponseDTO();
                bool removeAssessment = false;
                if (assessmentID > 0)
                {
                    Assessment assessment = this.assessmentRepository.GetAssessment(assessmentID).Result;
                    if (assessment != null)
                    {
                        if (!ValidAssessmentofPersonInAgency(assessment, loggedInAgencyID))
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                            return response;
                        }

                        AssessmentStatus assessmentStatus = this.assessmentStatusRepository.GetAssessmentStatusDetails(assessment.AssessmentStatusID).Result;
                        foreach (var role in roles)
                        {
                            if (role == PCISEnum.Roles.HelperRW || role == PCISEnum.Roles.Assessor)
                            {
                                if (assessmentStatus.Name == PCISEnum.AssessmentStatus.Returned || assessmentStatus.Name == PCISEnum.AssessmentStatus.EmailSent || assessmentStatus.Name == PCISEnum.AssessmentStatus.InProgress || (assessment != null && assessment.AssessmentStatus != null && assessment.AssessmentStatus.Name == PCISEnum.AssessmentStatus.Complete))
                                {
                                    removeAssessment = true;
                                    break;
                                }
                            }
                            else if (role == PCISEnum.Roles.Supervisor)
                            {
                                if (assessmentStatus.Name == PCISEnum.AssessmentStatus.EmailSent ||
                                    assessmentStatus.Name == PCISEnum.AssessmentStatus.InProgress || assessmentStatus.Name == PCISEnum.AssessmentStatus.Returned ||
                                    (assessment != null && assessment.AssessmentStatus != null && assessment.AssessmentStatus.Name == PCISEnum.AssessmentStatus.Complete))
                                {
                                    removeAssessment = true;
                                    break;
                                }
                            }
                            else if (role == PCISEnum.Roles.OrgAdminRW || role == PCISEnum.Roles.OrgAdminRO)
                            {
                                if (assessmentStatus.Name == PCISEnum.AssessmentStatus.EmailSent || assessmentStatus.Name == PCISEnum.AssessmentStatus.Approved || assessmentStatus.Name == PCISEnum.AssessmentStatus.Submitted ||
                                    assessmentStatus.Name == PCISEnum.AssessmentStatus.InProgress || assessmentStatus.Name == PCISEnum.AssessmentStatus.Returned ||
                                    (assessment != null && assessment.AssessmentStatus != null && assessment.AssessmentStatus.Name == PCISEnum.AssessmentStatus.Complete))
                                {
                                    removeAssessment = true;
                                    break;
                                }
                            }
                            else if (role == PCISEnum.Roles.SuperAdmin)
                            {
                                removeAssessment = true;
                                break;
                            }


                        }
                        assessment.IsRemoved = true;
                        assessment.UpdateDate = DateTime.UtcNow;
                        if (removeAssessment)
                        {
                            var result = this.assessmentRepository.UpdateAssessment(assessment);
                            if (result != null)
                            {
                                bool isReturned = false;
                                if (assessmentStatus.Name == PCISEnum.AssessmentStatus.InProgress)
                                {
                                    var returnStatus = this.assessmentStatusRepository.GetAllAssessmentStatus();
                                    var assessmentHistory = this.assessmentHistoryRepository.GetHistoryForAssessment(assessment.AssessmentID,
                                        returnStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Returned).Select(y => y.AssessmentStatusID).FirstOrDefault());
                                    if (assessmentHistory?.AssessmentReviewHistoryID > 0)
                                        isReturned = true;
                                }

                                if (assessmentStatus.Name == PCISEnum.AssessmentStatus.Returned || assessmentStatus.Name == PCISEnum.AssessmentStatus.Approved
                                    || assessmentStatus.Name == PCISEnum.AssessmentStatus.Submitted || isReturned)
                                {
                                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(assessment.AssessmentID.ToString());
                                    this._queue.Push(PCISEnum.AzureQueues.Dashboardmetricscalculation, Convert.ToBase64String(plainTextBytes));
                                }

                                var assessmentResponseData = this.assessmentResponseRepository.GetAssessmentResponse(result.AssessmentID).Result;
                                if (assessmentResponseData.Count > 0)
                                {
                                    List<AssessmentResponse> lstassessmentResponse = new List<AssessmentResponse>();
                                    foreach (AssessmentResponsesDTO assessmentResponseDTO in assessmentResponseData)
                                    {
                                        AssessmentResponse assessmentResponse = new AssessmentResponse();
                                        this.mapper.Map<AssessmentResponsesDTO, AssessmentResponse>(assessmentResponseDTO, assessmentResponse);
                                        assessmentResponse.IsRemoved = true;
                                        assessmentResponse.ItemID = assessmentResponse.ItemID == 0 ? null : (int?)assessmentResponse.ItemID;
                                        assessmentResponse.UpdateDate = DateTime.UtcNow;
                                        assessmentResponse.ItemID = assessmentResponse.ItemID == 0 ? null : (int?)assessmentResponse.ItemID;
                                        lstassessmentResponse.Add(assessmentResponse);
                                    }
                                    var assessmentResponseResult = this.assessmentResponseRepository.UpdateBulkAssessmentResponse(lstassessmentResponse);
                                    if (assessmentResponseResult != null)
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
                                // delete assessment notifications
                                List<string> assessmentNotificationNames = new List<string>();
                                assessmentNotificationNames.Add(PCISEnum.AssessmentNotificationType.Approve);
                                assessmentNotificationNames.Add(PCISEnum.AssessmentNotificationType.EmailSubmit);
                                assessmentNotificationNames.Add(PCISEnum.AssessmentNotificationType.Reject);
                                assessmentNotificationNames.Add(PCISEnum.AssessmentNotificationType.Submit);

                                var assessmentNotificationTypes = notificationTypeRepository.GetAllNotificationType();
                                List<int> assessmentNotificationTypeIDs = new List<int>();
                                assessmentNotificationTypeIDs = assessmentNotificationTypes.Where(x => assessmentNotificationNames.Contains(x.Name)).Select(x => x.NotificationTypeID).ToList();

                                var notificationLogsToRemove = notificationLogRepository.GetAssessmentNotificationLog(assessmentNotificationTypeIDs, assessmentID);

                                int assessmentAlertNotificationTypeID = assessmentNotificationTypes.Where(x => PCISEnum.NotificationType.Alert.Equals(x.Name)).Select(x => x.NotificationTypeID).First();
                                var alertNotificationLogsToRemove = notificationLogRepository.GetAssessmentAlertNotificationLog(assessmentAlertNotificationTypeID, assessmentID);

                                List<NotificationLog> notificationLogsToUpdate = notificationLogsToRemove.ToList<NotificationLog>();
                                List<NotificationLog> notificationAlertLogsToUpdate = alertNotificationLogsToRemove.ToList<NotificationLog>();
                                notificationLogsToUpdate.AddRange(notificationAlertLogsToUpdate);

                                if (notificationLogsToUpdate.Count > 0)
                                {
                                    foreach (var notificationLog in notificationLogsToUpdate)
                                    {
                                        notificationLog.IsRemoved = true;
                                        notificationLog.UpdateDate = DateTime.UtcNow;
                                    }
                                    var notificationLogsUpdateResult = notificationLogRepository.UpdateBulkNotificationLog(notificationLogsToUpdate);
                                    if (notificationLogsUpdateResult != null)
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
                                //Delete Reminder and Reschedule
                                //Find PersonQuestionnaire Id from assesmentID
                                long PersonQuestionnaireID = assessment.PersonQuestionnaireID;
                                //find personQuestionnaireScheduleToRemove
                                var personQuestionnaireScheduleToRemove = personQuestionnaireScheduleRepository.GetAsync(x => PersonQuestionnaireID == x.PersonQuestionnaireID && x.IsRemoved == false).Result.ToList();
                                if (personQuestionnaireScheduleToRemove.Count > 0)
                                {
                                    personQuestionnaireScheduleToRemove.ForEach(x => x.IsRemoved = true);
                                    personQuestionnaireScheduleService.UpdateBulkPersonQuestionnaireSchedule(personQuestionnaireScheduleToRemove);
                                }
                                //schedule Reminder again
                                StoreInQueue(PersonQuestionnaireID);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.PermissionDenied);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                    }
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ValidAssessmentofPersonInAgency(Assessment assessment, long loggedInAgencyID)
        {
            try
            {
                if (assessment?.AssessmentID != 0)
                {
                    var person = this.personQuestionnaireRepository.GetPersonByPersonQuestionnaireID(assessment.PersonQuestionnaireID);
                    var questionnaire = this.personQuestionnaireRepository.GetPersonQuestionnaireByID(assessment.PersonQuestionnaireID).Result;
                    if (person != null && questionnaire != null)
                        return this.personRepository.IsValidPersonInAgencyForQuestionnaire(person.PersonID, person.AgencyID, questionnaire.QuestionnaireID, loggedInAgencyID, false);
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// AddAssessmentForEmail
        /// </summary>
        /// <param name="assessmentData"></param>
        /// <param name="updateUserID"></param>
        /// <returns>CRUDResponseDTO</returns>
        public AddAssessmentResponseDTO AddAssessmentForEmail(AssessmentInputDTO assessmentData, int updateUserID, long agencyID, string agencyAbbrev)
        {
            try
            {
                int assessmentStatusID = 0;
                long personQuestionnaireID = 0;
                Assessment assessmentResult = null;
                AddAssessmentResponseDTO assessmentResponse = new AddAssessmentResponseDTO();
                if (assessmentData != null)
                {
                    var voiceType = this.voiceTypeRepository.GetVoiceType(assessmentData.VoiceTypeID);
                    if (voiceType.Name.ToLower() == PCISEnum.VoiceType.Support.ToLower() && assessmentData.PersonSupportID == 0)
                    {
                        assessmentResponse.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        assessmentResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, "PersonSupportID"));
                        return assessmentResponse;
                    }
                    if (voiceType.Name.ToLower() == PCISEnum.VoiceType.Consumer.ToLower() && assessmentData.PersonIndex == Guid.Empty)
                    {
                        assessmentResponse.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        assessmentResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, "PersonIndex"));
                        return assessmentResponse;
                    }
                    if (!this.personRepository.IsValidPersonInAgencyForQuestionnaire(assessmentData.PersonIndex, assessmentData.QuestionnaireID, agencyID))
                    {
                        assessmentResponse.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                        assessmentResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.Failure));
                        return assessmentResponse;
                    }
                    personQuestionnaireID = this.assessmentRepository.GetPersonQuestionnaireID(assessmentData.PersonIndex, assessmentData.QuestionnaireID);

                    assessmentStatusID = this.assessmentStatusRepository.GetAssessmentStatus(PCISEnum.AssessmentStatus.EmailSent).Result.AssessmentStatusID;

                    var assessmentDTO = new AssessmentDTO
                    {
                        PersonQuestionnaireID = personQuestionnaireID,
                        VoiceTypeID = assessmentData.VoiceTypeID,
                        VoiceTypeFKID = assessmentData.VoiceTypeFKID,
                        DateTaken = assessmentData.DateTaken,
                        ReasoningText = assessmentData.Note,
                        AssessmentReasonID = assessmentData.AssessmentReasonID,
                        AssessmentStatusID = assessmentStatusID,
                        PersonQuestionnaireScheduleID = null,
                        IsUpdate = true,
                        Approved = null,
                        CloseDate = assessmentData.CloseDate,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = updateUserID,
                        EventDate = assessmentData.EventDate,
                        EventNotes = assessmentData.EventNotes,
                        EventNoteUpdatedBy = string.IsNullOrEmpty(assessmentData.EventNotes) ? null : (int?)updateUserID

                    };
                    Assessment assessment = new Assessment();
                    this.mapper.Map<AssessmentDTO, Assessment>(assessmentDTO, assessment);
                    assessmentResult = this.assessmentRepository.AddAssessment(assessment);

                    if (assessmentResult != null && assessmentResult.AssessmentID > 0)
                    {
                        assessmentResponse.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                        assessmentResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);

                        var assessmentEmailInput = new AssessmentEmailLinkInputDTO()
                        {
                            AssessmentID = assessmentResult.AssessmentID,
                            VoiceTypeID = assessmentResult.VoiceTypeID,
                            UpdateUserID = updateUserID,
                            PersonIndex = assessmentData.PersonIndex,
                            QuestionnaireID = assessmentData.QuestionnaireID,
                            HelperID = assessmentData.HelperID,
                            PersonSupportID = assessmentData.PersonSupportID
                        };

                        var emailResponse = this.SendAssessmentEmailLink(assessmentEmailInput, agencyID, agencyAbbrev, false);
                        assessmentResponse.ResponseStatus = emailResponse.ResponseStatus;
                        assessmentResponse.ResponseStatusCode = emailResponse.ResponseStatusCode;
                        assessmentResponse.AssessmentID = assessmentResult.AssessmentID;
                        return assessmentResponse;
                    }
                }

                return assessmentResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ResendAssessmentEmail.
        /// </summary>
        /// <param name="assessmentEmailInput">assessmentEmailInput.</param>
        /// <param name="agencyID">agencyID</param>
        /// <param name="agencyAbbrev">agencyAbbrev.</param>
        /// <returns></returns>
        public AddAssessmentResponseDTO ResendAssessmentEmail(AssessmentEmailLinkInputDTO assessmentEmailInput, long agencyID, string agencyAbbrev)
        {

            AddAssessmentResponseDTO assessmentResponse = new AddAssessmentResponseDTO();
            var person = this.personRepository.GetPerson(assessmentEmailInput.PersonIndex);
            if (!this.personRepository.IsValidPersonInAgencyForQuestionnaire(person.PersonID, person.AgencyID, assessmentEmailInput.QuestionnaireID, agencyID))
            {
                assessmentResponse.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                assessmentResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.Failure));
                return assessmentResponse;
            }
            if (assessmentEmailInput.AssessmentID != 0)
            {
                var assessment = this.assessmentRepository.GetAssessment(assessmentEmailInput.AssessmentID).Result;
                if (assessment.ReasoningText != assessmentEmailInput.ReasoningText)
                {
                    assessment.ReasoningText = assessmentEmailInput.ReasoningText;
                    assessment.NoteUpdateDate = assessment.UpdateDate = DateTime.UtcNow;
                    assessment.NoteUpdateUserID = assessment.UpdateUserID = assessmentEmailInput.UpdateUserID;
                }
                if (assessmentEmailInput.EventDate != null)
                {
                    assessment.EventDate = assessmentEmailInput.EventDate;
                    assessment.EventNotes = assessmentEmailInput.EventNotes;
                    assessment.EventNoteUpdatedBy = assessment.EventNotes != assessmentEmailInput.EventNotes ? assessmentEmailInput.UpdateUserID : assessment.UpdateUserID;
                }
                if (assessment.ReasoningText != assessmentEmailInput.ReasoningText || assessmentEmailInput.EventDate != null)
                {
                    var newAssessment = this.assessmentRepository.UpdateAssessment(assessment);
                }
            }
            var emailResponse = this.SendAssessmentEmailLink(assessmentEmailInput, agencyID, agencyAbbrev, true);
            assessmentResponse.AssessmentID = assessmentEmailInput.AssessmentID;
            assessmentResponse.ResponseStatus = emailResponse.ResponseStatus;
            assessmentResponse.ResponseStatusCode = emailResponse.ResponseStatusCode;
            return assessmentResponse;
        }

        /// <summary>
        /// AddNotificationOnAssessment
        /// </summary>
        /// <param name="assesmentNotificationInputDTO"></param>
        /// <returns>CRUDResponseDTO</returns>
        public AddAssessmentResponseDTO AddNotificationOnAssessment(AssesmentNotificationInputDTO assesmentNotificationInputDTO, int? assessmentNoteID)
        {
            try
            {
                AddAssessmentResponseDTO response = new AddAssessmentResponseDTO();
                if (assesmentNotificationInputDTO != null)
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.NotificationFailed;
                    response.ResponseStatus = PCISEnum.StatusMessages.NotificationFailed;
                    if (assesmentNotificationInputDTO.AssessmentID != 0)
                    {
                        var detailsToNotificationlog = this.assessmentRepository.GetQuestionDetailsFromAssessment(assesmentNotificationInputDTO.AssessmentID);
                        NotificationType notificationType = new NotificationType();
                        int unresolvedId = notifiationResolutionStatusRepository.GetRowAsync(x => x.Name.ToLower() == PCISEnum.NotificationStatus.Unresolved.ToLower()).Result.NotificationResolutionStatusID;
                        int resolvedId = notifiationResolutionStatusRepository.GetRowAsync(x => x.Name.ToLower() == PCISEnum.NotificationStatus.Resolved.ToLower()).Result.NotificationResolutionStatusID;

                        int NotificationResolutionStatusID = unresolvedId;

                        string submittedHelperName = string.Empty;
                        if (assesmentNotificationInputDTO.SubmittedUserID.HasValue)
                        {
                            var helperDetails = this.helperRepository.GetUserDetails(assesmentNotificationInputDTO.SubmittedUserID.Value);
                            submittedHelperName = helperDetails.Name;
                        }
                        if (assesmentNotificationInputDTO.AssesmentNotificationType == PCISEnum.AssessmentNotificationType.Submit)
                        {
                            notificationType = notificationTypeRepository.GetRowAsync(x => x.Name == PCISEnum.AssessmentNotificationType.Submit).Result;
                        }
                        if (assesmentNotificationInputDTO.AssesmentNotificationType == PCISEnum.AssessmentNotificationType.EmailSubmit)
                        {
                            notificationType = notificationTypeRepository.GetRowAsync(x => x.Name == PCISEnum.AssessmentNotificationType.EmailSubmit).Result;
                        }
                        if (assesmentNotificationInputDTO.AssesmentNotificationType == PCISEnum.AssessmentNotificationType.Approve)
                        {
                            notificationType = notificationTypeRepository.GetRowAsync(x => x.Name == PCISEnum.AssessmentNotificationType.Approve).Result;
                            NotificationResolutionStatusID = resolvedId;
                        }
                        if (assesmentNotificationInputDTO.AssesmentNotificationType == PCISEnum.AssessmentNotificationType.Reject)
                        {
                            notificationType = notificationTypeRepository.GetRowAsync(x => x.Name == PCISEnum.AssessmentNotificationType.Reject).Result;
                        }
                        if (notificationType != null && notificationType.NotificationTypeID != 0)
                        {
                            var notificationLog = new NotificationLog
                            {
                                NotificationTypeID = notificationType.NotificationTypeID,
                                PersonID = assessmentRepository.GetPersonIdFromAssessment(assesmentNotificationInputDTO.AssessmentID),
                                FKeyValue = assesmentNotificationInputDTO.AssessmentID,
                                IsRemoved = false,
                                NotificationDate = assesmentNotificationInputDTO.NotificationDate,
                                UpdateUserID = assesmentNotificationInputDTO.UpdateUserId,
                                NotificationResolutionStatusID = NotificationResolutionStatusID,
                                UpdateDate = DateTime.UtcNow,
                                AssessmentNoteID = assessmentNoteID,
                                AssessmentID = assesmentNotificationInputDTO.AssessmentID,
                                Details = string.Format("{0} - {1}", detailsToNotificationlog?.QuestionnaireID, detailsToNotificationlog?.Name),
                                QuestionnaireID = detailsToNotificationlog?.QuestionnaireID,
                                HelperName = submittedHelperName
                            };

                            var ResponseResult = this.notificationLogRepository.AddNotificationLog(notificationLog);
                            if (ResponseResult != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.NotificationSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.NotificationSuccess);
                            }
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
        /// ChangeReviewStatus.
        /// </summary>
        /// <param name="assessmentReviewStatusData">assessmentReviewStatusData.</param>
        /// <returns></returns>
        public CRUDResponseDTO ChangeReviewStatus(AssessmentReviewStatusDTO assessmentReviewStatusData, long loggedInAgencyID)
        {
            try
            {
                var isSuccess = false;
                if (assessmentReviewStatusData != null)
                {
                    int assessmentStatusID = this.assessmentStatusRepository.GetAssessmentStatus(assessmentReviewStatusData.AssessmentStatus).Result.AssessmentStatusID;

                    var assessment = this.assessmentRepository.GetAssessment(assessmentReviewStatusData.AssessmentID).Result;
                    if (!ValidAssessmentofPersonInAgency(assessment, loggedInAgencyID))
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                        return response;
                    }
                    var assessmentDTO = new AssessmentDTO
                    {
                        AssessmentID = assessmentReviewStatusData.AssessmentID,
                        AssessmentStatusID = assessmentStatusID,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = assessmentReviewStatusData.ReviewUserID,
                        AssessmentReasonID = assessment.AssessmentReasonID,
                        PersonQuestionnaireID = assessment.PersonQuestionnaireID,
                        Approved = (assessmentReviewStatusData.AssessmentStatus == PCISEnum.AssessmentStatus.Approved ? 1 : 0),
                        CloseDate = assessment.CloseDate,
                        DateTaken = assessment.DateTaken,
                        IsRemoved = assessment.IsRemoved,
                        IsUpdate = assessment.IsUpdate,
                        PersonQuestionnaireScheduleID = assessment.PersonQuestionnaireScheduleID,
                        ReasoningText = assessment.ReasoningText,
                        VoiceTypeID = assessment.VoiceTypeID,
                        VoiceTypeFKID = assessment.VoiceTypeFKID,
                        EventNotes = assessment.EventNotes,
                        NoteUpdateUserID = assessment.NoteUpdateUserID,
                        EventDate = assessment.EventDate,
                        NoteUpdateDate = assessment.NoteUpdateDate,
                        EventNoteUpdatedBy = assessment.EventNoteUpdatedBy,
                        SubmittedUserID = assessment.SubmittedUserID,
                        NotifyReminderID = assessment.NotifyReminderID
                    };


                    int previousAssessmentStatusID = assessment.AssessmentStatusID;
                    this.mapper.Map<AssessmentDTO, Assessment>(assessmentDTO, assessment);
                    if (assessmentReviewStatusData.AssessmentStatus == PCISEnum.AssessmentStatus.Approved)
                    {
                        var person = this.personQuestionnaireRepository.GetPersonByPersonQuestionnaireID(assessmentDTO.PersonQuestionnaireID);
                        if (!string.IsNullOrEmpty(person.UniversalID))
                        {
                            assessment.EHRUpdateStatus = PCISEnum.EHRUpdateStatus.Pending;
                        }
                    }
                    Assessment assessmentResult = this.assessmentRepository.UpdateAssessment(assessment);

                    if (assessmentResult != null && assessmentResult.AssessmentID > 0)
                    {
                        if (assessmentReviewStatusData.AssessmentStatus == PCISEnum.AssessmentStatus.Approved)
                        {
                            StoreInQueueForAlert(assessmentResult.AssessmentID);
                        }

                        AssessmentHistoryDTO assessmentHistoryDTO = new AssessmentHistoryDTO
                        {
                            RecordedDate = DateTime.UtcNow,
                            StatusFrom = previousAssessmentStatusID,
                            StatusTo = assessment.AssessmentStatusID,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = assessmentReviewStatusData.ReviewUserID
                        };

                        ReviewerHistory assessmentHistory = new ReviewerHistory();
                        this.mapper.Map<AssessmentHistoryDTO, ReviewerHistory>(assessmentHistoryDTO, assessmentHistory);
                        ReviewerHistory assessmentHistoryResult = this.assessmentHistoryRepository.AddAssessmentHistory(assessmentHistory);

                        if (assessmentHistoryResult != null && assessmentHistoryResult.AssessmentReviewHistoryID > 0)
                        {
                            var noteDTO = new NoteDTO
                            {
                                NoteText = assessmentReviewStatusData.ReviewNote,
                                IsConfidential = false,
                                IsRemoved = false,
                                UpdateDate = DateTime.UtcNow,
                                UpdateUserID = assessmentReviewStatusData.ReviewUserID
                            };
                            Note note = new Note();
                            this.mapper.Map<NoteDTO, Note>(noteDTO, note);
                            Note noteResult = this.noteRepository.AddNote(note);
                            if (noteResult != null && noteResult.NoteID > 0)
                            {
                                var assessmentNoteDTO = new AssessmentNoteDTO
                                {
                                    AssessmentID = assessmentReviewStatusData.AssessmentID,
                                    NoteID = noteResult.NoteID,
                                    AssessmentReviewHistoryID = assessmentHistoryResult.AssessmentReviewHistoryID
                                };
                                AssessmentNote assessmentNote = new AssessmentNote();
                                this.mapper.Map<AssessmentNoteDTO, AssessmentNote>(assessmentNoteDTO, assessmentNote);
                                AssessmentNote assessmentNoteResult = this.assessmentNoteRepository.AddAssessmentNote(assessmentNote);
                                if (assessmentNoteResult != null && assessmentNoteResult.NoteID > 0)
                                {
                                    if (assessmentReviewStatusData.AssessmentStatus == PCISEnum.AssessmentStatus.Approved || assessmentReviewStatusData.AssessmentStatus == PCISEnum.AssessmentStatus.Returned)
                                    {
                                        var assesmentNotificationInputDTO = new AssesmentNotificationInputDTO()
                                        {
                                            AssesmentNotificationType = (assessmentReviewStatusData.AssessmentStatus == PCISEnum.AssessmentStatus.Approved ? PCISEnum.AssessmentNotificationType.Approve : PCISEnum.AssessmentNotificationType.Reject),
                                            AssessmentID = assessmentReviewStatusData.AssessmentID,
                                            NotificationDate = DateTime.UtcNow,
                                            UpdateUserId = assessmentReviewStatusData.ReviewUserID,
                                            SubmittedUserID = assessment.SubmittedUserID
                                        };
                                        var responseResult = this.AddNotificationOnAssessment(assesmentNotificationInputDTO, assessmentNoteResult.AssessmentNoteID);
                                        if (responseResult.ResponseStatusCode == PCISEnum.StatusCodes.NotificationSuccess)
                                        {
                                            isSuccess = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (isSuccess)
                {
                    // delete assessment notifications
                    List<string> assessmentNotificationNames = new List<string>();
                    assessmentNotificationNames.Add(PCISEnum.AssessmentNotificationType.Submit);
                    var assessmentNotificationTypes = notificationTypeRepository.GetAllNotificationType();
                    List<int> assessmentNotificationTypeIDs = new List<int>();
                    assessmentNotificationTypeIDs = assessmentNotificationTypes.Where(x => assessmentNotificationNames.Contains(x.Name)).Select(x => x.NotificationTypeID).ToList();
                    //var notificationLogsToRemove = notificationLogRepository.GetAssessmentNotificationLog(assessmentNotificationTypeIDs, assessmentID);
                    foreach (var item in assessmentNotificationTypeIDs)
                    {
                        var notificationLog = notificationLogRepository.GetRowAsync(x => x.NotificationTypeID == item && x.FKeyValue == assessmentReviewStatusData.AssessmentID && x.IsRemoved == false).Result;
                        if (notificationLog != null)
                        {
                            notificationLog.IsRemoved = true;
                            notificationLog.UpdateDate = DateTime.UtcNow;
                            notificationLog = notificationLogRepository.UpdateNotificationLog(notificationLog);
                            if (notificationLog != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.SaveSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.SaveSuccess);
                                return response;
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.SaveFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.SaveSuccessNotificationFailed);
                                return response;
                            }
                        }
                    }

                    response.ResponseStatusCode = PCISEnum.StatusCodes.SaveSuccess;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.SaveSuccess);
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.SaveFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.SaveFailed);
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> StoreInQueueForAlert(int assessmentID)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(assessmentID.ToString());
            return await this._queue.Push(PCISEnum.AzureQueues.Assessmentrisknotification, Convert.ToBase64String(plainTextBytes)) &&
            await this._queue.Push(PCISEnum.AzureQueues.Resolvenotificationalerts, Convert.ToBase64String(plainTextBytes));
        }

        public async Task<bool> StoreInQueue(int assessmentID)
        {

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(assessmentID.ToString());
            return await this._queue.Push(PCISEnum.AzureQueues.Dashboardmetricscalculation, Convert.ToBase64String(plainTextBytes)) &&
            await this._queue.Push(PCISEnum.AzureQueues.Resolveremindernotifications, Convert.ToBase64String(plainTextBytes));
        }
        public async Task<bool> StoreInQueue(long personQuestionnaireID)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(personQuestionnaireID.ToString());
            return await this._queue.Push(PCISEnum.AzureQueues.Assessmentremindernotification, Convert.ToBase64String(plainTextBytes));
        }

        /// <summary>
        /// Get Details From Assessment Email Link
        /// </summary>
        /// <param name="otpValue"></param>
        /// <param name="encryptedURL"></param>
        /// <returns>AssessmentEmailLinkDetailsResponseDTO</returns>
        public AssessmentEmailLinkDetailsResponseDTO GetDetailsFromAssessmentEmailLink(string otpValue, string encryptedURL, long agencyID)
        {
            AssessmentEmailLinkDetailsResponseDTO response = new AssessmentEmailLinkDetailsResponseDTO();

            response.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
            var purposeStringKey = this._config["AssessmentEmailLink-Key"];
            if (!string.IsNullOrEmpty(purposeStringKey))
            {
                string decryptedString = this._dataProtector.Decrypt(HttpUtility.UrlDecode(encryptedURL), purposeStringKey);
                string indexID = decryptedString.Split('|')?[0];
                var reqFrom = string.Empty;
                if (decryptedString.Split('|').Count() > 1)
                {
                    reqFrom = decryptedString.Split('|')[1];
                }

                if (!string.IsNullOrEmpty(indexID))
                {
                    var assessmentEmailLinkDetails = this._assessmentEmailLinkRepository.GetEmailLinkData(new Guid(indexID));
                    if (assessmentEmailLinkDetails != null)
                    {
                        AssessmentEmailOtp emailOtpResponse = null;
                        if (reqFrom == PCISEnum.Invitation.SMS && !string.IsNullOrEmpty(otpValue) && otpValue != "0")
                        {
                            emailOtpResponse = this.assessmentEmailOtpRepository.GetRowAsync(x => x.AssessmentEmailOtpID.ToString() == otpValue).Result;
                        }
                        else
                        {
                            emailOtpResponse = this.assessmentEmailOtpRepository.FindIsEmailOtpValid(otpValue, assessmentEmailLinkDetails.AssessmentEmailLinkDetailsID).Result;
                        }
                        var emailUrlExpiry = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.EmailLinkExpiry, agencyID);
                        var emailLinkExpiryTime = assessmentEmailLinkDetails.UpdateDate.AddHours(int.Parse(emailUrlExpiry.Value));
                        if (emailOtpResponse == null)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidExpired;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidEmailOtp);
                            return response;
                        }
                        else if (emailOtpResponse?.ExpiryTime < DateTime.UtcNow)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.OtpExpired;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailOtpExpired);
                            return response;
                        }
                        else if (assessmentEmailLinkDetails.UpdateDate > emailLinkExpiryTime)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.EmailAssessmentURLExpired;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailAssessmentURLExpired);
                            return response;
                        }

                        var person = this.personRepository.GetPerson(assessmentEmailLinkDetails.PersonIndex);
                        if (person != null)
                        {
                            var personName = this.personRepository.GetPersonInitials(assessmentEmailLinkDetails.PersonIndex);
                            AssessmentDTO assessmentDTO = new AssessmentDTO();
                            Assessment assessment = this.assessmentRepository.GetAssessment(assessmentEmailLinkDetails.AssessmentID).Result;
                            if (assessment != null)
                            {
                                this.mapper.Map<Assessment, AssessmentDTO>(assessment, assessmentDTO);
                                var personSupport = this.personRepository.GetPeopleSupportList(person.PersonID);
                                var responseemailLinkdetailsDTO = new AssessmentEmailLinkDetailsOutputDTO()
                                {
                                    Assessment = assessmentDTO,
                                    PersonIndex = assessmentEmailLinkDetails.PersonIndex,
                                    PersonInitials = personName.PersonInitials,
                                    PersonSupportID = assessmentEmailLinkDetails.PersonSupportID,
                                    QuestionnaireID = assessmentEmailLinkDetails.QuestionnaireID,
                                    UpdateUserID = assessmentEmailLinkDetails.UpdateUserID,
                                    HelperID = assessmentEmailLinkDetails.HelperID,
                                    personSupports = personSupport
                                };
                                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                                response.AssessmentEmailLinkDetails = responseemailLinkdetailsDTO;
                            }
                        }
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// QuestionnaireByAssessmentID.
        /// </summary>
        /// <param name="assessmentID">assessmentID.</param>
        /// <returns>QuestionsResponseDTO.</returns>
        public QuestionsResponseDTO QuestionnaireByAssessmentID(int assessmentID, long loggedInAgencyID)
        {
            try
            {
                QuestionsResponseDTO objQuestionsResponse = new QuestionsResponseDTO();
                objQuestionsResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                objQuestionsResponse.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                objQuestionsResponse.Questions = null;

                if (assessmentID > 0)
                {
                    var assessment = this.assessmentRepository.GetAssessment(assessmentID).Result;
                    if (!ValidAssessmentofPersonInAgency(assessment, loggedInAgencyID))
                    {
                        return objQuestionsResponse;
                    }
                    int questionnaireID = 0;
                    questionnaireID = this.questionnaireRepository.QuestionnaireByAssessmentID(assessmentID);
                    if (questionnaireID > 0)
                    {
                        var response = this.questionnaireRepository.GetQuestions(questionnaireID);
                        objQuestionsResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                        objQuestionsResponse.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        objQuestionsResponse.Questions = response;
                    }

                    return objQuestionsResponse;
                }
                return objQuestionsResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AssessmentByAssessmentID.
        /// </summary>
        /// <param name="assessmentID">assessmentID.</param>
        /// <returns>AssessmentResponseDTO.</returns>
        public AssessmentResponseDTO AssessmentByAssessmentID(int assessmentID, long loggedInAgencyID)
        {
            try
            {
                AssessmentResponseDTO objAssessmentDetailsResponseDTO = new AssessmentResponseDTO();
                var assessmentDetails = this.assessmentRepository.GetAssessment(assessmentID).Result;
                if (!ValidAssessmentofPersonInAgency(assessmentDetails, loggedInAgencyID))
                {
                    objAssessmentDetailsResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                    objAssessmentDetailsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    return objAssessmentDetailsResponseDTO;
                }
                var response = this.assessmentResponseRepository.GetAssessmentValuesByAssessmentID(assessmentID);
                objAssessmentDetailsResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                objAssessmentDetailsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                objAssessmentDetailsResponseDTO.AssessmentValues = response;
                objAssessmentDetailsResponseDTO.AssessmentDetails = this.mapper.Map<AssessmentDTO>(assessmentDetails);
                return objAssessmentDetailsResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AssessmentPriority.
        /// </summary>
        /// <param name="assessmentPriorityInputDTO">assessmentPriorityInputDTO.</param>
        /// <param name="userID">userID</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AssessmentPriority(AssessmentPriorityInputDTO assessmentPriorityInputDTO, int userID)
        {
            try
            {
                List<AssessmentResponse> assessmentResponsesList = new List<AssessmentResponse>();
                AssessmentResponse assessmentResponse = new AssessmentResponse();
                foreach (AssessmentPriorityDTO AP in assessmentPriorityInputDTO.AssessmentPriorityList)
                {
                    int id = AP.AssessmentResponseID;
                    assessmentResponse = this.assessmentResponseRepository.GetAssessmentResponses(id).Result;

                    assessmentResponse.UpdateDate = DateTime.UtcNow;
                    assessmentResponse.UpdateUserID = userID;
                    assessmentResponse.Priority = AP.Priority;
                    assessmentResponsesList.Add(assessmentResponse);
                }

                var result = this.assessmentResponseRepository.UpdateBulkAssessmentResponse(assessmentResponsesList);
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SendEmail Assessment Otp.
        /// </summary>
        /// <param name="encryptedURL">encryptedURL.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>AssessmentEmailOtpResponseDTO.</returns>
        public AssessmentEmailOtpResponseDTO SendEmailAssessmentOtp(string encryptedURL, long agencyID, string callFrom)
        {
            try
            {
                AssessmentEmailOtpResponseDTO response = new AssessmentEmailOtpResponseDTO();
                string from = string.Empty;
                response.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                var purposeStringKey = this._config["AssessmentEmailLink-Key"];
                if (!string.IsNullOrEmpty(purposeStringKey))
                {
                    string indexID = this._dataProtector.Decrypt(HttpUtility.UrlDecode(encryptedURL), purposeStringKey);
                    if (indexID.Split('|').Length > 1)
                    {
                        from = indexID.Split('|')?[1];
                        indexID = indexID.Split('|')?[0];
                    }

                    if (!string.IsNullOrEmpty(indexID))
                    {
                        var assessmentEmailLinkDetails = this._assessmentEmailLinkRepository.GetEmailLinkData(new Guid(indexID));
                        if (assessmentEmailLinkDetails == null)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidEmailAssessmentURL;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidEmailAssessmentURL);
                        }
                        var emailUrlExpiry = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.EmailLinkExpiry, agencyID);
                        var expiryTime = assessmentEmailLinkDetails.UpdateDate.AddHours(int.Parse(emailUrlExpiry.Value));
                        if (assessmentEmailLinkDetails.UpdateDate > expiryTime)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.EmailAssessmentURLExpired;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailAssessmentURLExpired);
                        }
                        else
                        {
                            var assessment = this.assessmentRepository.GetAssessment(assessmentEmailLinkDetails.AssessmentID).Result;
                            if (this.isAssessmentAlreadySubmitted(assessment))
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.EmailAssessmentAlreadySubmitted;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailAssessmentAlreadySubmitted);
                            }
                            else if (assessment.IsRemoved)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.EmailAssessmentDeleted;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailAssessmentDeleted);
                            }
                            else
                            {
                                var otpCallExpiry = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.OtpCallExpiry, agencyID);
                                var otpExisting = this.assessmentEmailOtpRepository.GetRowAsync(x => x.AssessmentEmailLinkDetailsID == assessmentEmailLinkDetails.AssessmentEmailLinkDetailsID).Result;
                                var otpCallExpiryTime = otpExisting?.UpdateDate.AddSeconds(int.Parse(otpCallExpiry?.Value ?? "60"));
                                if (otpExisting != null && callFrom != "Resend" && DateTime.UtcNow < otpCallExpiryTime && from != PCISEnum.Invitation.SMS)
                                {
                                    response.ResponseStatusCode = PCISEnum.StatusCodes.EmailSendSuccess;
                                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailSendSuccess);
                                }
                                else
                                {
                                    response = this.GenerateAssessmentEmailOtp(assessmentEmailLinkDetails, agencyID, from);
                                }
                            }
                        }
                    }
                }
                if (from == PCISEnum.Invitation.SMS)
                    response.IsOTPVerificationNeeded = false;
                else
                    response.IsOTPVerificationNeeded = true;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// check is Assessment Already Submitted
        /// </summary>
        /// <param name="assessment"></param>
        /// <returns>bool</returns>
        private bool isAssessmentAlreadySubmitted(Assessment assessment)
        {
            //check already is email asessment already saved 
            var assessmentStatusID = this.assessmentStatusRepository.GetAssessmentStatus(PCISEnum.AssessmentStatus.InProgress).Result.AssessmentStatusID;
            if (assessmentStatusID == assessment.AssessmentStatusID)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Generate Assessment EmailOtp
        /// </summary>
        /// <param name="assessmentEmailLinkDetails"></param>
        /// <param name="agencyID"></param>
        /// <param name="agencyAbbrev"></param>
        /// <returns>AssessmentEmailOtpResponseDTO</returns>
        private AssessmentEmailOtpResponseDTO GenerateAssessmentEmailOtp(AssessmentEmailLinkDetails assessmentEmailLinkDetails, long agencyID, string requestFrom)
        {
            try
            {
                AssessmentEmailOtpResponseDTO response = new AssessmentEmailOtpResponseDTO();
                HttpStatusCode emailresponse = HttpStatusCode.BadRequest;
                if (assessmentEmailLinkDetails != null)
                {
                    var voiceType = this.voiceTypeRepository.GetVoiceType(assessmentEmailLinkDetails.VoiceTypeID);
                    if (voiceType?.Name != PCISEnum.VoiceType.Communimetric)
                    {
                        var toDisplayName = string.Empty;
                        var toEmailID = string.Empty;
                        var textpermited = string.Empty;
                        if (voiceType.Name == PCISEnum.VoiceType.Consumer)
                        {
                            var personDetails = this.personRepository.GetRowAsync(x => x.PersonIndex == assessmentEmailLinkDetails.PersonIndex).Result;
                            agencyID = personDetails.AgencyID;
                            toDisplayName = string.Format("{0} {1} {2}", personDetails.FirstName, string.IsNullOrEmpty(personDetails.MiddleName) ? "" : personDetails.MiddleName, personDetails.LastName);
                            toDisplayName = toDisplayName.Replace("  ", " ");
                            toEmailID = personDetails.Email;
                        }
                        else if (voiceType.Name == PCISEnum.VoiceType.Helper && assessmentEmailLinkDetails.HelperID !=0)
                        {
                            var helperDetails = this.helperRepository.GetRowAsync(x => x.HelperID == assessmentEmailLinkDetails.HelperID).Result;
                            agencyID = helperDetails.AgencyID;
                            toDisplayName = string.Format("{0} {1} {2}", helperDetails.FirstName, string.IsNullOrEmpty(helperDetails.MiddleName) ? "" : helperDetails.MiddleName, helperDetails.LastName);
                            toDisplayName = toDisplayName.Replace("  ", " ");
                            toEmailID = helperDetails.Email;
                        }
                        else if (voiceType.Name == PCISEnum.VoiceType.Support && assessmentEmailLinkDetails?.PersonSupportID != 0)
                        {
                            var supportDetails = this._personSupportRepository.GetRowAsync(x => x.PersonSupportID == assessmentEmailLinkDetails.PersonSupportID).Result;
                            var personDetails = this.personRepository.GetRowAsync(x => x.PersonID == supportDetails.PersonID).Result;
                            agencyID = personDetails.AgencyID;
                            toDisplayName = string.Format("{0} {1} {2}", supportDetails.FirstName, string.IsNullOrEmpty(supportDetails.MiddleName) ? "" : supportDetails.MiddleName, supportDetails.LastName);
                            toDisplayName = toDisplayName.Replace("  ", " ");
                            toEmailID = supportDetails.Email;
                        }
                        if ((!string.IsNullOrEmpty(toEmailID) || requestFrom == PCISEnum.Invitation.SMS) && !string.IsNullOrEmpty(toDisplayName))
                        {
                            var otpTimeSpan = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmailOTP.otpTimeSpan, agencyID);
                            var expiryTime = DateTime.UtcNow.AddMinutes(int.Parse(otpTimeSpan.Value));
                            var otp = RandomOTPForEmailAssessment(agencyID);
                            var assessmentEmailOtpDTO = new AssessmentEmailOtpDTO()
                            {
                                AssessmentEmailLinkDetailsID = assessmentEmailLinkDetails.AssessmentEmailLinkDetailsID,
                                ExpiryTime = expiryTime,
                                Otp = otp,
                                UpdateDate = DateTime.UtcNow
                            };

                            var emailOtpResponse = InsertAssessmentEmailOtp(assessmentEmailOtpDTO);
                            if (emailOtpResponse > -1)
                            {
                                if (requestFrom != PCISEnum.Invitation.SMS)
                                {
                                    emailresponse = DraftForAssessmentEmailOtp(agencyID, otp, toDisplayName, toEmailID);
                                    if (emailresponse == HttpStatusCode.Accepted)
                                    {
                                        response.ResponseStatusCode = PCISEnum.StatusCodes.EmailSendSuccess;
                                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailSendSuccess);
                                    }
                                    else
                                    {
                                        response.ResponseStatusCode = PCISEnum.StatusCodes.EmailSendFailed;
                                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailSendFailed);
                                    }
                                }
                                else
                                {
                                    response.AssessmentEmailOtpID = emailOtpResponse;
                                    response.ResponseStatusCode = PCISEnum.StatusCodes.EmailSendSuccess;
                                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                                }
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidEmailAssessmentURL;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidEmailAssessmentURL);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.MissingEmailID;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.MissingEmailID);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidVoiceType;
                        response.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidVoiceType), voiceType?.Name);
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
        /// OTP generation logic
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        private string RandomOTPForEmailAssessment(long agencyID)
        {
            var OtpChars = PCISEnum.AssessmentEmailOTP.otpChars;
            var random = new Random();
            var otpLength = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmailOTP.otpLength, agencyID);
            var otp = new string(
                Enumerable.Repeat(OtpChars, int.Parse(otpLength.Value))
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return otp;
        }

        /// <summary>
        /// Draft Assessment Email Otp
        /// </summary>
        /// <param name="agencyID"></param>
        /// <param name="otp"></param>
        /// <param name="toDisplayName"></param>
        /// <param name="toEmailID"></param>
        /// <returns></returns>
        private HttpStatusCode DraftForAssessmentEmailOtp(long agencyID, string otp, string toDisplayName, string toEmailID)
        {
            var fromemail = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.FromEmail, agencyID);
            var fromemailDisplayName = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.FromDisplayName, agencyID);
            var emailText = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmailOTP.EmailText, agencyID);
            var emailSubject = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmailOTP.EmailSubject, agencyID);
            string assessmentEmailText = this.localize.GetLocalizedHtmlString(emailText.Value);

            var agency = this.agencyRepository.GetRowAsync(x => x.AgencyID == agencyID).Result;
            var agencyBaseURL = CreateBaseURL(agency.Abbrev);
            if (agencyBaseURL != null)
            {
                if (Uri.IsWellFormedUriString(agencyBaseURL, UriKind.Absolute))
                {
                    var agencyUri = new Uri(agencyBaseURL);
                    var baseUri = agencyUri.GetLeftPart(System.UriPartial.Authority);
                    //replace OTP code in email text
                    if (assessmentEmailText != null)
                    {
                        assessmentEmailText = assessmentEmailText.Replace(PCISEnum.AssessmentEmailOTP.applicationUrlReplaceText, baseUri);
                        assessmentEmailText = assessmentEmailText.Replace(PCISEnum.AssessmentEmailOTP.OTPCodeReplaceText, otp);
                        var emailUrlExpiry = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmailOTP.otpTimeSpan, agencyID);
                        assessmentEmailText = assessmentEmailText.Replace(PCISEnum.AssessmentEmailOTP.expiryCodeReplaceText, emailUrlExpiry.Value);
                    }
                }
            }

            //email body
            SendEmail sendEmail = new SendEmail()
            {
                Body = assessmentEmailText,
                IsHtmlEmail = true,
                Subject = this.localize.GetLocalizedHtmlString(emailSubject.Value),
                FromDisplayName = fromemailDisplayName.Value,
                FromEmail = fromemail.Value,
                ToDisplayName = toDisplayName,
                ToEmail = toEmailID
            };
            return _emailSender.SendEmailAsync(sendEmail);
        }

        /// <summary>
        /// Insert Assessment Email Otp
        /// </summary>
        /// <param name="assessmentEmailOtpDTO"></param>
        /// <returns>int</returns>
        public int InsertAssessmentEmailOtp(AssessmentEmailOtpDTO assessmentEmailOtpDTO)
        {
            try
            {
                var emailOtp = new AssessmentEmailOtp();
                if (!String.IsNullOrEmpty(assessmentEmailOtpDTO.Otp))
                {
                    var emailOtpRespose = this.assessmentEmailOtpRepository.FindEmailOtpByEmailLink(assessmentEmailOtpDTO.AssessmentEmailLinkDetailsID).Result;
                    if (emailOtpRespose != null)
                    {
                        assessmentEmailOtpDTO.AssessmentEmailOtpID = emailOtpRespose.AssessmentEmailOtpID;
                        emailOtp = this.assessmentEmailOtpRepository.UpdateEmailOtp(assessmentEmailOtpDTO);
                    }
                    else
                    {
                        emailOtp = this.assessmentEmailOtpRepository.AddAssessmentEmailOtpData(assessmentEmailOtpDTO);
                    }
                    if (emailOtp != null)
                    {
                        return emailOtp.AssessmentEmailOtpID;
                    }
                    else
                    {
                        return -1;
                    }
                }
                return -1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LastAssessmentResponseDTO GetLastAssessmentByPerson(Guid personIndex, long personQuestionnaireID, long personCollaborationID, int voiceTypeID, long voiceTypeFKID, UserTokenDetails userTokenDetails)
        {
            try
            {
                LastAssessmentResponseDTO response = new LastAssessmentResponseDTO();
                AssessmentDTO assessmentDTO = null;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                response.AssessmentData = null;
                var person = this.personRepository.GetPerson(personIndex);
                if (person != null)
                {
                    var sharedDTO = this.personRepository.GetSharedPersonQuestionnaireDetails(person.PersonID, userTokenDetails.AgencyID);
                    var helperColbDTO = new SharedDetailsDTO();
                    if (string.IsNullOrEmpty(sharedDTO?.SharedCollaborationIDs))
                    {
                        helperColbDTO = this.personRepository.GetPersonHelperCollaborationDetails(person.PersonID, userTokenDetails.AgencyID, userTokenDetails.UserID);
                    }
                    List<Assessment> assessment = this.assessmentRepository.GetLastAssessmentByPerson(person.PersonID, personQuestionnaireID, personCollaborationID, voiceTypeID, voiceTypeFKID, sharedDTO, userTokenDetails.AgencyID, helperColbDTO);
                    if (assessment != null && assessment?.Count != 0)
                    {
                        assessmentDTO = new AssessmentDTO();
                        this.mapper.Map<Assessment, AssessmentDTO>(assessment[0], assessmentDTO);
                    }
                    response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    response.AssessmentData = assessmentDTO;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetSharedAssessmentIDs For AssessmentDetails And AssessmnetVAlues
        /// </summary>
        /// <param name="personIndex"></param>
        /// <returns></returns>
        private string GetSharedAssessmentIDs(Guid personIndex, int questionnaireId, long agencyID)
        {
            try
            {
                var sharedAssessmentIDs = string.Empty;
                SharedPersonSearchDTO sharedPersonSearchDTO = new SharedPersonSearchDTO();
                sharedPersonSearchDTO.QuestionnaireID = questionnaireId;
                sharedPersonSearchDTO.PersonIndex = personIndex;
                sharedPersonSearchDTO.LoggedInAgencyID = agencyID;
                sharedAssessmentIDs = this.personRepository.GetSharedAssessmentIDs(sharedPersonSearchDTO);
                return sharedAssessmentIDs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetAssessmentById.
        /// </summary>
        /// <param name="assessmentId">assessmentId.</param>
        /// <returns>AssessmentResponseDetailsDTO.</returns>
        public AssessmentResponsesDetailDTO GetAssessmentById(int assessmentId)
        {
            try
            {
                AssessmentResponsesDetailDTO result = new AssessmentResponsesDetailDTO();
                var Response = this.assessmentRepository.GetAssessment(assessmentId).Result;
                if (Response != null)
                {

                    AssessmentDTO assessment = new AssessmentDTO();
                    this.mapper.Map<Assessment, AssessmentDTO>(Response, assessment);
                    result.Assessment = assessment;
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
        public CRUDResponseDTO BatchUploadAssessments(UploadAssessmentDTO assessmentDataToUpload)
        {
            try
            {
                int updateUserID = assessmentDataToUpload.UpdateUserID;
                long agencyID = assessmentDataToUpload.AgencyID;
                int questionnaireID = assessmentDataToUpload.QuestionnaireID;
                List<PersonQuestionnaire> list_personQuestionnaires = new List<PersonQuestionnaire>();
                List<long> list_personId = new List<long>();
                CRUDResponseDTO response = new CRUDResponseDTO();
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                if (assessmentDataToUpload?.AssessmentsToUpload.Count > 0)
                {
                    foreach (var assessment in assessmentDataToUpload.AssessmentsToUpload)
                    {
                        list_personId.Add(assessment.PersonID);
                        var personQuestionnaireID = this.assessmentRepository.GetPersonQuestionnaireID(assessment.PersonIndex, questionnaireID);
                        if (personQuestionnaireID == 0 && !(list_personQuestionnaires.Where(x => x.PersonID == assessment.PersonID).ToList().Count > 0))
                        {
                            var personQuestionnaireNew = new PersonQuestionnaire();
                            personQuestionnaireNew.CollaborationID = null;
                            personQuestionnaireNew.PersonID = assessment.PersonID;
                            personQuestionnaireNew.IsRemoved = false;
                            personQuestionnaireNew.QuestionnaireID = questionnaireID;
                            personQuestionnaireNew.UpdateUserID = updateUserID;
                            personQuestionnaireNew.UpdateDate = DateTime.UtcNow;
                            personQuestionnaireNew.IsActive = true;
                            personQuestionnaireNew.StartDate = DateTime.UtcNow;
                            list_personQuestionnaires.Add(personQuestionnaireNew);
                        }
                    }

                    this.personQuestionnaireRepository.AddBulkAsync(list_personQuestionnaires);
                    var list_personQuestionnaire = this.personQuestionnaireRepository.GetAllpersonQuestionnaire(list_personId, questionnaireID);
                    if (list_personQuestionnaire.Count() > 0)
                    {
                        List<Guid?> list_assessmentGUID = new List<Guid?>();
                        List<Assessment> list_assessmentsToUpload = new List<Assessment>();
                        var allVoicetype = this.voiceTypeRepository.GetAllVoiceType();
                        foreach (var assessmentDTO in assessmentDataToUpload.AssessmentsToUpload)
                        {
                            var voicetype = allVoicetype?.Where(x => x.VoiceTypeID == assessmentDTO.VoiceTypeID)?.FirstOrDefault()?.Name;
                            if ((voicetype == PCISEnum.VoiceType.Communimetric || voicetype == PCISEnum.VoiceType.Consumer) && assessmentDTO?.VoiceTypeFKID > 0)
                                assessmentDTO.VoiceTypeFKID = null;
                            var assessment = new Assessment
                            {
                                AssessmentID = 0,
                                PersonQuestionnaireID = list_personQuestionnaire.Where(x => x.PersonID == assessmentDTO.PersonID).Select(x => x.PersonQuestionnaireID).FirstOrDefault(),
                                VoiceTypeID = assessmentDTO.VoiceTypeID,
                                DateTaken = assessmentDTO.DateTaken,
                                ReasoningText = assessmentDTO.ReasoningText,
                                AssessmentReasonID = assessmentDTO.AssessmentReasonID,
                                AssessmentStatusID = assessmentDTO.AssessmentStatusID,
                                PersonQuestionnaireScheduleID = null,
                                IsUpdate = true,
                                Approved = null,
                                CloseDate = assessmentDTO.CloseDate,
                                IsRemoved = false,
                                UpdateDate = DateTime.UtcNow,
                                UpdateUserID = updateUserID,
                                NoteUpdateDate = DateTime.UtcNow,
                                NoteUpdateUserID = updateUserID,
                                EventDate = assessmentDTO.EventDate.HasValue ? assessmentDTO.EventDate.Value : assessmentDTO.EventDate,
                                EventNotes = assessmentDTO.EventNotes,
                                VoiceTypeFKID = assessmentDTO.VoiceTypeFKID,
                                AssessmentGUID = Guid.NewGuid()
                            };
                            assessmentDTO.AssessmentGUID = assessment.AssessmentGUID;
                            list_assessmentGUID.Add(assessment.AssessmentGUID);
                            list_assessmentsToUpload.Add(assessment);
                        }

                        var result = this.assessmentRepository.AddBulkAssessments(list_assessmentsToUpload);
                        var list_assessmentsUploaded = this.assessmentRepository.GetAssessmentListByGUID(list_assessmentGUID);

                        //----------------StorToQueueForMetrix Start--------------------//
                        var assessmentStatus = this.assessmentStatusRepository.GetAllAssessmentStatus();
                        var statusForMatrix = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Submitted || x.Name == PCISEnum.AssessmentStatus.Approved || x.Name == PCISEnum.AssessmentStatus.Returned).Select(x => x.AssessmentStatusID).ToList();
                        var list_assessmentsToMatrix = list_assessmentsUploaded.Where(x => statusForMatrix.Contains(x.AssessmentStatusID)).ToList();
                        foreach (var assessment in list_assessmentsToMatrix)
                        {
                            StoreInQueue(assessment.AssessmentID);
                        }
                        //----------------StorToQueueForMetrix End--------------------//

                        if (list_assessmentsUploaded.Count > 0)
                        {
                            List<AssessmentResponse> list_assessmentResponses = new List<AssessmentResponse>();
                            foreach (var assessmentDTO in assessmentDataToUpload.AssessmentsToUpload)
                            {
                                if (assessmentDTO.AssessmentResponses != null)
                                {
                                    foreach (var assessmentResponseInputDTO in assessmentDTO.AssessmentResponses)
                                    {
                                        var assessmentResponses = new AssessmentResponse
                                        {
                                            AssessmentResponseID = 0,
                                            AssessmentID = list_assessmentsUploaded.Where(x => x.AssessmentGUID == assessmentDTO.AssessmentGUID).Select(x => x.AssessmentID).FirstOrDefault(),
                                            PersonSupportID = assessmentResponseInputDTO.PersonSupportID,
                                            ResponseID = assessmentResponseInputDTO.ResponseID,
                                            ItemResponseBehaviorID = assessmentResponseInputDTO.ItemResponseBehaviorID == 0 ? null : assessmentResponseInputDTO.ItemResponseBehaviorID,
                                            IsRequiredConfidential = assessmentResponseInputDTO.IsRequiredConfidential,
                                            IsPersonRequestedConfidential = assessmentResponseInputDTO.IsPersonRequestedConfidential,
                                            IsOtherConfidential = assessmentResponseInputDTO.IsOtherConfidential,
                                            IsRemoved = false,
                                            UpdateDate = DateTime.UtcNow,
                                            UpdateUserID = updateUserID,
                                            QuestionnaireItemID = assessmentResponseInputDTO.QuestionnaireItemID,
                                            IsCloned = assessmentResponseInputDTO.IsCloned,
                                            CaregiverCategory = assessmentResponseInputDTO.CaregiverCategory,
                                            Priority = assessmentResponseInputDTO.Priority,
                                            AssessmentResponseGuid = assessmentResponseInputDTO.AssessmentResponseGUID ?? Guid.NewGuid(),
                                            Value = assessmentResponseInputDTO.Value,
                                            ItemID = null,
                                            ParentAssessmentResponseID = null
                                        };
                                        assessmentResponseInputDTO.AssessmentResponseGUID = assessmentResponses.AssessmentResponseGuid;
                                        list_assessmentResponses.Add(assessmentResponses);
                                    }
                                }
                            }
                            var result1 = this.assessmentResponseRepository.AddBulkAssessmentResponse(list_assessmentResponses);
                            var list_assessmentResponseGUID = result1.Select(x => x.AssessmentResponseGuid).ToList();
                            var list_assessmentResponse = this.assessmentResponseRepository.GetAssessmentResponseListByGUID(list_assessmentResponseGUID).Result;
                            if (list_assessmentResponse.Count > 0)
                            {
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.AssessmentImportSuccess, list_assessmentsUploaded.Count));
                                response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                                return response;
                            }
                        }
                    }
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.AssessmentImportFailed));
                    response.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetAssessmentByPersonQuestionaireIdAndStatus.
        /// </summary>
        /// <param name="personQuestionnaireId">personQuestionnaireId.</param>
        /// <param name="status">status.</param>
        /// <returns>AssessmentDetailsDTO</returns>
        public AssessmentResponsesDetailDTO GetAssessmentByPersonQuestionaireIdAndStatus(long personQuestionnaireId, string status)
        {
            try
            {

                AssessmentResponsesDetailDTO result = new AssessmentResponsesDetailDTO();
                var assessmentStatusID = this.assessmentStatusRepository.GetAssessmentStatus(PCISEnum.AssessmentStatus.Approved).Result.AssessmentStatusID;
                var response = this.assessmentRepository.GetAssessmentByPersonQuestionaireID(personQuestionnaireId, assessmentStatusID);
                if (response != null)
                {
                    AssessmentDTO assessment = new AssessmentDTO();
                    this.mapper.Map<Assessment, AssessmentDTO>(response, assessment);
                    result.Assessment = assessment;
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
        /// GetAssessmentResponses.
        /// </summary>
        /// <param name="assessmentId">assessmentId</param>
        /// <returns>AssessmentResponsesDTO.</returns>
        public AssessmentResponsesDetailsDTO GetAssessmentResponses(int assessmentId)
        {
            try
            {
                AssessmentResponsesDetailsDTO result = new AssessmentResponsesDetailsDTO();
                var response = this.assessmentResponseRepository.GetAllAssessmentResponses(assessmentId).Result.ToList();
                if (response != null)
                {
                    result.AssessmentResponses = response;
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
        /// GetAssessmentsByPersonQuestionaireID.
        /// </summary>
        /// <param name="personQuestionnaireId">personQuestionnaireId.</param>
        /// <returns>AssessmentResponsesDetailDTO.</returns>
        public AssessmentResponsesDetailDTO GetAssessmentsByPersonQuestionaireID(long personQuestionnaireId)
        {
            try
            {

                AssessmentResponsesDetailDTO result = new AssessmentResponsesDetailDTO();
                List<int> assessmentStatusIDList = new List<int>();
                assessmentStatusIDList.Add(this.assessmentStatusRepository.GetAssessmentStatus(PCISEnum.AssessmentStatus.Submitted).Result.AssessmentStatusID);
                assessmentStatusIDList.Add(this.assessmentStatusRepository.GetAssessmentStatus(PCISEnum.AssessmentStatus.Approved).Result.AssessmentStatusID);
                var response = this.personQuestionnaireRepository.GetAssessmentsByPersonQuestionaireID(personQuestionnaireId, assessmentStatusIDList).FirstOrDefault();
                if (response != null)
                {
                    AssessmentDTO assessment = new AssessmentDTO();
                    this.mapper.Map<Assessment, AssessmentDTO>(response, assessment);
                    result.Assessment = assessment;
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
        /// GetAssessmentResponseFOrDashboardCalculation.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <param name="assessmentId">assessmentId.</param>
        /// <returns>AssessmentResponsesDetailDTO.</returns>
        public AssessmentResponsesDetailsDTO GetAssessmentResponseFOrDashboardCalculation(long personId, int assessmentId)
        {
            try
            {

                AssessmentResponsesDetailsDTO result = new AssessmentResponsesDetailsDTO();
                var assessmentStatuses = this.assessmentStatusRepository.GetAllAssessmentStatus();
                var approvedStatusID = assessmentStatuses.Where(x => x.Name == PCISEnum.AssessmentStatus.Approved).FirstOrDefault().AssessmentStatusID;
                var submittedStatusID = assessmentStatuses.Where(x => x.Name == PCISEnum.AssessmentStatus.Submitted).FirstOrDefault().AssessmentStatusID;
                var returnedStatusID = assessmentStatuses.Where(x => x.Name == PCISEnum.AssessmentStatus.Returned).FirstOrDefault().AssessmentStatusID;
                var response = this.assessmentResponseRepository.GetAssessmentResponseFOrDashboardCalculation(personId, assessmentId, submittedStatusID, approvedStatusID, returnedStatusID);
                if (response != null)
                {
                    result.AssessmentResponses = response;
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
        /// GetQuestionnaireSkipLogic.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>SkipLogicResponseDetailsDTO.</returns>
        public SkipLogicResponseDetailsDTO GetQuestionnaireSkipLogic(int questionnaireId)
        {
            try
            {

                SkipLogicResponseDetailsDTO result = new SkipLogicResponseDetailsDTO();
                var response = this.questionnaireSkipLogicRuleRepository.GetQuestionnaireSkipLogic(questionnaireId);
                if (response != null)
                {
                    foreach (var item in response)
                    {
                        item.QuestionnaireSkipLogicRuleActions = this.questionnaireSkipLogicRuleActionRepository.GetQuestionnaireSkipLogicAction(item.QuestionnaireSkipLogicRuleID);
                        item.QuestionnaireSkipLogicRuleConditions = this.questionnaireSkipLogicRuleConditionRepository.GetQuestionnaireSkipLogicCondition(item.QuestionnaireSkipLogicRuleID);
                    }
                    result.QuestionnaireSkipLogicRules = response;
                    if (response.Count > 0)
                    {
                        result.QuestionnaireID = response[0]?.QuestionnaireID;
                        result.HasSkipLogic = response[0]?.HasSkipLogic;
                    }
                    else
                    {
                        result.QuestionnaireID = null;
                        result.HasSkipLogic = null;
                    }
                    result.QuestionnaireSkipLogicRules = response;
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
        /// GetAssessmentDetailsForEHRUpdate.
        /// </summary>
        /// <param name="ehrAssessmentIDs">ehrAssessmentIDs.</param>
        /// <returns>EHRAssessmentResponseDTO.</returns>
        public EHRAssessmentResponseDTO GetAssessmentDetailsForEHRUpdate(string ehrAssessmentIDs)
        {
            try
            {
                EHRAssessmentResponseDTO response = new EHRAssessmentResponseDTO();
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                var eHRAssessmentDTOlist = this.assessmentRepository.GetAssessmentDetailsForEHRUpdate(ehrAssessmentIDs);
                response.EHRAssessments = eHRAssessmentDTOlist ?? new List<EHRAssessmentDTO>();
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetAssessmentIDsForEHRUpdate.
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns>EHRAssessmentResponseDTO.</returns>
        public EHRAssessmentResponseDTO GetAssessmentIDsForEHRUpdate(long agencyID)
        {
            try
            {
                EHRAssessmentResponseDTO response = new EHRAssessmentResponseDTO();
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                var assessmentStatuses = this.assessmentStatusRepository.GetAllAssessmentStatus();
                var approvedStatusID = assessmentStatuses.Where(x => x.Name == PCISEnum.AssessmentStatus.Approved).FirstOrDefault().AssessmentStatusID;
                var eHRAssessmentIds = this.assessmentRepository.GetAssessmentIDsForEHRUpdate(approvedStatusID, agencyID, PCISEnum.EHRUpdateStatus.Pending);
                response.EHRAssessmentIDs = eHRAssessmentIds ?? new List<string>();
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// UpdateAssessmentFlagAfterEHRUpdate.
        /// </summary>
        /// <param name="ehrAssessmentIDsList">ehrAssessmentIDsList.</param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO UpdateAssessmentFlagAfterEHRUpdate(List<int> ehrAssessmentIDsList)
        {
            try
            {
                CRUDResponseDTO response = new CRUDResponseDTO();
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                var assessments = this.assessmentRepository.GetAssessmentListByID(ehrAssessmentIDsList);
                assessments.ForEach(x => x.EHRUpdateStatus = PCISEnum.EHRUpdateStatus.Done);
                this.assessmentRepository.UpdateBulkAssessments(assessments);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AssessmentDetailsResponseDTOForExternal GetAssessmentDetailsListsForExternal(AssessmentSearchInputDTO assessmentSearchInputDTO, LoggedInUserDTO loggedInUserDTO)
        {

            try
            {
                AssessmentDetailsResponseDTOForExternal response = new AssessmentDetailsResponseDTOForExternal();
                response.AssessmentDataList = null;
                response.TotalCount = 0;
                if (loggedInUserDTO.AgencyId != 0)
                {
                    List<QueryFieldMappingDTO> fieldMappingList = GetAssessmentListConfigurationForExternal();
                    var queryBuilderDTO = this.queryBuilder.BuildQueryForExternalAPI(assessmentSearchInputDTO, fieldMappingList);
                    List<AssessmentDetailsListDTO> personList = new List<AssessmentDetailsListDTO>();
                    if (queryBuilderDTO.Page <= 0)
                    {
                        response.AssessmentDataList = null;
                        response.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                        response.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        return response;
                    }
                    else if (queryBuilderDTO.PageSize <= 0)
                    {
                        response.AssessmentDataList = null;
                        response.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                        response.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        return response;
                    }
                    else
                    {
                        response.AssessmentDataList = this.assessmentRepository.GetAssessmentDetailsListsForExternal(loggedInUserDTO, queryBuilderDTO, assessmentSearchInputDTO?.SearchFields?.HelperIndex);
                        if (response.AssessmentDataList.Count > 0)
                        {
                            response.TotalCount = response.AssessmentDataList[0].TotalCount;
                        }
                    }
                }
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetPersonListConfigurationForExternal.
        /// Always the First item to the list should be the column deatils for order by (With fieldTable as OrderBy for just user identification).
        /// And the next item should be the fieldMapping for order by Column specified above.
        /// </summary>
        /// <returns></returns>
        private List<QueryFieldMappingDTO> GetAssessmentListConfigurationForExternal()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {   //Exclusively For Order By
                fieldName = "DateTaken",
                fieldAlias = "DateTaken",
                fieldTable = "OrderBy",
                fieldType = "desc"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "DateTaken",
                fieldAlias = "DateTaken",
                fieldTable = "A",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "QuestionnaireID",
                fieldAlias = "QuestionnaireID",
                fieldTable = "PQ",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "HelperIndex",
                fieldAlias = "HelperIndex",
                fieldTable = "H",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "PersonIndex",
                fieldAlias = "PersonIndex",
                fieldTable = "P",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "AssessmentID",
                fieldAlias = "AssessmentID",
                fieldTable = "A",
                fieldType = "int"
            });
            return fieldMappingList;
        }

        private string GetHelperAssessmentIDs(Guid personIndex, int questionnaireId, UserTokenDetails userTokenDetails)
        {
            try
            {
                var sharedAssessmentIDs = string.Empty;
                SharedPersonSearchDTO sharedPersonSearchDTO = new SharedPersonSearchDTO();
                sharedPersonSearchDTO.QuestionnaireID = questionnaireId;
                sharedPersonSearchDTO.PersonIndex = personIndex;
                sharedPersonSearchDTO.LoggedInAgencyID = userTokenDetails.AgencyID;
                sharedPersonSearchDTO.UserID = userTokenDetails.UserID;
                var sharedDetails = this.personRepository.GetHelpersAssessmentIDs(sharedPersonSearchDTO);
                return sharedDetails?.SharedAssessmentIDs;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// GetReceiversDetailsForReminderInvite.
        /// Get all receivers personal details for sending InviteToComplete for Reminders.
        /// </summary>
        /// <param name="personQuestionnaireIds"></param>
        /// <param name="voiceTypeOfInvite"></param>
        /// <returns></returns>
        public InviteMailReceiversDetailsResponseDTO GetReceiversDetailsForReminderInvite(List<long> personQuestionnaireIds, string voiceTypeOfInvite)
        {
            try
            {
                InviteMailReceiversDetailsResponseDTO response = new InviteMailReceiversDetailsResponseDTO();
                var result = this.assessmentRepository.GetReceiversDetailsForReminderInvite(personQuestionnaireIds, voiceTypeOfInvite);
                if (result.Count > 0)
                {
                    var voiceType = this.voiceTypeRepository.GetAllVoiceType();
                    int voiceTypeId = 0;
                    if (voiceTypeOfInvite == PCISEnum.InviteToCompleteReceivers.Person)
                    {
                        voiceTypeId = voiceType.Where(x => x.Name == PCISEnum.VoiceType.Consumer).FirstOrDefault().VoiceTypeID;
                    }
                    if (voiceTypeOfInvite == PCISEnum.InviteToCompleteReceivers.Helpers || voiceTypeOfInvite == PCISEnum.InviteToCompleteReceivers.LeadHelper)
                    {
                        voiceTypeId = voiceType.Where(x => x.Name == PCISEnum.VoiceType.Helper).FirstOrDefault().VoiceTypeID;
                    }
                    if (voiceTypeOfInvite == PCISEnum.InviteToCompleteReceivers.Supports)
                    {
                        voiceTypeId = voiceType.Where(x => x.Name == PCISEnum.VoiceType.Support).FirstOrDefault().VoiceTypeID;
                    }
                    var assessmentStatusID = this.assessmentStatusRepository.GetAssessmentStatus(PCISEnum.AssessmentStatus.EmailSent).Result.AssessmentStatusID;
                    result.ForEach(x => { x.VoiceTypeID = voiceTypeId; x.AssessmentStatusID = assessmentStatusID; });
                }
                response.IniviteToCompleteMailDetails = result;
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// BulkAddAssessmentsForReminders.
        /// Add assessment in bulk for the reminders of inviteToCOmplete
        /// </summary>
        /// <param name="assessmentDataToUpload"></param>
        /// <returns></returns>
        public BulkAddAssessmentResponseDTO BulkAddAssessmentsForReminders(AssessmentBulkAddOnInviteDTO assessmentDataToUpload)
        {
            try
            {
                BulkAddAssessmentResponseDTO response = new BulkAddAssessmentResponseDTO();
                if (assessmentDataToUpload.AssessmentsDTO.Count > 0)
                {
                    //Add assessments
                    var list_assessmentGUID = assessmentDataToUpload.AssessmentsDTO.Select(x => x.AssessmentGuid).ToList();
                    List<Assessment> assessments = new List<Assessment>();
                    this.mapper.Map<List<AssessmentDTO>, List<Assessment>>(assessmentDataToUpload.AssessmentsDTO, assessments);
                    this.assessmentRepository.AddBulkAssessments(assessments);
                    //Get assessmentID by assessmentGuid
                    var list_assessmentsUploaded = this.assessmentRepository.GetAssessmentListByGUID(list_assessmentGUID);

                    //update AssessmentEmailLinkDetailsDTO with the assessmentID from above assessments.
                    assessmentDataToUpload.AssessmentEmailLinkDetailsDTO.ForEach(x => x.AssessmentID = list_assessmentsUploaded.Where(y => y.AssessmentGUID == x.AssessmentGUID).FirstOrDefault().AssessmentID);
                    var assessmentEmaiLink = new List<AssessmentEmailLinkDetails>();
                    this.mapper.Map<List<AssessmentEmailLinkDetailsDTO>, List<AssessmentEmailLinkDetails>>(assessmentDataToUpload.AssessmentEmailLinkDetailsDTO, assessmentEmaiLink);
                    var result = this._assessmentEmailLinkRepository.AddBulkAssessmentEmailLinkDetails(assessmentEmaiLink);

                    //Get assessmentEmailLInkDetailsIndex by assessmentEmailLinkGuid
                    var list_assessmentEmailLinkGUID = assessmentDataToUpload.AssessmentEmailLinkDetailsDTO.Select(x => x.AssessmentEmailLinkGUID).ToList();
                    var list_assessmentsEmailLinks = this._assessmentEmailLinkRepository.GetEmailLinkDataByGuid(list_assessmentEmailLinkGUID);
                    if(list_assessmentsEmailLinks.Count>0)
                    {
                        response.AssessmentEmailLinkDetails = JsonConvert.SerializeObject(list_assessmentsEmailLinks);
                    }
                }
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }        
    }
}
