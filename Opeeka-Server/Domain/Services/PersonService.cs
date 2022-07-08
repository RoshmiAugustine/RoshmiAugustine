// -----------------------------------------------------------------------
// <copyright file="PersonService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Repositories;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Domain.Resources;
    using Opeeka.PICS.Infrastructure.Enums;
    using System;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using Opeeka.PICS.Domain.Interfaces.Common;
    using Opeeka.PICS.Domain.Entities;
    using System.Threading.Tasks;
    using Opeeka.PICS.Domain.Interfaces;
    using Opeeka.PICS.Domain.DTO.Input;
    using Opeeka.PICS.Domain.DTO.ExternalAPI;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the <see cref="PersonService" />.
    /// </summary>
    public class PersonService : BaseService, IPersonService
    {
        /// <summary>
        /// Defines the personRepository.
        /// </summary>
        private readonly IPersonRepository personRepository;

        /// <summary>
        /// Defines the logger.
        /// </summary>
        private readonly ILogger<PersonService> logger;

        /// <summary>
        /// Defines the addressRepository.
        /// </summary>
        private readonly IAddressrepository addressRepository;

        /// <summary>
        /// Defines the personAddressRepository.
        /// </summary>
        private readonly IPersonAddressRepository personAddressRepository;

        /// <summary>
        /// Defines the personSupportRepository.
        /// </summary>
        private readonly IPersonSupportRepository personSupportRepository;

        /// <summary>
        /// Defines the personRaceEthnicityRepository.
        /// </summary>
        private readonly IPersonRaceEthnicityRepository personRaceEthnicityRepository;

        /// <summary>
        /// Defines the personLanguageRepository.
        /// </summary>
        private readonly IPersonLanguageRepository personLanguageRepository;

        /// <summary>
        /// Defines the personIdentificationRepository.
        /// </summary>
        private readonly IPersonIdentificationRepository personIdentificationRepository;

        /// <summary>
        /// Defines the personCollaborationRepository.
        /// </summary>
        private readonly IPersonCollaborationRepository personCollaborationRepository;

        /// <summary>
        /// Defines the personHelperRepository.
        /// </summary>
        private readonly IPersonHelperRepository personHelperRepository;

        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;

        /// Initializes a new instance of the Utility class.
        private readonly IUtility utility;
        /// <summary>
        /// Defines the questionnaireRepository.
        /// </summary>
        private readonly IQuestionnaireRepository questionnaireRepository;

        /// <summary>
        /// Defines the personQuestionnaireRepository.
        /// </summary>
        private readonly IPersonQuestionnaireRepository personQuestionnaireRepository;

        /// <summary>
        /// Defines the NotifiationResolutionStatusRepository.
        /// </summary>
        private readonly INotifiationResolutionStatusRepository notifiationResolutionStatusRepository;

        /// <summary>
        /// Defines the NotificationLogRepository.
        /// </summary>
        private readonly INotificationLogRepository notificationLogRepository;

        private readonly IMapper mapper;

        /// <summary>
        /// Defines the NotifiationResolutionHistoryRepository.
        /// </summary>
        private readonly INotifiationResolutionHistoryRepository notifiationResolutionHistoryRepository;

        /// <summary>
        /// Defines the NoteRepository.
        /// </summary>
        private readonly INoteRepository noteRepository;

        /// <summary>
        /// Defines the NotifiationResolutionNoteRepository.
        /// </summary>
        private readonly INotifiationResolutionNoteRepository notifiationResolutionNoteRepository;

        /// Defines the collaborationQuestionnaireRepository.
        /// </summary>
        private readonly ICollaborationQuestionnaireRepository collaborationQuestionnaireRepository;
        /// <summary>
        /// Defines the PersonQuestionnaireRepository.
        /// </summary>
        private readonly IPersonQuestionnaireRepository PersonQuestionnaireRepository;
        /// <summary>
        /// Defines the queryBuilder.
        /// </summary>
        private readonly IQueryBuilder queryBuilder;
        public IQueue _queue { get; }
        private readonly IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository;
        private readonly IPersonQuestionnaireScheduleService personQuestionnaireScheduleService;
        private readonly IQuestionnaireNotifyRiskRuleConditionRepository questionnaireNotifyRiskRuleConditionRepository;
        private readonly IAuditPersonProfileRepository auditPersonProfileRepository;
        private readonly ISMSSender smsSender;
        private readonly ILookupRepository lookupRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly ISexualityRepository _sexualityrepository;
        private readonly IRaceEthnicityRepository _raceEthnicityRepository;
        private readonly IIdentificationTypeRepository _identificationrepository;
        private readonly IIdentifiedGenderRepository identifiedGenderRepository;
        private readonly ISupportTypeRepository _supportTypeRepository;
        private readonly INotifyReminderRepository notifyReminderRepository;
        private readonly INotificationTypeRepository notificationTypeRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        /// <param name="personRepository">The personRepository<see cref="IPersonRepository"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{AgencyService}"/>.</param>
        /// <param name="addressRepository">The addressRepository<see cref="IAddressrepository"/>.</param>
        /// <param name="personAddressRepository">The personAddressRepository<see cref="IPersonAddressRepository"/>.</param>
        /// <param name="personIdentificationRepository">The personIdentificationRepository<see cref="IPersonIdentificationRepository"/>.</param>
        /// <param name="personCollaborationRepository">The personCollaborationRepository<see cref="IPersonCollaborationRepository"/>.</param>
        /// <param name="personLanguageRepository">The personLanguageRepository<see cref="IPersonLanguageRepository"/>.</param>
        /// <param name="personSupportRepository">The personSupportRepository<see cref="IPersonSupportRepository"/>.</param>
        /// <param name="personRaceEthnicityRepository">The personRaceEthnicityRepository<see cref="IPersonRaceEthnicityRepository"/>.</param>
        /// <param name="personHelperRepository">The personHelperRepository<see cref="IPersonHelperRepository"/>.</param>
        /// <param name="notifiationResolutionStatusRepository">The notifiationResolutionStatusRepository<see cref="INotifiationResolutionStatusRepository"/>.</param>
        /// <param name="notificationLogRepository">The notificationLogRepository<see cref="INotificationLogRepository"/>.</param>
        /// <param name="mapper">The mapperrepository<see cref="IMapper"/>.</param>
        /// <param name="notifiationResolutionHistoryRepository">The notifiationResolutionHistoryRepository<see cref="INotifiationResolutionHistoryRepository"/>.</param>
        /// <param name="noteRepository">The noteRepository<see cref="INoteRepository"/>.</param>
        /// <param name="notifiationResolutionNoteRepository">The notifiationResolutionNoteRepository<see cref="INotifiationResolutionNoteRepository"/>.</param>

        public PersonService(ISMSSender smsSender, ICollaborationQuestionnaireRepository collaborationQuestionnaireRepository, IPersonQuestionnaireRepository PersonQuestionnaireRepository, IPersonRepository personRepository, ILogger<PersonService> logger, IAddressrepository addressRepository, IPersonAddressRepository personAddressRepository,
             IPersonIdentificationRepository personIdentificationRepository, IPersonCollaborationRepository personCollaborationRepository,
             IPersonLanguageRepository personLanguageRepository, IPersonSupportRepository personSupportRepository,
             IPersonRaceEthnicityRepository personRaceEthnicityRepository, IPersonHelperRepository personHelperRepository,
             IQuestionnaireRepository questionnaireRepository, IPersonQuestionnaireRepository personQuestionnaireRepository,
             INotifiationResolutionStatusRepository notifiationResolutionStatusRepository, INotificationLogRepository notificationLogRepository,
             IMapper mapper, LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext,
             INotifiationResolutionHistoryRepository notifiationResolutionHistoryRepository, INoteRepository noteRepository,
             INotifiationResolutionNoteRepository notifiationResolutionNoteRepository, IUtility utility, IQueryBuilder querybuild,
             IQueue _queue, IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository, IPersonQuestionnaireScheduleService personQuestionnaireScheduleService,
              IQuestionnaireNotifyRiskRuleConditionRepository questionnaireNotifyRiskRuleConditionRepository, IAuditPersonProfileRepository auditPersonProfileRepository
            , ILookupRepository lookupRepository, ILanguageRepository languageRepository, IGenderRepository _genderRepository,
               ISexualityRepository sexualityrepository, IIdentificationTypeRepository _identificationrepository,
            IRaceEthnicityRepository raceEthnicityRepository, IIdentifiedGenderRepository identifiedGenderRepository, ISupportTypeRepository _supportTypeRepository, INotifyReminderRepository notifyReminderRepository, INotificationTypeRepository notificationTypeRepository)
        : base(configRepo, httpContext)
        {
            this.personRepository = personRepository;
            this.logger = logger;
            this.addressRepository = addressRepository;
            this.personAddressRepository = personAddressRepository;
            this.personRaceEthnicityRepository = personRaceEthnicityRepository;
            this.personIdentificationRepository = personIdentificationRepository;
            this.personSupportRepository = personSupportRepository;
            this.personCollaborationRepository = personCollaborationRepository;
            this.personLanguageRepository = personLanguageRepository;
            this.personHelperRepository = personHelperRepository;
            this.questionnaireRepository = questionnaireRepository;
            this.personQuestionnaireRepository = personQuestionnaireRepository;
            this.notifiationResolutionStatusRepository = notifiationResolutionStatusRepository;
            this.notificationLogRepository = notificationLogRepository;
            this.mapper = mapper;
            this.collaborationQuestionnaireRepository = collaborationQuestionnaireRepository;
            this.localize = localizeService;
            this.utility = utility;
            this.notifiationResolutionHistoryRepository = notifiationResolutionHistoryRepository;
            this.noteRepository = noteRepository;
            this.notifiationResolutionNoteRepository = notifiationResolutionNoteRepository;
            this.PersonQuestionnaireRepository = PersonQuestionnaireRepository;
            this.queryBuilder = querybuild;
            this._queue = _queue;
            this.personQuestionnaireScheduleRepository = personQuestionnaireScheduleRepository;
            this.personQuestionnaireScheduleService = personQuestionnaireScheduleService;
            this.questionnaireNotifyRiskRuleConditionRepository = questionnaireNotifyRiskRuleConditionRepository;
            this.auditPersonProfileRepository = auditPersonProfileRepository;
            this.smsSender = smsSender;
            this.lookupRepository = lookupRepository;
            this.languageRepository = languageRepository;
            this._genderRepository = _genderRepository;
            this._sexualityrepository = sexualityrepository;
            this._raceEthnicityRepository = raceEthnicityRepository;
            this._identificationrepository = _identificationrepository;
            this.identifiedGenderRepository = identifiedGenderRepository;
            this._supportTypeRepository = _supportTypeRepository;
            this.notifyReminderRepository = notifyReminderRepository;
            this.notificationTypeRepository = notificationTypeRepository;
        }

        /// <summary>
        /// GetPersonList.
        /// </summary>
        /// <param name="userID">The userID<see cref="long"/>.</param>
        /// <param name="pageNumber">The pageNumber<see cref="int"/>.</param>
        /// <param name="pageSize">The pageSize<see cref="int"/>.</param>
        /// <returns>.</returns>
        public GetPersonListDTO GetPersonList(long userID, int pageNumber, int pageSize)
        {
            try
            {
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                GetPersonListDTO getPersonDTO = new GetPersonListDTO();
                List<PersonDTO> response = new List<PersonDTO>();
                int totalCount = 0;
                if (pageNumber <= 0)
                {
                    getPersonDTO.PersonList = null;
                    getPersonDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getPersonDTO;
                }
                else if (pageSize <= 0)
                {
                    getPersonDTO.PersonList = null;
                    getPersonDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    getPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getPersonDTO;
                }
                else if (userID <= 0)
                {
                    getPersonDTO.PersonList = null;
                    getPersonDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.UserId));
                    getPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getPersonDTO;
                }
                else
                {
                    var personList = this.personRepository.GetPersons(pageNumber, pageSize);

                    foreach (var item in personList)
                    {
                        var collaborationList = this.personRepository.GetPeopleCollaborationList(item.PersonID, 0).ToList();
                        if (collaborationList != null && collaborationList.Count > 0)
                        {
                            var collaboration = collaborationList.Where(x => x.IsPrimary).FirstOrDefault();
                            if (collaboration != null)
                            {
                                item.CollaborationID = collaboration.CollaborationID;
                                item.Collaboration = collaboration.CollaborationName;
                                item.StartDate = utility.ConvertToUtcDateTime(collaboration.CollaborationStartDate.Date, offset);
                            }
                            else
                            {
                                collaboration = collaborationList.FirstOrDefault();
                                item.CollaborationID = collaboration.CollaborationID;
                                item.Collaboration = collaboration.CollaborationName;
                                item.StartDate = utility.ConvertToUtcDateTime(collaboration.CollaborationStartDate.Date, offset);
                            }
                        }

                        var helperList = this.personRepository.GetPeopleHelperList(item.PersonID);
                        if (helperList != null && helperList.Count > 0)
                        {
                            var helper = helperList.Where(x => x.IsLead).FirstOrDefault();
                            if (helper != null)
                            {
                                item.Lead = helper.HelperName;
                            }
                            else
                            {
                                helper = helperList.FirstOrDefault();
                                item.Lead = helper.HelperName;
                            }
                        }

                    }

                    totalCount = this.personRepository.GetPersonsCount();

                    getPersonDTO.PersonList = personList;
                    getPersonDTO.TotalCount = totalCount;
                    getPersonDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    getPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return getPersonDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPeopleDetails.
        /// </summary>
        /// <param name="peopleIndex">.</param>
        /// <returns>PeopleDetailsResponseDTO.</returns>
        public PeopleDetailsResponseDTO GetPeopleDetails(Guid peopleIndex, long agencyID, int userID)
        {
            try
            {
                PeopleDetailsResponseDTO PeopleDetails = new PeopleDetailsResponseDTO();
                if (peopleIndex != Guid.Empty)
                {
                    var response = this.personRepository.GetPeopleDetails(peopleIndex);
                    PeopleDetails.PeopleData = response;
                    if (response.PersonID != 0)
                    {
                        if (!this.personRepository.IsValidPersonInAgency(response.PersonID, response.AgencyID, agencyID))
                        {
                            PeopleDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                            PeopleDetails.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                            PeopleDetails.PeopleData = null;
                            return PeopleDetails;
                        }
                        PeopleDetails.PeopleData.IsShared = response?.AgencyID != agencyID ? true : false;
                        var responseIdentifierList = this.personRepository.GetPeopleIdentifierList(response.PersonID);
                        PeopleDetails.PeopleData.peopleIdentifier = responseIdentifierList;
                        var responseRaceEthnicityList = this.personRepository.GetPeopleRaceEthnicityList(response.PersonID);
                        PeopleDetails.PeopleData.peopleRaceEthnicity = responseRaceEthnicityList;
                        var responseSupportList = this.personRepository.GetPeopleSupportList(response.PersonID);
                        PeopleDetails.PeopleData.peopleSupport = responseSupportList;
                        var responseHelperList = this.personRepository.GetPeopleHelperList(response.PersonID);
                        PeopleDetails.PeopleData.peopleHelper = responseHelperList;
                        var responseCollaborationList = this.personCollaborationRepository.GetPersonCollaboration(response.PersonID);
                        PeopleDetails.PeopleData.peopleCollaboration = responseCollaborationList;
                        PeopleDetails.PeopleData.PrimaryCollaborationHistory = new List<AuditPersonProfileDTO>();
                        PeopleDetails.PeopleData.LeadHelperHistory = new List<AuditPersonProfileDTO>();
                        PeopleDetails.PeopleData.IsHelperHasCollaboration = responseHelperList?.Where(x => x.UserID == userID && x.CollaborationID != 0).Count() > 0;
                    }
                    PeopleDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    PeopleDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;

                    return PeopleDetails;
                }
                else
                {
                    PeopleDetails.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.PeopleIndex);
                    PeopleDetails.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;

                    return PeopleDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddPeopleDetails.
        /// PCIS-3259 : 1) Added regions 
        ///             2) optimized personcollaboration and Personquestionnaire adding section
        /// </summary>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>PeopleDetailsResponseDTO.</returns>
        public AddPersonResponseDTO AddPeopleDetails(PeopleDetailsDTO peopleDetailsDTO)
        {
            try
            {
                AddPersonResponseDTO resultDTO = new AddPersonResponseDTO();
                var isPersonActive = CheckPersonCollaborationEndDate(peopleDetailsDTO.PersonCollaborations);
                peopleDetailsDTO.IsActive = isPersonActive;
                PeopleDTO peopleDTO = CreatePeople(peopleDetailsDTO);
                if (peopleDTO != null)
                {
                    peopleDTO = this.personRepository.AddPerson(peopleDTO);
                    peopleDetailsDTO.PersonID = peopleDTO.PersonID;
                    if (peopleDetailsDTO.PersonID != 0)
                    {
                        resultDTO.PersonIndex = peopleDTO.PersonIndex;
                        AddressDTO addressDTO = CreateAddressDTOOnAdd(peopleDetailsDTO);
                        AddToPersonAddress(addressDTO, peopleDetailsDTO.PersonID);

                        #region PersonIdentification
                        //Bulk insertion person identification
                        List<PersonIdentificationDTO> personIdentificationsDTOList = new List<PersonIdentificationDTO>();
                        foreach (PersonIdentificationDTO PersonIdentity in peopleDetailsDTO.PersonIdentifications)
                        {
                            PersonIdentificationDTO personIdentificationDTO = CreatePersonIdentification(PersonIdentity, peopleDetailsDTO);
                            if (personIdentificationDTO != null)
                                personIdentificationsDTOList.Add(personIdentificationDTO);

                        }
                        if (personIdentificationsDTOList.Count > 0)
                        {
                            this.personIdentificationRepository.AddBulkPersonIdentificationType(personIdentificationsDTOList);
                        }
                        #endregion

                        #region PersonCollaboration And PersonQuestionnaire
                        List<PersonCollaborationDTO> personCollaborationList = new List<PersonCollaborationDTO>();
                        List<PersonQuestionnaireDTO> PersonQuestionnaireList = new List<PersonQuestionnaireDTO>();
                        List<CollaborationQuestionnaireDTO> collaborationQuestionnairesList = new List<CollaborationQuestionnaireDTO>();
                        //Fetch all Questionnaires for all collaboration Ids
                        var collaborationIds = peopleDetailsDTO?.PersonCollaborations?.Select(x => x.CollaborationID).ToList();
                        if (collaborationIds.Count > 0)
                            collaborationQuestionnairesList = this.collaborationQuestionnaireRepository.GetCollaborationQuestionnaireData(collaborationIds);

                        foreach (PersonCollaborationDTO personCollaboration in peopleDetailsDTO.PersonCollaborations)
                        {
                            PersonCollaborationDTO personCollaborationDTO = CreatePersonCollaboration(personCollaboration, peopleDetailsDTO);
                            if (personCollaborationDTO != null)
                            {
                                personCollaborationList.Add(personCollaborationDTO);
                                var collaborationQuestionnaireData = collaborationQuestionnairesList?.Where(x => x.CollaborationID == personCollaboration.CollaborationID).ToList();

                                if (collaborationQuestionnaireData != null)
                                {
                                    foreach (CollaborationQuestionnaireDTO collaborationQuestionnaire in collaborationQuestionnaireData)
                                    {
                                        personCollaboration.PersonID = peopleDetailsDTO.PersonID;
                                        personCollaboration.UpdateUserID = peopleDetailsDTO.UpdateUserID;
                                        PersonQuestionnaireDTO personQuestionnairesDTO = CreatePersonQuestionnaireDTO(collaborationQuestionnaire, personCollaboration);
                                        PersonQuestionnaireList.Add(personQuestionnairesDTO);
                                    }
                                }
                            }
                        }
                        if (personCollaborationList.Count > 0)
                        {
                            this.personCollaborationRepository.AddBulkPersonCollaboration(personCollaborationList);
                        }
                        if (PersonQuestionnaireList.Count > 0)
                        {
                            this.PersonQuestionnaireRepository.AddBulkPersonQuestionnaire(PersonQuestionnaireList);
                            //Fetch PersonQuestionnireIds for all questionnaireIds whose Reminders On 
                            var personQuestionnaires = this.personQuestionnaireRepository.GetPersonQuestionnaireIdsWithReminderOn(peopleDTO.PersonID);
                            foreach (var item in personQuestionnaires)
                            {
                                StoreInQueue(item.PersonQuestionnaireID);
                            }
                        }
                        #endregion

                        #region PersonSupports
                        //Bulk insertion person support
                        List<PersonSupportDTO> personSupportList = new List<PersonSupportDTO>();
                        foreach (PersonSupportDTO personSupport in peopleDetailsDTO.PersonSupports)
                        {
                            if (personSupport.FirstName != null)
                            {
                                PersonSupportDTO personSupportDTO = CreatePersonSupport(personSupport, peopleDetailsDTO);
                                personSupportList.Add(personSupportDTO);
                            }
                        }

                        if (personSupportList.Count > 0)
                        {
                            this.personSupportRepository.AddBulkPersonSupport(personSupportList);
                        }
                        #endregion

                        #region PersonHelper
                        //Bulk insertion person helper
                        List<PersonHelperDTO> personhelperList = new List<PersonHelperDTO>();
                        foreach (PersonHelperDTO personhelper in peopleDetailsDTO.PersonHelpers)
                        {
                            PersonHelperDTO personHelperDTO = CreatePersonHelper(personhelper, peopleDetailsDTO);
                            personhelperList.Add(personHelperDTO);

                        }

                        if (personhelperList.Count > 0)
                        {
                            this.personHelperRepository.AddBulkPersonHelper(personhelperList);
                        }
                        #endregion

                        #region PersonRaceEthinicity
                        List<PersonRaceEthnicityDTO> personRaceEthnicityList = new List<PersonRaceEthnicityDTO>();
                        foreach (PersonRaceEthnicityDTO RaceEthnicity in peopleDetailsDTO.PersonRaceEthnicities)
                        {

                            PersonRaceEthnicityDTO personRaceEthnicityDTO = CreateRaceEthnicityDTO(RaceEthnicity, peopleDetailsDTO);
                            personRaceEthnicityList.Add(personRaceEthnicityDTO);
                        }
                        if (personRaceEthnicityList.Count > 0)
                        {
                            this.personRaceEthnicityRepository.AddBulkRaceEthnicity(personRaceEthnicityList);
                        }
                        #endregion

                        resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                        resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        resultDTO.Id = peopleDetailsDTO.PersonID;
                        return resultDTO;
                    }
                }
                if (resultDTO.ResponseStatusCode != PCISEnum.StatusCodes.InsertionSuccess)
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    resultDTO.ResponseStatus = PCISEnum.StatusMessages.insertionFailed;
                }
                return resultDTO;

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Exception occurred getting case note types. {ex.Message}");
                throw;
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
            PersonQuestionnaireDTO.StartDate = personCollaboration.EnrollDate;
            PersonQuestionnaireDTO.UpdateDate = DateTime.UtcNow;
            PersonQuestionnaireDTO.EndDueDate = null;
            return PersonQuestionnaireDTO;
        }

        public PersonQuestionnaireDTO EditPersonQuestionnaireDTO(CollaborationQuestionnaireDTO collaborationQuestionnaireData, PersonEditCollaborationDTO personCollaboration)
        {
            PersonQuestionnaireDTO PersonQuestionnaireDTO = new PersonQuestionnaireDTO();

            PersonQuestionnaireDTO.CollaborationID = collaborationQuestionnaireData.CollaborationID;
            PersonQuestionnaireDTO.QuestionnaireID = collaborationQuestionnaireData.QuestionnaireID;
            PersonQuestionnaireDTO.PersonID = personCollaboration.PersonID;
            PersonQuestionnaireDTO.UpdateUserID = personCollaboration.UpdateUserID;
            PersonQuestionnaireDTO.IsActive = true;
            PersonQuestionnaireDTO.StartDate = personCollaboration.EnrollDate;
            PersonQuestionnaireDTO.UpdateDate = DateTime.UtcNow;
            PersonQuestionnaireDTO.EndDueDate = null;
            return PersonQuestionnaireDTO;
        }

        /// <summary>
        /// The createRaceEthnicityDTO.
        /// </summary>
        /// <param name="PersonRaceEthnicity">The PersonRaceEthnicity<see cref="PersonRaceEthnicityDTO"/>.</param>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>The <see cref="PersonRaceEthnicityDTO"/>.</returns>
        public PersonRaceEthnicityDTO CreateRaceEthnicityDTO(PersonRaceEthnicityDTO PersonRaceEthnicity, PeopleDetailsDTO peopleDetailsDTO)
        {

            PersonRaceEthnicityDTO PersonRaceEthnicityDTO = new PersonRaceEthnicityDTO();

            if (peopleDetailsDTO.PersonID == 0 || PersonRaceEthnicity.RaceEthnicityID == 0)
            {
                return null;
            }
            else
            {
                PersonRaceEthnicityDTO.PersonID = peopleDetailsDTO.PersonID;
                PersonRaceEthnicityDTO.RaceEthnicityID = PersonRaceEthnicity.RaceEthnicityID;

                return PersonRaceEthnicityDTO;
            }
        }

        /// <summary>
        /// The EditRaceEthnicityDTO.
        /// </summary>
        /// <param name="PersonRaceEthnicity">The PersonRaceEthnicity<see cref="PersonRaceEthnicityDTO"/>.</param>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>The <see cref="PersonRaceEthnicityDTO"/>.</returns>
        public PersonRaceEthnicityDTO EditRaceEthnicityDTO(PersonEditRaceEthnicityDTO PersonRaceEthnicity, PeopleEditDetailsDTO peopleDetailsDTO)
        {

            PersonRaceEthnicityDTO PersonRaceEthnicityDTO = new PersonRaceEthnicityDTO();

            //if (peopleDetailsDTO.PersonID == 0 || PersonRaceEthnicity.RaceEthnicityID == 0)
            //{
            //    return null;
            //}
            //else
            //{
            PersonRaceEthnicityDTO.PersonRaceEthnicityID = PersonRaceEthnicity.PersonRaceEthnicityID;
            PersonRaceEthnicityDTO.PersonID = peopleDetailsDTO.PersonID;
            PersonRaceEthnicityDTO.RaceEthnicityID = PersonRaceEthnicity.RaceEthnicityID;

            return PersonRaceEthnicityDTO;
        }

        public PersonRaceEthnicity EditRaceEthnicityDTOData(PersonEditRaceEthnicityDTO PersonRaceEthnicity, PeopleEditDetailsDTO peopleDetailsDTO)
        {

            PersonRaceEthnicity PersonRaceEthnicityDTO = new PersonRaceEthnicity();

            //if (peopleDetailsDTO.PersonID == 0 || PersonRaceEthnicity.RaceEthnicityID == 0)
            //{
            //    return null;
            //}
            //else
            //{
            PersonRaceEthnicityDTO.PersonRaceEthnicityID = PersonRaceEthnicity.PersonRaceEthnicityID;
            PersonRaceEthnicityDTO.PersonID = peopleDetailsDTO.PersonID;
            PersonRaceEthnicityDTO.RaceEthnicityID = PersonRaceEthnicity.RaceEthnicityID;

            return PersonRaceEthnicityDTO;
        }

        /// <summary>
        /// The createPersonHelper.
        /// </summary>
        /// <param name="PersonHelperDTO">The PersonHelperDTO<see cref="PersonHelperDTO"/>.</param>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>The <see cref="PersonHelperDTO"/>.</returns>
        public PersonHelperDTO CreatePersonHelper(PersonHelperDTO PersonHelperDTO, PeopleDetailsDTO peopleDetailsDTO)
        {
            PersonHelperDTO personHelperDTO = new PersonHelperDTO();

            if (PersonHelperDTO.HelperID == 0 || peopleDetailsDTO.UpdateUserID == 0 || peopleDetailsDTO.PersonID == 0)
            {
                return null;
            }
            else
            {
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());

                personHelperDTO.PersonID = peopleDetailsDTO.PersonID;
                personHelperDTO.UpdateUserID = peopleDetailsDTO.UpdateUserID;
                personHelperDTO.HelperID = PersonHelperDTO.HelperID;
                personHelperDTO.UpdateDate = DateTime.UtcNow;
                personHelperDTO.EndDate = PersonHelperDTO.EndDate.HasValue ? PersonHelperDTO.EndDate.Value.Date : PersonHelperDTO.EndDate;
                personHelperDTO.StartDate = PersonHelperDTO.StartDate.Date;
                personHelperDTO.IsLead = PersonHelperDTO.IsLead;
                personHelperDTO.IsCurrent = PersonHelperDTO.EndDate == null ? true : PersonHelperDTO.IsCurrent;
                personHelperDTO.IsRemoved = false;
                personHelperDTO.CollaborationID = PersonHelperDTO.CollaborationID == 0 ? null : personHelperDTO.CollaborationID;

                return personHelperDTO;
            }
        }

        /// <summary>
        /// The EditPersonHelper.
        /// </summary>
        /// <param name="PersonHelperDTO">The PersonHelperDTO<see cref="PersonHelperDTO"/>.</param>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>The <see cref="PersonHelperDTO"/>.</returns>
        public PersonHelperDTO EditPersonHelper(PersonEditHelperDTO PersonHelperDTO, PeopleEditDetailsDTO peopleDetailsDTO)
        {
            PersonHelperDTO personHelperDTO = new PersonHelperDTO();

            //if (PersonHelperDTO.HelperID == 0 || peopleDetailsDTO.UpdateUserID == 0 || peopleDetailsDTO.PersonID == 0)
            //{
            //    return null;
            //}
            //else
            //{
            var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());

            personHelperDTO.PersonHelperID = PersonHelperDTO.PersonHelperID;
            personHelperDTO.UpdateDate = DateTime.UtcNow;
            personHelperDTO.PersonID = peopleDetailsDTO.PersonID;
            personHelperDTO.UpdateUserID = peopleDetailsDTO.UpdateUserID;
            personHelperDTO.HelperID = PersonHelperDTO.HelperID;
            personHelperDTO.EndDate = PersonHelperDTO.EndDate.HasValue ? PersonHelperDTO.EndDate.Value.Date : PersonHelperDTO.EndDate;
            personHelperDTO.StartDate = PersonHelperDTO.StartDate.Date;
            personHelperDTO.IsLead = PersonHelperDTO.IsLead;
            personHelperDTO.IsCurrent = PersonHelperDTO.EndDate == null ? true : PersonHelperDTO.IsCurrent;
            personHelperDTO.IsRemoved = false;
            personHelperDTO.CollaborationID = PersonHelperDTO.CollaborationID == 0 ? null : PersonHelperDTO.CollaborationID;

            return personHelperDTO;
        }

        /// <summary>
        /// The createPersonSupport.
        /// </summary>
        /// <param name="PersonSupportDTO">The PersonSupportDTO<see cref="PersonSupportDTO"/>.</param>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>The <see cref="PersonSupportDTO"/>.</returns>
        public PersonSupportDTO CreatePersonSupport(PersonSupportDTO PersonSupportDTO, PeopleDetailsDTO peopleDetailsDTO)
        {
            PersonSupportDTO personSupportDTO = new PersonSupportDTO();

            if (PersonSupportDTO != null)
            {
                if (String.IsNullOrEmpty(PersonSupportDTO.FirstName) || String.IsNullOrEmpty(PersonSupportDTO.LastName) || PersonSupportDTO.SupportTypeID == 0 || peopleDetailsDTO.UpdateUserID == 0 || peopleDetailsDTO.PersonID == 0)
                {
                    return null;
                }
                else
                {
                    var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());

                    personSupportDTO.PersonID = peopleDetailsDTO.PersonID;
                    personSupportDTO.UpdateUserID = peopleDetailsDTO.UpdateUserID;
                    personSupportDTO.SupportTypeID = PersonSupportDTO.SupportTypeID;

                    personSupportDTO.UpdateDate = DateTime.UtcNow;
                    personSupportDTO.StartDate = PersonSupportDTO.StartDate.Date;
                    personSupportDTO.EndDate = PersonSupportDTO.EndDate.HasValue ? PersonSupportDTO.EndDate.Value.Date : PersonSupportDTO.EndDate;
                    personSupportDTO.FirstName = PersonSupportDTO.FirstName;
                    personSupportDTO.LastName = PersonSupportDTO.LastName;
                    personSupportDTO.MiddleName = PersonSupportDTO.MiddleName;
                    personSupportDTO.Suffix = PersonSupportDTO.Suffix;
                    personSupportDTO.IsRemoved = false;
                    personSupportDTO.IsCurrent = PersonSupportDTO.IsCurrent;
                    personSupportDTO.Email = PersonSupportDTO.Email;
                    personSupportDTO.Note = PersonSupportDTO.Note;
                    personSupportDTO.Phone = PersonSupportDTO.Phone;
                    personSupportDTO.PhoneCode = PersonSupportDTO.PhoneCode;
                    personSupportDTO.UniversalID = PersonSupportDTO.UniversalID;
                    personSupportDTO.TextPermission = PersonSupportDTO.TextPermission;
                    personSupportDTO.EmailPermission = PersonSupportDTO.EmailPermission;
                }
            }
            return personSupportDTO;
        }

        /// <summary>
        /// The createPersonSupport.
        /// </summary>
        /// <param name="PersonSupportDTO">The PersonSupportDTO<see cref="PersonSupportDTO"/>.</param>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>The <see cref="PersonSupportDTO"/>.</returns>
        public PersonSupportDTO EditPersonSupport(PersonEditSupportDTO PersonSupportDTO, PeopleEditDetailsDTO peopleDetailsDTO, List<PersonSupportDTO> personSupportInDB)
        {
            PersonSupportDTO personSupportDTO = new PersonSupportDTO();

            //if (String.IsNullOrEmpty(PersonSupportDTO.FirstName) || String.IsNullOrEmpty(PersonSupportDTO.LastName) || PersonSupportDTO.SupportTypeID == 0 || PersonSupportDTO.UpdateUserID == 0 || peopleDetailsDTO.PersonID == 0)
            //{
            //    return null;
            //}
            //else
            //{
            var personSupport = personSupportInDB.Where(x => x.PersonSupportID == PersonSupportDTO.PersonSupportID).FirstOrDefault();
            var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
            personSupportDTO.PersonSupportID = PersonSupportDTO.PersonSupportID;
            personSupportDTO.UpdateDate = DateTime.UtcNow;
            personSupportDTO.PersonID = peopleDetailsDTO.PersonID;
            personSupportDTO.UpdateUserID = peopleDetailsDTO.UpdateUserID;
            personSupportDTO.SupportTypeID = PersonSupportDTO.SupportTypeID;

            personSupportDTO.UpdateDate = DateTime.UtcNow;
            personSupportDTO.StartDate = PersonSupportDTO.StartDate.Date;
            personSupportDTO.EndDate = PersonSupportDTO.EndDate.HasValue ? PersonSupportDTO.EndDate.Value.Date : PersonSupportDTO.EndDate;
            personSupportDTO.FirstName = PersonSupportDTO.FirstName;
            personSupportDTO.LastName = PersonSupportDTO.LastName;
            personSupportDTO.Suffix = PersonSupportDTO.Suffix;
            personSupportDTO.MiddleName = PersonSupportDTO.MiddleName;
            personSupportDTO.IsRemoved = false;
            personSupportDTO.IsCurrent = PersonSupportDTO.IsCurrent;
            personSupportDTO.Email = PersonSupportDTO.Email;
            personSupportDTO.Note = PersonSupportDTO.Note;
            personSupportDTO.Phone = PersonSupportDTO.Phone;
            personSupportDTO.PhoneCode = PersonSupportDTO.PhoneCode;
            personSupportDTO.TextPermission = PersonSupportDTO.TextPermission;
            personSupportDTO.EmailPermission = PersonSupportDTO.EmailPermission;
            personSupportDTO.UniversalID = personSupport != null ? personSupport.UniversalID : PersonSupportDTO.UniversalID;
            personSupportDTO.SMSConsentStoppedON = personSupport != null ? personSupport.SMSConsentStoppedON : null;
            return personSupportDTO;
        }

        /// <summary>
        /// The createPersonIdentification.
        /// </summary>
        /// <param name="PersonIdentification">The PersonIdentification<see cref="PersonIdentificationDTO"/>.</param>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>The <see cref="PersonIdentificationDTO"/>.</returns>
        public PersonIdentificationDTO CreatePersonIdentification(PersonIdentificationDTO PersonIdentification, PeopleDetailsDTO peopleDetailsDTO)
        {
            PersonIdentificationDTO personIdentificationDTO = new PersonIdentificationDTO();

            if (PersonIdentification.IdentificationTypeID == 0 || peopleDetailsDTO.UpdateUserID == 0 || peopleDetailsDTO.PersonID == 0)
            {
                return null;
            }
            else
            {
                personIdentificationDTO.PersonID = peopleDetailsDTO.PersonID;
                personIdentificationDTO.UpdateUserID = peopleDetailsDTO.UpdateUserID;
                personIdentificationDTO.IdentificationTypeID = PersonIdentification.IdentificationTypeID;
                personIdentificationDTO.IdentificationNumber = PersonIdentification.IdentificationNumber;
                personIdentificationDTO.UpdateDate = DateTime.UtcNow; ;
                personIdentificationDTO.IsRemoved = false;

                return personIdentificationDTO;
            }
        }

        /// <summary>
        /// The createPersonIdentification.
        /// </summary>
        /// <param name="PersonIdentification">The PersonIdentification<see cref="PersonIdentificationDTO"/>.</param>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>The <see cref="PersonIdentificationDTO"/>.</returns>
        public PersonIdentificationDTO EditPersonIdentification(PersonEditIdentificationDTO PersonIdentification, PeopleEditDetailsDTO peopleDetailsDTO)
        {
            PersonIdentificationDTO personIdentificationDTO = new PersonIdentificationDTO();
            if (PersonIdentification.PersonIdentificationID > 0 && PersonIdentification.IdentificationTypeID == 0)
            {
                var personIdentification = personIdentificationRepository.GetRowAsync(x => x.PersonIdentificationID == PersonIdentification.PersonIdentificationID).Result;
                this.mapper.Map<PersonIdentification, PersonIdentificationDTO>(personIdentification, personIdentificationDTO);
                personIdentificationDTO.IsRemoved = true;
                return personIdentificationDTO;
            }
            else if ((PersonIdentification.IdentificationTypeID == 0 || peopleDetailsDTO.UpdateUserID == 0 || peopleDetailsDTO.PersonID == 0)
                && PersonIdentification.PersonIdentificationID == 0)
            {
                return null;

            }
            else
            {
                personIdentificationDTO.PersonIdentificationID = PersonIdentification.PersonIdentificationID;
                personIdentificationDTO.UpdateDate = DateTime.UtcNow;
                personIdentificationDTO.PersonID = peopleDetailsDTO.PersonID;
                personIdentificationDTO.UpdateUserID = peopleDetailsDTO.UpdateUserID;
                personIdentificationDTO.IdentificationTypeID = PersonIdentification.IdentificationTypeID;
                personIdentificationDTO.IdentificationNumber = PersonIdentification.IdentificationNumber;
                personIdentificationDTO.IsRemoved = false;

                return personIdentificationDTO;
            }
        }

        /// <summary>
        /// The createPersonCollaboration.
        /// </summary>
        /// <param name="collaborationDTO">The collaborationDTO<see cref="PersonCollaborationDTO"/>.</param>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>The <see cref="PersonCollaborationDTO"/>.</returns>
        public PersonCollaborationDTO EditPersonCollaboration(PersonEditCollaborationDTO collaborationDTO, PeopleEditDetailsDTO peopleDetailsDTO)
        {
            PersonCollaborationDTO personCollaborationDTO = new PersonCollaborationDTO();

            //if (collaborationDTO.CollaborationID == 0 || peopleDetailsDTO.UpdateUserID == 0 || peopleDetailsDTO.PersonID == 0)
            //{
            //    return null;
            //}
            //else
            //{
            var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());

            personCollaborationDTO.PersonCollaborationID = collaborationDTO.PersonCollaborationID;
            personCollaborationDTO.UpdateDate = DateTime.UtcNow;
            personCollaborationDTO.PersonID = peopleDetailsDTO.PersonID;
            personCollaborationDTO.UpdateUserID = peopleDetailsDTO.UpdateUserID;
            personCollaborationDTO.EnrollDate = collaborationDTO.EnrollDate.Date;
            personCollaborationDTO.CollaborationID = collaborationDTO.CollaborationID;
            personCollaborationDTO.UpdateDate = DateTime.UtcNow; ;
            personCollaborationDTO.EndDate = collaborationDTO.EndDate.HasValue ? collaborationDTO.EndDate.Value.Date : collaborationDTO.EndDate;
            personCollaborationDTO.IsPrimary = collaborationDTO.EndDate == null || collaborationDTO.EndDate.Value.Date >= DateTime.UtcNow.Date ? collaborationDTO.IsPrimary : false;
            personCollaborationDTO.IsCurrent = collaborationDTO.EndDate == null || collaborationDTO.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;
            personCollaborationDTO.IsRemoved = false;

            return personCollaborationDTO;
        }

        public PersonCollaboration EditPersonCollaborationData(PersonEditCollaborationDTO collaborationDTO, PeopleEditDetailsDTO peopleDetailsDTO)
        {
            PersonCollaboration personCollaborationDTOData = new PersonCollaboration();

            //if (collaborationDTO.CollaborationID == 0 || peopleDetailsDTO.UpdateUserID == 0 || peopleDetailsDTO.PersonID == 0)
            //{
            //    return null;
            //}
            //else
            //{
            var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());

            personCollaborationDTOData.PersonCollaborationID = collaborationDTO.PersonCollaborationID;
            personCollaborationDTOData.UpdateDate = DateTime.UtcNow;
            personCollaborationDTOData.PersonID = peopleDetailsDTO.PersonID;
            personCollaborationDTOData.UpdateUserID = peopleDetailsDTO.UpdateUserID;
            personCollaborationDTOData.EnrollDate = collaborationDTO.EnrollDate.Date;
            personCollaborationDTOData.CollaborationID = collaborationDTO.CollaborationID;
            personCollaborationDTOData.UpdateDate = DateTime.UtcNow; ;
            personCollaborationDTOData.EndDate = collaborationDTO.EndDate.HasValue ? collaborationDTO.EndDate.Value.Date : collaborationDTO.EndDate;
            personCollaborationDTOData.IsPrimary = collaborationDTO.EndDate == null || collaborationDTO.EndDate.Value.Date >= DateTime.UtcNow.Date ? collaborationDTO.IsPrimary : false;
            personCollaborationDTOData.IsCurrent = collaborationDTO.EndDate == null || collaborationDTO.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;
            personCollaborationDTOData.IsRemoved = false;

            return personCollaborationDTOData;
        }

        /// <summary>
        /// The createPersonCollaboration.
        /// </summary>
        /// <param name="collaborationDTO">The collaborationDTO<see cref="PersonCollaborationDTO"/>.</param>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>The <see cref="PersonCollaborationDTO"/>.</returns>
        public PersonCollaborationDTO CreatePersonCollaboration(PersonCollaborationDTO collaborationDTO, PeopleDetailsDTO peopleDetailsDTO)
        {
            PersonCollaborationDTO personCollaborationDTO = new PersonCollaborationDTO();

            if (collaborationDTO.CollaborationID == 0 || peopleDetailsDTO.UpdateUserID == 0 || peopleDetailsDTO.PersonID == 0)
            {
                return null;
            }
            else
            {
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());

                personCollaborationDTO.PersonID = peopleDetailsDTO.PersonID;
                personCollaborationDTO.UpdateUserID = peopleDetailsDTO.UpdateUserID;
                personCollaborationDTO.EnrollDate = collaborationDTO.EnrollDate.Date;
                personCollaborationDTO.CollaborationID = collaborationDTO.CollaborationID;
                personCollaborationDTO.UpdateDate = DateTime.UtcNow;
                personCollaborationDTO.EndDate = collaborationDTO.EndDate.HasValue ? collaborationDTO.EndDate.Value.Date : collaborationDTO.EndDate;
                personCollaborationDTO.IsPrimary = collaborationDTO.EndDate == null || collaborationDTO.EndDate.Value.Date >= DateTime.UtcNow.Date ? collaborationDTO.IsPrimary : false;
                personCollaborationDTO.IsCurrent = collaborationDTO.EndDate == null || collaborationDTO.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;
                personCollaborationDTO.IsRemoved = false;

                return personCollaborationDTO;


            }
        }

        /// <summary>
        /// To create AgencyAddressDTO.
        /// </summary>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>AgencyAddressDTO.</returns>
        public PeopleDTO CreatePeople(PeopleDetailsDTO peopleDetailsDTO)
        {
            PeopleDTO peopleDTO = new PeopleDTO();

            if (String.IsNullOrEmpty(peopleDetailsDTO.FirstName) || String.IsNullOrEmpty(peopleDetailsDTO.LastName) || peopleDetailsDTO.PersonScreeningStatusID == 0 || peopleDetailsDTO.UpdateUserID == 0 || peopleDetailsDTO.AgencyID == 0)
            {
                return null;
            }
            else
            {
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());

                peopleDTO.UpdateUserID = peopleDetailsDTO.UpdateUserID;
                peopleDTO.FirstName = peopleDetailsDTO.FirstName;
                peopleDTO.MiddleName = peopleDetailsDTO.MiddleName;
                peopleDTO.LastName = peopleDetailsDTO.LastName;
                peopleDTO.Suffix = peopleDetailsDTO.Suffix;
                peopleDTO.PrimaryLanguageID = peopleDetailsDTO.PrimaryLanguageID <= 0 ? null : peopleDetailsDTO.PrimaryLanguageID;
                peopleDTO.PreferredLanguageID = peopleDetailsDTO.PreferredLanguageID <= 0 ? null : peopleDetailsDTO.PreferredLanguageID;
                peopleDTO.DateOfBirth = peopleDetailsDTO.DateOfBirth.Date;
                peopleDTO.GenderID = peopleDetailsDTO.GenderID;
                peopleDTO.SexualityID = peopleDetailsDTO.SexualityID <= 0 ? null : peopleDetailsDTO.SexualityID;
                peopleDTO.Suffix = peopleDetailsDTO.Suffix;
                peopleDTO.BiologicalSexID = peopleDetailsDTO.BiologicalSexID <= 0 ? null : peopleDetailsDTO.BiologicalSexID;
                peopleDTO.Email = peopleDetailsDTO.Email;
                peopleDTO.Phone1Code = peopleDetailsDTO.Phone1Code;
                peopleDTO.Phone2Code = peopleDetailsDTO.Phone2Code;
                peopleDTO.Phone1 = peopleDetailsDTO.Phone1;
                peopleDTO.Phone2 = peopleDetailsDTO.Phone2;
                peopleDTO.PersonScreeningStatusID = peopleDetailsDTO.PersonScreeningStatusID;
                peopleDTO.UpdateDate = DateTime.UtcNow;
                peopleDTO.AgencyID = peopleDetailsDTO.AgencyID;
                peopleDTO.StartDate = peopleDetailsDTO.StartDate != DateTime.MinValue ? peopleDetailsDTO.StartDate.Date : DateTime.Now.Date;
                peopleDTO.EndDate = peopleDetailsDTO.EndDate.HasValue ? peopleDetailsDTO.EndDate.Value.Date : peopleDetailsDTO.EndDate;
                peopleDTO.IsActive = peopleDetailsDTO.IsActive;
                peopleDTO.IsRemoved = false;
                peopleDTO.UniversalID = peopleDetailsDTO.UniversalID;
                peopleDTO.TextPermission = peopleDetailsDTO.TextPermission;
                peopleDTO.EmailPermission = peopleDetailsDTO.EmailPermission;


                return peopleDTO;

            }
        }

        /// <summary>
        /// To create AgencyAddressDTO.
        /// </summary>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleEditDetailsDTO"/>.</param>
        /// <returns>AgencyAddressDTO.</returns>
        public PeopleDTO EditPeople(PeopleEditDetailsDTO peopleDetailsDTO)
        {
            PeopleDTO peopleDTO = new PeopleDTO();

            if (String.IsNullOrEmpty(peopleDetailsDTO.FirstName) || String.IsNullOrEmpty(peopleDetailsDTO.LastName) || peopleDetailsDTO.PersonScreeningStatusID == 0 || peopleDetailsDTO.UpdateUserID == 0 || peopleDetailsDTO.AgencyID == 0)
            {
                return null;
            }
            else
            {
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());

                peopleDTO.PersonID = peopleDetailsDTO.PersonID;
                peopleDTO.PersonIndex = peopleDetailsDTO.PersonIndex;
                peopleDTO.UpdateDate = DateTime.UtcNow;
                peopleDTO.UpdateUserID = peopleDetailsDTO.UpdateUserID;
                peopleDTO.FirstName = peopleDetailsDTO.FirstName;
                peopleDTO.MiddleName = peopleDetailsDTO.MiddleName;
                peopleDTO.LastName = peopleDetailsDTO.LastName;
                peopleDTO.Suffix = peopleDetailsDTO.Suffix;
                peopleDTO.PrimaryLanguageID = peopleDetailsDTO.PrimaryLanguageID <= 0 ? null : peopleDetailsDTO.PrimaryLanguageID;
                peopleDTO.PreferredLanguageID = peopleDetailsDTO.PreferredLanguageID <= 0 ? null : peopleDetailsDTO.PreferredLanguageID;
                peopleDTO.DateOfBirth = peopleDetailsDTO.DateOfBirth.Date;
                peopleDTO.GenderID = peopleDetailsDTO.GenderID;
                peopleDTO.SexualityID = peopleDetailsDTO.SexualityID <= 0 ? null : peopleDetailsDTO.SexualityID;
                peopleDTO.Suffix = peopleDetailsDTO.Suffix;
                peopleDTO.BiologicalSexID = peopleDetailsDTO.BiologicalSexID <= 0 ? null : peopleDetailsDTO.BiologicalSexID;
                peopleDTO.Email = peopleDetailsDTO.Email;
                peopleDTO.Phone1Code = peopleDetailsDTO.Phone1Code;
                peopleDTO.Phone2Code = peopleDetailsDTO.Phone2Code;
                peopleDTO.Phone1 = peopleDetailsDTO.Phone1;
                peopleDTO.Phone2 = peopleDetailsDTO.Phone2;
                peopleDTO.PersonScreeningStatusID = peopleDetailsDTO.PersonScreeningStatusID;
                peopleDTO.UpdateDate = DateTime.UtcNow;
                peopleDTO.AgencyID = peopleDetailsDTO.AgencyID;
                peopleDTO.StartDate = utility.ConvertToUtcDateTime(peopleDetailsDTO.StartDate.Date, offset);
                peopleDTO.EndDate = peopleDetailsDTO.EndDate.HasValue ? utility.ConvertToUtcDateTime(peopleDetailsDTO.EndDate.Value.Date, offset) : peopleDetailsDTO.EndDate;
                peopleDTO.IsActive = peopleDetailsDTO.IsActive;
                peopleDTO.UniversalID = peopleDetailsDTO.UniversalID;
                peopleDTO.EmailPermission = peopleDetailsDTO.EmailPermission;
                peopleDTO.TextPermission = peopleDetailsDTO.TextPermission;
                peopleDTO.IsRemoved = false;

                return peopleDTO;

            }
        }

        /// <summary>
        /// To create AddressDTO.
        /// </summary>
        /// <param name="peopleDetailsDTO">consumerRepository.</param>
        /// <returns>AgencyAddressDTO.</returns>
        public AddressDTO CreateAddressDTOOnAdd(PeopleDetailsDTO peopleDetailsDTO)
        {
            AddressDTO addressDTO = new AddressDTO();

            if (!((string.IsNullOrEmpty(peopleDetailsDTO.Address1.Trim()) && string.IsNullOrEmpty(peopleDetailsDTO.Address2.Trim())
            && string.IsNullOrEmpty(peopleDetailsDTO.City.Trim()) && peopleDetailsDTO.CountryStateId == 0 && peopleDetailsDTO.CountryId == 0
            && string.IsNullOrEmpty(peopleDetailsDTO.Zip.Trim())) || peopleDetailsDTO.UpdateUserID == 0))
            {
                addressDTO.CountryStateID = peopleDetailsDTO.CountryStateId <= 0 ? null : peopleDetailsDTO.CountryStateId;
                addressDTO.UpdateUserID = peopleDetailsDTO.UpdateUserID;
                addressDTO.Address1 = peopleDetailsDTO.Address1;
                addressDTO.Address2 = peopleDetailsDTO.Address2;
                addressDTO.City = peopleDetailsDTO.City;
                addressDTO.Zip = peopleDetailsDTO.Zip;
                addressDTO.Zip4 = peopleDetailsDTO.Zip4;
                addressDTO.IsPrimary = true;
                addressDTO.IsRemoved = false;
                addressDTO.CountryID = peopleDetailsDTO.CountryId <= 0 ? null : peopleDetailsDTO.CountryId;
                return addressDTO;

            }
            return null;
        }

        /// <summary>
        /// To create AddressDTO.
        /// </summary>
        /// <param name="PeopleEditDetailsDTO">The PeopleEditDetailsDTO<see cref="PeopleEditDetailsDTO"/>.</param>
        /// <returns>AgencyAddressDTO.</returns>
        public AddressDTO EditAddress(PeopleEditDetailsDTO PeopleEditDetailsDTO)
        {
            AddressDTO addressDTO = new AddressDTO();

            if (PeopleEditDetailsDTO.UpdateUserID == 0)
            {
                return null;
            }
            else
            {
                addressDTO.CountryStateID = PeopleEditDetailsDTO.CountryStateId == 0 ? null : PeopleEditDetailsDTO.CountryStateId;
                addressDTO.AddressID = PeopleEditDetailsDTO.AddressID;
                addressDTO.AddressIndex = PeopleEditDetailsDTO.AddressIndex;
                addressDTO.UpdateDate = DateTime.UtcNow;
                addressDTO.UpdateUserID = PeopleEditDetailsDTO.UpdateUserID;
                addressDTO.Address1 = PeopleEditDetailsDTO.Address1;
                addressDTO.Address2 = PeopleEditDetailsDTO.Address2;
                addressDTO.City = PeopleEditDetailsDTO.City;
                addressDTO.Zip = PeopleEditDetailsDTO.Zip;
                addressDTO.Zip4 = PeopleEditDetailsDTO.Zip4;
                addressDTO.IsPrimary = true;
                addressDTO.IsRemoved = false;
                addressDTO.CountryID = PeopleEditDetailsDTO.CountryId == 0 ? null : PeopleEditDetailsDTO.CountryId;
                return addressDTO;

            }
        }

        /// <summary>
        /// RemovePeopleDetails.
        /// </summary>
        /// <param name="peopleIndex">The peopleIndex<see cref="peopleIndex"/>.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO RemovePeopleDetails(Guid peopleIndex, int updateUserID, long loggedInAgencyID)
        {
            try
            {
                CRUDResponseDTO resultDTO = new CRUDResponseDTO();

                if (peopleIndex != Guid.Empty)
                {
                    var Person = this.personRepository.GetPerson(peopleIndex);
                    if (Person != null)
                    {
                        if (Person.AgencyID != loggedInAgencyID)
                        {
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                            return resultDTO;
                        }
                        Person.IsRemoved = true;
                        Person.UpdateDate = DateTime.UtcNow;
                        Person.UpdateUserID = updateUserID;

                        var personResult = this.personRepository.UpdatePerson(Person);
                        if (personResult != null)
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
        /// To create AgencyAddressDTO.
        /// </summary>
        /// <param name="PersonID">The PersonID<see cref="long"/>.</param>
        /// <param name="AddressID">The AddressID<see cref="long"/>.</param>
        /// <returns>AgencyAddressDTO.</returns>
        public PersonAddressDTO CreatePersonAddress(long PersonID, long AddressID)
        {
            PersonAddressDTO personAddressDTO = new PersonAddressDTO();
            if (PersonID != 0 && AddressID != 0)
            {
                personAddressDTO.PersonID = PersonID;
                personAddressDTO.AddressID = AddressID;
                personAddressDTO.IsPrimary = true;
                return personAddressDTO;
            }
            else
            {
                return null;

            }
        }

        /// <summary>
        /// EditPeopleDetails.
        /// </summary>
        /// <param name="peopleEditDetailsDTO">The peopleEditDetailsDTO<see cref="PeopleEditDetailsDTO"/>.</param>
        /// <returns>PeopleDetailsResponseDTO.</returns>
        public CRUDResponseDTO EditPeopleDetails(PeopleEditDetailsDTO peopleEditDetailsDTO, long loggedInAgencyID, string CallingType = "")
        {
            try
            {

                CRUDResponseDTO resultDTO = new CRUDResponseDTO();
                List<PersonCollaborationDTO> PersonCollaborationDTO = new List<PersonCollaborationDTO>();
                if (peopleEditDetailsDTO.PersonIndex != Guid.Empty)
                {
                    var person = this.personRepository.GetPerson(peopleEditDetailsDTO.PersonIndex);
                    #region SharedAccessOrNot
                    if (!this.personRepository.IsValidPersonInAgency(person.PersonID, person.AgencyID, loggedInAgencyID))
                    {
                        resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                        resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                        return resultDTO;
                    }
                    #endregion
                    peopleEditDetailsDTO.PersonID = person.PersonID;
                    if (CallingType == PCISEnum.CallingType.EHR)
                        peopleEditDetailsDTO.UniversalID = person.UniversalID;

                    if (peopleEditDetailsDTO.PersonID != 0)
                    {
                        this.mapper.Map<List<PersonEditCollaborationDTO>, List<PersonCollaborationDTO>>(peopleEditDetailsDTO.PersonEditCollaborationDTO, PersonCollaborationDTO);
                        var personActive = CheckPersonCollaborationEndDate(PersonCollaborationDTO);
                        peopleEditDetailsDTO.IsActive = personActive;
                        peopleEditDetailsDTO.StartDate = CallingType != PCISEnum.CallingType.EHR ? person.StartDate : peopleEditDetailsDTO.StartDate;
                        PeopleDTO peopleDTO = EditPeople(peopleEditDetailsDTO);

                        if (peopleDTO != null)
                        {
                            //Fetch all existing person related data from DB.
                            var personCollaborationInDB = this.personCollaborationRepository.GetPersonCollaborationByDataId(peopleEditDetailsDTO.PersonID);
                            var personSupportInDB = this.personSupportRepository.GetPersonSupport(peopleEditDetailsDTO.PersonID);
                            var personHelperDataInDB = this.personHelperRepository.GetPersonHelperByDataId(peopleEditDetailsDTO.PersonID);
                            var PersonIdentificationDataInDB = this.personIdentificationRepository.GetPersonIdentificationTypeByDataId(peopleEditDetailsDTO.PersonID);
                            var personRaceEthnicityDataInDB = this.personRaceEthnicityRepository.GetRaceEthnicityByid(peopleEditDetailsDTO.PersonID);
                            if (!ValidateExternalAPIPersonEditForPrimaryKeyIDs(peopleEditDetailsDTO, personCollaborationInDB, personSupportInDB, personHelperDataInDB, PersonIdentificationDataInDB, personRaceEthnicityDataInDB, CallingType, resultDTO))
                                return resultDTO;

                            peopleDTO.SMSConsentStoppedON = person.SMSConsentStoppedON;
                            var personResult = this.personRepository.UpdatePerson(peopleDTO);
                            if (personResult != null)
                            {
                                if (CallingType != PCISEnum.CallingType.EHR)
                                {
                                    EditAddressForPerson(peopleEditDetailsDTO);
                                }

                                #region AuditPersonCollaboration
                                //For AuditPersonCollaboration---START-----------------------//
                                var existingPrimaryCollaborationInDB = personCollaborationInDB.Where(x => x.IsPrimary == true && x.IsRemoved == false).ToList();
                                var existingPrimaryCollaborationInDBFromUI = new List<PersonEditCollaborationDTO>();
                                if (existingPrimaryCollaborationInDB.Count > 0)
                                {
                                    existingPrimaryCollaborationInDBFromUI = peopleEditDetailsDTO.PersonEditCollaborationDTO.Where(x => x.PersonCollaborationID == existingPrimaryCollaborationInDB[0].PersonCollaborationID).ToList();
                                    var PrimaryCollaborationFromUI = peopleEditDetailsDTO.PersonEditCollaborationDTO.Where(x => x.IsPrimary == true && x.IsRemoved == false).ToList();
                                    InsertToPersonPrimaryCollaborationHistory(PrimaryCollaborationFromUI, existingPrimaryCollaborationInDB, existingPrimaryCollaborationInDBFromUI);
                                }
                                //For AuditPersonCollaboration---END---------------------- -//
                                #endregion

                                #region PersonCollaboration 
                                //List<PersonCollaboration> personCollaborationEditList = new List<PersonCollaboration>();
                                List<PersonCollaborationDTO> PersonCollaborationAddList = new List<PersonCollaborationDTO>();
                                if (peopleEditDetailsDTO.PersonEditCollaborationDTO.Count > 0)
                                {
                                    if (CallingType == PCISEnum.CallingType.EHR)
                                    {
                                        peopleEditDetailsDTO.PersonEditCollaborationDTO = ComparePersonCollaboration(personCollaborationInDB, peopleEditDetailsDTO.PersonEditCollaborationDTO, peopleEditDetailsDTO.AgencyID);
                                    }

                                    #region PersonCollaborations-To be Marked as Removed 
                                    //Mark the PersonCollaborations to be Removed
                                    var personCollaborationsToBeRemoved = personCollaborationInDB.Where(x => !peopleEditDetailsDTO.PersonEditCollaborationDTO.Select(y => y.PersonCollaborationID).ToList().Contains(x.PersonCollaborationID)).ToList();
                                    foreach (PersonCollaboration CollaborationDTO in personCollaborationsToBeRemoved)
                                    {
                                        CollaborationDTO.IsRemoved = true;
                                        CollaborationDTO.IsPrimary = false;
                                        CollaborationDTO.UpdateDate = DateTime.UtcNow;
                                        CollaborationDTO.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;
                                    }
                                    RemovePersonQuestionairesAndSchedules(personCollaborationsToBeRemoved,peopleEditDetailsDTO);
                                    #endregion

                                    #region PersonCollaboration-To be Updated/Added
                                    var allCollaborationIdsInDB = personCollaborationInDB.Where(x => x.IsRemoved == false).Select(x => x.CollaborationID).Distinct().ToList();
                                    List<PersonQuestionnaireDTO> personQuestionnaireAddList = new List<PersonQuestionnaireDTO>();
                                    List<int> editedCollaborationIds = new List<int>();
                                    foreach (PersonEditCollaborationDTO personCollaboration in peopleEditDetailsDTO.PersonEditCollaborationDTO)
                                    {
                                        var personCollaborationFromDB = personCollaborationInDB.Where(x => x.PersonCollaborationID == personCollaboration.PersonCollaborationID).FirstOrDefault();
                                        //Edit the existing PersonCollaboration
                                        if (personCollaborationFromDB != null)
                                        {
                                            if (personCollaborationFromDB.EndDate != personCollaboration.EndDate
                                                   || personCollaborationFromDB.EnrollDate.Value.Date != personCollaboration.EnrollDate.Date
                                                   || personCollaborationFromDB.CollaborationID != personCollaboration.CollaborationID)
                                            {
                                                editedCollaborationIds.Add(personCollaboration.CollaborationID);
                                            }

                                            personCollaborationFromDB.UpdateDate = DateTime.UtcNow;
                                            personCollaborationFromDB.EnrollDate = personCollaboration.EnrollDate.Date;
                                            personCollaborationFromDB.CollaborationID = personCollaboration.CollaborationID;
                                            personCollaborationFromDB.EndDate = personCollaboration.EndDate.HasValue ? personCollaboration.EndDate.Value.Date : personCollaboration.EndDate;
                                            personCollaborationFromDB.IsPrimary = personCollaboration.EndDate == null || personCollaboration.EndDate.Value.Date >= DateTime.UtcNow.Date ? personCollaboration.IsPrimary : false;
                                            personCollaborationFromDB.IsCurrent = personCollaboration.EndDate == null || personCollaboration.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;
                                            //personCollaborationEditList.Add(personCollaborationFromDB);
                                        }
                                        //Add the new PersonCollaboration
                                        else
                                        {
                                            PersonCollaborationDTO personCollaborationDTO = EditPersonCollaboration(personCollaboration, peopleEditDetailsDTO);
                                            PersonCollaborationAddList.Add(personCollaborationDTO);
                                        }
                                    }
                                    //To insert new PersonQuestionnaires if any
                                    var allCollaborationIds = peopleEditDetailsDTO.PersonEditCollaborationDTO.Select(x => x.CollaborationID).Distinct().ToList();
                                    var newCollaborationIds = allCollaborationIds.Where(x => !allCollaborationIdsInDB.Contains(x)).ToList();
                                    var CollaborationQuestionnaireData = this.collaborationQuestionnaireRepository.GetCollaborationQuestionnaireData(newCollaborationIds);
                                    if (CollaborationQuestionnaireData.Count > 0)
                                    {
                                        var personCollaboration = new PersonEditCollaborationDTO();
                                        personCollaboration.PersonID = peopleEditDetailsDTO.PersonID;
                                        personCollaboration.UpdateDate = DateTime.UtcNow;
                                        personCollaboration.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;
                                        foreach (CollaborationQuestionnaireDTO CollaborationQuestionnaire in CollaborationQuestionnaireData)
                                        {
                                            PersonQuestionnaireDTO PersonQuestionnaireDTO = EditPersonQuestionnaireDTO(CollaborationQuestionnaire, personCollaboration);
                                            personQuestionnaireAddList.Add(PersonQuestionnaireDTO);
                                        }
                                    }
                                    if (personCollaborationInDB.Count > 0)
                                    {
                                        this.personCollaborationRepository.UpdateBulkPersonCollaboration(personCollaborationInDB);
                                    }
                                    if (PersonCollaborationAddList.Count > 0)
                                    {
                                        this.personCollaborationRepository.AddBulkPersonCollaboration(PersonCollaborationAddList);
                                    }
                                    if (personQuestionnaireAddList.Count > 0)
                                    {
                                        this.PersonQuestionnaireRepository.AddBulkPersonQuestionnaire(personQuestionnaireAddList);
                                        //Combine New collaborationIds and edited collaboration IDs to find PersonQuestionnaires to be pushed to queue.
                                        newCollaborationIds.AddRange(editedCollaborationIds);
                                        var personQuestionnaires = this.personQuestionnaireRepository.GetPersonQuestionnaireIdsWithReminderOn(peopleDTO.PersonID, newCollaborationIds);
                                        foreach (var item in personQuestionnaires)
                                        {
                                            StoreInQueue(item.PersonQuestionnaireID);
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    foreach (PersonCollaboration personCOllaboration in personCollaborationInDB)
                                    {
                                        personCOllaboration.IsRemoved = true;
                                        personCOllaboration.IsPrimary = false;
                                        personCOllaboration.UpdateDate = DateTime.UtcNow;
                                        personCOllaboration.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;

                                    }
                                    if (personCollaborationInDB.Count > 0)
                                    {
                                        var personCollaborationResult = this.personCollaborationRepository.UpdateBulkPersonCollaboration(personCollaborationInDB);
                                    }
                                }
                                #endregion

                                #region PersonSupport
                                List<PersonSupportDTO> personSupportToAdd = new List<PersonSupportDTO>();
                                List<PersonSupportDTO> personSupportToEdit = new List<PersonSupportDTO>();

                                if (peopleEditDetailsDTO.PersonEditSupportDTO.Count > 0)
                                {
                                    if (CallingType == PCISEnum.CallingType.EHR)
                                    {
                                        peopleEditDetailsDTO.PersonEditSupportDTO = ComparePersonSupport(personSupportInDB, peopleEditDetailsDTO.PersonEditSupportDTO, peopleEditDetailsDTO.AgencyID);
                                    }
                                    var removablePersonSupport = personSupportInDB.Where(x => !peopleEditDetailsDTO.PersonEditSupportDTO.Select(y => y.PersonSupportID).ToList().Contains(x.PersonSupportID)).ToList();

                                    foreach (PersonSupportDTO supportDTO in removablePersonSupport)
                                    {
                                        supportDTO.IsRemoved = true;
                                        supportDTO.UpdateDate = DateTime.UtcNow;
                                        supportDTO.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;
                                        personSupportToEdit.Add(supportDTO);
                                    }

                                    foreach (PersonEditSupportDTO personSupport in peopleEditDetailsDTO.PersonEditSupportDTO)
                                    {
                                        PersonSupportDTO personSupportDTO = EditPersonSupport(personSupport, peopleEditDetailsDTO, personSupportInDB);

                                        if (personSupportDTO.PersonSupportID > 0)
                                        {
                                            personSupportToEdit.Add(personSupportDTO);
                                        }
                                        else
                                        {
                                            personSupportToAdd.Add(personSupportDTO);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (PersonSupportDTO supportDTO in personSupportInDB)
                                    {
                                        supportDTO.IsRemoved = true;
                                        supportDTO.UpdateDate = DateTime.UtcNow;
                                        supportDTO.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;
                                        personSupportToEdit.Add(supportDTO);
                                    }
                                }
                                if(personSupportToEdit.Count > 0)
                                {
                                    var personsupportList = new List<PersonSupport>();
                                    this.mapper.Map<List<PersonSupportDTO>, List<PersonSupport>>(personSupportToEdit, personsupportList);
                                    this.personSupportRepository.UpdateBulkPersonSupport(personsupportList);
                                }
                                if (personSupportToAdd.Count > 0)
                                {
                                    this.personSupportRepository.AddBulkPersonSupport(personSupportToAdd);
                                }
                                #endregion

                                #region AuditLeadHelper
                                //For AuditLeadHelper---START-----------------------//
                                var existingLeadInDB = personHelperDataInDB.Where(x => x.IsLead == true && x.IsRemoved == false).ToList();
                                if (existingLeadInDB.Count > 0)
                                {
                                    var existingLeadInDBFromUI = peopleEditDetailsDTO.PersonEditHelperDTO.Where(x => x.PersonHelperID == existingLeadInDB[0].PersonHelperID).ToList();
                                    var newLeadHelper = peopleEditDetailsDTO.PersonEditHelperDTO.Where(x => x.IsLead == true && x.IsRemoved == false).ToList();
                                    var leadHelper = this.mapper.Map<List<PersonHelper>>(newLeadHelper);
                                    InsertToLeadHelperHistory(leadHelper, existingLeadInDB, existingLeadInDBFromUI);
                                }
                                //For AuditLeadHelper---END-----------------------//
                                #endregion

                                #region PersonHelper
                                List<PersonHelperDTO> personHelperAddList = new List<PersonHelperDTO>();

                                if (peopleEditDetailsDTO.PersonEditHelperDTO.Count > 0)
                                {
                                    if (CallingType == PCISEnum.CallingType.EHR)
                                    {
                                        peopleEditDetailsDTO.PersonEditHelperDTO = ComparePersonHelper(personHelperDataInDB, peopleEditDetailsDTO.PersonEditHelperDTO, peopleEditDetailsDTO.AgencyID);
                                    }
                                    // ======================================================================
                                    var personHelpersToBeRemoved = personHelperDataInDB.Where(x => !peopleEditDetailsDTO.PersonEditHelperDTO.Select(y => y.PersonHelperID).ToList().Contains(x.PersonHelperID)).ToList();
                                    foreach (PersonHelper helperDTO in personHelpersToBeRemoved)
                                    {
                                        helperDTO.IsRemoved = true;
                                        helperDTO.UpdateDate = DateTime.UtcNow;
                                        helperDTO.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;
                                        //personHelperEditList.Add(helperDTO);
                                    }
                                    foreach (PersonEditHelperDTO personhelper in peopleEditDetailsDTO.PersonEditHelperDTO)
                                    {
                                        var personHelperInDB = personHelperDataInDB.Where(x => x.PersonHelperID == personhelper.PersonHelperID).FirstOrDefault();
                                        if(personHelperInDB != null)
                                        {
                                            personHelperInDB.UpdateDate = DateTime.UtcNow;
                                            personHelperInDB.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;
                                            personHelperInDB.StartDate = personhelper.StartDate;
                                            personHelperInDB.EndDate = personhelper.EndDate.HasValue ? personhelper.EndDate.Value.Date : personhelper.EndDate;
                                            personHelperInDB.HelperID = personhelper.HelperID;
                                            personHelperInDB.IsLead = personhelper.IsLead;
                                            personHelperInDB.IsRemoved = personhelper.IsRemoved;
                                            personHelperInDB.IsCurrent = personhelper.EndDate == null || personhelper.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;
                                            personHelperInDB.CollaborationID = personhelper.CollaborationID == 0 ? null : personhelper.CollaborationID; ;
                                            //personHelperEditList.Add(personHelperInDB);
                                        }
                                        else
                                        {
                                            PersonHelperDTO personHelperDTO = EditPersonHelper(personhelper, peopleEditDetailsDTO);
                                            personHelperAddList.Add(personHelperDTO);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (PersonHelper helperDTO in personHelperDataInDB)
                                    {
                                        helperDTO.IsRemoved = true;
                                        helperDTO.UpdateDate = DateTime.UtcNow;
                                        helperDTO.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;
                                    }
                                }
                                if (personHelperAddList.Count > 0)
                                {
                                    this.personHelperRepository.AddBulkPersonHelper(personHelperAddList);
                                }
                                if (personHelperDataInDB.Count > 0)
                                {
                                    this.personHelperRepository.UpdateBulkPersonHelper(personHelperDataInDB);
                                }
                                #endregion

                                #region PersonIdentification

                                List<PersonIdentificationDTO> personIdentificationAddList = new List<PersonIdentificationDTO>();
                                //List<PersonIdentification> personIdentificationEditList = new List<PersonIdentification>();

                                if (peopleEditDetailsDTO.PersonEditIdentificationDTO.Count > 0)
                                {
                                    var removableIdentification = PersonIdentificationDataInDB.Where(x => !peopleEditDetailsDTO.PersonEditIdentificationDTO.Select(y => y.PersonIdentificationID).ToList().Contains(x.PersonIdentificationID)).ToList();
                                    foreach (PersonIdentification Identity in removableIdentification)
                                    {
                                        Identity.IsRemoved = true;
                                        Identity.UpdateDate = DateTime.UtcNow;
                                        Identity.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;
                                        //personIdentificationEditList.Add(Identity);
                                    }
                                    foreach (PersonEditIdentificationDTO PersonIdentity in peopleEditDetailsDTO.PersonEditIdentificationDTO)
                                    {
                                        var personIdentificationInDB = PersonIdentificationDataInDB.Where(x => x.PersonIdentificationID == PersonIdentity.PersonIdentificationID).FirstOrDefault();
                                        if(personIdentificationInDB != null)
                                        {
                                            personIdentificationInDB.UpdateDate = DateTime.UtcNow;
                                            personIdentificationInDB.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;
                                            if (PersonIdentity.IdentificationTypeID != 0)
                                            {
                                                personIdentificationInDB.IdentificationTypeID = PersonIdentity.IdentificationTypeID;
                                            }
                                            personIdentificationInDB.IdentificationNumber = PersonIdentity.IdentificationNumber;
                                            //personIdentificationEditList.Add(PersonIdentificationInDB);
                                        }
                                        else
                                        {
                                            PersonIdentificationDTO personIdentificationDTO = EditPersonIdentification(PersonIdentity, peopleEditDetailsDTO);
                                            if (personIdentificationDTO != null)
                                            {
                                                personIdentificationAddList.Add(personIdentificationDTO);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (PersonIdentification Identity in PersonIdentificationDataInDB)
                                    {
                                        Identity.IsRemoved = true;
                                        Identity.UpdateDate = DateTime.UtcNow;
                                        Identity.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;
                                        //personIdentificationEditList.Add(Identity);
                                    }
                                }
                                if (PersonIdentificationDataInDB.Count > 0)
                                {
                                    this.personIdentificationRepository.UpdateBulkPersonIdentifications(PersonIdentificationDataInDB);
                                }
                                if (personIdentificationAddList.Count > 0)
                                {
                                    this.personIdentificationRepository.AddBulkPersonIdentificationType(personIdentificationAddList);
                                }
                                #endregion

                                #region PersonRaceEthnicity
                                List<PersonRaceEthnicityDTO> personRaceEthnicityDTOAddDTOList = new List<PersonRaceEthnicityDTO>();
                                if (peopleEditDetailsDTO.PersonEditRaceEthnicityDTO.Count > 0)
                                {
                                    if (CallingType == PCISEnum.CallingType.EHR)
                                    {
                                        peopleEditDetailsDTO.PersonEditRaceEthnicityDTO = ComparePersonRaceEthnicity(personRaceEthnicityDataInDB, peopleEditDetailsDTO.PersonEditRaceEthnicityDTO, peopleEditDetailsDTO.AgencyID);
                                    }
                                    foreach (PersonRaceEthnicity PersonRaceEthnicity in personRaceEthnicityDataInDB.Where(x => !peopleEditDetailsDTO.PersonEditRaceEthnicityDTO.Select(y => y.PersonRaceEthnicityID).ToList().Contains(x.PersonRaceEthnicityID)).ToList())
                                    {
                                        PersonRaceEthnicity.IsRemoved = true;
                                    }
                                    foreach (PersonEditRaceEthnicityDTO RaceEthnicity in peopleEditDetailsDTO.PersonEditRaceEthnicityDTO)
                                    {
                                        var personRaceEthnicityInDB = personRaceEthnicityDataInDB.Where(x => x.PersonRaceEthnicityID == RaceEthnicity.PersonRaceEthnicityID).FirstOrDefault();
                                        if(personRaceEthnicityInDB != null)
                                        {
                                            personRaceEthnicityInDB.RaceEthnicityID = RaceEthnicity.RaceEthnicityID;
                                        }
                                        else
                                        {
                                            PersonRaceEthnicityDTO personRaceEthnicityDTOs = EditRaceEthnicityDTO(RaceEthnicity, peopleEditDetailsDTO);
                                            personRaceEthnicityDTOAddDTOList.Add(personRaceEthnicityDTOs);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (PersonRaceEthnicity PersonRaceEthnicityDTO in personRaceEthnicityDataInDB)
                                    {
                                        PersonRaceEthnicityDTO.IsRemoved = true;
                                    }
                                }
                                if (personRaceEthnicityDTOAddDTOList.Count > 0)
                                {
                                    this.personRaceEthnicityRepository.AddBulkRaceEthnicity(personRaceEthnicityDTOAddDTOList);
                                }
                                if (personRaceEthnicityDataInDB.Count > 0)
                                {
                                    this.personRaceEthnicityRepository.UpdateBulkPersonRaceEthnicity(personRaceEthnicityDataInDB);
                                }
                                #endregion
                                resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                                resultDTO.IndexId = peopleEditDetailsDTO.PersonIndex;
                                resultDTO.ResultId = person.PersonID;
                                return resultDTO;
                            }

                        }

                    }
                }
                if (resultDTO.ResponseStatusCode != PCISEnum.StatusCodes.UpdationSuccess)
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }
                return resultDTO;

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Exception occurred getting case note types. {ex.Message}");
                throw;
            }
        }

        private void RemovePersonQuestionairesAndSchedules(List<PersonCollaboration> personCollaborationsToBeRemoved, PeopleEditDetailsDTO peopleEditDetailsDTO)
        {
            try
            {

                List<PersonQuestionnaire> personQuestionnairesEditList = new List<PersonQuestionnaire>();
                //Find the collaborationIds and its questionnairesIds to be removed.
                var collaborationIdsTobeRemoved = personCollaborationsToBeRemoved?.Select(x => x.CollaborationID)?.Where(x => x > 0).ToList();
                if (collaborationIdsTobeRemoved.Count > 0)
                {
                    //Fetch all Questionnaires under all collaboration Ids
                    var collaborationQuestionnairesToBeRemoved = this.collaborationQuestionnaireRepository.GetCollaborationQuestionnaireData(collaborationIdsTobeRemoved);
                    var questionnairesToBeRemoved = collaborationQuestionnairesToBeRemoved.Select(x => x.QuestionnaireID).ToList();
                    //Fetch all personQuestionnaires to be removed.
                    var personQuestionaniresDataToBeRemoved = this.PersonQuestionnaireRepository.GetPersonQuestionnaireWithNoAssessmentById(questionnairesToBeRemoved, peopleEditDetailsDTO.PersonID);
                    if (personQuestionaniresDataToBeRemoved.Count > 0)
                    {
                        personQuestionaniresDataToBeRemoved = personQuestionaniresDataToBeRemoved.Where(x => collaborationIdsTobeRemoved.Contains(x.CollaborationID ?? 0)).ToList();
                        //Mark the PersonQuestionnaires to be removed.
                        foreach (PersonQuestionnaire PersonQuestionnaireData in personQuestionaniresDataToBeRemoved)
                        {
                            PersonQuestionnaireData.IsRemoved = true;
                            PersonQuestionnaireData.UpdateDate = DateTime.UtcNow;
                            PersonQuestionnaireData.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;
                            personQuestionnairesEditList.Add(PersonQuestionnaireData);
                        }
                        //Fetch all schedules associated with personQuestionnaireIds
                        var personQuestionnaireIds = personQuestionaniresDataToBeRemoved.Select(x => x.PersonQuestionnaireID).ToList();
                        var personQuestionnaireScheduleToRemove = personQuestionnaireScheduleRepository.GetPersonQuestionnaireSchedule(personQuestionnaireIds);
                        //Mark the PersonQuestionnaireSchedules to be removed.
                        personQuestionnaireScheduleToRemove?.ForEach(x => x.IsRemoved = true);
                        personQuestionnaireScheduleService.UpdateBulkPersonQuestionnaireSchedule(personQuestionnaireScheduleToRemove);
                        if (personQuestionnairesEditList.Count > 0)
                        {
                            var result = this.personQuestionnaireRepository.UpdateBulkPersonQuestionnaires(personQuestionnairesEditList);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<PersonEditRaceEthnicityDTO> ComparePersonRaceEthnicity(IReadOnlyList<PersonRaceEthnicity> personRaceEthnicityInDB, List<PersonEditRaceEthnicityDTO> personEditRaceEthnicityDTO, long agencyID)
        {
            try
            {
                if (personEditRaceEthnicityDTO.Count > 0)
                {
                    var personRaceListByHelperIDList = personRaceEthnicityInDB.Where(x => personEditRaceEthnicityDTO.Select(x => x.RaceEthnicityID).ToList().Contains(x.RaceEthnicityID)).ToList();
                    foreach (var personHelper in personEditRaceEthnicityDTO)
                    {
                        var race = personRaceListByHelperIDList.Where(x => x.RaceEthnicityID == personHelper.RaceEthnicityID).ToList();
                        if (race.Count > 0)
                        {
                            personHelper.PersonRaceEthnicityID = race[0].PersonRaceEthnicityID;
                        }
                    }
                }
                return personEditRaceEthnicityDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// ComparePersonCollaboration. 
        /// This function is to avoid new record creation during edit of EHR person records.
        /// If the collaborationId already exists, then just edit the same record.
        /// </summary>
        /// <param name="personCollaborationInDB"></param>
        /// <param name="personEditCollaborationDTO"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        private List<PersonEditCollaborationDTO> ComparePersonCollaboration(IReadOnlyList<PersonCollaboration> personCollaborationInDB, List<PersonEditCollaborationDTO> personEditCollaborationDTO, long agencyID)
        {
            try
            {
                if (personEditCollaborationDTO.Count > 0)
                {
                    var personCollaborationList = personCollaborationInDB.Where(x => personEditCollaborationDTO.Select(x => x.CollaborationID).ToList().Contains(x.CollaborationID)).ToList();
                    foreach (var personCollaboration in personEditCollaborationDTO)
                    {
                        var collaboration = personCollaborationList.Where(x => x.CollaborationID == personCollaboration.CollaborationID).ToList();
                        if (collaboration.Count > 0)
                        {
                            personCollaboration.PersonCollaborationID = collaboration[0].PersonCollaborationID;
                        }
                    }
                }
                return personEditCollaborationDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<PersonEditHelperDTO> ComparePersonHelper(IReadOnlyList<PersonHelper> personHelperInDB, List<PersonEditHelperDTO> personEditHelperDTO, long agencyID)
        {

            try
            {
                if (personEditHelperDTO.Count > 0)
                {
                    var personHelperDataListByHelperIDList = personHelperInDB.Where(x => personEditHelperDTO.Select(x => x.HelperID).ToList().Contains(x.HelperID)).ToList();
                    foreach (var personHelper in personEditHelperDTO)
                    {
                        var helper = personHelperDataListByHelperIDList.Where(x => x.HelperID == personHelper.HelperID).ToList();
                        if (helper.Count > 0)
                        {
                            personHelper.PersonHelperID = helper[0].PersonHelperID;
                        }
                    }
                }
                return personEditHelperDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<PersonEditSupportDTO> ComparePersonSupport(IReadOnlyList<PersonSupportDTO> personSupporDTO, List<PersonEditSupportDTO> personEditSupportDTO, long agencyID)
        {
            try
            {
                if (personEditSupportDTO.Count > 0 && personSupporDTO.Count > 0)
                {
                    var personSupportDataListByUniversalIDList = personSupporDTO.Where(x => personEditSupportDTO.Select(x => x.UniversalID).ToList().Contains(x.UniversalID)).ToList();
                    foreach (var personSupport in personEditSupportDTO)
                    {
                        var support = personSupportDataListByUniversalIDList.Where(x => x.UniversalID == personSupport.UniversalID).ToList();
                        if (support.Count > 0)
                        {
                            personSupport.TextPermission = support[0].TextPermission;
                            personSupport.EmailPermission = support[0].TextPermission;
                            personSupport.PersonSupportID = support[0].PersonSupportID;
                        }
                    }
                }
                return personEditSupportDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetRiskNotificationList.
        /// </summary>
        /// <param name="personIndex">questionnaireID.</param>
        /// <returns>RiskNotificationDetailsResponseDTO</returns>
        public RiskNotificationDetailsResponseDTO GetRiskNotificationList(Guid personIndex)
        {
            try
            {
                RiskNotificationDetailsResponseDTO riskNotificationDetailsResponseDTO = new RiskNotificationDetailsResponseDTO();
                var peopleData = this.personRepository.GetPerson(personIndex);
                if (peopleData.PersonID != 0)
                {
                    var riskNotificationList = this.personRepository.GetRiskNotificationList(peopleData.PersonID);
                    if (riskNotificationList.Count > 0)
                    {
                        riskNotificationDetailsResponseDTO.RiskNotifications = riskNotificationList;
                    }
                    var reminderNotificationList = this.personRepository.GetReminderNotificationList(peopleData.PersonID);
                    if (reminderNotificationList.Count > 0)
                    {
                        riskNotificationDetailsResponseDTO.ReminderNotifications = reminderNotificationList;
                    }
                    riskNotificationDetailsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    riskNotificationDetailsResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                }
                return riskNotificationDetailsResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPastNotificationList.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>PastNotificationDetailsResponseDTO.</returns>
        public PastNotificationDetailsResponseDTO GetPastNotificationList(Guid personIndex)
        {
            try
            {
                PastNotificationDetailsResponseDTO pastNotificationDetailsResponseDTO = new PastNotificationDetailsResponseDTO();
                var peopleData = this.personRepository.GetPerson(personIndex);
                if (peopleData.PersonID != 0)
                {
                    var pastNotificationList = this.personRepository.GetPastNotificationList(peopleData.PersonID);
                    if (pastNotificationList.Count > 0)
                    {
                        pastNotificationDetailsResponseDTO.PastNotifications = pastNotificationList;
                    }
                    pastNotificationDetailsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    pastNotificationDetailsResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                }
                return pastNotificationDetailsResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllQuestionnaireForPerson.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>QuestionnairesForPersonDTO.</returns>
        public QuestionnairesForPersonDTO GetAllQuestionnaireForPerson(long agencyID, Guid personIndex, int pageNumber, int pageSize)
        {
            try
            {
                QuestionnairesForPersonDTO questionnairesForPersonDTO = new QuestionnairesForPersonDTO();
                if (pageNumber <= 0)
                {
                    questionnairesForPersonDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PageNumber));
                    questionnairesForPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return questionnairesForPersonDTO;
                }
                else if (pageSize <= 0)
                {
                    questionnairesForPersonDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PageSize));
                    questionnairesForPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return questionnairesForPersonDTO;
                }
                else if (personIndex != null)
                {
                    var response = this.personRepository.GetPerson(personIndex);
                    if (response.PersonID != 0)
                    {
                        if (this.personRepository.IsSharedPerson(response.PersonID, agencyID))
                        {
                            questionnairesForPersonDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                            questionnairesForPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                            return questionnairesForPersonDTO;
                        }
                        if (agencyID > 0)
                        {
                            var questionnaireList = this.questionnaireRepository.GetAllQuestionnairesWithAgency(agencyID);
                            if (questionnaireList.Count > 0)
                            {
                                var personQuestionnaireList = this.personQuestionnaireRepository.GetPersonQuestionnaireList(response.PersonID).Result;
                                if (personQuestionnaireList.Count > 0)
                                {
                                    var questionnaireData = questionnaireList.Where(x => !personQuestionnaireList.Select(y => y.QuestionnaireID).ToList().Contains(x.QuestionnaireID)).ToList();
                                    if (questionnaireData.Count > 0)
                                    {
                                        questionnairesForPersonDTO.TotalCount = questionnaireData.Count;
                                        foreach (var questionnaire in questionnaireData)
                                        {
                                            questionnaire.PersonID = response.PersonID;
                                        }
                                        questionnairesForPersonDTO.PersonQuestionnaireDataDTO = questionnaireData.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                                    }
                                }
                                else
                                {
                                    questionnairesForPersonDTO.PersonQuestionnaireDataDTO = questionnaireList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                                    questionnairesForPersonDTO.TotalCount = questionnaireList.Count;
                                }
                                questionnairesForPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                                questionnairesForPersonDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                            }
                            else
                            {
                                questionnairesForPersonDTO.PersonQuestionnaireDataDTO = null;
                                questionnairesForPersonDTO.TotalCount = 0;
                                questionnairesForPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                                questionnairesForPersonDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                            }
                        }
                    }
                }
                return questionnairesForPersonDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateNotificationStatus.
        /// </summary>
        /// <param name="notificationLogID">notificationLogID.</param>
        /// <param name="status">status.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateNotificationStatus(int notificationLogID, string status, long loggedInAgencyID)
        {
            try
            {
                CRUDResponseDTO responseDTO = new CRUDResponseDTO();
                if (notificationLogID > 0 && status != null)
                {
                    var notificationLogDetails = this.notificationLogRepository.GetNotificationLogByID(notificationLogID);
                    if (notificationLogDetails != null)
                    {
                        int statusID = this.notifiationResolutionStatusRepository.GetNotificationStatus(status).NotificationResolutionStatusID;
                        if (statusID > 0)
                        {
                            if (notificationLogDetails.NotificationTypeName == PCISEnum.NotificationType.Reminder)
                            {
                                var reminderNotificationList = this.notificationLogRepository.GetReminderNotifications(notificationLogDetails.FKeyValue.Value);
                                List<NotificationLog> listNotificationLog = new List<NotificationLog>();
                                List<NotificationResolutionHistory> listNotificationResolutionHistory = new List<NotificationResolutionHistory>();
                                foreach (var item in reminderNotificationList)
                                {
                                    NotificationLogDTO notificationLogDTO = new NotificationLogDTO();
                                    notificationLogDTO.NotificationLogID = item.NotificationLogID;
                                    notificationLogDTO.PersonID = item.PersonID;
                                    notificationLogDTO.NotificationResolutionStatusID = statusID;
                                    notificationLogDTO.NotificationDate = item.NotificationDate;
                                    notificationLogDTO.NotificationData = item.NotificationData;
                                    notificationLogDTO.NotificationTypeID = item.NotificationTypeID;
                                    notificationLogDTO.Details = item.Details;
                                    notificationLogDTO.AssessmentNoteID = item.AssessmentNoteID;
                                    notificationLogDTO.QuestionnaireID = item.QuestionnaireID;
                                    notificationLogDTO.AssessmentID = item.AssessmentID;
                                    notificationLogDTO.StatusDate = DateTime.UtcNow;
                                    notificationLogDTO.FKeyValue = item.FKeyValue;
                                    notificationLogDTO.IsRemoved = false;
                                    notificationLogDTO.UpdateDate = DateTime.UtcNow;
                                    notificationLogDTO.UpdateUserID = item.UpdateUserID;
                                    notificationLogDTO.HelperName = item.HelperName;

                                    NotificationLog notificationLog = new NotificationLog();
                                    this.mapper.Map<NotificationLogDTO, NotificationLog>(notificationLogDTO, notificationLog);
                                    listNotificationLog.Add(notificationLog);


                                    NotificationResolutionHistoryDTO notificationResolutionHistoryDTO = new NotificationResolutionHistoryDTO();

                                    notificationResolutionHistoryDTO.NotificationLogID = item.NotificationLogID;
                                    notificationResolutionHistoryDTO.StatusFrom = item.NotificationResolutionStatusID;
                                    notificationResolutionHistoryDTO.StatusTo = item.NotificationResolutionStatusID;
                                    notificationResolutionHistoryDTO.IsRemoved = false;
                                    notificationResolutionHistoryDTO.UpdateDate = DateTime.UtcNow;
                                    notificationResolutionHistoryDTO.UpdateUserID = item.UpdateUserID;

                                    NotificationResolutionHistory notificationResolutionHistory = new NotificationResolutionHistory();
                                    this.mapper.Map<NotificationResolutionHistoryDTO, NotificationResolutionHistory>(notificationResolutionHistoryDTO, notificationResolutionHistory);
                                    listNotificationResolutionHistory.Add(notificationResolutionHistory);
                                }

                                this.notificationLogRepository.UpdateBulkNotificationLog(listNotificationLog);
                                this.notifiationResolutionHistoryRepository.AddBulkNotificationResolutionHistory(listNotificationResolutionHistory);

                                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                            }
                            else
                            {
                                var notificationLogDTO = new NotificationLogDTO()
                                {
                                    NotificationLogID = notificationLogDetails.NotificationLogID,
                                    PersonID = notificationLogDetails.PersonID,
                                    NotificationResolutionStatusID = statusID,
                                    NotificationDate = notificationLogDetails.NotificationDate,
                                    NotificationData = notificationLogDetails.NotificationData,
                                    NotificationTypeID = notificationLogDetails.NotificationTypeID,
                                    Details = notificationLogDetails.Details,
                                    AssessmentNoteID = notificationLogDetails.AssessmentNoteID,
                                    QuestionnaireID = notificationLogDetails.QuestionnaireID,
                                    AssessmentID = notificationLogDetails.AssessmentID,
                                    StatusDate = DateTime.UtcNow,
                                    FKeyValue = notificationLogDetails.FKeyValue,
                                    IsRemoved = false,
                                    UpdateDate = DateTime.UtcNow,
                                    UpdateUserID = notificationLogDetails.UpdateUserID,
                                    HelperName = notificationLogDetails.HelperName
                                };

                                NotificationLog notificationLog = new NotificationLog();
                                this.mapper.Map<NotificationLogDTO, NotificationLog>(notificationLogDTO, notificationLog);
                                var notificationLogData = this.notificationLogRepository.UpdateNotificationLog(notificationLog);
                                if (notificationLogData != null)
                                {
                                    var notificationResolutionHistoryDTO = new NotificationResolutionHistoryDTO()
                                    {
                                        NotificationLogID = notificationLogData.NotificationLogID,
                                        StatusFrom = notificationLogDetails.NotificationResolutionStatusID,
                                        StatusTo = notificationLogData.NotificationResolutionStatusID,
                                        IsRemoved = false,
                                        UpdateDate = DateTime.UtcNow,
                                        UpdateUserID = notificationLogData.UpdateUserID
                                    };
                                    NotificationResolutionHistory notificationResolutionHistory = new NotificationResolutionHistory();
                                    this.mapper.Map<NotificationResolutionHistoryDTO, NotificationResolutionHistory>(notificationResolutionHistoryDTO, notificationResolutionHistory);
                                    int notificationResolutionHistoryID = this.notifiationResolutionHistoryRepository.AddNotificationResolutionHistory(notificationResolutionHistory).NotificationResolutionHistoryID;
                                    if (notificationResolutionHistoryID > 0)
                                    {
                                        responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                        responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                                    }
                                    else
                                    {
                                        responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                        responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                                    }
                                }
                                else
                                {
                                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                    responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                                }
                            }
                        }
                    }
                }
                else
                {
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }
                return responseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllNotes.
        /// </summary>
        /// <param name="notificationLogID">notificationLogID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>PastNotesResponseDTO.</returns>
        public PastNotesResponseDTO GetAllPastNotes(int notificationLogID, int pageNumber, int pageSize)
        {
            try
            {
                PastNotesResponseDTO pastNotesResponseDTO = new PastNotesResponseDTO();
                if (pageNumber <= 0)
                {
                    pastNotesResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PageNumber));
                    pastNotesResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return pastNotesResponseDTO;
                }
                else if (pageSize <= 0)
                {
                    pastNotesResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PageSize));
                    pastNotesResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return pastNotesResponseDTO;
                }
                else if (notificationLogID > 0)
                {
                    var notes = this.personRepository.GetAllPastNotes(notificationLogID, pageNumber, pageSize);

                    pastNotesResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    pastNotesResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);

                    if (notes.Count > 0)
                    {
                        foreach (var note in notes)
                        {
                            if (note.HelperTitle == null)
                            {
                                note.HelperTitle = PCISEnum.Roles.SuperAdmin;
                            }
                        }
                        pastNotesResponseDTO.NotificationNotes = notes;
                    }

                    var notification = this.notificationLogRepository.GetNotificationLogByID(notificationLogID);

                    if (notification?.NotificationTypeName == PCISEnum.NotificationType.Alert)
                    {
                        var questionnaireRiskItemDetails = this.questionnaireNotifyRiskRuleConditionRepository.GetRiskItemDetails(notificationLogID);
                        if (questionnaireRiskItemDetails.Count > 0)
                        {
                            pastNotesResponseDTO.QuestionnaireRiskItemDetails = questionnaireRiskItemDetails;
                        }
                    }
                    else if (notification?.NotificationTypeName == PCISEnum.NotificationType.Reminder && notification?.FKeyValue.HasValue == true)
                    {
                        var reminderNotificationList = this.notificationLogRepository.GetReminderNotifications(notification.FKeyValue.Value);
                        var notificationResolutionStatus = this.notifiationResolutionStatusRepository.GetNotificationStatusById(notification.NotificationResolutionStatusID);
                        reminderNotificationList.ForEach(x => x.Status = notificationResolutionStatus.Name);
                        if (reminderNotificationList.Count > 0)
                        {
                            pastNotesResponseDTO.ReminderList = reminderNotificationList.OrderByDescending(x => x.NotificationDate).ToList();
                        }
                    }
                }
                else
                {
                    pastNotesResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    pastNotesResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                }
                return pastNotesResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddNotificationNote.
        /// </summary>
        /// <param name="notificationNoteDataDTO">notificationNoteDataDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AddNotificationNote(NotificationNoteDataDTO notificationNoteDataDTO, int userID)
        {
            try
            {
                CRUDResponseDTO cRUDResponseDTO = new CRUDResponseDTO();
                if (notificationNoteDataDTO.NoteID > 0)
                {
                    var notes = this.noteRepository.GetNotes(notificationNoteDataDTO.NoteID).Result;
                    if (notes != null)
                    {
                        Note note = CreateNote(notificationNoteDataDTO, userID, PCISEnum.Constants.Edit);
                        if (note != null)
                        {
                            var updatedNotes = this.noteRepository.UpdateNote(note);
                            if (updatedNotes != null)
                            {
                                cRUDResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                cRUDResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                            }
                            else
                            {
                                cRUDResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                cRUDResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                            }
                        }
                    }
                }
                else if (notificationNoteDataDTO != null && notificationNoteDataDTO.NoteText != null && notificationNoteDataDTO.NoteID == 0)
                {
                    Note note = CreateNote(notificationNoteDataDTO, userID, PCISEnum.Constants.Add);
                    int noteID = this.noteRepository.AddNote(note).NoteID;
                    if (noteID > 0)
                    {
                        var notificationResolutionNoteDTO = new NotificationResolutionNoteDTO
                        {
                            NotificationLogID = notificationNoteDataDTO.NotificationLogID,
                            Note_NoteID = noteID,
                            NotificationResolutionHistoryID = null
                        };
                        NotificationResolutionNote notificationResolutionNote = new NotificationResolutionNote();
                        this.mapper.Map<NotificationResolutionNoteDTO, NotificationResolutionNote>(notificationResolutionNoteDTO, notificationResolutionNote);
                        int notificationResolutionNoteID = this.notifiationResolutionNoteRepository.AddNotificationResolutionNote(notificationResolutionNote).NotificationResolutionNoteID;
                        if (notificationResolutionNoteID > 0)
                        {
                            cRUDResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            cRUDResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }
                        else
                        {
                            cRUDResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                            cRUDResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                        }
                    }
                    else
                    {
                        cRUDResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                        cRUDResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                    }
                }
                else
                {
                    cRUDResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    cRUDResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                }
                return cRUDResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Note CreateNote(NotificationNoteDataDTO notificationNoteDataDTO, int userID, string from)
        {
            Note note = new Note();
            var noteDTO = new NoteDTO();
            if (from == PCISEnum.Constants.Edit)
            {
                if (notificationNoteDataDTO.NoteID > 0)
                {
                    noteDTO.NoteID = notificationNoteDataDTO.NoteID;
                }
            }
            if (notificationNoteDataDTO != null && notificationNoteDataDTO.NoteText != null)
            {
                noteDTO.NoteText = notificationNoteDataDTO.NoteText;
                noteDTO.IsConfidential = notificationNoteDataDTO.IsConfidential;
                noteDTO.IsRemoved = false;
                noteDTO.UpdateDate = DateTime.UtcNow;
                noteDTO.UpdateUserID = userID;
                this.mapper.Map<NoteDTO, Note>(noteDTO, note);
            }
            return note;
        }

        /// <summary>
        /// GetPeopleDetails.
        /// </summary>
        /// <param name="peopleIndex">.</param>
        /// <returns>PeopleDetailsResponseDTO.</returns>
        public CRUDResponseDTO AddPersonQuestionaire(PersonQuestionnaireDetailsDTO personQuestionnaireDetailsDTO, long updateUserID, long loggedInAgencyID)
        {
            try
            {
                CRUDResponseDTO CRUDResponseDTO = new CRUDResponseDTO();
                if (personQuestionnaireDetailsDTO.PersonIndex != Guid.Empty)
                {
                    PeopleDTO peopleDataDTO = this.personRepository.GetPerson(personQuestionnaireDetailsDTO.PersonIndex);
                    if (peopleDataDTO.AgencyID != loggedInAgencyID)
                    {
                        CRUDResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                        CRUDResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                        return CRUDResponseDTO;
                    }
                    PersonQuestionnaireDTO PersonQuestionnaireDTO = new PersonQuestionnaireDTO
                    {
                        QuestionnaireID = personQuestionnaireDetailsDTO.QuestionnaireID,
                        PersonID = peopleDataDTO.PersonID,
                        UpdateUserID = updateUserID,
                        IsActive = true,
                        StartDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        EndDueDate = null,
                        CollaborationID = null

                    };
                    var personPersonQuestionnaireId = this.PersonQuestionnaireRepository.AddPersonQuestionnaire(PersonQuestionnaireDTO);
                    if (personPersonQuestionnaireId != 0)
                    {
                        StoreInQueue(personPersonQuestionnaireId);
                        CRUDResponseDTO.ResponseStatus = PCISEnum.StatusMessages.InsertionSuccess;
                        CRUDResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                    }


                }
                if (CRUDResponseDTO.ResponseStatus != PCISEnum.StatusMessages.InsertionSuccess)
                {
                    CRUDResponseDTO.ResponseStatus = PCISEnum.StatusMessages.insertionFailed;
                    CRUDResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                }
                return CRUDResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonHelpingCount.
        /// </summary>
        /// <param name="helperID">helperID.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="roles">roles.</param>
        /// <returns>PersonHelping Count.</returns>
        public PersonHelpingResponseDTO GetPersonHelpingCount(int? helperID, long agencyID, List<string> roles, bool isSameAsLoggedInUser)
        {
            try
            {
                PersonHelpingResponseDTO personHelpingResponseDTO = new PersonHelpingResponseDTO();

                PersonSearchDTO personSearchDTO = new PersonSearchDTO();
                personSearchDTO.isSameAsLoggedInUser = isSameAsLoggedInUser;
                personSearchDTO.helperID = helperID.HasValue ? helperID.Value : 0;
                personSearchDTO.agencyID = agencyID;
                personSearchDTO.role = this.GetRoleName(roles);

                DynamicQueryBuilderDTO queryBuilderDTO = new DynamicQueryBuilderDTO();
                queryBuilderDTO.Page = 1;
                queryBuilderDTO.PageSize = 1;
                var HelpingCount = this.personRepository.GetPersonsListByHelperIDCount(personSearchDTO, queryBuilderDTO);

                if (HelpingCount != null)
                {
                    personHelpingResponseDTO.PersonHelpingCount = HelpingCount.Item1;
                    personHelpingResponseDTO.LeadHelpingCount = HelpingCount.Item2;
                }
                else
                {
                    personHelpingResponseDTO.PersonHelpingCount = 0;
                    personHelpingResponseDTO.LeadHelpingCount = 0;
                }
                personHelpingResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                personHelpingResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return personHelpingResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllQuestionnaireWithCompletedAssessment.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>AssessedQuestionnairesForPersonDTO.</returns>
        public AssessedQuestionnairesForPersonDTO GetAllQuestionnaireWithCompletedAssessment(UserTokenDetails userTokenDetails, Guid personIndex, int pageNumber, int pageSize, long personCollaborationID, int voiceTypeID, long voiceTypeFKID)
        {
            try
            {
                AssessedQuestionnairesForPersonDTO assessedQuestionnairesForPersonDTO = new AssessedQuestionnairesForPersonDTO();
                if (pageNumber <= 0)
                {
                    assessedQuestionnairesForPersonDTO.ResponseStatus = string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PageNumber);
                    assessedQuestionnairesForPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return assessedQuestionnairesForPersonDTO;
                }
                else if (pageSize <= 0)
                {
                    assessedQuestionnairesForPersonDTO.ResponseStatus = string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PageSize);
                    assessedQuestionnairesForPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return assessedQuestionnairesForPersonDTO;
                }
                else if (personIndex != null)
                {
                    var response = this.personRepository.GetPerson(personIndex);
                    if (response.PersonID != 0 && userTokenDetails.AgencyID > 0)
                    {
                        var sharedIDs = this.personRepository.GetSharedPersonQuestionnaireDetails(response.PersonID, userTokenDetails.AgencyID);
                        var helperColbIDs = new SharedDetailsDTO();
                        if (string.IsNullOrEmpty(sharedIDs?.SharedCollaborationIDs))
                        {
                            helperColbIDs = this.personRepository.GetPersonHelperCollaborationDetails(response.PersonID, userTokenDetails.AgencyID, userTokenDetails.UserID);
                        }
                        var queryResult = this.questionnaireRepository.GetAllQuestionnaireWithCompletedAssessment(userTokenDetails.AgencyID, response.PersonID, pageNumber, pageSize, personCollaborationID, voiceTypeID, voiceTypeFKID, sharedIDs, helperColbIDs);
                        if (queryResult != null && queryResult.Item1.Count > 0)
                        {
                            assessedQuestionnairesForPersonDTO.QuestionnaireData = queryResult.Item1;
                            assessedQuestionnairesForPersonDTO.TotalCount = queryResult.Item2;
                            assessedQuestionnairesForPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                            assessedQuestionnairesForPersonDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                        }
                    }
                    else
                    {
                        assessedQuestionnairesForPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                        assessedQuestionnairesForPersonDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                    }
                }
                return assessedQuestionnairesForPersonDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GetPersonListDTO GetPersonsListByHelperID(PersonSearchDTO personSearchDTO)
        {
            try
            {
                List<QueryFieldMappingDTO> fieldMappingList = GetPersonListConfiguration();
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQuery(personSearchDTO.SearchFilter, fieldMappingList);
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                GetPersonListDTO getPersonDTO = new GetPersonListDTO();
                List<PersonDTO> response = new List<PersonDTO>();
                int totalCount = 0;
                if (queryBuilderDTO.Page <= 0)
                {
                    getPersonDTO.PersonList = null;
                    getPersonDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    getPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getPersonDTO;
                }
                else if (queryBuilderDTO.PageSize <= 0)
                {
                    getPersonDTO.PersonList = null;
                    getPersonDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    getPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return getPersonDTO;
                }
                else
                {
                    personSearchDTO.role = this.GetRoleName(personSearchDTO.userRole);
                    response = this.personRepository.GetPersonsListByHelperID(personSearchDTO, queryBuilderDTO);
                    foreach (var item in response)
                    {
                        if (item.StartDate.HasValue)
                        {
                            item.StartDate = utility.ConvertToUtcDateTime(item.StartDate.Value, offset);
                        }
                    }
                    getPersonDTO.PersonList = response;
                    if (response.Count > 0)
                    {
                        getPersonDTO.TotalCount = response[0].TotalCount;
                    }
                    else
                    {
                        getPersonDTO.TotalCount = totalCount;
                    }

                    getPersonDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    getPersonDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return getPersonDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonInitials
        /// </summary>
        /// <param name="personIndex"></param>
        /// <returns>PersonInitialsResponseDTO</returns>
        public PersonInitialsResponseDTO GetPersonInitials(Guid personIndex)
        {
            try
            {
                PersonInitialsResponseDTO personInitialsResponseDTO = new PersonInitialsResponseDTO();
                personInitialsResponseDTO.PersonInitials = this.personRepository.GetPersonInitials(personIndex);
                personInitialsResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                personInitialsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return personInitialsResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// EditAddressForPerson either edit or add new
        /// </summary>
        /// <param name="peopleEditDetailsDTO"></param>
        /// <returns></returns>
        private bool EditAddressForPerson(PeopleEditDetailsDTO peopleEditDetailsDTO)
        {
            try
            {
                peopleEditDetailsDTO.AddressID = this.personAddressRepository.GetPersonAddress(peopleEditDetailsDTO.PersonID).Result.AddressID;
                if (peopleEditDetailsDTO.AddressID != 0)
                {
                    peopleEditDetailsDTO.AddressIndex = this.addressRepository.GetAddress(peopleEditDetailsDTO.AddressID).Result.AddressIndex;
                    AddressDTO addressDTO = EditAddress(peopleEditDetailsDTO);
                    var addressResult = this.addressRepository.UpdateAddress(addressDTO);
                }
                else if (peopleEditDetailsDTO.AddressID == 0)
                {
                    AddressDTO addressDTO = CreateAddressDTOOnEdit(peopleEditDetailsDTO);
                    AddToPersonAddress(addressDTO, peopleEditDetailsDTO.PersonID);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// CreateAddressDTOOnEdit using peopleEditDetailsDTO
        /// </summary>
        /// <param name="peopleEditDetailsDTO"></param>
        /// <returns></returns>
        private AddressDTO CreateAddressDTOOnEdit(PeopleEditDetailsDTO peopleEditDetailsDTO)
        {
            try
            {
                AddressDTO addressDTO = new AddressDTO();

                if (!((string.IsNullOrEmpty(peopleEditDetailsDTO.Address1) && string.IsNullOrEmpty(peopleEditDetailsDTO.Address2)
                && string.IsNullOrEmpty(peopleEditDetailsDTO.City) && peopleEditDetailsDTO.CountryStateId == 0 && peopleEditDetailsDTO.CountryId == 0
                && string.IsNullOrEmpty(peopleEditDetailsDTO.Zip)) || peopleEditDetailsDTO.UpdateUserID == 0))
                {
                    addressDTO.CountryStateID = peopleEditDetailsDTO.CountryStateId == 0 ? null : peopleEditDetailsDTO.CountryStateId;
                    addressDTO.UpdateUserID = peopleEditDetailsDTO.UpdateUserID;
                    addressDTO.Address1 = peopleEditDetailsDTO.Address1;
                    addressDTO.Address2 = peopleEditDetailsDTO.Address2;
                    addressDTO.City = peopleEditDetailsDTO.City;
                    addressDTO.Zip = peopleEditDetailsDTO.Zip;
                    addressDTO.Zip4 = peopleEditDetailsDTO.Zip4;
                    addressDTO.IsPrimary = true;
                    addressDTO.IsRemoved = false;
                    addressDTO.CountryID = peopleEditDetailsDTO.CountryId == 0 ? null : peopleEditDetailsDTO.CountryId;
                    return addressDTO;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Add addressid and personid to personAddress
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <param name="personID"></param>
        private void AddToPersonAddress(AddressDTO addressDTO, long personID)
        {
            try
            {
                if (addressDTO != null)
                {
                    var addressID = this.addressRepository.AddAddress(addressDTO);

                    if (addressID != 0)
                    {
                        PersonAddressDTO personAddressDTO = CreatePersonAddress(personID, addressID);

                        if (personAddressDTO != null)
                        {
                            var personAddressID = this.personAddressRepository.AddPersonAddress(personAddressDTO);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<QueryFieldMappingDTO> GetPersonListConfiguration()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "name",
                fieldTable = "P",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Lead",
                fieldAlias = "lead",
                fieldTable = "pl",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "EnrollDate",
                fieldAlias = "startDate",
                fieldTable = "pm",
                fieldType = "string",
                orginalType = "date"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "EndDate",
                fieldAlias = "endDate",
                fieldTable = "pm",
                fieldType = "string",
                orginalType = "date"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Name",
                fieldAlias = "collaboration",
                fieldTable = "c",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(Days,0)",
                fieldAlias = "days",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(pa.Assessed,0)",
                fieldAlias = "assessed",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(NeedsEver,0)",
                fieldAlias = "needsEver",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(NeedsAddressing,0)",
                fieldAlias = "needsAddressing",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(StrengthEver,0)",
                fieldAlias = "strengthEver",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(StrengthBuilding,0)",
                fieldAlias = "strengthBuilding",
                fieldTable = "",
                fieldType = "number"
            });
            return fieldMappingList;
        }

        public PersonCollaborationResponseDTO GetPeopleCollaborationList(Guid personIndex, UserTokenDetails userTokenDetails, int questionnaireID)
        {
            try
            {
                PersonCollaborationResponseDTO personCollaborationResponseDTO = new PersonCollaborationResponseDTO();
                var response = this.personRepository.GetPerson(personIndex);
                if (response.PersonID != 0)
                {
                    if (!this.personRepository.IsValidPersonInAgency(response.PersonID, response.AgencyID, userTokenDetails.AgencyID))
                    {
                        personCollaborationResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                        personCollaborationResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                        return personCollaborationResponseDTO;
                    }
                    List<PeopleCollaborationDTO> peopleCollaborationDTO = new List<PeopleCollaborationDTO>();
                    personCollaborationResponseDTO.PersonCollaborations = this.personRepository.GetPeopleCollaborationList(response.PersonID, userTokenDetails.AgencyID, questionnaireID, userTokenDetails.UserID);
                    foreach (PeopleCollaborationDTO personCollaboration in personCollaborationResponseDTO.PersonCollaborations)
                    {
                        personCollaboration.CollaborationEndDate = personCollaboration.CollaborationEndDate == null ? DateTime.Today.AddDays(personCollaboration.WindowCloseOffsetDays) : personCollaboration.CollaborationEndDate;
                    }
                    personCollaborationResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                    personCollaborationResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                }
                return personCollaborationResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PersonCollaborationResponseDTO GetPeopleCollaborationListForReport(Guid personIndex, long personQuestionaireID, int voiceTypeID, UserTokenDetails userTokenDetails)
        {
            try
            {
                PersonCollaborationResponseDTO personCollaborationResponseDTO = new PersonCollaborationResponseDTO();
                var response = this.personRepository.GetPerson(personIndex);
                if (response.PersonID != 0)
                {
                    List<PeopleCollaborationDTO> peopleCollaborationDTO = new List<PeopleCollaborationDTO>();
                    personCollaborationResponseDTO.PersonCollaborations = this.personRepository.GetPeopleCollaborationListForReport(response.PersonID, personQuestionaireID, voiceTypeID, userTokenDetails);
                    personCollaborationResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                    personCollaborationResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                }
                return personCollaborationResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonDetails.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>PersonDetailsResponseDTO.</returns>
        public PersonDetailsResponseDTO GetPersonDetails(Guid personIndex, long loggedInAgencyID)
        {
            try
            {
                PersonDetailsResponseDTO personInitialsResponseDTO = new PersonDetailsResponseDTO();
                if (!this.personRepository.IsValidPersonInAgency(personIndex, loggedInAgencyID))
                {
                    personInitialsResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                    personInitialsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    return personInitialsResponseDTO;
                }
                personInitialsResponseDTO.PersonDetails = this.personRepository.GetPersonDetails(personIndex);
                personInitialsResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                personInitialsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return personInitialsResponseDTO;
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
                fieldName = "P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '')",
                fieldAlias = "personName",
                fieldTable = "",
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
                fieldAlias = "notificationtype",
                fieldTable = "",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Details",
                fieldAlias = "details",
                fieldTable = "",
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
                fieldName = "NotificationData",
                fieldAlias = "notificationData",
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
                fieldName = "PersonIndex",
                fieldAlias = "personIndex",
                fieldTable = "P",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Status",
                fieldAlias = "status",
                fieldTable = "NR",
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
                fieldTable = "",
                fieldType = "string"
            });
            return fieldMappingList;
        }

        /// <summary>
        /// GetPastNotificationList.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="notificationLogSearchDTO">notificationLogSearchDTO.</param>
        /// <returns>NotificationLogResponseDTO.</returns>
        public NotificationLogResponseDTO GetPastNotifications(Guid personIndex, NotificationLogSearchDTO notificationLogSearchDTO)
        {
            try
            {
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                List<QueryFieldMappingDTO> fieldMappingList = GetNotificationLogConfiguration(offset);
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQuery(notificationLogSearchDTO.SearchFilter, fieldMappingList);
                NotificationLogResponseDTO notificationLogDTO = new NotificationLogResponseDTO();
                List<NotificationLogDTO> response = new List<NotificationLogDTO>();
                int totalCount = 0;
                var peopleData = this.personRepository.GetPerson(personIndex);
                if (peopleData.PersonID != 0)
                {
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
                        if (!this.personRepository.IsValidPersonInAgency(peopleData.PersonID, peopleData.AgencyID, notificationLogSearchDTO.agencyID))
                        {
                            notificationLogDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                            notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                            return notificationLogDTO;
                        }
                        notificationLogSearchDTO.role = this.GetRoleName(notificationLogSearchDTO.userRole);

                        var sharedIDs = personRepository.GetSharedPersonQuestionnaireDetails(peopleData.PersonID, notificationLogSearchDTO.agencyID);
                        SharedDetailsDTO helperColbIDs = new SharedDetailsDTO();
                        if (string.IsNullOrEmpty(sharedIDs?.SharedCollaborationIDs))
                        {
                            helperColbIDs = this.personRepository.GetPersonHelperCollaborationDetails(peopleData.PersonID, notificationLogSearchDTO.agencyID, notificationLogSearchDTO.UserID);
                        }
                        var queryResponse = this.notificationLogRepository.GetPastNotifications(notificationLogSearchDTO, queryBuilderDTO, peopleData.PersonID, sharedIDs, peopleData.AgencyID, helperColbIDs);
                        response = queryResponse.Item1;
                        totalCount = queryResponse.Item2;
                        notificationLogDTO.NotificationLog = response;
                        notificationLogDTO.TotalCount = totalCount;
                        notificationLogDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                        notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return notificationLogDTO;
                    }
                }
                return notificationLogDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPresentNotifications.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="notificationLogSearchDTO">notificationLogSearchDTO.</param>
        /// <returns>NotificationLogResponseDTO.</returns>
        public NotificationLogResponseDTO GetPresentNotifications(Guid personIndex, NotificationLogSearchDTO notificationLogSearchDTO)
        {
            try
            {
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                List<QueryFieldMappingDTO> fieldMappingList = GetNotificationLogConfiguration(offset);
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQuery(notificationLogSearchDTO.SearchFilter, fieldMappingList);
                NotificationLogResponseDTO notificationLogDTO = new NotificationLogResponseDTO();
                List<NotificationLogDTO> response = new List<NotificationLogDTO>();
                int totalCount = 0;
                var peopleData = this.personRepository.GetPerson(personIndex);
                if (peopleData.PersonID != 0)
                {
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
                        if (!this.personRepository.IsValidPersonInAgency(peopleData.PersonID, peopleData.AgencyID, notificationLogSearchDTO.agencyID))
                        {
                            notificationLogDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                            notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                            return notificationLogDTO;
                        }
                        notificationLogSearchDTO.role = this.GetRoleName(notificationLogSearchDTO.userRole);

                        var sharedIDs = personRepository.GetSharedPersonQuestionnaireDetails(peopleData.PersonID, notificationLogSearchDTO.agencyID);
                        SharedDetailsDTO helperColbIDs = new SharedDetailsDTO();
                        if (string.IsNullOrEmpty(sharedIDs?.SharedCollaborationIDs))
                        {
                            helperColbIDs = this.personRepository.GetPersonHelperCollaborationDetails(peopleData.PersonID, notificationLogSearchDTO.agencyID, notificationLogSearchDTO.UserID);
                        }
                        var queryResponse = this.notificationLogRepository.GetPresentNotifications(notificationLogSearchDTO, queryBuilderDTO, peopleData.PersonID, sharedIDs, peopleData.AgencyID, helperColbIDs);
                        response = queryResponse.Item1;
                        totalCount = queryResponse.Item2;
                        notificationLogDTO.NotificationLog = response;
                        notificationLogDTO.TotalCount = totalCount;
                        notificationLogDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                        notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return notificationLogDTO;
                    }
                }
                return notificationLogDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// check person collaboration end date
        /// </summary>
        /// <param name="PersonCollaborations"></param>
        /// <returns></returns>
        public bool CheckPersonCollaborationEndDate(List<PersonCollaborationDTO> PersonCollaborations)
        {
            var isActive = false;
            foreach (PersonCollaborationDTO personCollaboration in PersonCollaborations)
            {
                if (personCollaboration.EndDate == null || !personCollaboration.EndDate.HasValue || personCollaboration.EndDate?.Date >= DateTime.Now.Date)
                {
                    isActive = true;
                    break;
                }
                else
                {
                    isActive = false;
                }
            }
            return isActive;
        }
        /// <summary>
        /// GetPeopleSharingDetails.
        /// </summary>
        /// <param name="peopleIndex">.</param>
        /// <returns>PersonSharingDetailsDTO.</returns>
        public PersonSharedDetailsResponseDTO GetPersonSharingDetails(Guid peopleIndex, long agencyID)
        {
            try
            {
                PersonSharedDetailsResponseDTO peopleSharingDetails = new PersonSharedDetailsResponseDTO();
                if (peopleIndex != Guid.Empty)
                {
                    var response = this.personRepository.GetPerson(peopleIndex);
                    peopleSharingDetails.IsShared = this.personRepository.IsSharedPerson(response.PersonID, agencyID);
                    if (peopleSharingDetails.IsShared)
                    {
                        var sharedDetails = this.personRepository.GetPersonSharingDetails(peopleIndex, agencyID);
                        peopleSharingDetails.AgencySharingIndex = sharedDetails.OrderByDescending(x => x.AgencySharingWeight).Select(x => x.AgencySharingIndex).FirstOrDefault();
                        peopleSharingDetails.CollaborationSharingIndex = sharedDetails.OrderByDescending(x => x.CollaborationSharingWeight).Select(x => x.CollaborationSharingIndex).FirstOrDefault();
                        peopleSharingDetails.IsActiveForSharing = sharedDetails.Max(x => x.IsActiveForSharing);
                    }
                    peopleSharingDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    peopleSharingDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return peopleSharingDetails;
                }
                else
                {
                    peopleSharingDetails.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.PeopleIndex);
                    peopleSharingDetails.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return peopleSharingDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// RemovePersonQuestionnaire
        /// </summary>
        /// <param name="personQuestionnaireID"></param>
        /// <param name="updateUserID"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO RemovePersonQuestionnaire(Guid personIndex, int updateUserID, int questionnaireID, long loggedInAgencyID)
        {
            try
            {
                CRUDResponseDTO resultDTO = new CRUDResponseDTO();

                var assessments = this.personQuestionnaireRepository.GetAssessmentsByPersonIndex(personIndex, questionnaireID);
                if (assessments != null && assessments.Count > 0)
                {
                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.AssessmentExist);
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.AssessmentExist;
                }
                else
                {
                    var person = this.personRepository.GetPerson(personIndex);
                    if (person.AgencyID != loggedInAgencyID)
                    {
                        resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                        resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                        return resultDTO;
                    }
                    var personQuestionnaireList = this.personQuestionnaireRepository.GetPersonQuestionnaireByPersonIndex(personIndex, questionnaireID);
                    foreach (var item in personQuestionnaireList)
                    {
                        item.IsRemoved = true;
                        item.UpdateDate = DateTime.UtcNow;
                        item.UpdateUserID = updateUserID;
                    }

                    var result = this.personQuestionnaireRepository.UpdateBulkPersonQuestionnaires(personQuestionnaireList);
                    foreach (var personQuestionnaire in personQuestionnaireList)
                    {
                        var personQuestionnaireScheduleToRemove = personQuestionnaireScheduleRepository.GetAsync(x => personQuestionnaire.PersonQuestionnaireID == x.PersonQuestionnaireID).Result.ToList();
                        personQuestionnaireScheduleToRemove.ForEach(x => x.IsRemoved = true);
                        personQuestionnaireScheduleService.UpdateBulkPersonQuestionnaireSchedule(personQuestionnaireScheduleToRemove);
                    }
                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                }
                return resultDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// UpsertPerson.
        /// </summary>
        /// <param name="peopleDetailsDTO">List of peopleDetailsDTO. </param>
        /// <returns>AddPersonResponseDTO.</returns>
        public CRUDResponseDTO UpsertPerson(List<PeopleDetailsDTO> peopleDetailsList, bool IsClosed)
        {
            try
            {
                CRUDResponseDTO response = new CRUDResponseDTO();
                long agencyId = peopleDetailsList.Select(x => x.AgencyID).FirstOrDefault();
                List<string> universalIDlist = peopleDetailsList.Select(x => x.UniversalID).ToList();
                var personDataListByUniversalIDList = personRepository.getPersonByUniversalIdList(universalIDlist, agencyId);
                List<PeopleDetailsDTO> peopleDetailsListToAdd = new List<PeopleDetailsDTO>();

                peopleDetailsListToAdd = peopleDetailsList.Where(x => !personDataListByUniversalIDList.Select(y => y.UniversalID).ToList().Contains(x.UniversalID)).ToList();
                foreach (var peopleDetailsDTO in peopleDetailsList)
                {
                    var personData = personDataListByUniversalIDList.Where(x => x.UniversalID == peopleDetailsDTO.UniversalID).FirstOrDefault();
                    if (personData != null)
                    {
                        PeopleEditDetailsDTO personDetails = FormatEditData(peopleDetailsDTO, personData);
                        response = EditPeopleDetails(personDetails, personDetails.AgencyID, PCISEnum.CallingType.EHR);
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                    }
                }
                if (peopleDetailsListToAdd.Count > 0 && !IsClosed)
                {
                    var result = AddBulkPeopleDetails(peopleDetailsListToAdd, universalIDlist, agencyId);
                    response.ResponseStatus = result.ResponseStatus;
                    response.ResponseStatusCode = result.ResponseStatusCode;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private PeopleEditDetailsDTO FormatEditData(PeopleDetailsDTO peopleDetailsDTO, PeopleDataDTO personData)
        {
            var PersonEditCollaborationDetails = new List<PersonEditCollaborationDTO>();
            var PersonEditHelperDetails = new List<PersonEditHelperDTO>();
            var PersonEditIdentificationDetails = new List<PersonEditIdentificationDTO>();
            var PersonEditRaceEthnicityDetails = new List<PersonEditRaceEthnicityDTO>();
            var PersonEditSupportDetails = new List<PersonEditSupportDTO>();

            foreach (var item in peopleDetailsDTO.PersonCollaborations)
            {
                var PersonEditCollaboration = new PersonEditCollaborationDTO();
                this.mapper.Map<PersonCollaborationDTO, PersonEditCollaborationDTO>(item, PersonEditCollaboration);
                PersonEditCollaborationDetails.Add(PersonEditCollaboration);
            }

            foreach (var item in peopleDetailsDTO.PersonHelpers)
            {
                var PersonEditHelper = new PersonEditHelperDTO();
                this.mapper.Map<PersonHelperDTO, PersonEditHelperDTO>(item, PersonEditHelper);
                PersonEditHelper.CollaborationID = null;
                PersonEditHelperDetails.Add(PersonEditHelper);
            }

            foreach (var item in peopleDetailsDTO.PersonIdentifications)
            {
                var PersonEditIdentification = new PersonEditIdentificationDTO();
                this.mapper.Map<PersonIdentificationDTO, PersonEditIdentificationDTO>(item, PersonEditIdentification);
                PersonEditIdentificationDetails.Add(PersonEditIdentification);
            }

            foreach (var item in peopleDetailsDTO.PersonRaceEthnicities)
            {
                var PersonEditRaceEthnicity = new PersonEditRaceEthnicityDTO();
                this.mapper.Map<PersonRaceEthnicityDTO, PersonEditRaceEthnicityDTO>(item, PersonEditRaceEthnicity);
                PersonEditRaceEthnicityDetails.Add(PersonEditRaceEthnicity);
            }

            foreach (var item in peopleDetailsDTO.PersonSupports)
            {
                var PersonEditSupport = new PersonEditSupportDTO();
                this.mapper.Map<PersonSupportDTO, PersonEditSupportDTO>(item, PersonEditSupport);
                PersonEditSupportDetails.Add(PersonEditSupport);
            }


            var personDetails = new PeopleEditDetailsDTO();
            personDetails.PersonEditHelperDTO = PersonEditHelperDetails;
            personDetails.PersonEditIdentificationDTO = PersonEditIdentificationDetails;
            personDetails.PersonEditRaceEthnicityDTO = PersonEditRaceEthnicityDetails;
            personDetails.PersonEditSupportDTO = PersonEditSupportDetails;
            personDetails.PersonEditCollaborationDTO = PersonEditCollaborationDetails;
            personDetails.Address1 = peopleDetailsDTO.Address1;
            personDetails.Address2 = peopleDetailsDTO.Address2;
            personDetails.AddressID = peopleDetailsDTO.AddressID;
            personDetails.AddressIndex = peopleDetailsDTO.AddressIndex;
            personDetails.AgencyID = peopleDetailsDTO.AgencyID;
            personDetails.BiologicalSexID = peopleDetailsDTO.BiologicalSexID;
            personDetails.City = peopleDetailsDTO.City;
            personDetails.CountryStateId = peopleDetailsDTO.CountryStateId;
            personDetails.DateOfBirth = peopleDetailsDTO.DateOfBirth;
            personDetails.Email = peopleDetailsDTO.Email;
            personDetails.EndDate = peopleDetailsDTO.EndDate;
            personDetails.FirstName = peopleDetailsDTO.FirstName;
            personDetails.GenderID = peopleDetailsDTO.GenderID;
            personDetails.IsActive = peopleDetailsDTO.IsActive;
            personDetails.IsPreferred = peopleDetailsDTO.IsPreferred;
            personDetails.IsPrimary = peopleDetailsDTO.IsPrimary;
            personDetails.IsRemoved = personData.IsRemoved;
            personDetails.LanguageID = peopleDetailsDTO.LanguageID;
            personDetails.LastName = peopleDetailsDTO.LastName;
            personDetails.MiddleName = peopleDetailsDTO.MiddleName;
            personDetails.PersonAddressID = personData.PersonAddressID;
            personDetails.PersonID = personData.PersonID;
            personDetails.PersonIndex = personData.PersonIndex;
            personDetails.PersonScreeningStatusID = peopleDetailsDTO.PersonScreeningStatusID;
            personDetails.Phone1 = peopleDetailsDTO.Phone1;
            personDetails.Phone1Code = peopleDetailsDTO.Phone1Code;
            personDetails.Phone2 = peopleDetailsDTO.Phone2Code;
            personDetails.Phone2Code = peopleDetailsDTO.Phone2Code;
            personDetails.PreferredLanguageID = peopleDetailsDTO.PreferredLanguageID;
            personDetails.PrimaryLanguageID = peopleDetailsDTO.PrimaryLanguageID;
            personDetails.SexualityID = peopleDetailsDTO.SexualityID;
            personDetails.StartDate = peopleDetailsDTO.StartDate;
            personDetails.Suffix = peopleDetailsDTO.Suffix;
            personDetails.UniversalID = personData.UniversalID;
            personDetails.UpdateDate = DateTime.UtcNow;
            personDetails.UpdateUserID = 1;
            personDetails.Zip = peopleDetailsDTO.Zip;
            personDetails.Zip4 = peopleDetailsDTO.Zip4;
            personDetails.EmailPermission = personData.EmailPermission;
            personDetails.TextPermission = personData.TextPermission;
            return personDetails;
        }

        /// <summary>
        /// AddAgencyDetails.
        /// </summary>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>PeopleDetailsResponseDTO.</returns>
        public AddPersonResponseDTO AddBulkPeopleDetails(List<PeopleDetailsDTO> peopleDetailsDTO, List<string> universalIDlist, long agencyID)
        {
            try
            {
                List<PeopleDTO> peopleDTOs = new List<PeopleDTO>();
                AddPersonResponseDTO resultDTO = new AddPersonResponseDTO();
                foreach (var item in peopleDetailsDTO)
                {
                    var isPersonActive = CheckPersonCollaborationEndDate(item.PersonCollaborations);
                    item.IsActive = isPersonActive;
                    PeopleDTO peopleDTO = CreatePeople(item);
                    if (peopleDTO != null)
                    {
                        peopleDTOs.Add(peopleDTO);
                    }
                }
                List<Person> personList = new List<Person>();
                this.mapper.Map(peopleDTOs, personList);
                if (personList.Count > 0)
                {
                    this.personRepository.AddBulkAsync(personList).Wait();
                    var personDataListByUniversalIDList = personRepository.getPersonByUniversalIdList(universalIDlist, agencyID);
                    foreach (var item in personDataListByUniversalIDList)
                    {
                        peopleDetailsDTO.Where(x => x.UniversalID == item.UniversalID).ToList().ForEach(x => x.PersonID = item.PersonID);
                    }

                    if (peopleDetailsDTO != null)
                    {
                        List<AddressDTO> addressList = new List<AddressDTO>();

                        foreach (var people in peopleDetailsDTO)
                        {
                            AddressDTO addressDTO = CreateAddressDTOOnAdd(people);
                            if (addressDTO != null)
                            {
                                addressDTO.AddressIndex = Guid.NewGuid();
                                addressList.Add(addressDTO);
                            }
                            //AddToPersonAddress(addressDTO, people.PersonID);
                            List<PersonIdentificationDTO> personIdentificationDTOList = new List<PersonIdentificationDTO>();
                            foreach (PersonIdentificationDTO PersonIdentity in people.PersonIdentifications)
                            {
                                PersonIdentificationDTO personIdentificationDTO = CreatePersonIdentification(PersonIdentity, people);
                                personIdentificationDTOList.Add(personIdentificationDTO);

                            }
                            if (personIdentificationDTOList.Count > 0)
                            {
                                this.personIdentificationRepository.AddBulkPersonIdentificationType(personIdentificationDTOList);
                            }
                            List<PersonCollaborationDTO> personCollaborationDTOList = new List<PersonCollaborationDTO>();
                            foreach (PersonCollaborationDTO personCollaboration in people.PersonCollaborations)
                            {
                                PersonCollaborationDTO personCollaborationDTO = CreatePersonCollaboration(personCollaboration, people);
                                personCollaborationDTOList.Add(personCollaborationDTO);
                                if (personCollaborationDTOList != null)
                                {
                                    var CollaborationQuestionnaireData = this.collaborationQuestionnaireRepository.GetCollaborationQuestionnaireData(personCollaborationDTO.CollaborationID);

                                    if (CollaborationQuestionnaireData != null)
                                    {
                                        List<PersonQuestionnaireDTO> PersonQuestionnaireDTOList = new List<PersonQuestionnaireDTO>();
                                        foreach (CollaborationQuestionnaireDTO CollaborationQuestionnaire in CollaborationQuestionnaireData)
                                        {
                                            personCollaboration.PersonID = people.PersonID;
                                            personCollaboration.UpdateUserID = people.UpdateUserID;

                                            PersonQuestionnaireDTO PersonQuestionnaireDTO = CreatePersonQuestionnaireDTO(CollaborationQuestionnaire, personCollaboration);
                                            PersonQuestionnaireDTOList.Add(PersonQuestionnaireDTO);
                                        }
                                        if (PersonQuestionnaireDTOList.Count > 0)
                                        {
                                            this.PersonQuestionnaireRepository.AddBulkPersonQuestionnaire(PersonQuestionnaireDTOList);
                                            foreach (var item in PersonQuestionnaireDTOList)
                                            {
                                                var personQuestionnaire = this.personQuestionnaireRepository.GetPersonQuestionnaireByCollaborationQuestionnaireAndPersonID(item.CollaborationID ?? 0, item.QuestionnaireID, item.PersonID).Result;
                                                var collaborationQuestionnaire = this.collaborationQuestionnaireRepository.GetCollaborationQuestionnaireByCollaborationAndQuestionnaire(personCollaboration.CollaborationID, item.QuestionnaireID);
                                                if (collaborationQuestionnaire.IsReminderOn && personQuestionnaire != null)
                                                {
                                                    StoreInQueue(personQuestionnaire.PersonQuestionnaireID);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (personCollaborationDTOList.Count > 0)
                            {
                                this.personCollaborationRepository.AddBulkPersonCollaboration(personCollaborationDTOList);
                            }
                            List<PersonSupportDTO> personSupportDTOList = new List<PersonSupportDTO>();
                            foreach (PersonSupportDTO personSupport in people.PersonSupports)
                            {
                                if (personSupport.FirstName != null)
                                {
                                    PersonSupportDTO personSupportDTO = CreatePersonSupport(personSupport, people);
                                    personSupportDTOList.Add(personSupportDTO);

                                }
                            }
                            if (personSupportDTOList.Count > 0)
                            {
                                this.personSupportRepository.AddBulkPersonSupport(personSupportDTOList);
                            }
                            List<PersonHelperDTO> personHelperDTOList = new List<PersonHelperDTO>();
                            foreach (PersonHelperDTO personhelper in people.PersonHelpers)
                            {
                                PersonHelperDTO personHelperDTO = CreatePersonHelper(personhelper, people);
                                personHelperDTOList.Add(personHelperDTO);

                            }
                            if (personHelperDTOList.Count > 0)
                            {
                                this.personHelperRepository.AddBulkPersonHelper(personHelperDTOList);
                            }
                            List<PersonRaceEthnicityDTO> personRaceEthnicityDTOList = new List<PersonRaceEthnicityDTO>();
                            foreach (PersonRaceEthnicityDTO RaceEthnicity in people.PersonRaceEthnicities)
                            {

                                PersonRaceEthnicityDTO personRaceEthnicityDTO = CreateRaceEthnicityDTO(RaceEthnicity, people);
                                personRaceEthnicityDTOList.Add(personRaceEthnicityDTO);
                            }
                            if (personRaceEthnicityDTOList.Count > 0)
                            {
                                this.personRaceEthnicityRepository.AddBulkRaceEthnicity(personRaceEthnicityDTOList);
                            }
                        }
                        if (addressList.Count > 0)
                        {
                            List<Address> address = new List<Address>();
                            this.mapper.Map<List<AddressDTO>, List<Address>>(addressList, address);
                            this.addressRepository.AddBulkAsync(address).Wait();

                            List<Guid> addressIndexList = new List<Guid>();
                            addressIndexList = addressList.Select(x => x.AddressIndex).ToList();
                            var addressListByIndex = this.addressRepository.GetAddressListByIndex(addressIndexList).Result;
                            foreach (var item in addressListByIndex)
                            {
                                addressList.Where(x => x.AddressIndex == item.AddressIndex).ToList().ForEach(x => x.AddressID = item.AddressID);
                            }
                            List<PersonAddressDTO> personAddressDTOList = new List<PersonAddressDTO>();
                            foreach (var addressItem in addressList)
                            {
                                foreach (var personItem in peopleDetailsDTO)
                                {
                                    PersonAddressDTO personAddressDTO = CreatePersonAddress(personItem.PersonID, addressItem.AddressID);
                                    personAddressDTOList.Add(personAddressDTO);
                                }
                            }
                            if (personAddressDTOList.Count > 0)
                            {
                                this.personAddressRepository.AddBulkPersonAddress(personAddressDTOList);
                            }
                        }
                        resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                        resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        return resultDTO;
                    }
                    if (resultDTO.ResponseStatusCode != PCISEnum.StatusCodes.InsertionSuccess)
                    {
                        resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                        resultDTO.ResponseStatus = PCISEnum.StatusMessages.insertionFailed;
                    }
                    return resultDTO;
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
        /// GetPersonQuestionnaireById.
        /// </summary>
        /// <param name="personQuestionnaireId">personQuestionnaireId.</param>
        /// <returns>PersonQuestionnaireDetailsDTo.</returns>
        public PersonQuestionnaireDetailsDTo GetPersonQuestionnaireById(int personQuestionnaireId)
        {
            try
            {
                PersonQuestionnaireDetailsDTo PersonQuestionnaire = new PersonQuestionnaireDetailsDTo();
                if (personQuestionnaireId > 0)
                {
                    PersonQuestionnaire.PersonQuestionnaire = this.PersonQuestionnaireRepository.GetPersonQuestionnaireByID(personQuestionnaireId).Result;
                    PersonQuestionnaire.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    PersonQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return PersonQuestionnaire;
                }
                else
                {
                    PersonQuestionnaire.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.PersonQuestionnaireID);
                    PersonQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return PersonQuestionnaire;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonQuestionaireByPersonQuestionaireID.
        /// </summary>
        /// <param name="personQuestionnaireId">personQuestionnaireId.</param>
        /// <returns>PersonQuestionnaireListDetailsDTO.</returns>
        public PersonQuestionnaireListDetailsDTO GetPersonQuestionaireByPersonQuestionaireID(int personQuestionnaireId)
        {
            try
            {
                PersonQuestionnaireListDetailsDTO PersonQuestionnaire = new PersonQuestionnaireListDetailsDTO();
                if (personQuestionnaireId > 0)
                {
                    var response = this.PersonQuestionnaireRepository.GetPersonQuestionaireByPersonQuestionaireID(personQuestionnaireId);
                    List<PersonQuestionnaireDTO> listPersonQustionnaire = new List<PersonQuestionnaireDTO>();
                    this.mapper.Map<List<PersonQuestionnaire>, List<PersonQuestionnaireDTO>>(response, listPersonQustionnaire);
                    PersonQuestionnaire.PersonQuestionnaire = listPersonQustionnaire;
                    PersonQuestionnaire.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    PersonQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return PersonQuestionnaire;
                }
                else
                {
                    PersonQuestionnaire.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.PersonQuestionnaireID);
                    PersonQuestionnaire.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return PersonQuestionnaire;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonCollaborationByPersonIdAndCollaborationId.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <param name="collaborationId">collaborationId.</param>
        /// <returns>PersonCollaborationDetailsDTO.</returns>
        public PersonCollaborationDetailsDTO GetPersonCollaborationByPersonIdAndCollaborationId(long personId, int? collaborationId)
        {
            try
            {
                PersonCollaborationDetailsDTO PersonCollaboration = new PersonCollaborationDetailsDTO();
                if (personId > 0)
                {
                    PersonCollaborationDTO personCollaboration = new PersonCollaborationDTO();
                    var response = this.personCollaborationRepository.GetPersonCollaborationByPersonIdAndCollaborationId(personId, collaborationId);
                    this.mapper.Map<PersonCollaboration, PersonCollaborationDTO>(response, personCollaboration);
                    PersonCollaboration.PersonCollaboration = personCollaboration;
                    PersonCollaboration.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    PersonCollaboration.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return PersonCollaboration;
                }
                else
                {
                    PersonCollaboration.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.PersonId);
                    PersonCollaboration.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return PersonCollaboration;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireSchedule.
        /// </summary>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <param name="questionnaireWindowId">QuestionnaireWindowId.</param>
        /// <returns>PersonQuestionnaireScheduleDetailsDTO.</returns>
        public PersonQuestionnaireScheduleDetailsDTO GetPersonQuestionnaireSchedule(long personQuestionnaireID, int questionnaireWindowId)
        {
            try
            {
                PersonQuestionnaireScheduleDetailsDTO PersonQuestionnaireScheduleDetails = new PersonQuestionnaireScheduleDetailsDTO();
                if (personQuestionnaireID > 0)
                {
                    List<PersonQuestionnaireScheduleDTO> PersonQuestionnaireSchedule = new List<PersonQuestionnaireScheduleDTO>();
                    var response = this.personQuestionnaireScheduleRepository.GetPersonQuestionnaireSchedule(personQuestionnaireID, questionnaireWindowId).Result;
                    this.mapper.Map<List<PersonQuestionnaireSchedule>, List<PersonQuestionnaireScheduleDTO>>(response, PersonQuestionnaireSchedule);
                    PersonQuestionnaireScheduleDetails.PersonQuestionnaireSchedules = PersonQuestionnaireSchedule;
                    PersonQuestionnaireScheduleDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    PersonQuestionnaireScheduleDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return PersonQuestionnaireScheduleDetails;
                }
                else
                {
                    PersonQuestionnaireScheduleDetails.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.PersonQuestionnaireID);
                    PersonQuestionnaireScheduleDetails.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return PersonQuestionnaireScheduleDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateBulkPersonQuestionnaireSchedule.
        /// October 1,2021 : PCIS-3165 changes to remove all in NotifyReminders and NotificationLogs associated with Removed PersonQuestionnaireSchedule from AF-ReminderNotificationProcess based on IsRemoveReminderNotifications flag. While For the same function call from AF-ReminderAutoResolveProcess should only update PersonQuestionnaireSchedule to be removed bcz the rest is handled in azure function for updating reminders to autoresolve.
        /// </summary>
        /// <param name="personQuestionnaireSchedules">personQuestionnaireSchedules.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateBulkPersonQuestionnaireSchedule(List<PersonQuestionnaireScheduleDTO> personQuestionnaireSchedules)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                List<PersonQuestionnaireSchedule> PersonQuestionnaireSchedule = new List<PersonQuestionnaireSchedule>();
                this.mapper.Map<List<PersonQuestionnaireScheduleDTO>, List<PersonQuestionnaireSchedule>>(personQuestionnaireSchedules, PersonQuestionnaireSchedule);
                var IsRemoveReminderNotifications = personQuestionnaireSchedules?.Count(x => x.IsRemoveReminderNotifications);
                if (IsRemoveReminderNotifications > 0)
                {   //get All NotifyReminders
                    var notifyReminderIdToBeRemoved = new GetNotifyReminderInputDTO() { personQuestionnaireScheduleIDList = personQuestionnaireSchedules.Select(x => x.PersonQuestionnaireScheduleID).ToList() };
                    var removedNotifyRemindersDTO = this.notifyReminderRepository.GetNotifyReminders(notifyReminderIdToBeRemoved, false);
                    //Update All NotifyReminders to be removed.
                    List<NotifyReminder> notifyReminders = new List<NotifyReminder>();
                    this.mapper.Map<List<NotifyReminderDTO>, List<NotifyReminder>>(removedNotifyRemindersDTO, notifyReminders);

                    if (notifyReminders.Count > 0)
                    {
                        notifyReminders?.ForEach(x => x.IsRemoved = true);
                        this.notifyReminderRepository.UpdateBulkNotifyReminder(notifyReminders);
                        var notificationType = this.notificationTypeRepository.GetNotificationType(PCISEnum.NotificationType.Reminder).Result;
                        //get All NotificationLogs for reminders associated with removed NotifyReminderIDs.
                        var notificationLog = this.notificationLogRepository.GetNotifcationLogForReminder(notifyReminders?.Select(x => x.NotifyReminderID).ToList(), notificationType.NotificationTypeID);
                        //Update all NotificationLogs to be removed
                        notificationLog?.ForEach(x => x.IsRemoved = true);
                        this.notificationLogRepository.UpdateBulkNotificationLog(notificationLog);
                    }
                }
                var Response = personQuestionnaireScheduleRepository.UpdateBulkPersonQuestionnaireSchedule(PersonQuestionnaireSchedule);
                if (result != null)
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
        /// AddPersonQuestionnaireSchedule.
        /// </summary>
        /// <param name="personQuestionnaireSchedules">personQuestionnaireSchedules.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public AddPersonQuestionnaireScheduleResponseDTO AddPersonQuestionnaireSchedule(PersonQuestionnaireScheduleDTO personQuestionnaireSchedules)
        {
            try
            {
                AddPersonQuestionnaireScheduleResponseDTO result = new AddPersonQuestionnaireScheduleResponseDTO();
                PersonQuestionnaireSchedule PersonQuestionnaireSchedule = new PersonQuestionnaireSchedule();
                this.mapper.Map<PersonQuestionnaireScheduleDTO, PersonQuestionnaireSchedule>(personQuestionnaireSchedules, PersonQuestionnaireSchedule);
                var Response = personQuestionnaireScheduleRepository.AddPersonQuestionnaireSchedule(PersonQuestionnaireSchedule);
                if (Response > 0)
                {
                    result.PersonQuestionnaireScheduleID = Response;
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
        /// GetPersonQuestionnaireScheduleForReminder.
        /// </summary>
        /// <param name="personQuestionnaireSchedule">personQuestionnaireSchedule.</param>
        /// <returns>PersonQuestionnaireScheduleDetailsDTO.</returns>
        public PersonQuestionnaireScheduleDetailsDTO GetPersonQuestionnaireScheduleForReminder(PersonQuestionnaireScheduleInputDTO personQuestionnaireSchedule)
        {
            try
            {
                PersonQuestionnaireScheduleDetailsDTO PersonQuestionnaireScheduleDetails = new PersonQuestionnaireScheduleDetailsDTO();
                if (personQuestionnaireSchedule != null)
                {
                    List<PersonQuestionnaireScheduleDTO> PersonQuestionnaireSchedule = new List<PersonQuestionnaireScheduleDTO>();
                    var response = this.personQuestionnaireScheduleRepository.GetPersonQuestionnaireScheduleList
                        (personQuestionnaireSchedule.DateTaken, personQuestionnaireSchedule.QuestionnaireWindowListID, personQuestionnaireSchedule.PersonQuestionnaireID).Result;
                    this.mapper.Map<List<PersonQuestionnaireSchedule>, List<PersonQuestionnaireScheduleDTO>>(response, PersonQuestionnaireSchedule);
                    PersonQuestionnaireScheduleDetails.PersonQuestionnaireSchedules = PersonQuestionnaireSchedule;
                    PersonQuestionnaireScheduleDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    PersonQuestionnaireScheduleDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return PersonQuestionnaireScheduleDetails;
                }
                else
                {
                    PersonQuestionnaireScheduleDetails.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.PersonQuestionnaireID);
                    PersonQuestionnaireScheduleDetails.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return PersonQuestionnaireScheduleDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ImportPerson.
        /// </summary>
        /// <param name="peopleDetailsDTO">peopleDetailsDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO ImportPerson(List<PeopleDetailsDTO> peopleDetailsDTO)
        {
            try
            {
                List<PeopleDTO> peopleDTOs = new List<PeopleDTO>();
                List<PersonHelperDTO> personHelperListDTO = new List<PersonHelperDTO>();
                List<PersonCollaborationDTO> personCollaborationListDTO = new List<PersonCollaborationDTO>();
                List<PersonRaceEthnicityDTO> personRaceEthnicityListDTO = new List<PersonRaceEthnicityDTO>();
                List<PersonIdentificationDTO> personIdentificationListDTO = new List<PersonIdentificationDTO>();

                CRUDResponseDTO resultDTO = new CRUDResponseDTO();
                List<Guid> PersonIndexGuids = new List<Guid>();
                if (peopleDetailsDTO.Count > 0)
                {
                    foreach (var item in peopleDetailsDTO)
                    {
                        var isPersonActive = CheckPersonCollaborationEndDate(item.PersonCollaborations);
                        item.IsActive = isPersonActive;
                        PeopleDTO peopleDTO = CreatePeople(item);
                        peopleDTO.PersonIndex = Guid.NewGuid();
                        PersonIndexGuids.Add(peopleDTO.PersonIndex);
                        if (peopleDTO != null)
                        {
                            peopleDTOs.Add(peopleDTO);
                        }
                        foreach (var personhelper in item.PersonHelpers)
                        {
                            personhelper.PersonIndexGuid = peopleDTO.PersonIndex;
                            personhelper.UpdateUserID = item.UpdateUserID;
                            personhelper.CollaborationID = null;
                            personHelperListDTO.Add(personhelper);
                        }
                        foreach (var personcollaboration in item.PersonCollaborations)
                        {
                            personcollaboration.PersonIndexGuid = peopleDTO.PersonIndex;
                            personcollaboration.UpdateUserID = item.UpdateUserID;
                            personCollaborationListDTO.Add(personcollaboration);
                        }
                        foreach (var personRaceEthnicity in item.PersonRaceEthnicities)
                        {
                            personRaceEthnicity.PersonIndexGuid = peopleDTO.PersonIndex;
                            personRaceEthnicityListDTO.Add(personRaceEthnicity);
                        }
                        foreach (var PersonIdentity in item.PersonIdentifications)
                        {
                            PersonIdentity.PersonIndexGuid = peopleDTO.PersonIndex;
                            personIdentificationListDTO.Add(PersonIdentity);
                        }
                    }
                    List<Person> personList = new List<Person>();
                    this.mapper.Map(peopleDTOs, personList);
                    if (personList.Count > 0)
                    {
                        List<Person> result = this.personRepository.ImportPersonBulkInsert(personList);
                        if (result.Count > 0)
                        {
                            var personListByGuid = personRepository.GetPersonListByGUID(PersonIndexGuids).Result;
                            foreach (var item in personHelperListDTO)
                            {
                                item.PersonID = personListByGuid.Where(x => x.PersonIndex == item.PersonIndexGuid).FirstOrDefault().PersonID;
                            }
                            personHelperRepository.AddBulkPersonHelper(personHelperListDTO);

                            foreach (var item in personCollaborationListDTO)
                            {
                                item.PersonID = personListByGuid.Where(x => x.PersonIndex == item.PersonIndexGuid).FirstOrDefault().PersonID;
                            }
                            personCollaborationRepository.AddBulkPersonCollaboration(personCollaborationListDTO);
                            foreach (var item in personRaceEthnicityListDTO)
                            {
                                item.PersonID = personListByGuid.Where(x => x.PersonIndex == item.PersonIndexGuid).FirstOrDefault().PersonID;
                            }
                            personRaceEthnicityRepository.AddBulkRaceEthnicity(personRaceEthnicityListDTO);
                            foreach (var item in personIdentificationListDTO)
                            {
                                item.PersonID = personListByGuid.Where(x => x.PersonIndex == item.PersonIndexGuid).FirstOrDefault().PersonID;
                            }
                            personIdentificationRepository.AddBulkPersonIdentificationType(personIdentificationListDTO);

                            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }
                    }
                }
                return resultDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EHRLookupResponseDTO GetVoiceTypeRelatedDetailsOfPerson(PersonIndexToUploadDTO personstoUpload)
        {
            try
            {
                Dictionary<string, PersonVoiceTypeDetailsForImportDTO> filterdPersons = new Dictionary<string, PersonVoiceTypeDetailsForImportDTO>();
                var agencyID = personstoUpload.AgencyID;
                foreach (var personIndex in personstoUpload.PersonIndexesToUpload)
                {
                    var personInPCIS = this.personRepository.GetAsync(x => x.PersonIndex == personIndex && x.AgencyID == agencyID).Result;
                    if (personInPCIS.Count > 0)
                    {
                        filterdPersons.Add(personIndex.ToString(), new PersonVoiceTypeDetailsForImportDTO() { PersonHelperlookups = new List<Lookup>(), PersonSupportlookups = new List<Lookup>() });
                        var supportsInPCIS = this.personRepository.GetPeopleSupportList(personInPCIS[0].PersonID)?.Select(x => new Lookup() { Id = x.PersonSupportID.ToString(), Value = "" }).ToList();
                        var helpersInPCIS = this.personHelperRepository.GetPersonHelperDetails(personInPCIS[0].PersonID)?.Select(x => new Lookup() { Id = x.PersonHelperID.ToString(), Value = x.Email }).ToList();
                        filterdPersons[personIndex.ToString()].PersonSupportlookups = supportsInPCIS;
                        filterdPersons[personIndex.ToString()].PersonHelperlookups = helpersInPCIS;
                        filterdPersons[personIndex.ToString()].PersonID = personInPCIS[0].PersonID;
                    }
                }
                EHRLookupResponseDTO response = new EHRLookupResponseDTO();
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.personVoiceTypeDetails = filterdPersons;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetPersonsAndHelpersByPersonIDListForAlert.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>PersonHelperEmailDetailDTO.</returns>
        public PersonHelperEmailDetailDTO GetPersonsAndHelpersByPersonIDListForAlert(long personId)
        {
            try
            {
                PersonHelperEmailDetailDTO PersonQuestionnaireScheduleDetails = new PersonHelperEmailDetailDTO();
                if (personId > 0)
                {
                    List<PersonQuestionnaireScheduleDTO> PersonQuestionnaireSchedule = new List<PersonQuestionnaireScheduleDTO>();
                    var response = this.personRepository.getPersonsAndHelpersByPersonIDListForAlert(personId);
                    PersonQuestionnaireScheduleDetails.PersonHelper = response;
                    PersonQuestionnaireScheduleDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    PersonQuestionnaireScheduleDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return PersonQuestionnaireScheduleDetails;
                }
                else
                {
                    PersonQuestionnaireScheduleDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                    PersonQuestionnaireScheduleDetails.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    return PersonQuestionnaireScheduleDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetDetailsByPersonQuestionScheduleList.
        /// </summary>
        /// <param name="personScheduleId">personScheduleId.</param>
        /// <returns>ReminderNotificationScheduleResponse.</returns>
        public ReminderNotificationScheduleResponse GetDetailsByPersonQuestionScheduleList(List<long> personScheduleId)
        {
            try
            {
                ReminderNotificationScheduleResponse PersonQuestionnaireScheduleDetails = new ReminderNotificationScheduleResponse();
                if (personScheduleId.Count > 0)
                {
                    List<PersonQuestionnaireScheduleDTO> PersonQuestionnaireSchedule = new List<PersonQuestionnaireScheduleDTO>();
                    var response = this.personRepository.getDetailsByPersonQuestionScheduleList(personScheduleId);
                    PersonQuestionnaireScheduleDetails.PersonQuestionnaireScheduleEmails = response;
                    PersonQuestionnaireScheduleDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    PersonQuestionnaireScheduleDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return PersonQuestionnaireScheduleDetails;
                }
                else
                {
                    PersonQuestionnaireScheduleDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                    PersonQuestionnaireScheduleDetails.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    return PersonQuestionnaireScheduleDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonsAndHelpersByPersonIDList.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>PersonHelperEmailDetailDTO.</returns>
        public PersonHelperEmailDetailDTO GetPersonsAndHelpersByPersonIDList(List<long> personId)
        {
            try
            {
                PersonHelperEmailDetailDTO PersonQuestionnaireScheduleDetails = new PersonHelperEmailDetailDTO();
                if (personId.Count > 0)
                {
                    List<PersonQuestionnaireScheduleDTO> PersonQuestionnaireSchedule = new List<PersonQuestionnaireScheduleDTO>();
                    var response = this.personRepository.getPersonsAndHelpersByPersonIDList(personId);
                    PersonQuestionnaireScheduleDetails.PersonHelper = response;
                    PersonQuestionnaireScheduleDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    PersonQuestionnaireScheduleDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return PersonQuestionnaireScheduleDetails;
                }
                else
                {
                    PersonQuestionnaireScheduleDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                    PersonQuestionnaireScheduleDetails.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    return PersonQuestionnaireScheduleDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetActivePersons.
        /// </summary>
        /// <returns>ActivePersonResponseDTO.</returns>

        public ActivePersonResponseDTO GetActivePersons()
        {
            try
            {
                ActivePersonResponseDTO result = new ActivePersonResponseDTO();
                var response = this.personRepository.GetActivePersons();
                result.PersonIds = response;
                result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetActivePersonCollaboration.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>ActivePersonResponseDTO.</returns>
        public ActivePersonResponseDTO GetActivePersonCollaboration(List<long> personId)
        {
            try
            {
                ActivePersonResponseDTO result = new ActivePersonResponseDTO();
                var response = this.personRepository.GetActivePersonsCollaboration(personId);
                result.PersonIds = response;
                result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateIsActiveForPerson.
        /// </summary>
        /// <param name="personIds">personIds.</param>
        /// <returns>CRUDResponseDTO.</returns>

        public CRUDResponseDTO UpdateIsActiveForPerson(List<long> personIds)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                var personList = this.personRepository.GetAsync(x => personIds.Contains(x.PersonID)).Result.ToList();
                personList.ForEach(x => x.IsActive = false);
                personList.ForEach(x => x.UpdateDate = DateTime.UtcNow);
                this.personRepository.UpdateBulkPersons(personList);
                result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotifyReminderScheduledForToday.
        /// Updated to fetch reminders between last runtime and current runtime time. PCIS-3225
        /// </summary>
        /// <returns>ReminderNotificationScheduleResponse.</returns>        
        public ReminderNotificationScheduleResponse GetNotifyReminderScheduledForToday(DateTime lastRunTime, DateTime currentRunTime)
        {
            try
            {
                ReminderNotificationScheduleResponse result = new ReminderNotificationScheduleResponse();
                var response = this.personQuestionnaireScheduleRepository.GetNotifyReminderScheduledForToday(lastRunTime, currentRunTime);
                result.ReminderNotification = response;
                result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotifyReminderScheduledCountForToday.
        /// Updated to fetch reminders between last runtime and current runtime time. PCIS-3225
        /// </summary>
        /// <returns>ReminderNotificationScheduleResponse.</returns>
        public ReminderNotificationScheduleResponse GetNotifyReminderScheduledCountForToday(DateTime lastRunTime, DateTime currentRunTime)
        {
            try
            {
                ReminderNotificationScheduleResponse result = new ReminderNotificationScheduleResponse();
                var response = this.personQuestionnaireScheduleRepository.GetNotifyReminderScheduledCountForToday(lastRunTime, currentRunTime);
                result.Count = response;
                result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// InsertToLeadHelperHistory.
        /// </summary>
        /// <param name="leadHelperFromUI">leadHelperFromUI.</param>
        /// <param name="existingDBLead">existingLead.</param>
        /// <param name="modeOfOperation"></param>
        private void InsertToLeadHelperHistory(List<PersonHelper> leadHelperFromUI, List<PersonHelper> existingDBLead, List<PersonEditHelperDTO> existingLeadInDBFromUI)
        {
            try
            {
                if (leadHelperFromUI.Count > 0 && existingDBLead.Count > 0)
                {
                    var existingLeadInDB = existingDBLead[0];
                    if (existingLeadInDB.HelperID != leadHelperFromUI[0].HelperID || (existingLeadInDB.HelperID == leadHelperFromUI[0].HelperID && (existingLeadInDB.StartDate.Date != leadHelperFromUI[0].StartDate.Date || existingLeadInDB.EndDate.GetValueOrDefault().Date != leadHelperFromUI[0].EndDate.GetValueOrDefault().Date)))
                    {
                        var leadToHistoryTable = existingLeadInDB;
                        if (existingLeadInDBFromUI.Count > 0)
                        {
                            if (existingLeadInDB.HelperID != leadHelperFromUI[0].HelperID && existingLeadInDB.EndDate != existingLeadInDBFromUI[0].EndDate)
                            {
                                leadToHistoryTable.EndDate = existingLeadInDBFromUI[0].EndDate;
                            }
                        }
                        AuditPersonProfile auditPersonProfile = new AuditPersonProfile()
                        {
                            ParentID = leadToHistoryTable.PersonID,
                            ChildRecordID = leadToHistoryTable.HelperID,
                            StartDate = leadToHistoryTable.StartDate,
                            EndDate = leadToHistoryTable.EndDate,
                            TypeName = PCISEnum.AuditPersonProfileType.Helper,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = leadToHistoryTable.UpdateUserID
                        };
                        var result = this.auditPersonProfileRepository.AddPersonProfileDetailsForAudit(auditPersonProfile);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// InsertToPersonCollaborationHistory.
        /// </summary>
        /// <param name="primaryCollaborationFromUI">primaryCollaborationFromUI.</param>
        /// <param name="existingDBprimaryCollaboration">existingprimaryCollaboration.</param>
        /// <param name="existingPrimaryCollaborationInDBfromUI">existingPrimaryCollaborationInDBfromUI.</param>
        /// <param name="modeOfOperation"></param>
        private void InsertToPersonPrimaryCollaborationHistory(List<PersonEditCollaborationDTO> primaryCollaborationFromUI, List<PersonCollaboration> existingDBprimaryCollaboration, List<PersonEditCollaborationDTO> existingPrimaryCollaborationInDBfromUI)
        {
            try
            {
                if (existingDBprimaryCollaboration.Count > 0 && existingPrimaryCollaborationInDBfromUI.Count > 0 && primaryCollaborationFromUI.Count == 0)
                {
                    var primaryCollaborationToHistoryTable = existingDBprimaryCollaboration[0];
                    AuditPersonProfile auditPersonProfile = new AuditPersonProfile()
                    {
                        ParentID = primaryCollaborationToHistoryTable.PersonID,
                        ChildRecordID = primaryCollaborationToHistoryTable.CollaborationID,
                        StartDate = Convert.ToDateTime(primaryCollaborationToHistoryTable.EnrollDate),
                        EndDate = existingPrimaryCollaborationInDBfromUI[0].EndDate,
                        TypeName = PCISEnum.AuditPersonProfileType.Collaboration,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = primaryCollaborationToHistoryTable.UpdateUserID
                    };
                    var result = this.auditPersonProfileRepository.AddPersonProfileDetailsForAudit(auditPersonProfile);
                }
                if (primaryCollaborationFromUI.Count > 0 && existingDBprimaryCollaboration.Count > 0)
                {
                    var existingprimaryCollaborationInDB = existingDBprimaryCollaboration[0];
                    if (existingprimaryCollaborationInDB.CollaborationID != primaryCollaborationFromUI[0].CollaborationID || (existingprimaryCollaborationInDB.CollaborationID == primaryCollaborationFromUI[0].CollaborationID && (existingprimaryCollaborationInDB.EnrollDate?.Date != primaryCollaborationFromUI[0].EnrollDate.Date || existingprimaryCollaborationInDB.EndDate.GetValueOrDefault().Date != primaryCollaborationFromUI[0].EndDate.GetValueOrDefault().Date)))
                    {
                        var primaryCollaborationToHistoryTable = existingprimaryCollaborationInDB;
                        if (existingPrimaryCollaborationInDBfromUI.Count > 0)
                        {
                            if (existingprimaryCollaborationInDB.PersonCollaborationID != primaryCollaborationFromUI[0].PersonCollaborationID && existingprimaryCollaborationInDB.EndDate != existingPrimaryCollaborationInDBfromUI[0].EndDate)
                            {
                                primaryCollaborationToHistoryTable.EndDate = existingPrimaryCollaborationInDBfromUI[0].EndDate;
                            }
                        }
                        AuditPersonProfile auditPersonProfile = new AuditPersonProfile()
                        {
                            ParentID = primaryCollaborationToHistoryTable.PersonID,
                            ChildRecordID = primaryCollaborationToHistoryTable.CollaborationID,
                            StartDate = Convert.ToDateTime(primaryCollaborationToHistoryTable.EnrollDate),
                            EndDate = primaryCollaborationToHistoryTable.EndDate,
                            TypeName = PCISEnum.AuditPersonProfileType.Collaboration,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = primaryCollaborationToHistoryTable.UpdateUserID
                        };
                        var result = this.auditPersonProfileRepository.AddPersonProfileDetailsForAudit(auditPersonProfile);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetAuditPersonProfileDetails.
        /// </summary>
        /// <param name="peopleIndex"></param>
        /// <param name="agencyID"></param>
        /// <param name="HistoryType"></param>
        /// <returns></returns>
        public AuditPersonProfileResponseDTO GetAuditPersonProfileDetails(Guid peopleIndex, long agencyID, string historyType)
        {
            try
            {
                var response = new AuditPersonProfileResponseDTO();
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                var historyDetails = new List<AuditPersonProfileDTO>();
                var person = this.personRepository.GetPerson(peopleIndex);
                if (person?.PersonID > 0)
                {
                    if (!this.personRepository.IsValidPersonInAgency(person.PersonID, person.AgencyID, agencyID))
                    {
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                        response.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                        return response;
                    }
                    if (historyType == PCISEnum.AuditPersonProfileType.Helper)
                    {
                        historyDetails = this.auditPersonProfileRepository.getHelperHistoryDetails(person.PersonID);
                    }
                    else if (historyType == PCISEnum.AuditPersonProfileType.Collaboration)
                    {
                        historyDetails = this.auditPersonProfileRepository.getCollaborationHistoryDetails(person.PersonID);
                    }
                    historyDetails = historyDetails?.OrderByDescending(x => x.AuditPersonProfileID).ToList();
                }
                response.HistoryDetails = historyDetails ?? new List<AuditPersonProfileDTO>();
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PeopleDetailsResponseDTOForExternal GetPeopleDetailsListForExternal(PersonSearchInputDTO personSearchInputDTO, LoggedInUserDTO loggedInUserDTO)
        {
            try
            {
                PeopleDetailsResponseDTOForExternal response = new PeopleDetailsResponseDTOForExternal();
                response.PeopleDataList = null;
                response.TotalCount = 0;
                if (loggedInUserDTO.AgencyId != 0)
                {
                    List<QueryFieldMappingDTO> fieldMappingList = GetPersonListConfigurationForExternal();
                    var queryBuilderDTO = this.queryBuilder.BuildQueryForExternalAPI(personSearchInputDTO, fieldMappingList);
                    List<PeopleProfileDetailsDTO> personList = new List<PeopleProfileDetailsDTO>();
                    if (queryBuilderDTO.Page <= 0)
                    {
                        response.PeopleDataList = null;
                        response.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                        response.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        return response;
                    }
                    else if (queryBuilderDTO.PageSize <= 0)
                    {
                        response.PeopleDataList = null;
                        response.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                        response.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        return response;
                    }
                    else
                    {
                        response.PeopleDataList = this.personRepository.GetPersonsDetailsListForExternal(loggedInUserDTO, queryBuilderDTO);
                        if (response.PeopleDataList.Count > 0)
                        {
                            response.TotalCount = response.PeopleDataList[0].TotalCount;
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
        private List<QueryFieldMappingDTO> GetPersonListConfigurationForExternal()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {   //Exclusively For Order By
                fieldName = "FullName",
                fieldAlias = "Name",
                fieldTable = "OrderBy",
                fieldType = "asc"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "FullName",
                fieldAlias = "Name",
                fieldTable = "P",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Email",
                fieldAlias = "Email",
                fieldTable = "P",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "UniversalID",
                fieldAlias = "ExternalId",
                fieldTable = "P",
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
                fieldName = "PersonId",
                fieldAlias = "PersonId",
                fieldTable = "P",
                fieldType = "long"
            });
            return fieldMappingList;
        }

        /// <summary>
        /// CreatePersonObjectForEditExternal
        /// </summary>
        /// <param name="personEditInputDTO">personEditInputDTO</param>
        /// <param name="loggedInUserDTO">loggedInUserDTO</param>
        /// <returns></returns>
        public PeopleEditDetailsDTO CreatePersonObjectForEditExternal(PeopleEditDetailsForExternalDTO personEditInputDTO, LoggedInUserDTO loggedInUserDTO)
        {

            PeopleEditDetailsDTO peopleEditDetailsDTO = new PeopleEditDetailsDTO();
            this.mapper.Map<PeopleEditDetailsForExternalDTO, PeopleEditDetailsDTO>(personEditInputDTO, peopleEditDetailsDTO);
            this.mapper.Map<List<PersonEditCollaborationForExternalDTO>, List<PersonEditCollaborationDTO>>(personEditInputDTO.PersonEditCollaborationDTO, peopleEditDetailsDTO.PersonEditCollaborationDTO);
            this.mapper.Map<List<PersonEditIdentificationForExternalDTO>, List<PersonEditIdentificationDTO>>(personEditInputDTO.PersonEditIdentificationDTO, peopleEditDetailsDTO.PersonEditIdentificationDTO);
            this.mapper.Map<List<PersonEditHelperForExternalDTO>, List<PersonEditHelperDTO>>(personEditInputDTO.PersonEditHelperDTO, peopleEditDetailsDTO.PersonEditHelperDTO);
            this.mapper.Map<List<PersonEditRaceEthnicityForExternalDTO>, List<PersonEditRaceEthnicityDTO>>(personEditInputDTO.PersonEditRaceEthnicityDTO, peopleEditDetailsDTO.PersonEditRaceEthnicityDTO);
            this.mapper.Map<List<PersonEditSupportForExternalDTO>, List<PersonEditSupportDTO>>(personEditInputDTO.PersonEditSupportDTO, peopleEditDetailsDTO.PersonEditSupportDTO);

            peopleEditDetailsDTO.AgencyID = loggedInUserDTO.AgencyId;
            peopleEditDetailsDTO.UpdateUserID = loggedInUserDTO.UserId;
            peopleEditDetailsDTO.PersonScreeningStatusID = 1;

            foreach (var item in peopleEditDetailsDTO.PersonEditSupportDTO)
            {
                item.IsRemoved = false;
                item.IsCurrent = item.EndDate == null || item.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;
            }

            foreach (var item in peopleEditDetailsDTO.PersonEditIdentificationDTO)
            {
                item.IsRemoved = false;
            }
            //Get the list of Active Helpers i.e helpers with EndDate >= Today under agency.
            var activeHelperList = this.lookupRepository.GetAllAgencyHelperLookup(peopleEditDetailsDTO.AgencyID);
            foreach (var item in peopleEditDetailsDTO.PersonEditHelperDTO)
            {
                item.IsRemoved = false;
                item.IsCurrent = item.EndDate == null || item.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;
                //Helper can be a Lead only if he is IsCurrent.
                item.IsLead = item.IsCurrent ? item.IsLead : false;
                //Helper can be IsCurrent only if he is active under agency.
                if (activeHelperList.Where(x => x.HelperID == item.HelperID).FirstOrDefault() == null)
                {
                    item.IsCurrent = false;
                }
            }

            //Get the list of Active Collaborations i.e collaborations with EndDate >= Today under agency.
            var activecollaborationList = this.lookupRepository.GetCollaborationLookupForOrgAdmin(peopleEditDetailsDTO.AgencyID);
            foreach (var item in peopleEditDetailsDTO.PersonEditCollaborationDTO)
            {
                item.IsRemoved = false;

                //Collaboration marked as IsCurrent = true If PersonCollaboration EndDate >= Today.
                item.IsCurrent = item.EndDate == null || item.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;

                //Collaboration can be Primary only if it is IsCurrent.
                item.IsPrimary = item.IsCurrent ? item.IsPrimary : false;

                //Collaboration can be IsCurrent only if it is active under agency.
                if (activecollaborationList.Where(x => x.CollaborationID == item.CollaborationID).FirstOrDefault() == null)
                {
                    item.IsCurrent = false;
                }
            }
            return peopleEditDetailsDTO;
        }

        /// <summary>
        /// UpdatePersonForExternal.
        /// </summary>
        /// <param name="personEditInputDTO">personEditInputDTO.</param>
        /// <returns>CRUDResponseDTOForExternalPersoneEdit.</returns>
        public CRUDResponseDTOForExternalPersoneEdit UpdatePersonForExternal(PeopleEditDetailsForExternalDTO personEditInputDTO, LoggedInUserDTO loggedInUserDTO)
        {
            try
            {
                var peopleEditDetailsDTO = CreatePersonObjectForEditExternal(personEditInputDTO, loggedInUserDTO);
                CRUDResponseDTOForExternalPersoneEdit resultDTO = new CRUDResponseDTOForExternalPersoneEdit();
                ApplyDefaultLeadHelperAndPrimaryCollabOnEdit(ref peopleEditDetailsDTO);
                var ValidationResult = ValidateExternalPersonData(CreatePersonObjectForValidatePersonDataInEdit(peopleEditDetailsDTO));
                if (!string.IsNullOrEmpty(ValidationResult))
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    resultDTO.ResponseStatus = ValidationResult;
                    return resultDTO;
                }
                var response = EditPeopleDetails(peopleEditDetailsDTO, loggedInUserDTO.AgencyId, PCISEnum.CallingType.ExternalAPI);
                resultDTO.personSupport = this.personRepository.GetPeopleSupportForExternalByPersonId(response.ResultId);
                resultDTO.personHelper = this.personRepository.GetPeopleHelperForExternalByPersonId(response.ResultId);
                resultDTO.personCollaboration = this.personRepository.GetPeopleCollaborationForExternalByPersonId(response.ResultId);
                resultDTO.ResponseStatus = response.ResponseStatus;
                resultDTO.ResponseStatusCode = response.ResponseStatusCode;
                resultDTO.Index = response.IndexId;
                resultDTO.Id = response.ResultId;
                return resultDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Add person details for external.
        /// </summary>
        /// <param name="personAddInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        public CRUDResponseDTOForExternalPersoneAdd AddPersonForExternal(PeopleAddDetailsForExternalDTO personAddInputDTO, LoggedInUserDTO loggedInUserDTO)
        {
            try
            {
                var peopleAddDetailsDTO = CreatePersonObjectForAddExternal(personAddInputDTO, loggedInUserDTO);
                CRUDResponseDTOForExternalPersoneAdd resultDTO = new CRUDResponseDTOForExternalPersoneAdd();
                ApplyDefaultLeadHelperAndPrimaryCollabOnAdd(ref peopleAddDetailsDTO);
                string ValidationResult = ValidateExternalPersonData(CreatePersonObjectForValidatePersonDataInAdd(peopleAddDetailsDTO));
                if (!string.IsNullOrEmpty(ValidationResult))
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    resultDTO.ResponseStatus = ValidationResult;
                    return resultDTO;
                }
                var response = AddPeopleDetails(peopleAddDetailsDTO);
                resultDTO.personSupport = this.personRepository.GetPeopleSupportForExternalByPersonId(response.Id);
                resultDTO.personHelper = this.personRepository.GetPeopleHelperForExternalByPersonId(response.Id);
                resultDTO.personCollaboration = this.personRepository.GetPeopleCollaborationForExternalByPersonId(response.Id);
                resultDTO.ResponseStatus = response.ResponseStatus;
                resultDTO.ResponseStatusCode = response.ResponseStatusCode;
                resultDTO.Index = response.PersonIndex;
                resultDTO.Id = response.Id;
                return resultDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// create person object for adding person from external api.
        /// </summary>
        /// <param name="personAddInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        public PeopleDetailsDTO CreatePersonObjectForAddExternal(PeopleAddDetailsForExternalDTO personAddInputDTO, LoggedInUserDTO loggedInUserDTO)
        {

            PeopleDetailsDTO peopleDetailsDTO = new PeopleDetailsDTO();
            this.mapper.Map<PeopleAddDetailsForExternalDTO, PeopleDetailsDTO>(personAddInputDTO, peopleDetailsDTO);
            this.mapper.Map<List<PersonAddCollaborationForExternalDTO>, List<PersonCollaborationDTO>>(personAddInputDTO.PersonCollaborations, peopleDetailsDTO.PersonCollaborations);
            this.mapper.Map<List<PersonAddIdentificationForExternalDTO>, List<PersonIdentificationDTO>>(personAddInputDTO.PersonIdentifications, peopleDetailsDTO.PersonIdentifications);
            this.mapper.Map<List<PersonAddHelperForExternalDTO>, List<PersonHelperDTO>>(personAddInputDTO.PersonHelpers, peopleDetailsDTO.PersonHelpers);
            this.mapper.Map<List<PersonAddRaceEthnicityForExternalDTO>, List<PersonRaceEthnicityDTO>>(personAddInputDTO.PersonRaceEthnicities, peopleDetailsDTO.PersonRaceEthnicities);
            this.mapper.Map<List<PersonAddSupportForExternalDTO>, List<PersonSupportDTO>>(personAddInputDTO.PersonSupports, peopleDetailsDTO.PersonSupports);

            peopleDetailsDTO.AgencyID = loggedInUserDTO.AgencyId;
            peopleDetailsDTO.UpdateUserID = loggedInUserDTO.UserId;
            peopleDetailsDTO.PersonScreeningStatusID = 1;
            peopleDetailsDTO.StartDate = DateTime.Now.Date;

            foreach (var item in peopleDetailsDTO.PersonSupports)
            {
                item.IsRemoved = false;
                item.IsCurrent = item.EndDate == null || item.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;
            }

            foreach (var item in peopleDetailsDTO.PersonIdentifications)
            {
                item.IsRemoved = false;
            }

            //Get active helpers with EndDate >= Today under agency.
            var activeHelperList = this.lookupRepository.GetAllAgencyHelperLookup(peopleDetailsDTO.AgencyID);
            foreach (var item in peopleDetailsDTO.PersonHelpers)
            {
                item.IsRemoved = false;
                item.IsCurrent = item.EndDate == null || item.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;
                //Helper can be a Lead only if he is IsCurrent.
                item.IsLead = item.IsCurrent ? item.IsLead : false;
                //Helper can be IsCurrent only if he is active under agency.
                if (activeHelperList.Where(x => x.HelperID == item.HelperID).FirstOrDefault() == null)
                {
                    item.IsCurrent = false;
                }
            }
            //Get active collaboration with EndDate >= Today under agency.
            var activecollaborationList = this.lookupRepository.GetCollaborationLookupForOrgAdmin(peopleDetailsDTO.AgencyID);
            foreach (var item in peopleDetailsDTO.PersonCollaborations)
            {
                item.IsRemoved = false;
                item.IsCurrent = item.EndDate == null || item.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;
                //Collaboration can be primary only if it is IsCurrent.
                item.IsPrimary = item.IsCurrent ? item.IsPrimary : false;
                //Collaboration can be IsCurrent only if it is active under agency.
                if (activecollaborationList.Where(x => x.CollaborationID == item.CollaborationID).FirstOrDefault() == null)
                {
                    item.IsCurrent = false;
                }
            }
            return peopleDetailsDTO;
        }

        public string StopSMSInvitationSending(TwilioRequest twilioRequest)
        {
            try
            {
                var stopingKey = this.GetConfigurationByName(PCISEnum.AssessmentSMS.StopKey);
                var twilioConfigNumber = this.GetConfigurationByName(PCISEnum.AssessmentSMS.ConfigKeyToFromNo);
                string response = $@"Please use {stopingKey} to unsubscribe";
                if (!string.IsNullOrEmpty(twilioRequest?.From) && twilioRequest?.Body.ToLower() == stopingKey?.Value?.ToLower() && twilioRequest.To == twilioConfigNumber?.Value)
                {
                    var phnNumber = twilioRequest.From;
                    var personsWithPhnNo = this.personRepository.GetAsync(x => x.Phone1Code + x.Phone1 == phnNumber || x.Phone2Code + x.Phone2 == phnNumber).Result.ToList();
                    foreach (var person in personsWithPhnNo)
                    {
                        person.TextPermission = false;
                        person.SMSConsentStoppedON = DateTime.UtcNow;
                    }
                    var result1 = this.personRepository.UpdateBulkPersons(personsWithPhnNo);

                    var supportPersonsWithPhnNo = this.personSupportRepository.GetAsync(x => x.PhoneCode + x.Phone == phnNumber).Result.ToList();
                    foreach (var support in supportPersonsWithPhnNo)
                    {
                        support.TextPermission = false;
                        support.SMSConsentStoppedON = DateTime.UtcNow;
                    }
                    var result2 = this.personSupportRepository.UpdateBulkPersonSupport(supportPersonsWithPhnNo);
                    if (personsWithPhnNo.Count > 0 || supportPersonsWithPhnNo.Count > 0)
                    {
                        var configUnsub = this.GetConfigurationByName(PCISEnum.AssessmentSMS.StopMessage);
                        var smsResult = this.smsSender.SendSMS(configUnsub.Value, twilioRequest.From, twilioRequest.To);
                        response = $@"Request to {twilioRequest.To} : SMS consent stopped for {twilioRequest.From} on {DateTime.UtcNow} with status : {smsResult}";
                        return response;
                    }
                    response = $@"Request to {twilioRequest.To} : SMS consent stopped for {twilioRequest.From} on {DateTime.UtcNow} with status : No persons matching";
                    return response;
                }
                response = $@"Request to {twilioRequest.To} : SMS consent stopped for {twilioRequest.From} on {DateTime.UtcNow} with status : Invalid key to stop";
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private SharedDetailsDTO GetHelperAssessmentIDs(Guid personIndex, int questionnaireId, long agencyID, int userID)
        {
            try
            {
                var sharedAssessmentIDs = string.Empty;
                SharedPersonSearchDTO sharedPersonSearchDTO = new SharedPersonSearchDTO();
                sharedPersonSearchDTO.QuestionnaireID = questionnaireId;
                sharedPersonSearchDTO.PersonIndex = personIndex;
                sharedPersonSearchDTO.LoggedInAgencyID = agencyID;
                sharedPersonSearchDTO.UserID = userID;
                sharedPersonSearchDTO.QueryType = "Notifications";
                var sharedDetails = this.personRepository.GetHelpersAssessmentIDs(sharedPersonSearchDTO);
                return sharedDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Create person object for validating persondata from external api.
        /// </summary>
        /// <param name="peopleDetailsDTO"></param>
        /// <returns></returns>
        private ValidatePersonDTOForExternal CreatePersonObjectForValidatePersonDataInAdd(PeopleDetailsDTO peopleDetailsDTO)
        {
            ValidatePersonDTOForExternal validatePersonDTOForExternal = new ValidatePersonDTOForExternal();

            this.mapper.Map<PeopleDetailsDTO, ValidatePersonDTOForExternal>(peopleDetailsDTO, validatePersonDTOForExternal);
            validatePersonDTOForExternal.PersonCollaborations = peopleDetailsDTO.PersonCollaborations;
            validatePersonDTOForExternal.PersonHelpers = peopleDetailsDTO.PersonHelpers;
            validatePersonDTOForExternal.PersonIdentifications = peopleDetailsDTO.PersonIdentifications;
            validatePersonDTOForExternal.PersonRaceEthnicities = peopleDetailsDTO.PersonRaceEthnicities;
            validatePersonDTOForExternal.PersonSupports = peopleDetailsDTO.PersonSupports;
            validatePersonDTOForExternal.AgencyID = peopleDetailsDTO.AgencyID;
            return validatePersonDTOForExternal;
        }

        /// <summary>
        /// Create person object for validating persondata from external api.
        /// </summary>
        /// <param name="peopleEditDetailsDTO"></param>
        /// <returns></returns>
        private ValidatePersonDTOForExternal CreatePersonObjectForValidatePersonDataInEdit(PeopleEditDetailsDTO peopleEditDetailsDTO)
        {
            ValidatePersonDTOForExternal validatePersonDTOForExternal = new ValidatePersonDTOForExternal();

            this.mapper.Map<PeopleEditDetailsDTO, ValidatePersonDTOForExternal>(peopleEditDetailsDTO, validatePersonDTOForExternal);
            validatePersonDTOForExternal.PersonIdentifications = this.mapper.Map<List<PersonIdentificationDTO>>(peopleEditDetailsDTO.PersonEditIdentificationDTO);
            validatePersonDTOForExternal.PersonRaceEthnicities = this.mapper.Map<List<PersonRaceEthnicityDTO>>(peopleEditDetailsDTO.PersonEditRaceEthnicityDTO);
            validatePersonDTOForExternal.PersonCollaborations = this.mapper.Map<List<PersonCollaborationDTO>>(peopleEditDetailsDTO.PersonEditCollaborationDTO);
            validatePersonDTOForExternal.PersonHelpers = this.mapper.Map<List<PersonHelperDTO>>(peopleEditDetailsDTO.PersonEditHelperDTO);
            validatePersonDTOForExternal.PersonSupports = this.mapper.Map<List<PersonSupportDTO>>(peopleEditDetailsDTO.PersonEditSupportDTO);
            validatePersonDTOForExternal.AgencyID = peopleEditDetailsDTO.AgencyID;
            return validatePersonDTOForExternal;
        }

        /// <summary>
        ///  Validate persondata from external api.
        /// </summary>
        /// <param name="peopleDetailsDTO"></param>
        /// <returns></returns>
        private string ValidateExternalPersonData(ValidatePersonDTOForExternal peopleDetailsDTO)
        {
            string ResponseStatus = string.Empty;
            List<CountryLookupDTO> countries = this.lookupRepository.GetAllCountries();
            List<LanguageDTO> languages = this.languageRepository.GetAgencyLanguageList(peopleDetailsDTO.AgencyID);
            int i = 0;
            #region CountryId
            if (peopleDetailsDTO.CountryId > 0 && peopleDetailsDTO.CountryId != null)
            {
                int index = countries.FindIndex(x => x.CountryID == peopleDetailsDTO.CountryId);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.CountryID));
                    return ResponseStatus;
                }
            }
            #endregion  CountryId

            #region CountryStateId
            if (peopleDetailsDTO.CountryStateId > 0 && peopleDetailsDTO.CountryStateId != null)
            {
                if (peopleDetailsDTO?.CountryId == 0)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.InvalidCountryIdForStatetId, PCISEnum.Parameters.CountryID));
                    return ResponseStatus;
                }
                List<CountryStateDTO> countryStates = this.lookupRepository.GetAllState().Result;
                int index = countryStates.FindIndex(x => x.CountryStateID == peopleDetailsDTO.CountryStateId && x.CountryID == peopleDetailsDTO.CountryId);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.CountryStateId));
                    return ResponseStatus;
                }
            }
            #endregion  CountryStateId

            #region DOB
            if (peopleDetailsDTO.DateOfBirth.Date > DateTime.UtcNow.Date)
            {
                ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.DateOfBirth));
                return ResponseStatus;
            }
            #endregion

            #region Email
            if (!string.IsNullOrEmpty(peopleDetailsDTO.Email))
            {
                bool isEmail = Regex.IsMatch(peopleDetailsDTO.Email, PCISEnum.InputDTOValidationPattern.Email, RegexOptions.IgnoreCase);
                if (!isEmail)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.Email));
                    return ResponseStatus;
                }
            }
            #endregion  Email

            #region GenderID
            if (peopleDetailsDTO.GenderID > 0)
            {
                var genderList = this._genderRepository.GetAgencyGenderList(peopleDetailsDTO.AgencyID);
                int index = genderList.FindIndex(x => x.GenderID == peopleDetailsDTO.GenderID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.GenderID));
                    return ResponseStatus;
                }
            }
            #endregion  GenderID

            #region BiologicalSexID
            if (peopleDetailsDTO.BiologicalSexID > 0)
            {
                List<IdentifiedGender> genderList = this.identifiedGenderRepository.GetIdentifiedGenderList(peopleDetailsDTO.AgencyID);
                int index = genderList.FindIndex(x => x.IdentifiedGenderID == peopleDetailsDTO.BiologicalSexID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.IdentificationGenderID));
                    return ResponseStatus;
                }
            }
            #endregion  IdentifiedGenderID

            #region SexualityID
            if (peopleDetailsDTO.SexualityID > 0)
            {
                var sexualityList = this._sexualityrepository.GetAgencySexuality(peopleDetailsDTO.AgencyID);
                int index = sexualityList.FindIndex(x => x.SexualityID == peopleDetailsDTO.SexualityID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.SexualityID));
                    return ResponseStatus;
                }
            }
            #endregion  SexualityID

            #region PrimaryLanguageID
            if (peopleDetailsDTO.PrimaryLanguageID > 0 && peopleDetailsDTO.PrimaryLanguageID != null)
            {
                int index = languages.FindIndex(x => x.LanguageID == peopleDetailsDTO.PrimaryLanguageID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PrimaryLanguageID));
                    return ResponseStatus;
                }
            }
            #endregion  PrimaryLanguageID

            #region PreferredLanguageID
            if (peopleDetailsDTO.PreferredLanguageID > 0 && peopleDetailsDTO.PreferredLanguageID != null)
            {
                int index = languages.FindIndex(x => x.LanguageID == peopleDetailsDTO.PreferredLanguageID);
                if (index == -1)
                {

                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PreferredLanguageID));
                    return ResponseStatus;
                }
            }
            #endregion  PreferredLanguageID

            #region PersonIdentifications

            if (peopleDetailsDTO.PersonIdentifications != null)
            {
                var identificationTypList = this._identificationrepository.GetAgencyIdentificationTypeList(peopleDetailsDTO.AgencyID);
                i = -1;
                foreach (var item in peopleDetailsDTO.PersonIdentifications)
                {
                    ++i;
                    int index = identificationTypList.FindIndex(x => x.IdentificationTypeID == item.IdentificationTypeID);
                    if (index == -1)
                    {
                        ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequireValidationInArrays, "PersonIdentifications", i, "IdentificationTypeID"));
                        return ResponseStatus;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(item.IdentificationNumber))
                        {
                            ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequireValidationInArrays, "PersonIdentifications", i, "IdentificationNumber"));
                            return ResponseStatus;
                        }
                    }
                }
            }
            #endregion  PersonIdentifications

            #region PersonRaceEthnicities

            if (peopleDetailsDTO.PersonRaceEthnicities != null)
            {
                i = -1;
                var raceEthnicityList = this._raceEthnicityRepository.GetAgencyRaceEthnicityList(peopleDetailsDTO.AgencyID);
                foreach (var item in peopleDetailsDTO.PersonRaceEthnicities)
                {
                    ++i;
                    int index = raceEthnicityList.FindIndex(x => x.RaceEthnicityID == item.RaceEthnicityID);
                    if (index == -1)
                    {
                        ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequireValidationInArrays, "PersonRaceEthnicities", i, "RaceEthnicityID"));
                        return ResponseStatus;
                    }
                }
            }
            #endregion  PersonRaceEthnicities

            #region PersonHelpers

            if (peopleDetailsDTO.PersonHelpers != null)
            {
                i = -1;
                var helperList = this.lookupRepository.GetAllAgencyHelperLookup(peopleDetailsDTO.AgencyID, false);
                int leadCount = 0;
                foreach (var item in peopleDetailsDTO.PersonHelpers)
                {
                    i++;
                    int index = helperList.FindIndex(x => x.HelperID == item.HelperID);
                    if (index == -1)
                    {
                        ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.InvalidHelperId, i));
                        return ResponseStatus;
                    }
                    //Check for helper marked as lead is active too and take the count.
                    if (item.IsLead && item.IsCurrent)
                    {
                        leadCount++;
                    }

                }
                //Return a validation if there are no active lead helpers.
                if (leadCount == 0)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.LeadHelperMissing);
                    return ResponseStatus;
                }
                //Return a validation if there are multiple helpers marked as lead.
                if (leadCount > 1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.LeadHelperError);
                    return ResponseStatus;
                }
            }
            #endregion  PersonHelpers

            #region PersonCollaborations
            if (peopleDetailsDTO.PersonCollaborations != null)
            {
                i = -1;
                var collaborationList = this.lookupRepository.GetCollaborationLookupForOrgAdmin(peopleDetailsDTO.AgencyID, false);
                int primaryCount = 0;
                foreach (var item in peopleDetailsDTO.PersonCollaborations)
                {
                    ++i;
                    int index = collaborationList.FindIndex(x => x.CollaborationID == item.CollaborationID);
                    if (index == -1)
                    {
                        ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.InvalidCollaborationId, i));
                        return ResponseStatus;
                    }
                    //Check for collaboration marked as lead is active too and take the count.
                    if (item.IsPrimary && item.IsCurrent)
                    {
                        primaryCount++;
                    }
                }
                //Return a validation if there are multiple helpers marked as lead.
                if (primaryCount > 1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.PrimaryCollaborationError);
                    return ResponseStatus;
                }
            }
            #endregion  PersonCollaborations

            #region PersonSupports

            if (peopleDetailsDTO.PersonSupports != null)
            {
                i = -1;
                var supportTypeList = this._supportTypeRepository.GetAgencySupportTypeList(peopleDetailsDTO.AgencyID);
                foreach (var item in peopleDetailsDTO.PersonSupports)
                {
                    ++i;
                    int index = supportTypeList.FindIndex(x => x.SupportTypeID == item.SupportTypeID);
                    if (index == -1)
                    {
                        ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequireValidationInArrays, "PersonSupports", i, "SupportTypeID"));
                        return ResponseStatus;
                    }

                    if (!string.IsNullOrEmpty(item.Email))
                    {
                        bool isEmail = Regex.IsMatch(item.Email, PCISEnum.InputDTOValidationPattern.Email, RegexOptions.IgnoreCase);
                        if (!isEmail)
                        {
                            ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequireValidationInArrays, "PersonSupports", i, PCISEnum.Parameters.Email));
                            return ResponseStatus;
                        }
                    }
                }
            }
            #endregion  PersonSupports

            return ResponseStatus;
        }

        /// <summary>
        /// Validate Zip and Phonecode
        /// </summary>
        /// <param name="personCountryId"></param>
        /// <param name="UScountryID"></param>
        /// <param name="personPhone1"></param>
        /// <param name="type"></param>
        /// <param name="responseStatus"></param>
        /// <returns></returns>
        private string ValidatePhoneAndZipPattern(int? personCountryId, int? UScountryID, string personPhone1, ref string responseStatus, string type = "")
        {
            try
            {
                responseStatus = string.Empty;
                if (type != PCISEnum.Parameters.Zip)
                {
                    Regex phnNumberRegexUS = new Regex(PCISEnum.InputDTOValidationPattern.PhoneNumber_US);
                    Regex phnNumberRegex = new Regex(PCISEnum.InputDTOValidationPattern.PhoneNumber);
                    if (personCountryId == UScountryID && !phnNumberRegexUS.IsMatch(personPhone1))
                    {
                        responseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, type));
                    }
                    else if (personCountryId != UScountryID && !phnNumberRegex.IsMatch(personPhone1))
                    {
                        responseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, type));
                    }
                }
                else
                {
                    Regex zipRegexUS = new Regex(PCISEnum.InputDTOValidationPattern.Zip_US);
                    Regex zipRegex = new Regex(PCISEnum.InputDTOValidationPattern.Zip);
                    if (personCountryId == UScountryID && !zipRegexUS.IsMatch(personPhone1))
                    {
                        responseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.Zip));
                    }
                    else if (personCountryId != UScountryID && !zipRegex.IsMatch(personPhone1))
                    {
                        responseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.Zip));
                    }

                }
                return responseStatus;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// ValidateExternalAPIPersonEditForPrimaryKeyIDs -To validate whether the PH IDs for edit matches those in DB
        /// </summary>
        /// <param name="peopleEditDetailsDTO"></param>
        /// <param name="personCollaborationset"></param>
        /// <param name="personSupport"></param>
        /// <param name="personHelperData"></param>
        /// <param name="personIdentificationData"></param>
        /// <param name="personRaceEthnicityData"></param>
        /// <param name="callingType"></param>
        /// <param name="resultDTO"></param>
        /// <returns></returns>
        private bool ValidateExternalAPIPersonEditForPrimaryKeyIDs(PeopleEditDetailsDTO peopleEditDetailsDTO, List<PersonCollaboration> personCollaborationset, IReadOnlyList<PersonSupportDTO> personSupport, List<PersonHelper> personHelperData, List<PersonIdentification> personIdentificationData, List<PersonRaceEthnicity> personRaceEthnicityData, string callingType, CRUDResponseDTO resultDTO)
        {
            try
            {
                if (callingType == PCISEnum.CallingType.ExternalAPI)
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    var personColaborationIDsInDB = personCollaborationset?.Select(x => x.PersonCollaborationID.ToString()).ToList();
                    var personColaborationIDsToEdit = peopleEditDetailsDTO.PersonEditCollaborationDTO?.Select(x => x.PersonCollaborationID.ToString()).ToList();
                    if (personColaborationIDsToEdit.Count > 0)
                    {
                        personColaborationIDsToEdit = personColaborationIDsToEdit.Where(x => x != "0").ToList();
                        var invalidIDs = personColaborationIDsToEdit.Where(x => !personColaborationIDsInDB.Contains(x)).ToList();
                        if (invalidIDs.Count > 0)
                        {
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(String.Format(PCISEnum.StatusMessages.InvalidPrimaryKeysInEdit, PCISEnum.Parameters.PersonCollaborationID.Replace(" ", ""), string.Join(",", invalidIDs)));
                            return false;
                        }
                    }
                    var personSupportIDsInDB = personSupport?.Select(x => x.PersonSupportID.ToString()).ToList();
                    var personSupportIDsToEdit = peopleEditDetailsDTO.PersonEditSupportDTO?.Select(x => x.PersonSupportID.ToString()).ToList();
                    if (personSupportIDsToEdit.Count > 0)
                    {
                        personSupportIDsToEdit = personSupportIDsToEdit.Where(x => x != "0").ToList();
                        var invalidIDs = personSupportIDsToEdit.Where(x => !personSupportIDsInDB.Contains(x)).ToList();
                        if (invalidIDs.Count > 0)
                        {
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(String.Format(PCISEnum.StatusMessages.InvalidPrimaryKeysInEdit, "PersonSupportID", string.Join(",", invalidIDs)));
                            return false;
                        }
                    }
                    var personHelperIDsInDB = personHelperData?.Select(x => x.PersonHelperID.ToString()).ToList();
                    var personHelperIDsToEdit = peopleEditDetailsDTO.PersonEditHelperDTO?.Select(x => x.PersonHelperID.ToString()).ToList();
                    if (personHelperIDsToEdit.Count > 0)
                    {
                        personHelperIDsToEdit = personHelperIDsToEdit.Where(x => x != "0").ToList();
                        var invalidIDs = personHelperIDsToEdit.Where(x => !personHelperIDsInDB.Contains(x)).ToList();
                        if (invalidIDs.Count > 0)
                        {
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(String.Format(PCISEnum.StatusMessages.InvalidPrimaryKeysInEdit, "PersonHelperID", string.Join(",", invalidIDs)));
                            return false;
                        }
                    }
                    var personIdentificationIDsInDB = personIdentificationData?.Select(x => x.PersonIdentificationID.ToString()).ToList();
                    var personIdentificationIDsToEdit = peopleEditDetailsDTO.PersonEditIdentificationDTO?.Select(x => x.PersonIdentificationID.ToString()).ToList();
                    if (personIdentificationIDsToEdit.Count > 0)
                    {
                        personIdentificationIDsToEdit = personIdentificationIDsToEdit.Where(x => x != "0").ToList();
                        var invalidIDs = personIdentificationIDsToEdit.Where(x => !personIdentificationIDsInDB.Contains(x)).ToList();
                        if (invalidIDs.Count > 0)
                        {
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(String.Format(PCISEnum.StatusMessages.InvalidPrimaryKeysInEdit, "PersonIdentificationID", string.Join(",", invalidIDs)));
                            return false;
                        }
                    }
                    var personRaceEthnicityIDsInDB = personRaceEthnicityData?.Select(x => x.PersonRaceEthnicityID.ToString()).ToList();
                    var personRaceEthnicityIDsToEdit = peopleEditDetailsDTO.PersonEditRaceEthnicityDTO?.Select(x => x.PersonRaceEthnicityID.ToString()).ToList();
                    if (personRaceEthnicityIDsToEdit.Count > 0)
                    {
                        personRaceEthnicityIDsToEdit = personRaceEthnicityIDsToEdit.Where(x => x != "0").ToList();
                        var invalidIDs = personRaceEthnicityIDsToEdit.Where(x => !personRaceEthnicityIDsInDB.Contains(x)).ToList();
                        if (invalidIDs.Count > 0)
                        {
                            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(String.Format(PCISEnum.StatusMessages.InvalidPrimaryKeysInEdit, "PersonRaceEthnicityID", string.Join(",", invalidIDs)));
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

        /// <summary>
        /// Getall Person Questionnaire schedules.
        /// 1.Updated for PCIS-3225 : Retrieve all last occurence reminders for scheduling the next occurence reminders by FutureReminderNotificationProcess.
        /// 2.Updated for PCIS-3226 : Optimization : Retrieve schedule for all requested personquestionnaires only.
        /// </summary>
        /// <returns>ReminderNotificationScheduleResponse.</returns>        
        public PersonQuestionnairesRegularScheduleResponse GetAllPersonQuestionnairesRegularSchedule(List<long> lst_PersonQuestionnaireIds)
        {
            try
            {
                PersonQuestionnairesRegularScheduleResponse result = new PersonQuestionnairesRegularScheduleResponse();
                var reminderLimtForFuture = this.GetConfigurationByName(PCISEnum.ConfigurationParameters.Reminder_LimitInMonth_If_EndDate_Null);
                var response = this.personQuestionnaireScheduleRepository.GetAllLatestSchedulesWithMaXDueDate(lst_PersonQuestionnaireIds);
                result.PersonQuestionnaireSchedules = response;
                result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AddBulkPersonQuestionnaireScheduleResponseDTO AddBulkPersonQuestionnaireSchedule(List<PersonQuestionnaireScheduleDTO> personQuestionnaireScheduleDTO)
        {
            try
            {

                AddBulkPersonQuestionnaireScheduleResponseDTO result = new AddBulkPersonQuestionnaireScheduleResponseDTO();
                List<PersonQuestionnaireSchedule> personQuestionnaireSchedule = new List<PersonQuestionnaireSchedule>();
                this.mapper.Map<List<PersonQuestionnaireScheduleDTO>, List<PersonQuestionnaireSchedule>>(personQuestionnaireScheduleDTO, personQuestionnaireSchedule);
                var resultSchedules = new Dictionary<Guid, long>();
                var response = this.personQuestionnaireScheduleRepository.AddBulkPersonQuestionnaireSchedule(personQuestionnaireSchedule);
                var guidList = personQuestionnaireScheduleDTO?.Select(x => x.PersonQuestionnaireScheduleIndex).ToList();
                var scheduleDetailsForIndex = this.personQuestionnaireScheduleRepository.GetAllPersonQuestionnaireScheduleID(guidList);
                if (scheduleDetailsForIndex.Count > 0)
                {
                    scheduleDetailsForIndex.ForEach(x => resultSchedules.Add(x.PersonQuestionnaireScheduleIndex ?? Guid.Empty, x.PersonQuestionnaireScheduleID));
                    result.PersonQuestionnaireSchedule = JsonConvert.SerializeObject(resultSchedules);
                }
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
        /// ApplyDefaultLeadHelperAndPrimaryCollabOnEdit. PCIS-3193 changes on updating a Person
        /// </summary>
        /// <param name="peopleEditDetailsDTO"></param>
        private void ApplyDefaultLeadHelperAndPrimaryCollabOnEdit(ref PeopleEditDetailsDTO peopleEditDetailsDTO)
        {
            try
            {
                //If only one personHelper mark it as lead. But only if he is an active current helper.
                if (peopleEditDetailsDTO.PersonEditHelperDTO.Count == 1)
                {
                    peopleEditDetailsDTO.PersonEditHelperDTO[0].IsLead = peopleEditDetailsDTO.PersonEditHelperDTO[0].IsCurrent == true ? true : false;
                }
                //If only one personCollaboration mark it as primary. But only if it is an active current collaboration.
                if (peopleEditDetailsDTO?.PersonEditCollaborationDTO?.Count == 1)
                {
                    peopleEditDetailsDTO.PersonEditCollaborationDTO[0].IsPrimary = peopleEditDetailsDTO.PersonEditCollaborationDTO[0].IsCurrent == true ? true : false;
                }

                if (peopleEditDetailsDTO.PersonEditHelperDTO.Count > 1)
                {
                    //If more than one personHelpers.Find the active current lead Helper from input details.
                    var leadHelperFromInpuDTO = peopleEditDetailsDTO.PersonEditHelperDTO.FirstOrDefault(x => x.IsLead == true && x.IsCurrent == true);
                    if (leadHelperFromInpuDTO == null)
                    {
                        //If no lead helper, Pick the active current helper with most recent startdate as lead.
                        var nextActiveHelper = peopleEditDetailsDTO.PersonEditHelperDTO.Where(x => x.IsCurrent == true).OrderByDescending(x => x.StartDate).FirstOrDefault();
                        peopleEditDetailsDTO.PersonEditHelperDTO.ForEach(x => x.IsLead = false);
                        if (nextActiveHelper != null)
                        {
                            nextActiveHelper.IsLead = true;
                        }

                    }
                }
                if (peopleEditDetailsDTO.PersonEditCollaborationDTO.Count > 1)
                {
                    //If more than one personCollaborations.Find the active current primary collaboration from input details.
                    var primaryCollabFromInpuDTO = peopleEditDetailsDTO.PersonEditCollaborationDTO.FirstOrDefault(x => x.IsPrimary == true && x.IsCurrent == true);
                    if (primaryCollabFromInpuDTO == null)
                    {
                        //If no primary collaboration, Pick the active current collaboration with most recent startdate as primary.
                        var nextActiveCollab = peopleEditDetailsDTO.PersonEditCollaborationDTO.Where(x => x.IsCurrent == true).OrderByDescending(x => x.EnrollDate).FirstOrDefault();
                        peopleEditDetailsDTO.PersonEditCollaborationDTO.ForEach(x => x.IsPrimary = false);
                        if (nextActiveCollab != null)
                        {
                            nextActiveCollab.IsPrimary = true;
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
        /// ApplyDefaultLeadHelperAndPrimaryCollabOnAdd. PCIS-3193 changes on adding a Person
        /// </summary>
        /// <param name="peopleDetailsDTO"></param>
        private void ApplyDefaultLeadHelperAndPrimaryCollabOnAdd(ref PeopleDetailsDTO peopleDetailsDTO)
        {
            try
            {
                //If only one personHelper, mark it as lead. But only if he is an active current helper.
                if (peopleDetailsDTO.PersonHelpers.Count == 1)
                {
                    peopleDetailsDTO.PersonHelpers[0].IsLead = peopleDetailsDTO.PersonHelpers[0].IsCurrent == true ? true : false;
                }
                //If only one personCollaboration, mark it as primary. But only if it is an active current collaboration.
                if (peopleDetailsDTO?.PersonCollaborations?.Count == 1)
                {
                    peopleDetailsDTO.PersonCollaborations[0].IsPrimary = peopleDetailsDTO.PersonCollaborations[0].IsCurrent == true ? true : false;
                }
                if (peopleDetailsDTO.PersonHelpers.Count > 1)
                {
                    //If more than one personHelpers.Find the active current lead Helper from input details.
                    var leadHelperFromInpuDTO = peopleDetailsDTO.PersonHelpers.FirstOrDefault(x => x.IsLead == true && x.IsCurrent == true);
                    if (leadHelperFromInpuDTO == null)
                    {
                        //If no lead helper, Pick the active current helper with most recent startdate as lead.
                        var nextActiveHelper = peopleDetailsDTO.PersonHelpers.Where(x => x.IsCurrent == true).OrderByDescending(x => x.StartDate).FirstOrDefault();
                        peopleDetailsDTO.PersonHelpers.ForEach(x => x.IsLead = false);
                        if (nextActiveHelper != null)
                        {
                            nextActiveHelper.IsLead = true;
                        }

                    }
                }
                if (peopleDetailsDTO.PersonCollaborations.Count > 1)
                {
                    //If more than one personCollaborations.Find the active current primary collaboration from input details.
                    var primaryCollabFromInpuDTO = peopleDetailsDTO.PersonCollaborations.FirstOrDefault(x => x.IsPrimary == true && x.IsCurrent == true);
                    if (primaryCollabFromInpuDTO == null)
                    {
                        //If no primary collaboration, Pick the active current collaboration with most recent startdate as primary.
                        var nextActiveCollab = peopleDetailsDTO.PersonCollaborations.Where(x => x.IsCurrent == true).OrderByDescending(x => x.EnrollDate).FirstOrDefault();
                        peopleDetailsDTO.PersonCollaborations.ForEach(x => x.IsPrimary = false);
                        if (nextActiveCollab != null)
                        {
                            nextActiveCollab.IsPrimary = true;
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
        /// SavePersonHelperDetailsForExternal
        /// </summary>
        /// <param name="personHelperDetailsDTO"></param>
        /// <param name="updateUserID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        public AddPersonHelperResponseDTOForExternal SavePersonHelperDetailsForExternal(PersonHelperAddDTOForExternal personHelperDetailsDTO, int updateUserID, long loggedInAgencyID)
        {
            try
            {

                AddPersonHelperResponseDTOForExternal resultDTO = new AddPersonHelperResponseDTOForExternal();
                PersonHelperDTO personHelperDTO = CreatePersonHelperObjectForExternal(personHelperDetailsDTO, updateUserID);
                var ValidationResult = ValidateExternalPersonHelperData(personHelperDTO, loggedInAgencyID);
                if (!string.IsNullOrEmpty(ValidationResult))
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    resultDTO.ResponseStatus = ValidationResult;
                    return resultDTO;
                }
                var personHelperFromDB = this.personHelperRepository.GetPersonHelperByDataId(personHelperDetailsDTO.PersonID);
                var previuosLead = personHelperFromDB?.Where(x => x.IsLead == true).ToList();
                if (personHelperDetailsDTO.IsLead == true && previuosLead.Count > 0)
                {
                    previuosLead.ForEach(x => x.IsLead = false);
                    this.personHelperRepository.UpdateBulkPersonHelper(previuosLead);

                }
                else
                {

                    personHelperDTO.IsLead = previuosLead.Count == 0 ? true : false;

                }
                var PersonHelperID = this.personHelperRepository.AddPersonHelper(personHelperDTO);
                resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                resultDTO.Id = PersonHelperID;
                return resultDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// ExpirePersonHelperDetailsForExternal
        /// </summary>
        /// <param name="expirePersonHelperDetailsDTO"></param>
        /// <returns></returns>
        public ExpirePersonHelperResponseDTOForExternal ExpirePersonHelperDetailsForExternal(ExpirePersonHelperAddDTOForExternal expirePersonHelperDetailsDTO)
        {
            try
            {

                ExpirePersonHelperResponseDTOForExternal resultDTO = new ExpirePersonHelperResponseDTOForExternal();
                PersonHelper personHelper = new PersonHelper();
                List<PersonHelper> personHelperTobeUpdate = new List<PersonHelper>();
                var ValidationResult = ValidateExternalExpirePersonHelperData(expirePersonHelperDetailsDTO.PersonHelperID, expirePersonHelperDetailsDTO.EndDate, ref personHelper);
                if (!string.IsNullOrEmpty(ValidationResult))
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    resultDTO.ResponseStatus = ValidationResult;
                    return resultDTO;
                }
                var personHelperFromDB = this.personHelperRepository.GetPersonHelperByDataId(personHelper.PersonID);
                if (personHelper.IsLead == true && expirePersonHelperDetailsDTO.EndDate.Date <= DateTime.UtcNow.Date.Date)
                {

                    personHelper.IsCurrent = false;
                    personHelper.IsLead = false;
                    //find lastest start date and set that to is lead true
                    var nextActiveHelper = personHelperFromDB?.Where(x => x.IsCurrent == true && x.PersonHelperID != personHelper.PersonHelperID).OrderByDescending(x => x.StartDate).FirstOrDefault();
                    if (nextActiveHelper != null)
                    {
                        nextActiveHelper.IsLead = true;
                        personHelperTobeUpdate.Add(nextActiveHelper);

                    }

                }
                personHelper.EndDate = expirePersonHelperDetailsDTO.EndDate.Date;
                personHelper.IsCurrent = expirePersonHelperDetailsDTO.EndDate.Date >= DateTime.UtcNow.Date.Date ? true : false;
                personHelper.IsLead = personHelper.IsCurrent == true && personHelperFromDB?.Count == 1 ? true : false;
                personHelperTobeUpdate.Add(personHelper);
                this.personHelperRepository.UpdateBulkPersonHelper(personHelperTobeUpdate);
                resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                return resultDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// ValidateExternalPersonHelperData
        /// </summary>
        /// <param name="peopleHelperDetailsDTO"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        private string ValidateExternalPersonHelperData(PersonHelperDTO peopleHelperDetailsDTO, long loggedInAgencyID)
        {
            string ResponseStatus = string.Empty;
            #region personID
            if (peopleHelperDetailsDTO.PersonID > 0)
            {
                var person = this.personRepository.GetPersonByPersonId(peopleHelperDetailsDTO.PersonID, loggedInAgencyID);
                if (person?.PersonID <= 0)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PersonId));
                    return ResponseStatus;
                }
            }
            #endregion  personID
            #region HelperID
            if (peopleHelperDetailsDTO.HelperID > 0)
            {
                var helperList = this.lookupRepository.GetAllAgencyHelperLookup(loggedInAgencyID, true);
                int index = helperList.FindIndex(x => x.HelperID == peopleHelperDetailsDTO.HelperID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.HelperID));
                    return ResponseStatus;
                }
            }
            #endregion  HelperID
            #region CollaborationID
            if (peopleHelperDetailsDTO.CollaborationID != null)
            {

                var responseCollaborationList = this.personRepository.GetPeopleCollaborationList(peopleHelperDetailsDTO.PersonID, loggedInAgencyID);
                int index = responseCollaborationList.FindIndex(x => x.CollaborationID == peopleHelperDetailsDTO.CollaborationID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.CollaborationID));
                    return ResponseStatus;
                }
            }
            #endregion  CollaborationID
            return ResponseStatus;
        }
        /// <summary>
        /// ValidateExternalExpirePersonHelperData
        /// </summary>
        /// <param name="personHelperID"></param>
        /// <param name="personHelper"></param>
        /// <returns></returns>
        private string ValidateExternalExpirePersonHelperData(long personHelperID, DateTime EndDate, ref PersonHelper personHelper)
        {
            string ResponseStatus = string.Empty;
            #region PersonHelperID
            if (personHelperID > 0)
            {
                personHelper = this.personHelperRepository.GetPersonHelperByPersonHelperId(personHelperID);

                if (personHelper == null)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PersonHelperID));
                    return ResponseStatus;
                }
            }
            #endregion  PersonHelperID

            if (personHelper.StartDate > EndDate.Date)
            {
                ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.EndDate));
                return ResponseStatus;
            }
            return ResponseStatus;
        }
        /// <summary>
        /// CreatePersonHelperObjectForExternal
        /// </summary>
        /// <param name="personHelperDetailsDTO"></param>
        /// <param name="updateUserID"></param>
        /// <returns></returns>
        private PersonHelperDTO CreatePersonHelperObjectForExternal(PersonHelperAddDTOForExternal personHelperDetailsDTO, int updateUserID)
        {
            PersonHelperDTO personHelperDTO = new PersonHelperDTO();

            if (personHelperDetailsDTO.HelperID == 0 || updateUserID == 0 || personHelperDetailsDTO.PersonID == 0)
            {
                return null;
            }
            else
            {

                personHelperDTO.PersonID = personHelperDetailsDTO.PersonID;
                personHelperDTO.UpdateUserID = updateUserID;
                personHelperDTO.HelperID = personHelperDetailsDTO.HelperID;
                personHelperDTO.UpdateDate = DateTime.UtcNow;
                personHelperDTO.StartDate = personHelperDetailsDTO.StartDate.Date;
                personHelperDTO.IsLead = personHelperDetailsDTO.IsLead;
                personHelperDTO.IsCurrent = true;
                personHelperDTO.IsRemoved = false;
                personHelperDTO.CollaborationID = personHelperDetailsDTO.CollaborationID <= 0 ? null : personHelperDetailsDTO.CollaborationID;

                return personHelperDTO;
            }
        }

        /// <summary>
        /// SavePersonCollaborationDetailsForExternal
        /// </summary>
        /// <param name="PersonCollaborationDetailsDTO"></param>
        /// <param name="updateUserID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        public AddPersonCollaborationResponseDTOForExternal SavePersonCollaborationDetailsForExternal(PersonCollaborationAddDTOForExternal PersonCollaborationDetailsDTO, int updateUserID, long loggedInAgencyID)
        {
            try
            {

                AddPersonCollaborationResponseDTOForExternal resultDTO = new AddPersonCollaborationResponseDTOForExternal();
                PersonCollaborationDTO personCollaboratioDTO = CreatePersonCollaborationObjectForExternal(PersonCollaborationDetailsDTO, updateUserID);
                var ValidationResult = ValidateExternalPersonCollaborationData(personCollaboratioDTO, loggedInAgencyID);
                if (!string.IsNullOrEmpty(ValidationResult))
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    resultDTO.ResponseStatus = ValidationResult;
                    return resultDTO;
                }
                var personCollaborationFromDB = this.personCollaborationRepository.GetPersonCollaborationByDataId(PersonCollaborationDetailsDTO.PersonID);
                var previuosIsPrimary = personCollaborationFromDB?.Where(x => x.IsPrimary == true).ToList();
                if (PersonCollaborationDetailsDTO.isPrimary == true && previuosIsPrimary.Count > 0)
                {
                    previuosIsPrimary.ForEach(x => x.IsPrimary = false);
                    this.personCollaborationRepository.UpdateBulkPersonCollaboration(previuosIsPrimary);

                }
                else
                {

                    personCollaboratioDTO.IsPrimary = previuosIsPrimary.Count == 0 ? true : false;

                }
                var PersonCollaborationID = this.personCollaborationRepository.AddPersonCollaboration(personCollaboratioDTO);
                resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                resultDTO.Id = PersonCollaborationID;
                return resultDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// ValidateExternalPersonCollaborationData
        /// </summary>
        /// <param name="peopleCollaborationDetailsDTO"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        private string ValidateExternalPersonCollaborationData(PersonCollaborationDTO peopleCollaborationDetailsDTO, long loggedInAgencyID)
        {
            string ResponseStatus = string.Empty;
            #region personID
            if (peopleCollaborationDetailsDTO.PersonID > 0)
            {
                var person = this.personRepository.GetPersonByPersonId(peopleCollaborationDetailsDTO.PersonID, loggedInAgencyID);
                if (person?.PersonID <= 0)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PersonId));
                    return ResponseStatus;
                }
            }
            #endregion  personID

            #region CollaborationID
            if (peopleCollaborationDetailsDTO.CollaborationID > 0)
            {

                var collaborationList = this.lookupRepository.GetCollaborationLookupForOrgAdmin(loggedInAgencyID, true);

                int index = collaborationList.FindIndex(x => x.CollaborationID == peopleCollaborationDetailsDTO.CollaborationID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.CollaborationID));
                    return ResponseStatus;
                }
            }
            #endregion  CollaborationID
            return ResponseStatus;
        }

        /// <summary>
        /// ExpirePersonCollaborationDetailsForExternal
        /// </summary>
        /// <param name="expirePersonCollaborationDetailsDTO"></param>
        /// <returns></returns>
        public ExpirePersonCollaborationResponseDTOForExternal ExpirePersonCollaborationDetailsForExternal(ExpirePersonCollaborationAddDTOForExternal expirePersonCollaborationDetailsDTO)
        {
            try
            {

                ExpirePersonCollaborationResponseDTOForExternal resultDTO = new ExpirePersonCollaborationResponseDTOForExternal();
                PersonCollaboration personCollaboration = new PersonCollaboration();
                List<PersonCollaboration> personCollaborationTobeUpdate = new List<PersonCollaboration>();
                var ValidationResult = ValidateExternalExpirePersonCollaborationData(expirePersonCollaborationDetailsDTO.PersonCollaborationID, expirePersonCollaborationDetailsDTO.EndDate, ref personCollaboration);
                if (!string.IsNullOrEmpty(ValidationResult))
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    resultDTO.ResponseStatus = ValidationResult;
                    return resultDTO;
                }
                var personCollaborationFromDB = this.personCollaborationRepository.GetPersonCollaborationByDataId(personCollaboration.PersonID);


                if (personCollaboration.IsPrimary == true && expirePersonCollaborationDetailsDTO.EndDate.Date <= DateTime.UtcNow.Date.Date)
                {

                    personCollaboration.IsCurrent = false;
                    personCollaboration.IsPrimary = false;
                    //find lastest start date and set that to is primary true
                    var nextActiveCollaborationPrimary = personCollaborationFromDB?.Where(x => x.IsCurrent == true && x.PersonCollaborationID != personCollaboration.PersonCollaborationID).OrderByDescending(x => x.EnrollDate).FirstOrDefault();

                    if (nextActiveCollaborationPrimary != null)
                    {
                        nextActiveCollaborationPrimary.IsPrimary = true;
                        personCollaborationTobeUpdate.Add(nextActiveCollaborationPrimary);

                    }

                }

                personCollaboration.EndDate = expirePersonCollaborationDetailsDTO.EndDate.Date;
                personCollaboration.IsCurrent = expirePersonCollaborationDetailsDTO.EndDate.Date >= DateTime.UtcNow.Date.Date ? true : false;
                personCollaboration.IsPrimary = personCollaboration.IsCurrent == true && personCollaborationFromDB?.Count == 1 ? true : false;
                personCollaborationTobeUpdate.Add(personCollaboration);
                this.personCollaborationRepository.UpdateBulkPersonCollaboration(personCollaborationTobeUpdate);


                resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);

                return resultDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// ValidateExternalExpirePersonCollaborationData
        /// </summary>
        /// <param name="personCollaborationID"></param>
        /// <param name="personCollaboration"></param>
        /// <returns></returns>
        private string ValidateExternalExpirePersonCollaborationData(long personCollaborationID, DateTime EndDate, ref PersonCollaboration personCollaboration)
        {
            string ResponseStatus = string.Empty;
            #region PersonCollaborationId
            if (personCollaborationID > 0)
            {
                personCollaboration = this.personCollaborationRepository.GetPersonCollaborationByPersonCollaborationId(personCollaborationID);

                if (personCollaboration == null)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PersonCollaborationId));
                    return ResponseStatus;
                }
            }
            #endregion  PersonCollaborationId


            if (personCollaboration.EnrollDate > EndDate.Date)
            {
                ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.EndDate));
                return ResponseStatus;
            }
            return ResponseStatus;
        }
        /// <summary>
        /// CreatePersonCollaborationObjectForExternal
        /// </summary>
        /// <param name="personCollaborationDetailsDTO"></param>
        /// <param name="UpdateUserID"></param>
        /// <returns></returns>
        private PersonCollaborationDTO CreatePersonCollaborationObjectForExternal(PersonCollaborationAddDTOForExternal personCollaborationDetailsDTO, int UpdateUserID)
        {
            PersonCollaborationDTO personCollaborationDTO = new PersonCollaborationDTO();

            if (personCollaborationDetailsDTO.CollaborationID == 0 || UpdateUserID == 0 || personCollaborationDetailsDTO.PersonID == 0)
            {
                return null;
            }
            else
            {
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());

                personCollaborationDTO.PersonID = personCollaborationDetailsDTO.PersonID;
                personCollaborationDTO.UpdateUserID = UpdateUserID;
                personCollaborationDTO.EnrollDate = personCollaborationDetailsDTO.EnrollDate.Date;
                personCollaborationDTO.CollaborationID = personCollaborationDetailsDTO.CollaborationID;
                personCollaborationDTO.UpdateDate = DateTime.UtcNow;
                personCollaborationDTO.IsPrimary = personCollaborationDetailsDTO.isPrimary;
                personCollaborationDTO.IsCurrent = true;
                personCollaborationDTO.IsRemoved = false;

                return personCollaborationDTO;


            }
        }

        /// <summary>
        /// GetAllPersonCollaborationForReminders.
        /// </summary>
        /// <param name="list_personCollaborationIds"></param>
        /// <returns></returns>
        public PersonCollaborationResponseDTO GetAllPersonCollaborationForReminders(List<long> list_personCollaborationIds)
        {
            try
            {
                PersonCollaborationResponseDTO resultDTO = new PersonCollaborationResponseDTO();
                var personCollaboration = this.collaborationQuestionnaireRepository.GetAllPersonCollaborationForReminders(list_personCollaborationIds);
                resultDTO.PersonCollaborations = personCollaboration;
                resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return resultDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// AddPersonSupportDetailsForExternal.
        /// </summary>
        /// <param name="addPersonSupportInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        public CRUDResponseDTOForExternalPersonSupport AddPersonSupportDetailsForExternal(AddPersonSupportDTOForExternal addPersonSupportInputDTO, LoggedInUserDTO loggedInUserDTO)
        {
            try
            {
                PersonSupportDTO personSupportDTO = new PersonSupportDTO();
                this.mapper.Map<AddPersonSupportDTOForExternal, PersonSupportDTO>(addPersonSupportInputDTO, personSupportDTO);
                return AddOrEditPersonSupportForExternal(personSupportDTO, loggedInUserDTO, PCISEnum.Constants.Add);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// EditPersonSupportDetailsForExternal.
        /// </summary>
        /// <param name="editPersonSupportInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        public CRUDResponseDTOForExternalPersonSupport EditPersonSupportDetailsForExternal(EditPersonSupportDTOForExternal editPersonSupportInputDTO, LoggedInUserDTO loggedInUserDTO)
        {
            try
            {

                //var personSupportDTO = this.personSupportRepository.GetPersonSupportDetails(editPersonSupportInputDTO.PersonSupportID);
                PersonSupportDTO personSupportDTO = new PersonSupportDTO();
                this.mapper.Map<EditPersonSupportDTOForExternal, PersonSupportDTO>(editPersonSupportInputDTO, personSupportDTO);
                return AddOrEditPersonSupportForExternal(personSupportDTO, loggedInUserDTO, PCISEnum.Constants.Edit);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// AddOrEditPersonSupportForExternal.
        /// Common function for add and edit of personsupport.
        /// </summary>
        /// <param name="personSupportDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <param name="CallingType"></param>
        /// <returns></returns>
        private CRUDResponseDTOForExternalPersonSupport AddOrEditPersonSupportForExternal(PersonSupportDTO personSupportDTO, LoggedInUserDTO loggedInUserDTO, string CallingType)
        {
            try
            {
                CRUDResponseDTOForExternalPersonSupport resultDTO = new CRUDResponseDTOForExternalPersonSupport();
                personSupportDTO.EndDate = personSupportDTO.EndDate.HasValue ? personSupportDTO.EndDate.Value.Date : (DateTime?)null;
                string ValidationResult = ValidateExternalPersonSupportData(ref personSupportDTO, loggedInUserDTO.AgencyId);
                if (!string.IsNullOrEmpty(ValidationResult))
                {
                    resultDTO.ResponseStatusCode = CallingType == PCISEnum.Constants.Add ? PCISEnum.StatusCodes.insertionFailed : PCISEnum.StatusCodes.UpdationFailed;
                    resultDTO.ResponseStatus = ValidationResult;
                    return resultDTO;
                }
                //If Endate null then keep it as null itself and Iscurrent = true;.
                //If Endate is a future date then Iscurrent = true;
                //If Endate date less than currentdate mark as past support(ie IsCurrent = false)
                personSupportDTO.IsCurrent = personSupportDTO.EndDate == null || personSupportDTO.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;
                personSupportDTO.IsRemoved = false;
                personSupportDTO.UpdateDate = DateTime.UtcNow;
                personSupportDTO.UpdateUserID = loggedInUserDTO.UserId;
                var personSupportID = 0;
                if (CallingType == PCISEnum.Constants.Add)
                {
                    personSupportID = this.personSupportRepository.AddPersonSupport(personSupportDTO);
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                }
                else
                {
                    personSupportID = this.personSupportRepository.UpdatePersonSupport(personSupportDTO).PersonSupportID;
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                }
                resultDTO.Id = personSupportID;
                return resultDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// ValidateExternalPersonSupportData.
        /// </summary>
        /// <param name="personSupportDTO"></param>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        private string ValidateExternalPersonSupportData(ref PersonSupportDTO personSupportDTO, long agencyId)
        {
            try
            {
                var responseStatus = string.Empty;
                #region personID
                if (personSupportDTO.PersonID > 0)
                {
                    var person = this.personRepository.GetPersonByPersonId(personSupportDTO.PersonID, agencyId);
                    if (person?.PersonID == 0)
                    {
                        responseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PersonId));
                        return responseStatus;
                    }
                }
                #endregion personID

                #region personSupportID
                if (personSupportDTO.PersonSupportID > 0)
                {
                    var personSupport = this.personSupportRepository.GetPersonSupportDetails(personSupportDTO.PersonSupportID);
                    if (personSupport == null || personSupport?.PersonSupportID == 0 || personSupport?.PersonID != personSupportDTO.PersonID)
                    {
                        responseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PersonSupportID));
                        return responseStatus;
                    }
                }
                #endregion personID

                #region SupportType
                var supportTypeID = personSupportDTO.SupportTypeID;
                var supportTypeList = this._supportTypeRepository.GetAgencySupportTypeList(agencyId);
                int? index = supportTypeList?.FindIndex(x => x.SupportTypeID == supportTypeID) ?? -1;
                if (index == -1)
                {
                    responseStatus = this.localize.GetLocalizedHtmlString(String.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.SupportTypeID));
                    return responseStatus;
                }
                #endregion

                #region Email
                if (!string.IsNullOrWhiteSpace(personSupportDTO.Email))
                {
                    bool isEmail = Regex.IsMatch(personSupportDTO.Email, PCISEnum.InputDTOValidationPattern.Email, RegexOptions.IgnoreCase);
                    if (!isEmail)
                    {

                        responseStatus = this.localize.GetLocalizedHtmlString(String.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.Email));
                        return responseStatus;
                    }
                }
                #endregion

                #region DateValid
                if (personSupportDTO.EndDate != null && personSupportDTO.StartDate.Date > personSupportDTO.EndDate.Value.Date)
                {
                    responseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.EndDate));
                    return responseStatus;
                }
                #endregion

                #region TextPermission
                //If phonenumber is empty keep it false
                if (personSupportDTO.TextPermission)
                {
                    bool isValidPhoneNumber = string.IsNullOrWhiteSpace(personSupportDTO.Phone) ? false : true;
                    if (!isValidPhoneNumber)
                    {
                        personSupportDTO.TextPermission = false;
                    }
                }
                #endregion

                #region EmailPermission
                //If Email is empty keep it false
                if (personSupportDTO.EmailPermission)
                {
                    bool isValidEmail = string.IsNullOrWhiteSpace(personSupportDTO.Email) ? false : true;
                    if (!isValidEmail)
                    {
                        personSupportDTO.EmailPermission = false;
                    }
                }
                #endregion

                return responseStatus;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// ExpirePersonSupportForExternal.
        /// </summary>
        /// <param name="expirePersonSupportInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        public CRUDResponseDTOForExternalPersonSupport ExpirePersonSupportForExternal(ExpirePersonSupportDTOForExternal expirePersonSupportInputDTO, LoggedInUserDTO loggedInUserDTO)
        {
            try
            {
                CRUDResponseDTOForExternalPersonSupport resultDTO = new CRUDResponseDTOForExternalPersonSupport();
                var personSupportDTO = this.personSupportRepository.GetPersonSupportDetails(expirePersonSupportInputDTO.PersonSupportID);
                if (personSupportDTO == null || personSupportDTO?.PersonSupportID == 0)
                {
                    var responseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PersonSupportID));
                    resultDTO.ResponseStatus = responseStatus;
                    return resultDTO;
                }
                //If null then currentdate
                //If a future date then Iscurrent = true;
                //If a date less than currentdate mark as past support(ie IsCurrent = false)
                personSupportDTO.EndDate = expirePersonSupportInputDTO.EndDate == null ? DateTime.UtcNow.Date : expirePersonSupportInputDTO.EndDate.Value.Date;
                personSupportDTO.IsCurrent = expirePersonSupportInputDTO.EndDate == null || expirePersonSupportInputDTO.EndDate.Value.Date >= DateTime.UtcNow.Date ? true : false;
                personSupportDTO.UpdateDate = DateTime.UtcNow;
                personSupportDTO.UpdateUserID = loggedInUserDTO.UserId;
                personSupportDTO.IsRemoved = expirePersonSupportInputDTO.IsRemoved;

                var personSupportID = this.personSupportRepository.UpdatePersonSupport(personSupportDTO).PersonSupportID;
                resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                resultDTO.Id = personSupportID;
                return resultDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetAllPersonQuestionnairesToBeScheduled.
        /// </summary>
        /// <returns></returns>
        public PersonQuestionnairesRegularScheduleResponse GetAllPersonQuestionnairesToBeScheduled()
        {
            try
            {
                PersonQuestionnairesRegularScheduleResponse response = new PersonQuestionnairesRegularScheduleResponse();
                var result = this.personQuestionnaireScheduleRepository.GetAllPersonQuestionnairesToBeScheduled();
                if (result.Count > 0)
                {
                    response.list_PersonQuestionnaireIds = result?.Select(x => x.PersonQuestionnaireID).Distinct().ToList();
                    response.list_QuestionnaireIds = result?.Select(x => x.QuestionnaireID).Distinct().ToList();
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

        /// <summary>
        /// GetPeopleSupportListForExternal.
        /// </summary>
        /// <param name="personSearchInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        public PersonSupportResponseDTOForExternal GetPeopleSupportListForExternal(PersonSupportSearchInputDTO personSearchInputDTO, LoggedInUserDTO loggedInUserDTO)
        {
            try
            {

                PersonSupportResponseDTOForExternal response = new PersonSupportResponseDTOForExternal();
                response.PersonSupportDataList = null;
                response.TotalCount = 0;
                if (loggedInUserDTO.AgencyId != 0)
                {
                    List<QueryFieldMappingDTO> fieldMappingList = GetPersonSupportListConfigurationForExternal();
                    var queryBuilderDTO = this.queryBuilder.BuildQueryForExternalAPI(personSearchInputDTO, fieldMappingList);
                    List<PeopleProfileDetailsDTO> personList = new List<PeopleProfileDetailsDTO>();
                    if (queryBuilderDTO.Page <= 0)
                    {
                        response.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                        response.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        return response;
                    }
                    else if (queryBuilderDTO.PageSize <= 0)
                    {
                        response.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                        response.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        return response;
                    }
                    else
                    {
                        response.PersonSupportDataList = this.personSupportRepository.GetPersonSupportListForExternal(loggedInUserDTO, queryBuilderDTO);
                        if (response.PersonSupportDataList.Count > 0)
                        {
                            response.TotalCount = response.PersonSupportDataList[0].TotalCount;
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
        /// GetPersonSupportListConfigurationForExternal.
        /// Always the First item to the list should be the column deatils for order by (With fieldTable as OrderBy for just user identification).
        /// And the next item should be the fieldMapping for order by Column specified above.
        /// </summary>
        /// <returns></returns>
        private List<QueryFieldMappingDTO> GetPersonSupportListConfigurationForExternal()
        {
            try
            {

                List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
                fieldMappingList.Add(new QueryFieldMappingDTO
                {   //Exclusively For Order By
                    fieldName = "PersonSupportId",
                    fieldAlias = "PersonSupportId",
                    fieldTable = "OrderBy",
                    fieldType = "desc"
                });
                fieldMappingList.Add(new QueryFieldMappingDTO
                {
                    fieldName = "PersonSupportId",
                    fieldAlias = "PersonSupportId",
                    fieldTable = "PS",
                    fieldType = "int"
                });
                fieldMappingList.Add(new QueryFieldMappingDTO
                {
                    fieldName = "FullName",
                    fieldAlias = "Name",
                    fieldTable = "PS",
                    fieldType = "string"
                });
                fieldMappingList.Add(new QueryFieldMappingDTO
                {
                    fieldName = "Email",
                    fieldAlias = "Email",
                    fieldTable = "PS",
                    fieldType = "string"
                });
                fieldMappingList.Add(new QueryFieldMappingDTO
                {
                    fieldName = "PersonId",
                    fieldAlias = "PersonId",
                    fieldTable = "PS",
                    fieldType = "long"
                });
                fieldMappingList.Add(new QueryFieldMappingDTO
                {
                    fieldName = "PersonIndex",
                    fieldAlias = "PersonIndex",
                    fieldTable = "PS",
                    fieldType = "string"
                });
                return fieldMappingList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
