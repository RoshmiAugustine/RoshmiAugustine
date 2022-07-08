// -----------------------------------------------------------------------
// <copyright file="CollaborationService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.Domain.Interfaces.Common;
using AutoMapper;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.Interfaces;
using Newtonsoft.Json;
using Opeeka.PICS.Domain.DTO.ExternalAPI;

namespace Opeeka.PICS.Domain.Services
{
    public class CollaborationService : BaseService, ICollaborationService
    {
        /// Initializes a new instance of the collaborationRepository/> class.
        private readonly ICollaborationRepository collaborationRepository;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<AgencyService> logger;

        /// Initializes a new instance of the collaborationQuestionnaireRepository/> class.
        private readonly ICollaborationQuestionnaireRepository collaborationQuestionnaireRepository;

        /// Initializes a new instance of the collaborationTagRepository/> class.
        private readonly ICollaborationTagRepository collaborationTagRepository;

        /// Initializes a new instance of the collaborationLeadRepository/> class.
        private readonly ICollaborationLeadRepository collaborationLeadRepository;
        /// <summary>
        /// Defines the PersonQuestionnaireRepository.
        /// </summary>
        private readonly IPersonQuestionnaireRepository personQuestionnaireRepository;

        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;

        /// Initializes a new instance of the Utility class.
        private readonly IUtility utility;

        /// Initializes a new instance of the httpContext class.
        private readonly IHttpContextAccessor httpContext;

        private readonly IQueryBuilder queryBuilder;

        private readonly IMapper mapper;

        private readonly IPersonCollaborationRepository personCollaborationRepository;
        public IQueue _queue { get; }

        private readonly IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository;
        private readonly IPersonQuestionnaireScheduleService personQuestionnaireScheduleService;
        private readonly ILookupRepository lookupRepository;
        private readonly IAgencyRepository _agencyRepository;
 

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaborationService"/> class.
        /// </summary>
        /// <param name="personRepository,logger">personRepository.</param>
        public CollaborationService(ICollaborationRepository collaborationRepository, ILogger<AgencyService> logger, ICollaborationQuestionnaireRepository collaborationQuestionnaireRepository, ICollaborationTagRepository collaborationTagRepository, ICollaborationLeadRepository collaborationLeadRepository, LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, IPersonQuestionnaireRepository personQuestionnaireRepository, IUtility utility, IQueryBuilder querybuild, IMapper mapper, IPersonCollaborationRepository personCollaborationRepository, IQueue _queue, IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository, IPersonQuestionnaireScheduleService personQuestionnaireScheduleService, ILookupRepository lookupRepository,
      IAgencyRepository _agencyRepository)
            : base(configRepo, httpContext)
        {
            this.collaborationRepository = collaborationRepository;
            this.logger = logger;
            this.collaborationQuestionnaireRepository = collaborationQuestionnaireRepository;
            this.collaborationTagRepository = collaborationTagRepository;
            this.collaborationLeadRepository = collaborationLeadRepository;
            this.personQuestionnaireRepository = personQuestionnaireRepository;
            this.localize = localizeService;
            this.utility = utility;
            this.httpContext = httpContext;
            this.queryBuilder = querybuild;
            this.mapper = mapper;
            this.personCollaborationRepository = personCollaborationRepository;
            this._queue = _queue;
            this.personQuestionnaireScheduleRepository = personQuestionnaireScheduleRepository;
            this.lookupRepository = lookupRepository;
            this._agencyRepository = _agencyRepository;
            this.personQuestionnaireScheduleService = personQuestionnaireScheduleService;
        }

        /// <summary>
        /// To add Collaboration.
        /// </summary>
        /// <returns>.</returns>
        public CollaborationResponseDTO AddCollaborationDetails(CollaborationDetailsDTO collaborationDetailsDTO)
        {
            try
            {
                CollaborationResponseDTO resultDTO = new CollaborationResponseDTO();
                CollaborationDTO collaborationDTO = CreateCollaboration(collaborationDetailsDTO, PCISEnum.Constants.Add);
                if (collaborationDTO.UpdateUserID != 0)
                {
                    var collaborationID = this.collaborationRepository.AddCollaboration(collaborationDTO);
                    if (collaborationID != 0)
                    {
                        foreach (var collaborationQuestionnaire in collaborationDetailsDTO.Questionnaire)
                        {
                            if (collaborationQuestionnaire.QuestionnaireID != 0)
                            {

                                var questionnaireList = CreateCollaborationQuestionnaire(collaborationID, collaborationQuestionnaire, PCISEnum.Constants.Add);
                                if (questionnaireList != null)
                                {
                                    var collaborationQuestionnaireID = this.collaborationQuestionnaireRepository.AddCollaborationQuestionnaire(questionnaireList);
                                    if (collaborationQuestionnaireID == 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        foreach (var categories in collaborationDetailsDTO.Category)
                        {
                            var categoryList = CreateCollaborationTag(collaborationID, categories, PCISEnum.Constants.Add);
                            if (categoryList != null)
                            {
                                var collaborationCategoryID = this.collaborationTagRepository.AddCollaborationTag(categoryList);
                                if (collaborationCategoryID == 0)
                                {
                                    break;
                                }
                            }
                        }
                        foreach (var collaborationLead in collaborationDetailsDTO.Lead)
                        {
                            var leadList = CreateCollaborationLead(collaborationID, collaborationLead, PCISEnum.Constants.Add);
                            if (leadList != null)
                            {
                                var collaborationLeadID = this.collaborationLeadRepository.AddCollaborationLead(leadList);
                                if (collaborationLeadID == 0)
                                {
                                    break;
                                }
                            }
                        }
                        var collaboration = this.collaborationRepository.GetCollaborationAsync(collaborationID).Result;

                        resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                        resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        resultDTO.Id = collaboration.CollaborationIndex;
                        resultDTO.CollaborationId = collaborationID;
                        return resultDTO;
                    }
                }
                return resultDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Exception occurred while adding collaboration. {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// To create CollaborationDTO.
        /// </summary>
        /// <param name="collaborationDetailsDTO">collaborationDetailsDTO.</param>
        /// <param name="from">from.</param>
        /// <returns>CollaborationDTO.</returns>
        public CollaborationDTO CreateCollaboration(CollaborationDetailsDTO collaborationDetailsDTO, string from)
        {
            CollaborationDTO collaborationDTO = new CollaborationDTO();
            if (from == PCISEnum.Constants.Edit)
            {
                if (collaborationDetailsDTO.CollaborationID != 0)
                {
                    collaborationDTO.CollaborationID = collaborationDetailsDTO.CollaborationID;
                }
            }
            if (String.IsNullOrEmpty(collaborationDetailsDTO.Name) || collaborationDetailsDTO.UpdateUserID == 0)
            {
                return null;

            }
            else
            {
                collaborationDTO.Name = collaborationDetailsDTO.Name;
                if (from == PCISEnum.Constants.Edit)
                {
                    collaborationDTO.CollaborationIndex = collaborationDetailsDTO.CollaborationIndex;
                    collaborationDTO.UpdateDate = DateTime.UtcNow;
                }
                collaborationDTO.UpdateUserID = collaborationDetailsDTO.UpdateUserID;
                collaborationDTO.TherapyTypeID = collaborationDetailsDTO.TherapyTypeID;
                collaborationDTO.StartDate = collaborationDetailsDTO.StartDate.Date;
                collaborationDTO.EndDate = collaborationDetailsDTO.EndDate.HasValue ? collaborationDetailsDTO.EndDate.Value.Date : collaborationDetailsDTO.EndDate;
                collaborationDTO.Code = collaborationDetailsDTO.Code;
                collaborationDTO.IsRemoved = false;
                collaborationDTO.CollaborationLevelID = collaborationDetailsDTO.CollaborationLevelID;
                collaborationDTO.AgencyID = collaborationDetailsDTO.AgencyID;
                collaborationDTO.Description = collaborationDetailsDTO.Description;
                collaborationDTO.Abbreviation = collaborationDetailsDTO.Abbreviation;
            }
            return collaborationDTO;
        }

        ///<summary>
        /// To create CollaborationQuestionnaireDTO.
        /// </summary>
        /// <param name="collaborationID">collaborationID.</param>
        /// <param name="collaborationQuestionnaire">collaborationQuestionnaire.</param>
        /// <param name="from">from.</param>
        /// <returns>CollaborationQuestionnaireDTO.</returns>
        public CollaborationQuestionnaireDTO CreateCollaborationQuestionnaire(int collaborationID, CollaborationQuestionnaireDTO collaborationQuestionnaire, string from)
        {
            CollaborationQuestionnaireDTO questionnaireDTO = new CollaborationQuestionnaireDTO();

            if (collaborationID != 0 && collaborationQuestionnaire != null)
            {
                if (from == PCISEnum.Constants.Edit)
                {
                    questionnaireDTO.CollaborationQuestionnaireID = collaborationQuestionnaire.CollaborationQuestionnaireID;
                }
                questionnaireDTO.CollaborationID = collaborationID;
                questionnaireDTO.QuestionnaireID = collaborationQuestionnaire.QuestionnaireID;
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                questionnaireDTO.StartDate = collaborationQuestionnaire.StartDate.HasValue ? collaborationQuestionnaire.StartDate.Value.Date : collaborationQuestionnaire.StartDate;
                questionnaireDTO.EndDate = collaborationQuestionnaire.EndDate.HasValue ? collaborationQuestionnaire.EndDate.Value.Date : collaborationQuestionnaire.EndDate;
                questionnaireDTO.IsMandatory = collaborationQuestionnaire.IsMandatory;
                questionnaireDTO.IsReminderOn = collaborationQuestionnaire.IsReminderOn;
                return questionnaireDTO;
            }
            else
            {
                return null;
            }
        }

        /// <summary>        
        /// To create CollaborationTagDTO.
        /// </summary>
        /// <param name="collaborationID">collaborationID.</param>
        /// <param name="collaborationTag">collaborationTag.</param>
        /// <param name="from">from.</param>
        /// <returns>CollaborationTagDTO List.</returns>
        public CollaborationTagDTO CreateCollaborationTag(int collaborationID, CollaborationTagDTO collaborationTag, string from)
        {
            CollaborationTagDTO collaborationTagDTO = new CollaborationTagDTO();

            if (collaborationID != 0 && collaborationTag != null)
            {
                if (from == PCISEnum.Constants.Edit)
                {
                    collaborationTagDTO.CollaborationTagID = collaborationTag.CollaborationTagID;
                }
                collaborationTagDTO.CollaborationID = collaborationID;
                collaborationTagDTO.CollaborationTagTypeID = collaborationTag.CollaborationTagTypeID;
                return collaborationTagDTO;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// To create CollaborationLead.
        /// </summary>
        /// <param name="collaborationID">collaborationID.</param>
        /// <param name="collaborationLeadHistory">collaborationLeadHistory.</param>
        /// <param name="from">from.</param>
        /// <returns>CollaborationLeadHistoryDTO list.</returns>
        public CollaborationLeadHistoryDTO CreateCollaborationLead(int collaborationID, CollaborationLeadHistoryDTO collaborationLeadHistory, string from)
        {
            CollaborationLeadHistoryDTO collaborationLeadHistoryDTO = new CollaborationLeadHistoryDTO();

            if (collaborationID != 0 && collaborationLeadHistory != null)
            {
                if (from == PCISEnum.Constants.Edit)
                {
                    collaborationLeadHistoryDTO.CollaborationLeadHistoryID = collaborationLeadHistory.CollaborationLeadHistoryID;
                }
                collaborationLeadHistoryDTO.CollaborationID = collaborationID;
                collaborationLeadHistoryDTO.LeadUserID = collaborationLeadHistory.LeadUserID;
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                collaborationLeadHistoryDTO.StartDate = collaborationLeadHistory.StartDate.HasValue ? collaborationLeadHistory.StartDate.Value.Date : collaborationLeadHistory.StartDate;
                collaborationLeadHistoryDTO.EndDate = collaborationLeadHistory.EndDate.HasValue ? collaborationLeadHistory.EndDate.Value.Date : collaborationLeadHistory.EndDate;
                return collaborationLeadHistoryDTO;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Update Collaboration Details.
        /// </summary>
        /// <param name="collaborationDetailsDTO">collaborationDetailsDTO.</param>
        /// <returns>CollaborationResponseDTO.</returns>
        public CollaborationResponseDTO UpdateCollaborationDetails(CollaborationDetailsDTO collaborationDetailsDTO, long updateUserID, string callingType = "")
        {
            try
            {
                CollaborationResponseDTO resultDTO = new CollaborationResponseDTO();
                if (collaborationDetailsDTO.CollaborationIndex != Guid.Empty)
                {
                    CollaborationDTO collaborationDTO = CreateCollaboration(collaborationDetailsDTO, PCISEnum.Constants.Edit);
                    var collaboration = this.collaborationRepository.GetAsync(collaborationDetailsDTO.CollaborationIndex).Result;
                    collaborationDTO.CollaborationID = collaboration.CollaborationID;

                    if (collaborationDTO != null && collaborationDTO?.CollaborationID != 0)
                    {
                        List<CollaborationQuestionnaireDTO> collaborationQuestionnaireData = this.collaborationQuestionnaireRepository.GetCollaborationQuestionnaireData(collaborationDTO.CollaborationID);
                        var collaborationCategorylist = this.collaborationTagRepository.GetCollaborationCategories(collaborationDTO.CollaborationID);
                        var collaborationLeadlist = this.collaborationLeadRepository.GetCollaborationLeads(collaborationDTO.CollaborationID);
                        if (!ValidateExternalAPICollaborationEditForPrimaryKeyIDs(collaborationQuestionnaireData, collaborationCategorylist, collaborationLeadlist, collaborationDetailsDTO, resultDTO, callingType))
                            return resultDTO;

                        var collaborationresult = this.collaborationRepository.UpdateCollaboration(collaborationDTO);
                        if (collaborationresult != null)
                        {
                            if (collaborationDTO.CollaborationID != 0)
                            {
                                if (collaborationDetailsDTO.Questionnaire != null && collaborationDetailsDTO.Questionnaire.Count > 0)
                                {
                                    List<CollaborationQuestionnaireDTO> removableQuestionnaireData = collaborationQuestionnaireData.Where(x => !collaborationDetailsDTO.Questionnaire.Select(y => y.CollaborationQuestionnaireID).ToList().Contains(x.CollaborationQuestionnaireID)).ToList();

                                    foreach (var collaborationQuestionnaire in removableQuestionnaireData)
                                    {
                                        UpdateCollaborationAndPerson_Questionaires(collaborationQuestionnaire);
                                    }

                                    foreach (var collaborationQuestionnaire in collaborationDetailsDTO.Questionnaire)
                                    {
                                        CollaborationQuestionnaireDTO questionnaireList = CreateCollaborationQuestionnaire(collaborationDTO.CollaborationID, collaborationQuestionnaire, PCISEnum.Constants.Edit);

                                        if (questionnaireList.CollaborationQuestionnaireID > 0)
                                        {
                                            var UpdatedCollaborationQuestionnaire = this.collaborationQuestionnaireRepository.UpdateCollaborationQuestionnaire(questionnaireList);
                                            var previousSettings = collaborationQuestionnaireData.Where(x => x.IsReminderOn == false && x.CollaborationQuestionnaireID == collaborationQuestionnaire.CollaborationQuestionnaireID);

                                            if (collaborationQuestionnaire.IsReminderOn == true && previousSettings?.Count() > 0)
                                            {
                                                var personQuestionnairesToBeAddedToQueue = this.personQuestionnaireRepository.GetAllPersonQuestionnaireIdsByCollaborationID(UpdatedCollaborationQuestionnaire.CollaborationID);
                                                personQuestionnairesToBeAddedToQueue = personQuestionnairesToBeAddedToQueue?.Distinct().ToList();
                                                personQuestionnairesToBeAddedToQueue?.ForEach(x => StoreInQueue(x));
                                            }
                                            else if (collaborationQuestionnaire.IsReminderOn == false)
                                            {

                                                var personQuestionnaire = this.personQuestionnaireRepository.GetPersonQuestionnaireByCollaborationAndQuestionnaire(collaborationDetailsDTO.CollaborationID, collaborationQuestionnaire.QuestionnaireID).Result;
                                                var personQuestionnaireIDs = personQuestionnaire.Select(x => x.PersonQuestionnaireID).ToList();
                                                if (personQuestionnaireIDs.Count > 0)
                                                {
                                                    var personQuestionnaireScheduleToRemove = personQuestionnaireScheduleRepository.GetAsync(x => personQuestionnaireIDs.Contains(x.PersonQuestionnaireID)).Result.ToList();
                                                    if (personQuestionnaireScheduleToRemove.Count > 0)
                                                    {
                                                        personQuestionnaireScheduleToRemove.ForEach(x => x.IsRemoved = true);
                                                        personQuestionnaireScheduleService.UpdateBulkPersonQuestionnaireSchedule(personQuestionnaireScheduleToRemove);
                                                    }
                                                }
                                            }
                                        
                                        }
                                        else
                                        {
                                            var collaborationQuestionnaireID = this.collaborationQuestionnaireRepository.AddCollaborationQuestionnaire(questionnaireList);
                                            var personCollaborationList = this.personCollaborationRepository.GetPersonCollaborationByCollaborationID(questionnaireList.CollaborationID).Result;

                                                foreach (var personCollaboration in personCollaborationList)
                                                {
                                                    PersonQuestionnaireDTO PersonQuestionnaireDTO = CreatePersonQuestionnaireDTO(collaborationQuestionnaire, personCollaboration);
                                                    var personQuestionnaireID = this.personQuestionnaireRepository.AddPersonQuestionnaire(PersonQuestionnaireDTO);
                                                    if (personQuestionnaireID > 0 && questionnaireList.IsReminderOn)
                                                    {
                                                        StoreInQueue(personQuestionnaireID);
                                                    }
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var collaborationQuestionnaire in collaborationQuestionnaireData)
                                    {
                                        UpdateCollaborationAndPerson_Questionaires(collaborationQuestionnaire); 
                                    }
                                }

                                if (collaborationDetailsDTO != null && collaborationDetailsDTO.Category.Count > 0)
                                {
                                    var removablecollaborationCategorylist = collaborationCategorylist.Where(x => !collaborationDetailsDTO.Category.Select(y => y.CollaborationTagID).ToList().Contains(x.CollaborationTagID)).ToList();
                                    foreach (var categories in removablecollaborationCategorylist)
                                    {
                                        categories.IsRemoved = true;
                                        var updatedCategoryData = this.collaborationTagRepository.UpdateCollaborationTag(categories);
                                    }

                                    foreach (var categories in collaborationDetailsDTO.Category)
                                    {
                                        var categoryList = CreateCollaborationTag(collaborationDTO.CollaborationID, categories, PCISEnum.Constants.Edit);

                                        if (categoryList.CollaborationTagID > 0)
                                        {
                                            var updatedCategoryData = this.collaborationTagRepository.UpdateCollaborationTag(categoryList);
                                        }
                                        else
                                        {
                                            var collaborationCategoryID = this.collaborationTagRepository.AddCollaborationTag(categoryList);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var categories in collaborationCategorylist)
                                    {
                                        categories.IsRemoved = true;
                                        var updatedCategoryData = this.collaborationTagRepository.UpdateCollaborationTag(categories);
                                    }
                                }

                                if (collaborationDetailsDTO.Lead != null && collaborationDetailsDTO.Lead.Count > 0)
                                {
                                    var removablecollaborationLeadlist = collaborationLeadlist.Where(x => !collaborationDetailsDTO.Lead.Select(y => y.CollaborationLeadHistoryID).ToList().Contains(x.CollaborationLeadHistoryID)).ToList();

                                    foreach (var collaborationLead in removablecollaborationLeadlist)
                                    {
                                        collaborationLead.IsRemoved = true;
                                        var collaborationLeadData = this.collaborationLeadRepository.UpdateCollaborationLeadHistory(collaborationLead);
                                    }

                                    foreach (var collaborationLead in collaborationDetailsDTO.Lead)
                                    {
                                        var leadList = CreateCollaborationLead(collaborationDTO.CollaborationID, collaborationLead, PCISEnum.Constants.Edit);

                                        if (leadList.CollaborationLeadHistoryID > 0 && leadList != null)
                                        {
                                            var collaborationLeadData = this.collaborationLeadRepository.UpdateCollaborationLeadHistory(leadList);
                                        }
                                        else
                                        {
                                            var collaborationLeadID = this.collaborationLeadRepository.AddCollaborationLead(leadList);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var collaborationLead in collaborationLeadlist)
                                    {
                                        collaborationLead.IsRemoved = true;
                                        var collaborationLeadData = this.collaborationLeadRepository.UpdateCollaborationLeadHistory(collaborationLead);
                                    }
                                }
                                //if (valid == true)
                                //{
                                resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                                resultDTO.Id = collaborationDetailsDTO.CollaborationIndex;
                                resultDTO.CollaborationId = collaboration.CollaborationID;
                                //}
                                //else
                                //{
                                //    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                //    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.ValidationFailure);
                                //}

                            }
                        }
                        else
                        {
                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                        }
                    }
                }
                return resultDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Exception occurred Update Collaboration Details. {ex.Message}");
                throw;
            }
        }

        public void UpdateCollaborationAndPerson_Questionaires(CollaborationQuestionnaireDTO collaborationQuestionnaire)
        {
            collaborationQuestionnaire.IsRemoved = true;
            var UpdatedCollaborationQuestionnaire = this.collaborationQuestionnaireRepository.UpdateCollaborationQuestionnaire(collaborationQuestionnaire);
            var personQuestionnaire = this.personQuestionnaireRepository.GetPersonQuestionnaireByCollaborationAndQuestionnaire(collaborationQuestionnaire.CollaborationID, collaborationQuestionnaire.QuestionnaireID).Result;
            if (personQuestionnaire != null)
            {
                PersonQuestionnaire personQuestionnaireToUpdate = new PersonQuestionnaire();

                foreach (var item in personQuestionnaire)
                {
                    PersonQuestionnaireDTO PersonQuestionnaireDTO = this.personQuestionnaireRepository.GetPersonQuestionnaireWithNoAssessment(item.QuestionnaireID, item.PersonID);
                    if (PersonQuestionnaireDTO.PersonQuestionnaireID != 0)
                    {
                        item.IsRemoved = true;
                        item.UpdateDate = DateTime.UtcNow;
                        this.mapper.Map(item, personQuestionnaireToUpdate);
                        this.personQuestionnaireRepository.UpdatePersonQuestionnaire(personQuestionnaireToUpdate);
                    }
                }
                var personQuestionnaireIDs = personQuestionnaire.Select(x => x.PersonQuestionnaireID).ToList();
                if (personQuestionnaireIDs.Count > 0)
                {
                    var personQuestionnaireScheduleToRemove = personQuestionnaireScheduleRepository.GetAsync(x => personQuestionnaireIDs.Contains(x.PersonQuestionnaireID)).Result.ToList();
                    if (personQuestionnaireScheduleToRemove.Count > 0)
                    {
                        personQuestionnaireScheduleToRemove.ForEach(x => x.IsRemoved = true);
                        personQuestionnaireScheduleService.UpdateBulkPersonQuestionnaireSchedule(personQuestionnaireScheduleToRemove);
                    }
                }
            }
        }

        public async Task<bool> StoreInQueue(long personQuestionnaireID)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(personQuestionnaireID.ToString());
            return await this._queue.Push(PCISEnum.AzureQueues.Assessmentremindernotification, Convert.ToBase64String(plainTextBytes));
        }

        public PersonQuestionnaireDTO CreatePersonQuestionnaireDTO(CollaborationQuestionnaireDTO collaborationQuestionnaireData, PersonCollaborationDTO personCollaboration)
        {
            PersonQuestionnaireDTO PersonQuestionnaireDTO = new PersonQuestionnaireDTO();

            PersonQuestionnaireDTO.CollaborationID = collaborationQuestionnaireData.CollaborationID;
            PersonQuestionnaireDTO.QuestionnaireID = collaborationQuestionnaireData.QuestionnaireID;
            PersonQuestionnaireDTO.PersonID = personCollaboration.PersonID;
            PersonQuestionnaireDTO.UpdateUserID = personCollaboration.UpdateUserID;
            PersonQuestionnaireDTO.IsActive = true;
            PersonQuestionnaireDTO.StartDate = DateTime.UtcNow;
            PersonQuestionnaireDTO.UpdateDate = DateTime.UtcNow;
            PersonQuestionnaireDTO.EndDueDate = null;
            PersonQuestionnaireDTO.CollaborationID = personCollaboration.CollaborationID;
            return PersonQuestionnaireDTO;
        }

        /// <summary>
        /// GetCollaborationList
        /// </summary>
        /// <param name="collaborationSearchDTO">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>Collaboration List.</returns>
        public GetCollaborationListDTO GetCollaborationList(CollaborationSearchDTO collaborationSearchDTO, long agencyID)
        {
            try
            {
                List<QueryFieldMappingDTO> fieldMappingList = GetCollaborationListConfiguration();
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQuery(collaborationSearchDTO.SearchFilter, fieldMappingList);
                GetCollaborationListDTO getCollaborationDTO = new GetCollaborationListDTO();
                if (collaborationSearchDTO.pageNumber <= 0)
                {
                    getCollaborationDTO.CollaborationList = null;
                    getCollaborationDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getCollaborationDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getCollaborationDTO;
                }
                else if (collaborationSearchDTO.pageSize <= 0)
                {
                    getCollaborationDTO.CollaborationList = null;
                    getCollaborationDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    getCollaborationDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getCollaborationDTO;
                }
                else if (agencyID > 0)
                {
                    var response = this.collaborationRepository.GetCollaborationListForOrgAdmin(collaborationSearchDTO, queryBuilderDTO, agencyID);
                    getCollaborationDTO.CollaborationList = response.Item1;
                    getCollaborationDTO.TotalCount = response.Item2;
                    getCollaborationDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    getCollaborationDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                }
                else
                {
                    getCollaborationDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                    getCollaborationDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                }
                return getCollaborationDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationDetails.
        /// </summary>
        /// <param name="collaborationIndex">collaborationIndex.</param>
        /// <returns>CollaborationDetailsResponseDTO.</returns>
        public CollaborationDetailsResponseDTO GetCollaborationDetails(Guid collaborationIndex, long agencyID)
        {
            try
            {
                CollaborationDetailsResponseDTO collaborationDetails = new CollaborationDetailsResponseDTO();
                if (collaborationIndex != Guid.Empty)
                {
                    collaborationDetails.collaborationInfo = new CollaborationInfoDTO();
                    var response = this.collaborationRepository.GetCollaborationDetails(collaborationIndex,agencyID);
                    collaborationDetails.collaborationInfo = response;
                    if (response.CollaborationID != 0)
                    {
                        var questionnaireList = this.collaborationRepository.GetCollaborationQuestionnaireList(response.CollaborationID).Where(x => x.QuestionnaireID > 0).ToList();
                        if (questionnaireList != null && questionnaireList.Count > 0)
                        {
                            collaborationDetails.collaborationInfo.Questionnaire = questionnaireList;
                        }
                        else
                        {
                            collaborationDetails.collaborationInfo.Questionnaire = null;
                        }

                        var collaborationLeads = this.collaborationRepository.GetCollaborationLeads(response.CollaborationID).Where(x => x.CollaborationLeadUserID > 0).ToList();
                        if (collaborationLeads != null && collaborationLeads.Count > 0)
                        {
                            collaborationDetails.collaborationInfo.Lead = collaborationLeads;
                        }
                        else
                        {
                            collaborationDetails.collaborationInfo.Lead = null;
                        }

                        var collaborationCategories = this.collaborationRepository.GetCollaborationCategories(response.CollaborationID).Where(x => x.CategoryID > 0).ToList();
                        if (collaborationCategories != null && collaborationCategories.Count > 0)
                        {
                            collaborationDetails.collaborationInfo.Category = collaborationCategories;
                        }
                        else
                        {
                            collaborationDetails.collaborationInfo.Category = null;
                        }
                    }
                    collaborationDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    collaborationDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;

                    return collaborationDetails;
                }
                else
                {
                    collaborationDetails.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PeopleIndex));
                    collaborationDetails.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return collaborationDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<QueryFieldMappingDTO> GetCollaborationListConfiguration()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "name",
                fieldTable = "C",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "agency",
                fieldTable = "A",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "level",
                fieldTable = "CL",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "StartDate",
                fieldAlias = "startDate",
                fieldTable = "C",
                fieldType = "date"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(C.EndDate,'31-Dec-9999')",
                fieldAlias = "endDate",
                fieldTable = "",
                fieldType = "date"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Code",
                fieldAlias = "code",
                fieldTable = "C",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "type",
                fieldTable = "T",
                fieldType = "string"
            });
            return fieldMappingList;
        }

        public CollaborationDetailsResponseDTO GetCollaborationDetailsByName(string nameCSV, long agencyID)
        {
            try
            {
                CollaborationDetailsResponseDTO response = new CollaborationDetailsResponseDTO();

                dynamic helperEmailObj = JsonConvert.DeserializeObject(nameCSV);
                string CSV = "";
                foreach (var data in helperEmailObj)
                {
                    foreach (var dataItem in data)
                    {
                        if (CSV == "")
                            CSV = "'" + dataItem.Value.ToString() + "'";
                        else
                            CSV = CSV + ",'" + dataItem.Value.ToString() + "'";
                    }
                }
                nameCSV = CSV;
                response.collaborationList = this.collaborationRepository.GetCollaborationDetailsByName(nameCSV, agencyID);
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public CollaborationResponseDTOForExternal GetCollaborationDetailsListForExternal(CollaborationSearchInputDTO collaborationSearchInputDTO, LoggedInUserDTO loggedInUserDTO)
        {
            try
            {
                CollaborationResponseDTOForExternal response = new CollaborationResponseDTOForExternal();
                response.collaborationDetailsListDTO = null;
                response.TotalCount = 0;
                if (loggedInUserDTO.AgencyId != 0 )
                {
                    List<QueryFieldMappingDTO> fieldMappingList = GetCollaborationListConfigurationForExternal();
                    var queryBuilderDTO = this.queryBuilder.BuildQueryForExternalAPI(collaborationSearchInputDTO, fieldMappingList);

                    if (queryBuilderDTO.Page <= 0)
                    {
                        response.collaborationDetailsListDTO = null;
                        response.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                        response.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        return response;
                    }
                    else if (queryBuilderDTO.PageSize <= 0)
                    {
                        response.collaborationDetailsListDTO = null;
                        response.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                        response.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        return response;
                    }
                    else
                    {
                        response.collaborationDetailsListDTO = this.collaborationRepository.GetPCollaborationDetailsListForExternal(loggedInUserDTO, queryBuilderDTO, collaborationSearchInputDTO);
                        if (response.collaborationDetailsListDTO.Count > 0)
                        {
                            response.TotalCount = response.collaborationDetailsListDTO[0].TotalCount;
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
        /// GetCollaborationListConfigurationForExternal.
        /// Always the First item to the list should be the column deatils for order by (With fieldTable as OrderBy for just user identification).
        /// And the next item should be the fieldMapping for order by Column specified above.
        /// </summary>
        /// <returns></returns>
        private List<QueryFieldMappingDTO> GetCollaborationListConfigurationForExternal()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {   
                fieldName = "Name",
                fieldAlias = "Name",
                fieldTable = "OrderBy",
                fieldType = "asc"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "Name",
                fieldTable = "C",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "CollaborationIndex",
                fieldAlias = "CollaborationIndex",
                fieldTable = "C",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "PersonIndex",
                fieldAlias = "PersonIndex",
                fieldTable = "C",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "CollaborationId",
                fieldAlias = "CollaborationId",
                fieldTable = "C",
                fieldType = "int"
            });
            return fieldMappingList;
        }

        /// <summary>
        /// Function to add collabration details for external agency
        /// </summary>
        /// <param name="collaborationDetailsDTOForExternal"></param>
        /// <returns></returns>
        public AddCollaborationResponseDTOForExternal AddCollabrationForExternal(CollabrationAddDTOForExternal collaborationDetailsDTOForExternal,long agencyID,int updateUserID)
        {
            try
            {
                var collaborationDetailsDTO = CreateCollabrationObjectForAddExternal(collaborationDetailsDTOForExternal, agencyID, updateUserID);
                AddCollaborationResponseDTOForExternal resultDTO = new AddCollaborationResponseDTOForExternal();
                var ValidationResult = ValidateExternalCollaborationData(collaborationDetailsDTO);
                if (!string.IsNullOrEmpty(ValidationResult))
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    resultDTO.ResponseStatus = ValidationResult;
                    return resultDTO;
                }
                var response = AddCollaborationDetails(collaborationDetailsDTO);
                resultDTO.ResponseStatus = response.ResponseStatus;
                resultDTO.ResponseStatusCode = response.ResponseStatusCode;
                resultDTO.Index = response.Id;
                resultDTO.Id = response.CollaborationId;
                return resultDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Creating collabration object for external data adding
        /// </summary>
        /// <param name="collaborationDetailsDTOForExternal"></param>
        /// <returns></returns>
        public CollaborationDetailsDTO CreateCollabrationObjectForAddExternal(CollabrationAddDTOForExternal collaborationDetailsDTOForExternal,long agencyID,int updateUserID)
        {
            CollaborationDetailsDTO collaborationDetailsDTO = new CollaborationDetailsDTO();
            this.mapper.Map<CollabrationAddDTOForExternal, CollaborationDetailsDTO>(collaborationDetailsDTOForExternal,collaborationDetailsDTO );
            this.mapper.Map<List<CollaborationQuestionnaireDTOForExternal>, List<CollaborationQuestionnaireDTO>>(collaborationDetailsDTOForExternal.Questionnaire, collaborationDetailsDTO.Questionnaire);
            this.mapper.Map<List<CollaborationTagDTOForExternal>, List<CollaborationTagDTO>>(collaborationDetailsDTOForExternal.Category, collaborationDetailsDTO.Category);
            this.mapper.Map<List<CollaborationLeadHistoryDTOForExternal>, List<CollaborationLeadHistoryDTO>>(collaborationDetailsDTOForExternal.Lead, collaborationDetailsDTO.Lead);

            collaborationDetailsDTO.AgencyID = agencyID;
            collaborationDetailsDTO.UpdateUserID = updateUserID;


            return collaborationDetailsDTO;
        }

        /// <summary>
        /// Function to update collabration module for external
        /// </summary>
        /// <param name="collaborationDetailsDTOForExternal"></param>
        /// <returns></returns>
        public CRUDCollaborationResponseDTOForExternal UpdateCollabrationForExternal(CollabrationUpdateDTOForExternal collaborationDetailsDTOForExternal,long updateUserID,long agencyID)
        {
            try
            {
                var collaborationDetailsDTO = EditCollabrationObjectForExternal(collaborationDetailsDTOForExternal, agencyID, updateUserID);
                CRUDCollaborationResponseDTOForExternal resultDTO = new CRUDCollaborationResponseDTOForExternal();
                var ValidationResult = ValidateExternalCollaborationData(collaborationDetailsDTO);
                if (!string.IsNullOrEmpty(ValidationResult))
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    resultDTO.ResponseStatus = ValidationResult;
                    return resultDTO;
                }
                var response = UpdateCollaborationDetails(collaborationDetailsDTO, updateUserID, PCISEnum.CallingType.ExternalAPI);
                resultDTO.ResponseStatus = response.ResponseStatus;
                resultDTO.ResponseStatusCode = response.ResponseStatusCode;
                resultDTO.Index = response.Id;
                resultDTO.Id = response.CollaborationId;
                return resultDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// creating collabration dto for editing
        /// </summary>
        /// <param name="collaborationDetailsDTOForExternal"></param>
        /// <returns></returns>
        public CollaborationDetailsDTO EditCollabrationObjectForExternal(CollabrationUpdateDTOForExternal collaborationDetailsDTOForExternal,long agencyID,long updateUserID)
        {
            CollaborationDetailsDTO collaborationDetailsDTO = new CollaborationDetailsDTO();
            this.mapper.Map<CollabrationUpdateDTOForExternal, CollaborationDetailsDTO>(collaborationDetailsDTOForExternal, collaborationDetailsDTO);
            this.mapper.Map<List<CollaborationQuestionnaireDTOForEditExternal>, List<CollaborationQuestionnaireDTO>>(collaborationDetailsDTOForExternal.Questionnaire, collaborationDetailsDTO.Questionnaire);
            this.mapper.Map<List<CollaborationTagDTOForEditExternal>, List<CollaborationTagDTO>>(collaborationDetailsDTOForExternal.Category, collaborationDetailsDTO.Category);
            this.mapper.Map<List<CollaborationLeadHistoryDTOForEditExternal>, List<CollaborationLeadHistoryDTO>>(collaborationDetailsDTOForExternal.Lead, collaborationDetailsDTO.Lead);

            collaborationDetailsDTO.AgencyID = agencyID;
            collaborationDetailsDTO.UpdateUserID = Convert.ToInt32(updateUserID);
            return collaborationDetailsDTO;
        }

        /// <summary>
        /// Function to delete collabratiom details for external
        /// </summary>
        /// <param name="collabrationIndex"></param>
        /// <param name="updateUserID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        public CRUDCollaborationResponseDTOForExternal DeleteCollaborationDetail(Guid collabrationIndex, int updateUserID, long loggedInAgencyID)
        {
            try
            {
                CRUDCollaborationResponseDTOForExternal resultDTO = new CRUDCollaborationResponseDTOForExternal();

                if (collabrationIndex != Guid.Empty)
                {
                    var Collabration = this.collaborationRepository.GetAsync(collabrationIndex).Result;
                    if (Collabration != null)
                    {
                        var personCollaborationList = this.personCollaborationRepository.GetPersonCollaborationByCollaborationID(Collabration.CollaborationID).Result;
                        if(personCollaborationList != null && personCollaborationList.Count > 0)
                        {
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeleteRecordInUse);
                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            return resultDTO;
                        }

                        if (Collabration.AgencyID != loggedInAgencyID)
                        {
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                            return resultDTO;
                        }
                        Collabration.IsRemoved = true;
                        Collabration.UpdateDate = DateTime.UtcNow;
                        Collabration.UpdateUserID = updateUserID;

                        var collabrationResult = this.collaborationRepository.UpdateCollaboration(Collabration);
                        if (collabrationResult != null)
                        {
                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                            return resultDTO;
                        }
                        else
                        {
                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                            return resultDTO;
                        }

                    }
                }
                return resultDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Exception occurred getting case note types. {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Validate collaborationdata from external api.n
        /// </summary>
        /// <param name="collaborationDetailsDTO"></param>
        /// <returns></returns>
        private string ValidateExternalCollaborationData(CollaborationDetailsDTO collaborationDetailsDTO)
        {
            string ResponseStatus = string.Empty;
            int i = 0;
            #region AgencyID
            var agencylist = this._agencyRepository.GetAgencyLookupWithID(collaborationDetailsDTO.AgencyID);
            if (collaborationDetailsDTO.AgencyID >= 0)
            {
                int index = agencylist.FindIndex(x => x.AgencyID == collaborationDetailsDTO.AgencyID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.AgencyID));
                    return ResponseStatus;
                }
            }
            #endregion  AgencyID

            #region TherapyTypeID
            var therapyTypeList = this.lookupRepository.GetAgencyTherapyTypes(collaborationDetailsDTO.AgencyID);
            if (collaborationDetailsDTO.TherapyTypeID >= 0)
            {
                int index = therapyTypeList.FindIndex(x => x.TherapyTypeID == collaborationDetailsDTO.TherapyTypeID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.TherapyTypeID));
                    return ResponseStatus;
                }
            }
            #endregion TherapyTypeID

            #region CollaborationLevelID
            var levelList = this.lookupRepository.GetCollaborationLevels(collaborationDetailsDTO.AgencyID);
            if (collaborationDetailsDTO.CollaborationLevelID >= 0)
            {
                int index = levelList.FindIndex(x => x.CollaborationLevelID == collaborationDetailsDTO.CollaborationLevelID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.CollaborationLevelID));
                    return ResponseStatus;
                }
            }
            #endregion CollaborationLevelID

            #region Category
            var categoryList = this.lookupRepository.GetAgencyTagTypes(collaborationDetailsDTO.AgencyID);
            if (collaborationDetailsDTO?.Category != null)
            {
                i = -1;
                foreach (var item in collaborationDetailsDTO.Category)
                {
                    ++i;
                    int index = categoryList.FindIndex(x => x.CollaborationTagTypeID == item.CollaborationTagTypeID);
                    if (index == -1)
                    {
                        ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequireValidationInArrays, "Category", i, "CollaborationTagTypeID"));
                        return ResponseStatus;
                    }
                }
            }
            #endregion  Category

            #region Leads
            var leadList = this.lookupRepository.GetAllAgencyLeads(collaborationDetailsDTO.AgencyID, false);
            if (collaborationDetailsDTO?.Lead != null)
            {
                i = -1;
                foreach (var item in collaborationDetailsDTO.Lead)
                {
                    ++i;
                    int index = leadList.FindIndex(x => x.HelperID == item.LeadUserID);
                    if (index == -1)
                    {
                        ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequireValidationInArrays, "Lead", i, "LeadUserID"));
                        return ResponseStatus;
                    }
                }
            }
            #endregion  Leads

            #region Questionnaire
            var questionnaireList = this.lookupRepository.GetAllAgencyQuestionnaire(collaborationDetailsDTO.AgencyID);
            if (collaborationDetailsDTO?.Questionnaire != null)
            {
                i = -1;
                foreach (var item in collaborationDetailsDTO.Questionnaire)
                {
                    ++i;
                    int index = questionnaireList.FindIndex(x => x.QuestionnaireID == item.QuestionnaireID);
                    if (index == -1)
                    {
                        ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequireValidationInArrays, "Questionnaire", i, "QuestionnaireID"));
                        return ResponseStatus;
                    }
                }
            }
            #endregion  Questionnaire
            return ResponseStatus;
        }
        /// <summary>
        /// ValidateExternalAPICollaborationEditForPrimaryKeyIDs -To validate whether the PH IDs for edit matches those in DB
        /// </summary>
        /// <param name="collaborationQuestionnaireData"></param>
        /// <param name="collaborationCategorylist"></param>
        /// <param name="collaborationLeadlist"></param>
        /// <param name="collaborationDetailsDTO"></param>
        /// <param name="resultDTO"></param>
        /// <param name="callingType"></param>
        /// <returns></returns>
        private bool ValidateExternalAPICollaborationEditForPrimaryKeyIDs(List<CollaborationQuestionnaireDTO> collaborationQuestionnaireData, List<CollaborationTagDTO> collaborationCategorylist, List<CollaborationLeadHistoryDTO> collaborationLeadlist, CollaborationDetailsDTO collaborationDetailsDTO, CollaborationResponseDTO resultDTO, string callingType)
        {
            try
            {
                if (callingType == PCISEnum.CallingType.ExternalAPI)
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    var collaborationQuestionnaireIDsInDB = collaborationQuestionnaireData?.Select(x => x.CollaborationQuestionnaireID.ToString()).ToList();
                    var collaborationQuestionnaireIDsToEdit = collaborationDetailsDTO.Questionnaire?.Select(x => x.CollaborationQuestionnaireID.ToString()).ToList();
                    if (collaborationQuestionnaireIDsToEdit.Count > 0)
                    {
                        collaborationQuestionnaireIDsToEdit = collaborationQuestionnaireIDsToEdit.Where(x => x != "0").ToList();
                        var invalidIDs = collaborationQuestionnaireIDsToEdit.Where(x => !collaborationQuestionnaireIDsInDB.Contains(x)).ToList();
                        if (invalidIDs.Count > 0)
                        {
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(String.Format(PCISEnum.StatusMessages.InvalidPrimaryKeysInEdit, "CollaborationQuestionnaireID", string.Join(",", invalidIDs)));
                            return false;
                        }
                    }
                    var collaborationCategoryIDsInDB = collaborationCategorylist?.Select(x => x.CollaborationTagID.ToString()).ToList();
                    var collaborationCategoryIDsToEdit = collaborationDetailsDTO.Category?.Select(x => x.CollaborationTagID.ToString()).ToList();
                    if (collaborationCategoryIDsToEdit.Count > 0)
                    {
                        collaborationCategoryIDsToEdit = collaborationCategoryIDsToEdit.Where(x => x != "0").ToList();
                        var invalidIDs = collaborationCategoryIDsToEdit.Where(x => !collaborationCategoryIDsInDB.Contains(x)).ToList();
                        if (invalidIDs.Count > 0)
                        {
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(String.Format(PCISEnum.StatusMessages.InvalidPrimaryKeysInEdit, "CollaborationTagID", string.Join(",", invalidIDs)));
                            return false;
                        }
                    }
                    var collaborationLeadIDsInDB = collaborationLeadlist?.Select(x => x.CollaborationLeadHistoryID.ToString()).ToList();
                    var collaborationLeadIDsToEdit = collaborationDetailsDTO.Lead?.Select(x => x.CollaborationLeadHistoryID.ToString()).ToList();
                    if (collaborationLeadIDsToEdit.Count > 0)
                    {
                        collaborationLeadIDsToEdit = collaborationLeadIDsToEdit.Where(x => x != "0").ToList();
                        var invalidIDs = collaborationLeadIDsToEdit.Where(x => !collaborationLeadIDsInDB.Contains(x)).ToList();
                        if (invalidIDs.Count > 0)
                        {
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(String.Format(PCISEnum.StatusMessages.InvalidPrimaryKeysInEdit, "CollaborationLeadHistoryID", string.Join(",", invalidIDs)));
                            return false;
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


    }
}

