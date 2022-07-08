// -----------------------------------------------------------------------
// <copyright file="ConsumerService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.Services
{
    public class LookupService : BaseService, ILookupService
    {
        private readonly ILookupRepository _lookupRepository;
        private readonly IAgencyRepository _agencyRepository;

        private readonly ILogger<LookupService> _logger;
        private readonly IGenderRepository _genderRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ISexualityRepository _sexualityrepository;
        private readonly IRaceEthnicityRepository _raceEthnicityRepository;
        private readonly IIdentificationTypeRepository _identificationrepository;
        private readonly ISupportTypeRepository _supportTypeRepository;
        private readonly ICollaborationRepository _collaborationRepository;
        private readonly IHelperTitleRepository _helperTitleRepository;
        private readonly ISharingPolicyRepository _sharingPolicyRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IPersonQuestionnaireRepository _personQuestionnaireRepository;
        private readonly IQuestionnaireRepository _questionnaireRepository;
        private readonly IAssessmentReasonRepository _assessmentReasonRepository;
        private readonly IVoiceTypeRepository _voiceTypeRepository;
        private readonly INotificationTypeRepository _notificationTypeRepository;
        private readonly IMapper mapper;
        private readonly IHelperRepository _helperRepository;
        private readonly IHttpContextAccessor httpContext;
        private readonly IIdentifiedGenderRepository identifiedGenderRepository;
        private readonly IAgencySharingPolicyRepository agecySharingPolicyRepository;
        private readonly ICollaborationSharingPolicyRepository collaborationSharingPolicyRepository;
        private readonly IConfigurationRepository configurationRepository;
        private readonly IEmailDetailRepository emailDetailRepository;
        private readonly IQuestionnaireReminderTypeRepository questionnaireReminderTypeRepository;
        private readonly IAssessmentStatusRepository assessmentStatusRepository;
        private readonly INotificationLevelRepository notificationLevelRepository;
        private readonly IResponseRepository responseRepository;
        private readonly IBackgroundProcessLogRepository backgroundProcessLogRepository;
        private readonly IReminderInviteToCompleteRepository reminderInviteToCompleteRepository;
        private readonly INotifyReminderRepository notifyReminderRepository;


        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;
        /// <summary>
        /// Initializes a new instance of the <see cref="LookupService"/> class.
        /// </summary>
        /// <param name="LookupRepository">LookupRepository.</param>
        public LookupService(IBackgroundProcessLogRepository backgroundProcessLogRepository, IResponseRepository responseRepository, INotificationLevelRepository notificationLevelRepository, IQuestionnaireReminderTypeRepository questionnaireReminderTypeRepository, IEmailDetailRepository emailDetailRepository, ICollaborationSharingPolicyRepository collaborationSharingPolicyRepository, IAgencySharingPolicyRepository agecySharingPolicyRepository, IHelperRepository helperRepository, ISupportTypeRepository supportTypeRepository, ICollaborationRepository collaborationRepository, IIdentificationTypeRepository identificationrepository, IAgencyRepository agencyRepository, ILookupRepository lookupRepository, IGenderRepository genderRepository, ILanguageRepository languageRepository,
            ISexualityRepository sexualityrepository, IHelperTitleRepository helperTitleRepository,
            IRaceEthnicityRepository raceEthnicityRepository, ILogger<LookupService> logger,
            ISharingPolicyRepository sharingPolicyRepository, IPersonRepository personRepository, IPersonQuestionnaireRepository personQuestionnaireRepository, IQuestionnaireRepository questionnaireRepository,
            IAssessmentReasonRepository assessmentReasonRepository, IVoiceTypeRepository voiceTypeRepository, IMapper mapper, LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, INotificationTypeRepository notificationTypeRepository, IIdentifiedGenderRepository identifiedGenderRepository, IAssessmentStatusRepository assessmentStatusRepository, IReminderInviteToCompleteRepository reminderInviteToCompleteRepository, INotifyReminderRepository notifyReminderRepository)
            : base(configRepo, httpContext)
        {
            this._lookupRepository = lookupRepository;
            this._sexualityrepository = sexualityrepository;
            this._raceEthnicityRepository = raceEthnicityRepository;
            this._languageRepository = languageRepository;
            this._genderRepository = genderRepository;
            this._agencyRepository = agencyRepository;
            this._collaborationRepository = collaborationRepository;
            this._identificationrepository = identificationrepository;
            this._supportTypeRepository = supportTypeRepository;
            this._logger = logger;
            this._helperTitleRepository = helperTitleRepository;
            this._sharingPolicyRepository = sharingPolicyRepository;
            this._personRepository = personRepository;
            this._personQuestionnaireRepository = personQuestionnaireRepository;
            this._questionnaireRepository = questionnaireRepository;
            this._assessmentReasonRepository = assessmentReasonRepository;
            this._voiceTypeRepository = voiceTypeRepository;
            this._notificationTypeRepository = notificationTypeRepository;
            this.mapper = mapper;
            this.localize = localizeService;
            this._helperRepository = helperRepository;
            this.httpContext = httpContext;
            this.identifiedGenderRepository = identifiedGenderRepository;
            this.agecySharingPolicyRepository = agecySharingPolicyRepository;
            this.collaborationSharingPolicyRepository = collaborationSharingPolicyRepository;
            this.configurationRepository = configRepo;
            this.emailDetailRepository = emailDetailRepository;
            this.questionnaireReminderTypeRepository = questionnaireReminderTypeRepository;
            this.assessmentStatusRepository = assessmentStatusRepository;
            this.notificationLevelRepository = notificationLevelRepository;
            this.responseRepository = responseRepository;
            this.backgroundProcessLogRepository = backgroundProcessLogRepository;
            this.reminderInviteToCompleteRepository = reminderInviteToCompleteRepository;
            this.notifyReminderRepository = notifyReminderRepository;
        }
        /// <summary>
        /// To get all states.
        /// </summary>
        /// <returns>.</returns>
        public CountryStateResponseDTO GetAllCountryState()
        {
            try
            {
                CountryStateResponseDTO countryStateResponseDTO = new CountryStateResponseDTO();
                countryStateResponseDTO.countryStates = this._lookupRepository.GetAllState().Result;
                countryStateResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                countryStateResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return countryStateResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get all countries.
        /// </summary>
        /// <returns>.</returns>
        public CountryLookupResponseDTO GetAllCountries()
        {
            try
            {
                CountryLookupResponseDTO countryLookupResponseDTO = new CountryLookupResponseDTO();
                countryLookupResponseDTO.countries = this._lookupRepository.GetAllCountries();
                countryLookupResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                countryLookupResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return countryLookupResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// GetAllAgencyLookup.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>AgencyLookupResponseDTO</returns>
        public AgencyLookupResponseDTO GetAllAgencyLookup(long agencyID)
        {
            try
            {
                AgencyLookupResponseDTO AgencyLookupResponseDTO = new AgencyLookupResponseDTO();
                var agencylist = this._agencyRepository.GetAgencyLookupWithID(agencyID);
                AgencyLookupResponseDTO.AgencyLookup = agencylist;
                AgencyLookupResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                AgencyLookupResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return AgencyLookupResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllRolesLookup.
        /// </summary>
        /// <returns>RoleLookupResponseDTO.</returns>
        public RoleLookupResponseDTO GetAllRolesLookup()
        {
            try
            {
                RoleLookupResponseDTO RoleLookupResponsDTO = new RoleLookupResponseDTO();
                var response = this._lookupRepository.GetAllRolesLookup();
                RoleLookupResponsDTO.RoleLookup = response;
                RoleLookupResponsDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                RoleLookupResponsDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;

                return RoleLookupResponsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllHelperLookup.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>RoleLookupResponseDTO.</returns>
        public HelperLookupResponseDTO GetAllHelperLookup(long agencyID)
        {
            try
            {
                HelperLookupResponseDTO helperLookupResponseDTO = new HelperLookupResponseDTO();
                var helperList = this._lookupRepository.GetAllAgencyHelperLookup(agencyID);
                helperLookupResponseDTO.HelperLookup = helperList;
                helperLookupResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                helperLookupResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return helperLookupResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To get all type of genders.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns> GenderDTO.</returns>
        public GenderResponseDTO GetAllGender(long agencyID)
        {
            try
            {
                GenderResponseDTO genderResponseDTO = new GenderResponseDTO();
                var genderList = this._genderRepository.GetAgencyGenderList(agencyID);
                genderResponseDTO.Genders = genderList;
                genderResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                genderResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return genderResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To get all type of language.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns> LanguageResponseDTO.</returns>
        public LanguageResponseDTO GetAllLanguages(long agencyID)
        {
            try
            {
                LanguageResponseDTO languageResponseDTO = new LanguageResponseDTO();
                var languageList = this._languageRepository.GetAgencyLanguageList(agencyID);
                languageResponseDTO.Languages = languageList;
                languageResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                languageResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return languageResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get all type of races.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns> RaceEthnicityResponseDTO.</returns>
        public RaceEthnicityResponseDTO GetAllRaceEthnicity(long agencyID)
        {
            try
            {
                RaceEthnicityResponseDTO raceEthnicityResponseDTO = new RaceEthnicityResponseDTO();
                var raceEthnicityList = this._raceEthnicityRepository.GetAgencyRaceEthnicityList(agencyID);
                raceEthnicityResponseDTO.RaceEthnicities = raceEthnicityList;
                raceEthnicityResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                raceEthnicityResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return raceEthnicityResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get all type of sexuality.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns> SexualityResponseDTO.</returns>
        public SexualityResponseDTO GetAllSexuality(long agencyID)
        {
            try
            {
                SexualityResponseDTO sexualityResponseDTO = new SexualityResponseDTO();
                var sexualityList = this._sexualityrepository.GetAgencySexuality(agencyID);
                sexualityResponseDTO.Sexualities = sexualityList;
                sexualityResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                sexualityResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return sexualityResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all SupportType.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>ActionResult.<SupportType>.</returns>
        public SupportTypeResponseDTO GetAllSupportType(long agencyID)
        {
            try
            {
                SupportTypeResponseDTO supportTypeResponseDTO = new SupportTypeResponseDTO();
                var supportTypeList = this._supportTypeRepository.GetAgencySupportTypes(agencyID);
                supportTypeResponseDTO.SupportTypes = supportTypeList;
                supportTypeResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                supportTypeResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return supportTypeResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get all type of IdentificationType.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns> IdentificationTypesResponseDTO.</returns>
        public IdentificationTypesResponseDTO GetAllIdentificationType(long agencyID)
        {
            try
            {
                IdentificationTypesResponseDTO identificationTypesResponseDTO = new IdentificationTypesResponseDTO();
                var identificationTypList = this._identificationrepository.GetAgencyIdentificationTypeList(agencyID);
                identificationTypesResponseDTO.identificationTypes = identificationTypList;
                identificationTypesResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                identificationTypesResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return identificationTypesResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get all type of Collaboration.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns> CollaborationTypesResponseDTO.</returns>
        public CollaborationTypesResponseDTO GetAllCollaboration(long agencyID)
        {
            try
            {
                CollaborationTypesResponseDTO collaborationTypesResponseDTO = new CollaborationTypesResponseDTO();
                var collaborationList = this._lookupRepository.GetCollaborationLookupForOrgAdmin(agencyID);
                collaborationTypesResponseDTO.CollaborationCategories = collaborationList;
                collaborationTypesResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                collaborationTypesResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return collaborationTypesResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllItemResponseBehavior.
        /// </summary>
        /// <returns>ItemResponseBehaviorResponseDTO.</returns>
        public ItemResponseBehaviorResponseDTO GetAllItemResponseBehavior()
        {
            try
            {
                ItemResponseBehaviorResponseDTO collaborationTypesResponseDTO = new ItemResponseBehaviorResponseDTO();
                collaborationTypesResponseDTO.ItemResponseBehavior = this._lookupRepository.GetAllItemResponseBehavior();
                collaborationTypesResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                collaborationTypesResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return collaborationTypesResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllItemResponseType.
        /// </summary>
        /// <returns>ItemResponseTypeDTO.</returns>
        public ItemResponseTypeResponseDTO GetAllItemResponseType()
        {
            try
            {
                ItemResponseTypeResponseDTO collaborationTypesResponseDTO = new ItemResponseTypeResponseDTO();
                collaborationTypesResponseDTO.ItemResponseType = this._lookupRepository.GetAllItemResponseType();
                collaborationTypesResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                collaborationTypesResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return collaborationTypesResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllNotificationLevel.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>NotificationLevelResponseDTO.</returns>
        public NotificationLevelResponseDTO GetAllNotificationLevel(long agencyID)
        {
            try
            {
                NotificationLevelResponseDTO notificationLevel = new NotificationLevelResponseDTO();
                var notificationLevelList = this._lookupRepository.GetAgencyNotificationLevelList(agencyID);
                notificationLevel.NotificationLevels = notificationLevelList;
                notificationLevel.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                notificationLevel.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return notificationLevel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllQuestionnaireItems.
        /// </summary>
        /// <param name="id">Questionnaire ID.</param>
        /// <returns>QuestionnaireItemsResponseDTO.</returns>
        public QuestionnaireItemsResponseDTO GetAllQuestionnaireItems(int id)
        {
            try
            {
                QuestionnaireItemsResponseDTO questionnaireItem = new QuestionnaireItemsResponseDTO();
                questionnaireItem.QuestionnaireItems = this._lookupRepository.GetAllQuestionnaireItems(id);
                questionnaireItem.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                questionnaireItem.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return questionnaireItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllQuestionnaire.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>QuestionnaireResponseDTO.</returns>
        public QuestionnaireResponseDTO GetAllQuestionnaire(long agencyID)
        {
            try
            {
                QuestionnaireResponseDTO questionnaireResponse = new QuestionnaireResponseDTO();
                var questionnaireList = this._lookupRepository.GetAllAgencyQuestionnaire(agencyID);
                questionnaireResponse.Questionnaire = questionnaireList;
                questionnaireResponse.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                questionnaireResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return questionnaireResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllCategories.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CategoryResponseDTO.</returns>
        public CategoryResponseDTO GetAllCategories(long agencyID)
        {
            try
            {
                CategoryResponseDTO categoryResponse = new CategoryResponseDTO();
                var categoryList = this._lookupRepository.GetAgencyTagTypeList(agencyID);
                categoryResponse.Categories = categoryList;
                categoryResponse.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                categoryResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return categoryResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllLeads.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>HelperLookupResponseDTO.</returns>
        public HelperLookupResponseDTO GetAllLeads(long agencyID)
        {
            try
            {
                HelperLookupResponseDTO helperLookupResponse = new HelperLookupResponseDTO();
                var leadList = this._lookupRepository.GetAllAgencyLeads(agencyID);
                helperLookupResponse.HelperLookup = leadList;
                helperLookupResponse.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                helperLookupResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return helperLookupResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllLevels.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationLevelResponseDTO.</returns>
        public CollaborationLevelResponseDTO GetAllLevels(long agencyID)
        {
            try
            {
                CollaborationLevelResponseDTO collaborationLevelResponse = new CollaborationLevelResponseDTO();
                var levelList = this._lookupRepository.GetCollaborationLevelLookup(agencyID);
                collaborationLevelResponse.CollaborationLevels = levelList;
                collaborationLevelResponse.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                collaborationLevelResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return collaborationLevelResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllTherapyTypes.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>TherapyTypesResponseDTO.</returns>
        public TherapyTypesResponseDTO GetAllTherapyTypes(long agencyID)
        {
            try
            {
                TherapyTypesResponseDTO therapyTypesResponse = new TherapyTypesResponseDTO();
                var therapyTypeList = this._lookupRepository.GetAgencyTherapyTypeList(agencyID);
                therapyTypesResponse.TherapyTypes = therapyTypeList;
                therapyTypesResponse.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                therapyTypesResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return therapyTypesResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllResponse.
        /// </summary>
        /// <returns>ItemResponseLookupDTO.</returns>
        public ItemResponseLookupDTO GetAllResponse()
        {
            try
            {

                ItemResponseLookupDTO itemResponseLookup = new ItemResponseLookupDTO();
                itemResponseLookup.Response = this._lookupRepository.GetAllResponse();
                itemResponseLookup.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                itemResponseLookup.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return itemResponseLookup;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all HelperTitle.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>ActionResult.<HelperTitle>.</returns>
        public HelperTitleResponseDTO GetAllHelperTitle(long agencyID)
        {
            try
            {
                HelperTitleResponseDTO helperTitleResponseDTO = new HelperTitleResponseDTO();
                var helperTitleList = this._helperTitleRepository.GetAgencyHelperTitleList(agencyID);
                helperTitleResponseDTO.HelperTitles = helperTitleList;
                helperTitleResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                helperTitleResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return helperTitleResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all GetSharingPolicyList.
        /// </summary>
        /// <returns>ActionResult.<SharingPolicyResponseDTO>.</returns>
        public SharingPolicyResponseDTO GetSharingPolicyList()
        {
            try
            {
                SharingPolicyResponseDTO sharingPolicyResponseDTO = new SharingPolicyResponseDTO();
                sharingPolicyResponseDTO.sharingPolicyDTOs = this._sharingPolicyRepository.GetAllSharingPolicy().Result;
                if (sharingPolicyResponseDTO.sharingPolicyDTOs.Count != 0)
                {
                    sharingPolicyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    sharingPolicyResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                }
                else
                {
                    sharingPolicyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    sharingPolicyResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Failure;
                }

                return sharingPolicyResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AgencyCollaboration.
        /// </summary>
        /// <returns>CollaborationsResponseDTO.</returns>
        public CollaborationsResponseDTO GetAgencyCollaboration(long id)
        {
            try
            {
                CollaborationsResponseDTO collaborationsResponseDTO = new CollaborationsResponseDTO();
                if (id != 0)
                {
                    collaborationsResponseDTO.Collaborations = this._collaborationRepository.GetAllAgencycollaborations(id);
                    if (collaborationsResponseDTO.Collaborations.Count != 0)
                    {
                        collaborationsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        collaborationsResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    }
                }
                else
                {
                    collaborationsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    collaborationsResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Failure;
                }

                return collaborationsResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllSupportsForPerson.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>PersonSupportLookupResponseDTO.</returns>
        public PersonSupportLookupResponseDTO GetAllSupportsForPerson(Guid personIndex)
        {
            try
            {
                PersonSupportLookupResponseDTO personSupportLookupResponseDTO = new PersonSupportLookupResponseDTO();
                PeopleDTO response = this._personRepository.GetPerson(personIndex);
                if (response.PersonID != 0)
                {
                    var responseSupportList = this._personRepository.GetPeopleSupportList(response.PersonID);
                    if (responseSupportList != null && responseSupportList.Count > 0)
                    {
                        personSupportLookupResponseDTO.PersonSupportLookup = responseSupportList;
                    }
                }
                personSupportLookupResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                personSupportLookupResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return personSupportLookupResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllAssessmentReason
        /// </summary>
        /// <returns>AssessmentReasonLookupResponseDTO</returns>
        public AssessmentReasonLookupResponseDTO GetAllAssessmentReason()
        {
            try
            {
                AssessmentReasonLookupResponseDTO assessmentReasonLookupResponseDTO = new AssessmentReasonLookupResponseDTO();
                List<AssessmentReason> AssessmentReasons = this._assessmentReasonRepository.GetAllAssessmentReason();
                assessmentReasonLookupResponseDTO.AssessmentReasonLookup = this.mapper.Map<List<AssessmentReasonLookupDTO>>(AssessmentReasons);
                assessmentReasonLookupResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                assessmentReasonLookupResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return assessmentReasonLookupResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllVoiceType
        /// </summary>
        /// <returns>VoiceTypeLookupResponseDTO</returns>
        public VoiceTypeLookupResponseDTO GetAllVoiceType()
        {
            try
            {
                VoiceTypeLookupResponseDTO voiceTypeLookupResponseDTO = new VoiceTypeLookupResponseDTO();
                var VoiceTypes = this._voiceTypeRepository.GetAllVoiceType();
                voiceTypeLookupResponseDTO.VoiceTypeLookup = this.mapper.Map<List<VoiceTypeLookupDTO>>(VoiceTypes);
                voiceTypeLookupResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                voiceTypeLookupResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return voiceTypeLookupResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllNotificationType
        /// </summary>
        /// <returns>NotificationTypeListResponseDTO</returns>
        public NotificationTypeListResponseDTO GetAllNotificationType()
        {
            try
            {
                NotificationTypeListResponseDTO notificationTypeListResponseDTO = new NotificationTypeListResponseDTO();
                var notificationTypes = this._notificationTypeRepository.GetAllNotificationType();
                notificationTypeListResponseDTO.NotificationTypes = this.mapper.Map<List<NotificationTypeDTO>>(notificationTypes);
                notificationTypeListResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                notificationTypeListResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return notificationTypeListResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllManager
        /// </summary>
        /// <returns>ManagerLookupResponseDTO</returns>
        public ManagerLookupResponseDTO GetAllManager(long agencyID)
        {
            try
            {
                ManagerLookupResponseDTO managerLookupResponseDTO = new ManagerLookupResponseDTO();
                var Managers = this._helperRepository.GetAllManager(agencyID, PCISEnum.Roles.OrgAdminRO, PCISEnum.Roles.OrgAdminRW, PCISEnum.Roles.Supervisor);
                managerLookupResponseDTO.ManagerLookup = this.mapper.Map<List<ManagerLookupDTO>>(Managers);
                managerLookupResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                managerLookupResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                return managerLookupResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetTimeFrameDetails
        /// </summary>
        /// <param name="daysInService">daysInService</param>
        /// <returns>TimeFrameResponseDTO</returns>
        public TimeFrameResponseDTO GetTimeFrameDetails(int daysInService)
        {
            try
            {
                TimeFrameResponseDTO timeframeResponseDTO = new TimeFrameResponseDTO();
                var timeFrame = this._lookupRepository.GetTimeFrameDetails(daysInService);
                timeframeResponseDTO.TimeFrameDetails = this.mapper.Map<TimeFrameDTO>(timeFrame);
                timeframeResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                timeframeResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                return timeframeResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllAssessments.
        /// </summary>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <returns>AssessmentsResponseDTO.</returns>
        public AssessmentsResponseDTO GetAllAssessments(int personQuestionnaireID)
        {
            try
            {
                AssessmentsResponseDTO assessmentDetailsResponseDTO = new AssessmentsResponseDTO();
                var response = this._lookupRepository.GetAllAssessments(personQuestionnaireID);
                assessmentDetailsResponseDTO.AssessmentList = response;
                assessmentDetailsResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                assessmentDetailsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return assessmentDetailsResponseDTO;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllAssessments.
        /// </summary>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <param name="personCollaborationID">personCollaborationID.</param>
        /// <param name="voiceTypeID">voiceTypeID.</param>
        /// <returns>AssessmentsResponseDTO.</returns>
        public AssessmentsResponseDTO GetAllAssessments(long personQuestionnaireID, long personCollaborationID, int voiceTypeID, long voiceTypeFKID, UserTokenDetails userTokenDetails)
        {
            try
            {
                var person = this._personQuestionnaireRepository.GetPersonQuestionnaireByID(personQuestionnaireID).Result;
                var sharedIDs = this.GetSharedPersonQuestionnaireDetails(person.PersonID, userTokenDetails.AgencyID);
                var helperColbIDs = new SharedDetailsDTO();
                if (string.IsNullOrEmpty(sharedIDs?.SharedCollaborationIDs))
                {
                    helperColbIDs = this.GetHelperAssessmentIDs(person.PersonID, person.QuestionnaireID, userTokenDetails);
                }
                AssessmentsResponseDTO assessmentDetailsResponseDTO = new AssessmentsResponseDTO();
                var response = this._lookupRepository.GetAllAssessments(person.PersonID, personQuestionnaireID, personCollaborationID, voiceTypeID, voiceTypeFKID, sharedIDs, helperColbIDs);
                assessmentDetailsResponseDTO.AssessmentList = response;
                assessmentDetailsResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                assessmentDetailsResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return assessmentDetailsResponseDTO;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllVoiceTypeInDetail-Along With Peroson and consumer names in detail for Reports.
        /// </summary>
        /// <param name="personIndex"></param>
        /// <returns>VoiceTypeLookupResponseDTO</returns>
        public VoiceTypeResponseDTO GetAllVoiceTypeInDetail(Guid personIndex, long personQuestionaireID, long personCollaborationID, UserTokenDetails userTokenDetails)
        {
            VoiceTypeResponseDTO voiceTypeLookupResponseDTO = new VoiceTypeResponseDTO();
            voiceTypeLookupResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
            voiceTypeLookupResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
            var personID = this._personRepository.GetPerson(personIndex).PersonID;
            if (personID != 0)
            {
                var sharedDetails = this.GetSharedPersonQuestionnaireDetails(personID, userTokenDetails.AgencyID);
                var helperColbDetails = new SharedDetailsDTO();
                if (string.IsNullOrEmpty(sharedDetails?.SharedCollaborationIDs))
                {
                    helperColbDetails = this._personRepository.GetPersonHelperCollaborationDetails(personID, userTokenDetails.AgencyID, userTokenDetails.UserID);
                }
                var voiceTypes = this._voiceTypeRepository.GetAllVoiceTypeInDetail(personID, personQuestionaireID, personCollaborationID, sharedDetails, helperColbDetails);
                voiceTypeLookupResponseDTO.VoiceTypeLookup = voiceTypes;
            }
            return voiceTypeLookupResponseDTO;
        }

        /// <summary>
        /// GetAllIdentifiedGender.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentifiedGenderResponseDTO.</returns>
        public IdentifiedGenderResponseDTO GetAllIdentifiedGender(long agencyID)
        {
            try
            {
                IdentifiedGenderResponseDTO genderResponseDTO = new IdentifiedGenderResponseDTO();
                List<IdentifiedGenderDTO> genderListDTO = new List<IdentifiedGenderDTO>();
                List<IdentifiedGender> genderList = this.identifiedGenderRepository.GetIdentifiedGenderList(agencyID);
                this.mapper.Map<List<IdentifiedGender>, List<IdentifiedGenderDTO>>(genderList, genderListDTO);
                genderResponseDTO.IdentifiedGender = genderListDTO;
                genderResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                genderResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return genderResponseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetAllVoiceTypeInDetail-Along With Peroson and consumer names in detail
        /// </summary>
        /// <param name="personIndex"></param>
        /// <returns>VoiceTypeLookupResponseDTO</returns>
        public VoiceTypeResponseDTO GetActiveVoiceTypeInDetail(Guid personIndex)
        {
            VoiceTypeResponseDTO voiceTypeLookupResponseDTO = new VoiceTypeResponseDTO();
            voiceTypeLookupResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
            voiceTypeLookupResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
            var personID = this._personRepository.GetPerson(personIndex).PersonID;
            if (personID != 0)
            {
                var voiceTypes = this._voiceTypeRepository.GetActiveVoiceTypeInDetail(personID);
                voiceTypeLookupResponseDTO.VoiceTypeLookup = voiceTypes;
            }
            return voiceTypeLookupResponseDTO;
        }

        /// <summary>
        /// GetVoiceTypeForFilter.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="personQuestionaireID">personQuestionaireID.</param>
        /// <returns>VoiceTypeResponseDTO.</returns>
        public VoiceTypeResponseDTO GetVoiceTypeForFilter(Guid personIndex, long personQuestionaireID, long agencyID)
        {
            VoiceTypeResponseDTO voiceTypeLookupResponseDTO = new VoiceTypeResponseDTO();
            long personID = this._personRepository.GetPerson(personIndex).PersonID;
            if (personID != 0)
            {
                string sharedAssessmentIDS = this.GetSharedAssessmentIds(personID, agencyID, personQuestionaireID, 0, 0, 0);
                List<VoiceTypeDTO> voiceTypes = this._voiceTypeRepository.GetVoiceTypeForFilter(personID, personQuestionaireID, sharedAssessmentIDS);
                voiceTypeLookupResponseDTO.VoiceTypeLookup = voiceTypes;
                if (voiceTypes != null)
                {
                    voiceTypeLookupResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    voiceTypeLookupResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                }
                else
                {
                    voiceTypeLookupResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    voiceTypeLookupResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                }
            }
            return voiceTypeLookupResponseDTO;
        }


        /// <summary>
        /// GetAllAgencySharingPolicy.
        /// </summary>
        /// <returns>AgencySharingPolicyResponseDTO.</returns
        public AgencySharingPolicyResponseDTO GetAllAgencySharingPolicy()
        {
            try
            {
                AgencySharingPolicyResponseDTO agencySharingPolicyResponseDTO = new AgencySharingPolicyResponseDTO();
                var agencySharingPolicy = this.agecySharingPolicyRepository.GetAgencySharingPolicy().Result;
                agencySharingPolicyResponseDTO.AgencySharingPolicy = agencySharingPolicy;
                if (agencySharingPolicy != null)
                {
                    agencySharingPolicyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    agencySharingPolicyResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                }
                else
                {
                    agencySharingPolicyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    agencySharingPolicyResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                }
                return agencySharingPolicyResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllAgencyForSharing.
        /// </summary>
        /// <returns>AgencyLookupResponseDTO.</returns>
        public AgencyLookupResponseDTO GetAllAgencyForSharing()
        {
            try
            {
                AgencyLookupResponseDTO agencyResponseDTO = new AgencyLookupResponseDTO();
                var agency = this._agencyRepository.GetAllAgencyForSharing();
                agencyResponseDTO.AgencyLookup = agency;
                if (agency != null)
                {
                    agencyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    agencyResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                }
                else
                {
                    agencyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    agencyResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                }
                return agencyResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string GetSharedAssessmentIds(long personID, long agencyID, long personQuestionnaireID, long personCollaborationID, int voiceTypeID, long voiceTypeFKID)
        {
            try
            {
                var sharedAssessmentIDs = string.Empty;
                SharedPersonSearchDTO sharedPersonSearchDTO = new SharedPersonSearchDTO();
                sharedPersonSearchDTO.PersonID = personID;
                sharedPersonSearchDTO.QuestionnaireID = 0;
                sharedPersonSearchDTO.PersonQuestionnaireID = personQuestionnaireID;
                sharedPersonSearchDTO.PersonCollaborationID = personCollaborationID;
                sharedPersonSearchDTO.VoiceTypeFKID = voiceTypeFKID;
                sharedPersonSearchDTO.VoiceTypeID = voiceTypeID;
                sharedPersonSearchDTO.LoggedInAgencyID = agencyID;
                sharedAssessmentIDs = this._personRepository.GetSharedAssessmentIDs(sharedPersonSearchDTO);
                return sharedAssessmentIDs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllCollaborationSharingPolicy.
        /// </summary>
        /// <returns>CollaborationSharingPolicyResponseDTO.</returns>
        public CollaborationSharingPolicyResponseDTO GetAllCollaborationSharingPolicy()
        {
            try
            {
                CollaborationSharingPolicyResponseDTO collaborationSharingPolicyResponseDTO = new CollaborationSharingPolicyResponseDTO();
                var collaborationSharingPolicy = this.collaborationSharingPolicyRepository.GetAllCollaborationSharingPolicy().Result;
                collaborationSharingPolicyResponseDTO.collaborationSharingPolicy = collaborationSharingPolicy;
                if (collaborationSharingPolicy != null)
                {
                    collaborationSharingPolicyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    collaborationSharingPolicyResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                }
                else
                {
                    collaborationSharingPolicyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                    collaborationSharingPolicyResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                }
                return collaborationSharingPolicyResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private SharedDetailsDTO GetSharedPersonQuestionnaireDetails(long personID, long agencyID)
        {
            try
            {
                var sharedDTO = new SharedDetailsDTO();
                sharedDTO = this._personRepository.GetSharedPersonQuestionnaireDetails(personID, agencyID);
                return sharedDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public ConfigurationResponseDTO GetConfigurationValueByName(string key, long agencyID)
        {
            try
            {
                var response = new ConfigurationResponseDTO();
                var result = this.configurationRepository.GetConfigurationByName(key, agencyID);
                response.ConfigurationValue = result?.Value;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AllConfigurationsResponseDTO GetAllConfigurationsForAgency(long agencyID)
        {
            try
            {
                var response = new AllConfigurationsResponseDTO();
                var dict_result = new Dictionary<string, string>();
                var list_result = this.configurationRepository.GetConfigurationList(agencyID);
                if (list_result.Count > 0)
                {
                    foreach (var val in list_result)
                    {
                        dict_result.Add(val.Name, val.Value);
                    }
                }
                response.Configurations = dict_result;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EHRLookupResponseDTO GetAllLookupsForEHRAgency(long agencyID)
        {
            try
            {
                var response = new EHRLookupResponseDTO();
                var dict_result = new Dictionary<string, object>();
                var genders = GetAllGenderForEHR(agencyID);
                dict_result.Add(PCISEnum.EHRLookups.Gender, genders);
                var races = GetAllRaceEthinicityForEHR(agencyID);
                dict_result.Add(PCISEnum.EHRLookups.Race, races);
                var languages = GetAllLanguagesForEHR(agencyID);
                dict_result.Add(PCISEnum.EHRLookups.Language, languages);
                var supportTypes = GetAllSupportTypesForEHR(agencyID);
                dict_result.Add(PCISEnum.EHRLookups.SupportType, supportTypes);
                var helpers = GetAllExternalHelpersForEHR(agencyID);
                dict_result.Add(PCISEnum.EHRLookups.Helper, helpers);
                var univeralsIDCount = GetUniversalIDCountForEHR(agencyID);
                dict_result.Add(PCISEnum.EHRLookups.UniveralsIDCount, univeralsIDCount);
                response.lookups = dict_result;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private object GetAllExternalHelpersForEHR(long agencyID)
        {
            try
            {
                var helpers = this._helperRepository.GetAllExternalHelpersForAgency(agencyID);
                var result = helpers.Select(x => new { ID = x.HelperID, Value = x.HelperExternalID }).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private object GetAllSupportTypesForEHR(long agencyID)
        {
            try
            {
                var relation = GetAllSupportType(agencyID);
                var result = relation.SupportTypes.Select(x => new { Value = x.Name, ID = x.SupportTypeID }).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private object GetAllLanguagesForEHR(long agencyID)
        {
            try
            {
                var language = GetAllLanguages(agencyID);
                var result = language.Languages.Select(x => new { Value = x.Name, ID = x.LanguageID }).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private object GetAllRaceEthinicityForEHR(long agencyID)
        {
            try
            {
                var race = GetAllRaceEthnicity(agencyID);
                var result = race.RaceEthnicities.Select(x => new { Value = x.Name, ID = x.RaceEthnicityID }).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private object GetAllGenderForEHR(long agencyID)
        {
            try
            {
                var gender = GetAllIdentifiedGender(agencyID);
                var result = gender.IdentifiedGender.Select(x => new { Value = x.Name, ID = x.IdentifiedGenderID }).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private object GetUniversalIDCountForEHR(long agencyID)
        {
            try
            {


                var count = this._personRepository.GetUniversalIDCountByAgency(agencyID);
                var result = new[]
                           {
                               new { ID = PCISEnum.EHRLookups.UniveralsIDCount, Value = count }
                           }.ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetEmailDetails.
        /// </summary>
        /// <returns>EmailDetailsResponseDTO.</returns>
        public EmailDetailsResponseDTO GetEmailDetails()
        {
            try
            {
                EmailDetailsResponseDTO response = new EmailDetailsResponseDTO();
                var result = this.emailDetailRepository.GetEmailDetails();
                response.EmailDetails = result;
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
        /// UpdateEmailDetails.
        /// </summary>
        /// <param name="emailDetails">emailDetails.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateEmailDetails(List<EmailDetailsDTO> emailDetails)
        {
            try
            {
                CRUDResponseDTO response = new CRUDResponseDTO();
                var result = this.emailDetailRepository.UpdateEmailDetails(this.mapper.Map<List<EmailDetail>>(emailDetails));
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
        /// AddEmailDetails.
        /// </summary>
        /// <param name="emailDetails">emailDetails.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AddEmailDetails(List<EmailDetailsDTO> emailDetails)
        {
            try
            {
                CRUDResponseDTO response = new CRUDResponseDTO();
                this.emailDetailRepository.AddEmailDetails(this.mapper.Map<List<EmailDetail>>(emailDetails));
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
        /// GetAllQuestionnaireReminderType.
        /// </summary>
        /// <returns>QuestionnaireReminderTypeResponseDTO.</returns>
        public QuestionnaireReminderTypeResponseDTO GetAllQuestionnaireReminderType()
        {
            try
            {
                QuestionnaireReminderTypeResponseDTO questionnaireReminderTypeResponseDTO = new QuestionnaireReminderTypeResponseDTO();
                var response = this.questionnaireReminderTypeRepository.GetAllQuestionnaireReminderType();
                questionnaireReminderTypeResponseDTO.QuestionnaireReminderType = this.mapper.Map<List<QuestionnaireReminderTypeDTO>>(response);
                questionnaireReminderTypeResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                questionnaireReminderTypeResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return questionnaireReminderTypeResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetIdentificationTypeDetailsByName.
        /// </summary>
        /// <param name="nameCSV">nameCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns></returns>
        public IdentificationTypesResponseDTO GetIdentificationTypeDetailsByName(string nameCSV, long agencyID)
        {
            try
            {
                IdentificationTypesResponseDTO response = new IdentificationTypesResponseDTO();
                nameCSV = ConvertJsonKeyValuestoCSV(nameCSV);
                response.identificationTypes = this._identificationrepository.GetIdentificationTypeDetailsByName(nameCSV, agencyID);
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
        /// GetRaceEthnicityDetailsByName.
        /// </summary>
        /// <param name="nameCSV">nameCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>RaceEthnicityResponseDTO.</returns>
        public RaceEthnicityResponseDTO GetRaceEthnicityDetailsByName(string nameCSV, long agencyID)
        {
            try
            {
                RaceEthnicityResponseDTO response = new RaceEthnicityResponseDTO();
                nameCSV = ConvertJsonKeyValuestoCSV(nameCSV);
                response.RaceEthnicities = this._raceEthnicityRepository.GetRaceEthnicityDetailsByName(nameCSV, agencyID);
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
        /// ConvertJsonKeyValuestoCSV.
        /// </summary>
        /// <param name="helperEmailCSV">helperEmailCSV.</param>
        /// <returns>string.</returns>
        public string ConvertJsonKeyValuestoCSV(string helperEmailCSV)
        {
            dynamic helperEmailObj = JsonConvert.DeserializeObject(helperEmailCSV);
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
            return CSV;
        }


        public EHRLookupResponseDTO GetAllLookupsForAssessmentUpload(long agencyID, int questionnaireID)
        {
            try
            {
                var response = new EHRLookupResponseDTO();
                var dict_result = new Dictionary<string, object>();
                var assessmentReasons = GetAllAssessmentReason(agencyID);
                dict_result.Add(PCISEnum.ImportAssessmentLookups.AssessmentReasons, assessmentReasons);
                var assessmentStatuses = GetAllAssessmentStatus(agencyID);
                dict_result.Add(PCISEnum.ImportAssessmentLookups.AssessmentStatus, assessmentStatuses);
                var voiceTypes = GetAllVoiceTypes(agencyID);
                dict_result.Add(PCISEnum.ImportAssessmentLookups.VoiceTypes, voiceTypes);
                response.lookups = dict_result;
                response.QuestionItemDetails = GetAllQuestionnaireItemsWithResponses(questionnaireID);
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<QuestionnaireItemsForImportDTO> GetAllQuestionnaireItemsWithResponses(int questionnaireID)
        {
            try
            {
                var result = this._questionnaireRepository.GetAllQuestionnaireItemsWithResponses(questionnaireID);                
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private object GetAllHelpers(long agencyID)
        {
            try
            {
                var helpers = this._lookupRepository.GetAllHelperByAgencyID(agencyID);
                var result = helpers.Select(x => new { Value = x.HelperID, ID = x.Email }).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private object GetAllVoiceTypes(long agencyID)
        {
            try
            {
                var voiceTypes = this._voiceTypeRepository.GetAllVoiceType();
                var result = voiceTypes.Select(x => new { Value = x.Name, ID = x.VoiceTypeID }).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private object GetAllAssessmentStatus(long agencyID)
        {
            try
            {
                var assessmentStatus = this.assessmentStatusRepository.GetAllAssessmentStatus();
                var result = assessmentStatus.Select(x => new { Value = x.Name, ID = x.AssessmentStatusID }).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private object GetAllAssessmentReason(long agencyID)
        {
            try
            {
                var reason = this._assessmentReasonRepository.GetAllAssessmentReason();
                var result = reason.Select(x => new { Value = x.Name, ID = x.AssessmentReasonID }).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetNotificationType.
        /// </summary>
        /// <param name="type">type.</param>
        /// <returns>NotificationTypeListResponseDTO.</returns>
        public NotificationTypeListResponseDTO GetNotificationType(string type)
        {
            try
            {
                NotificationTypeListResponseDTO notificationTypeListResponseDTO = new NotificationTypeListResponseDTO();
                var notificationTypes = this._notificationTypeRepository.GetNotificationType(type).Result;
                notificationTypeListResponseDTO.NotificationType = this.mapper.Map<NotificationTypeDTO>(notificationTypes);
                notificationTypeListResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                notificationTypeListResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return notificationTypeListResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetIdentifiedGenderDetailsByName.
        /// </summary>
        /// <param name="jsonData">jsonData.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentifiedGenderResponseDTO.</returns>
        public IdentifiedGenderResponseDTO GetIdentifiedGenderDetailsByName(string jsonData, long agencyID)
        {
            try
            {
                IdentifiedGenderResponseDTO response = new IdentifiedGenderResponseDTO();
                string nameCSV = ConvertJsonKeyValuestoCSV(jsonData);
                response.IdentifiedGender = this.identifiedGenderRepository.GetIdentifiedGenderDetailsByName(nameCSV, agencyID);
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
        /// GetNotificationLevel.
        /// </summary>
        /// <param name="notificationlevelId">notificationlevelId.</param>
        /// <returns>NotificationLevelResponseDTO.</returns>
        public NotificationLevelResponseDTO GetNotificationLevel(int notificationlevelId)
        {
            try
            {
                NotificationLevelResponseDTO notificationLevelResponseDTO = new NotificationLevelResponseDTO();
                var notificationlevel = this.notificationLevelRepository.GetNotificationLevel(notificationlevelId).Result;
                notificationLevelResponseDTO.NotificationLevel = notificationlevel;
                notificationLevelResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                notificationLevelResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return notificationLevelResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetResponse.
        /// </summary>
        /// <param name="responseId">responseId</param>
        /// <returns>ResponseDetailsDTO.</returns>
        public ResponseDetailsDTO GetResponse(int responseId)
        {
            try
            {
                ResponseDetailsDTO result = new ResponseDetailsDTO();
                var response = this.responseRepository.GetResponse(responseId).Result;
                result.response = response;
                result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetBackgroundProcessLog.
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>ResponseDetailsDTO.</returns>
        public BackgroundProcessResponseDTO GetBackgroundProcessLog(string name)
        {
            try
            {
                BackgroundProcessResponseDTO result = new BackgroundProcessResponseDTO();
                var response = this.backgroundProcessLogRepository.GetBackgroundProcessLog(name);
                result.BackgroundProcess = this.mapper.Map<BackgroundProcessLogDTO>(response); ;
                result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddBackgroundProcessLog.
        /// </summary>
        /// <param name="backgroundProcessLog">backgroundProcessLog</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AddBackgroundProcessLog(BackgroundProcessLogDTO backgroundProcessLog)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                BackgroundProcessLog backgroundProcessLogEntity = new BackgroundProcessLog();
                this.mapper.Map<BackgroundProcessLogDTO, BackgroundProcessLog>(backgroundProcessLog, backgroundProcessLogEntity);
                var Response = this.backgroundProcessLogRepository.AddBackgroundProcessLog(backgroundProcessLogEntity);
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
        /// UpdateBackgroundProcessLog.
        /// </summary>
        /// <param name="backgroundProcessLog">backgroundProcessLog</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateBackgroundProcessLog(BackgroundProcessLogDTO backgroundProcessLog)
        {
            try
            {
                CRUDResponseDTO result = new CRUDResponseDTO();
                BackgroundProcessLog backgroundProcessLogEntity = new BackgroundProcessLog();
                this.mapper.Map<BackgroundProcessLogDTO, BackgroundProcessLog>(backgroundProcessLog, backgroundProcessLogEntity);
                var Response = this.backgroundProcessLogRepository.UpdateBackgroundProcessLog(backgroundProcessLogEntity);
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
        /// GetCategoryAndItemforSkipLogic.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>CategoryAndItemForSkipLogicResponseDTO.</returns>
        public CategoryAndItemForSkipLogicResponseDTO GetCategoryAndItemforSkipLogic(int questionnaireId)
        {
            try
            {
                CategoryAndItemForSkipLogicResponseDTO result = new CategoryAndItemForSkipLogicResponseDTO();
                result.ItemsList = this._lookupRepository.GetCategoryAndItemforSkipLogic(questionnaireId);
                result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllActionType.
        /// </summary>
        /// <returns>ActionTypeResponseDTO.</returns>
        public ActionTypeResponseDTO GetAllActionType()
        {
            try
            {
                ActionTypeResponseDTO result = new ActionTypeResponseDTO();
                result.ActionTypes = this._lookupRepository.GetAllActionType();
                result.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                result.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private SharedDetailsDTO GetHelperAssessmentIDs(long personID, int questionnaireId, UserTokenDetails userTokenDetails)
        {
            try
            {
                var sharedAssessmentIDs = string.Empty;
                SharedPersonSearchDTO sharedPersonSearchDTO = new SharedPersonSearchDTO();
                sharedPersonSearchDTO.QuestionnaireID = questionnaireId;
                sharedPersonSearchDTO.PersonID = personID;
                sharedPersonSearchDTO.LoggedInAgencyID = userTokenDetails.AgencyID;
                sharedPersonSearchDTO.UserID = userTokenDetails.UserID;
                sharedPersonSearchDTO.QueryType = "";
                var sharedDetails = this._personRepository.GetHelpersAssessmentIDs(sharedPersonSearchDTO);
                return sharedDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RegularReminderScheduleLookupDTO GetLookupsForRegularReminderSchedules()
        {
            try
            {
                RegularReminderScheduleLookupDTO response = new RegularReminderScheduleLookupDTO();
                response.OffsetType = this._lookupRepository.GetAllOffsetType();
                response.RecurrenceDay = this._lookupRepository.GetAllRecurrenceDay();
                response.RecurrenceEndType = this._lookupRepository.GetAllRecurrenceEndType();
                response.RecurrenceMonth = this._lookupRepository.GetAllRecurrenceMonth();
                response.RecurrenceOrdinal = this._lookupRepository.GetAllRecurrenceOrdinal();
                var patterns = this._lookupRepository.GetAllRecurrencePattern();
                List<RecurrencePatternGroupDTO> patternGroup = new List<RecurrencePatternGroupDTO>();
                var patternGroups = patterns.GroupBy(u => u.GroupName).ToList();
                foreach(var group in patternGroups)
                {
                    patternGroup.Add(new RecurrencePatternGroupDTO() { GroupName = group.Key, Pattern = group.ToList() });
                }
                response.RecurrencePattern = patternGroup;
                response.InviteToCompleteReceiver = this._lookupRepository.GetAllInviteToCompleteReceivers();
                response.TimeZones = this._lookupRepository.GetAllTimeZones();
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
        /// AddReminderInviteDetails for InviteToCOmplete Triggering.
        /// </summary>
        /// <param name="inviteDetailsDTO"></param>
        /// <returns></returns>
        public CRUDResponseDTO AddReminderInviteDetails(List<ReminderInviteToCompleteDTO> inviteDetailsDTO)
        {
            try
            {
                CRUDResponseDTO response = new CRUDResponseDTO();

                //Fetch NotifyReminders invitationmailstatus to be updated as status Processing
                var assessmnetNotifyIds = inviteDetailsDTO.Select(x => x.NotifyReminderID).Distinct().ToList();
                if (assessmnetNotifyIds.Count > 0)
                {
                    var reminders = this.notifyReminderRepository.GetNotifyRemindersByIds(assessmnetNotifyIds);
                    reminders.ForEach(x => { x.InviteToCompleteMailStatus = PCISEnum.EmailStatus.Processing; });
                    this.notifyReminderRepository.UpdateBulkNotifyReminder(reminders);
                }
                var inviteDetail = this.mapper.Map<List<ReminderInviteToComplete>>(inviteDetailsDTO);
                this.reminderInviteToCompleteRepository.AddReminderInviteDetails(inviteDetail);
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
        /// GetReminderInviteDetails - Get all detils for sending email/sms for invite
        /// </summary>
        /// <returns></returns>
        public ReminderInviteToCompleteResponseDTO GetReminderInviteDetails()
        {
            try
            {
                ReminderInviteToCompleteResponseDTO response = new ReminderInviteToCompleteResponseDTO();
                var result = this.reminderInviteToCompleteRepository.GetReminderInviteDetails();
                response.reminderInviteDetails = result;
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
        /// UpdateReminderInviteDetails.
        /// update the email/SMS status in ReminderInviteToCOmplete.
        /// ALso update corresponding NotifyReminder mail status and time too
        /// </summary>
        /// <param name="inviteDetails"></param>
        /// <returns></returns>
        public CRUDResponseDTO UpdateReminderInviteDetails(List<ReminderInviteToCompleteDTO> inviteDetails)
        {
            try
            {
                CRUDResponseDTO response = new CRUDResponseDTO();
                var inviteToUpdate = this.mapper.Map<List<ReminderInviteToComplete>>(inviteDetails);
                this.reminderInviteToCompleteRepository.UpdateReminderInviteDetails(inviteToUpdate);
                //Update NotifyReminders with emailsentTime and InvitationMail status = sent/Failed
                if (inviteToUpdate.Count > 0)
                {
                    var reminderIds = inviteDetails.Select(x => x.NotifyReminderID).Distinct().ToList();
                    var lst_notifyReminders = this.notifyReminderRepository.GetNotifyRemindersByIds(reminderIds);
                    foreach (var reminder in lst_notifyReminders)
                    {
                        var assessmentSMS = inviteDetails.Where(x => x.NotifyReminderID == reminder.NotifyReminderID).FirstOrDefault();
                        reminder.InviteToCompleteMailStatus = assessmentSMS.Status;
                        reminder.InviteToCompleteSentAt = assessmentSMS.UpdateDate;
                    }
                    this.notifyReminderRepository.UpdateBulkNotifyReminder(lst_notifyReminders);
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
        /// GetAllResponseValueTypeLookup
        /// </summary>
        /// <returns></returns>
        public ResponseValueTypeResponseDTO GetAllResponseValueTypeLookup()
        {

            try
            {
                ResponseValueTypeResponseDTO responseDTO = new ResponseValueTypeResponseDTO();
                responseDTO.ResponseValueTypes = this._lookupRepository.GetAllResponseValueTypes();
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return responseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
