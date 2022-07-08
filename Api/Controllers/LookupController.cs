// -----------------------------------------------------------------------
// <copyright file="LookupController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// ConsumerController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class LookupController : BaseController
    {
        /// Initializes a new instance of the <see cref="lookupService"/> class.
        private readonly ILookupService lookupService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<LookupController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="lookupService">lookupService.</param>
        public LookupController(ILogger<LookupController> logger, ILookupService lookupService)
        {
            this.lookupService = lookupService;
            this.logger = logger;
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <returns>ActionResult.<CountryStateResponseDTO>.</returns>
        [HttpGet]
        [Route("country-states")]
        public ActionResult<CountryStateResponseDTO> GetAllCountryState()
        {
            try
            {
                CountryStateResponseDTO countryStateResponseDTO = this.lookupService.GetAllCountryState();
                return countryStateResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllCountryState/GET : Getting country state : Exception  : Exception occurred getting country state. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving country state. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllAgencyLookup.
        /// </summary>
        /// <returns>AgencyLookupResponseDTO.</returns>
        [HttpGet]
        [Route("agency/lookup")]
        public ActionResult<AgencyLookupResponseDTO> GetAllAgencyLookup()
        {
            try
            {
                long agencyID = this.GetTenantID();
                AgencyLookupResponseDTO agencyLookupResponseDTO = this.lookupService.GetAllAgencyLookup(agencyID);
                return agencyLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllAgencyLookup/GET : Getting item : Exception  : Exception occurred getting agency lookup. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving agency lookup. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllRolesLookup.
        /// </summary>
        /// <returns>RoleLookupResponseDTO.</returns>
        [HttpGet]
        [Route("roles")]
        public ActionResult<RoleLookupResponseDTO> GetAllRolesLookup()
        {
            try
            {
                RoleLookupResponseDTO roleLookupResponseResponseDTO = this.lookupService.GetAllRolesLookup();
                return roleLookupResponseResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllRolesLookup/GET : Getting item : Exception  : Exception occurred getting roles lookup. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving roles lookup. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllHelperLookup.
        /// </summary>
        /// <returns>RoleLookupResponseDTO.</returns>
        [HttpGet]
        [Route("helper/lookup/{userID}")]
        public ActionResult<HelperLookupResponseDTO> GetAllHelperLookup(int userID)
        {
            try
            {
                long agencyID = this.GetTenantID();
                HelperLookupResponseDTO helperLookupResponseDTO = this.lookupService.GetAllHelperLookup(agencyID);
                return helperLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllHelperLookup/GET : Getting item : Exception  : Exception occurred getting helper lookup. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving helper lookup. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get All genders.
        /// </summary>
        /// <returns>ActionResult.<GenderResponseDTO>.</returns>
        [HttpGet]
        [Route("gender")]
        public ActionResult<GenderResponseDTO> GetAllGender()
        {
            try
            {
                long agencyID = this.GetTenantID();
                GenderResponseDTO genderResponseDTO = this.lookupService.GetAllGender(agencyID);
                return genderResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllGender/GET : Getting item : Exception  : Exception occurred getting all gender. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving all gender. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get all race.
        /// </summary>
        /// <returns>ActionResult.<RaceEthnicityResponseDTO>.</returns>
        [HttpGet]
        [Route("race-ethnicity")]
        public ActionResult<RaceEthnicityResponseDTO> GetAllRaceEthnicity()
        {
            try
            {
                long agencyID = this.GetTenantID();
                RaceEthnicityResponseDTO raceEthnicityResponseDTO = this.lookupService.GetAllRaceEthnicity(agencyID);
                return raceEthnicityResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllRaceEthnicity/GET : Getting item : Exception  : Exception occurred getting all race ethnicity. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving all race ethnicity. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get all languages.
        /// </summary>
        /// <returns>ActionResult.<LanguageResponseDTO>.</returns>
        [HttpGet]
        [Route("languages")]
        public ActionResult<LanguageResponseDTO> GetAllLanguages()
        {
            try
            {
                long agencyID = this.GetTenantID();
                LanguageResponseDTO languageResponseDTO = this.lookupService.GetAllLanguages(agencyID);
                return languageResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllLanguages/GET : Getting item : Exception  : Exception occurred getting all languages. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving all languages. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get all sexuality.
        /// </summary>
        /// <returns>ActionResult.<SexualityResponseDTO>.</returns>
        [HttpGet]
        [Route("sexuality")]
        public ActionResult<SexualityResponseDTO> GetAllSexuality()
        {
            try
            {
                long agencyID = this.GetTenantID();
                SexualityResponseDTO sexualityResponseDTO = this.lookupService.GetAllSexuality(agencyID);
                return sexualityResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllSexuality/GET : Getting item : Exception  : Exception occurred getting all sexuality. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving all sexuality. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get all SupportType.
        /// </summary>
        /// <returns>ActionResult.<supportTypeResponseDTO>.</returns>
        [HttpGet]
        [Route("support-type")]
        public ActionResult<SupportTypeResponseDTO> GetAllSupportType()
        {
            try
            {
                long agencyID = this.GetTenantID();
                SupportTypeResponseDTO supportTypeResponseDTO = this.lookupService.GetAllSupportType(agencyID);
                return supportTypeResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllSupportType/GET : Getting item : Exception  : Exception occurred getting all support type. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving all support type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get All IdentificationType.
        /// </summary>
        /// <returns>ActionResult.<IdentificationTypesResponseDTO>.</returns>
        [HttpGet]
        [Route("identification-types")]
        public ActionResult<IdentificationTypesResponseDTO> GetAllIdentificationType()
        {
            try
            {
                long agencyID = this.GetTenantID();
                IdentificationTypesResponseDTO identificationTypesResponseDTO = this.lookupService.GetAllIdentificationType(agencyID);
                return identificationTypesResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllIdentificationType/GET : Getting item : Exception  : Exception occurred getting all identification type. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving all identification type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get All Collaboration.
        /// </summary>
        /// <returns>ActionResult.<CollaborationTypesResponseDTO>.</returns>
        [HttpGet]
        [Route("collaboration/lookups")]
        public ActionResult<CollaborationTypesResponseDTO> GetAllCollaboration()
        {
            try
            {
                long agencyID = this.GetTenantID();
                CollaborationTypesResponseDTO collaborationTypesResponseDTO = this.lookupService.GetAllCollaboration(agencyID);
                return collaborationTypesResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllCollaboration/GET : Getting item : Exception occurred getting all collaboration. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving all collaboration. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllItemResponseBehavior.
        /// </summary>
        /// <returns>ItemResponseBehaviorResponseDTO.</returns>
        [HttpGet]
        [Route("item-Response-Behavior")]
        public ActionResult<ItemResponseBehaviorResponseDTO> GetAllItemResponseBehavior()
        {
            try
            {
                ItemResponseBehaviorResponseDTO itemResponseBehavior = this.lookupService.GetAllItemResponseBehavior();
                return itemResponseBehavior;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllItemResponseBehavior/GET : Getting item : Exception  : Exception occurred getting Item Response Behavior.{ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Item Response Behavior. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllNotificationLevel.
        /// </summary>
        /// <returns>NotificationLevelResponseDTO.</returns>
        [HttpGet]
        [Route("notification-level")]
        public ActionResult<NotificationLevelResponseDTO> GetAllNotificationLevel()
        {
            try
            {
                long agencyID = this.GetTenantID();
                NotificationLevelResponseDTO notificationLevel = this.lookupService.GetAllNotificationLevel(agencyID);
                return notificationLevel;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllNotificationLevel/GET : Getting item : Exception  : Exception occurred getting notification level.{ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Notification Level. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllQuestionnaireItems.
        /// </summary>
        /// <param name="id">Questionnaire ID.</param>
        /// <returns>QuestionnaireItemsResponseDTO.</returns>
        [HttpGet]
        [Route("questionnaire-items/{id}")]
        public ActionResult<QuestionnaireItemsResponseDTO> GetAllQuestionnaireItems(int id)
        {
            try
            {
                QuestionnaireItemsResponseDTO questionnaireItems = this.lookupService.GetAllQuestionnaireItems(id);
                return questionnaireItems;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllQuestionnaireItems/GET : Getting item : Exception  : Exception occurred getting questionnaire-items. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving questionnaire-items. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllQuestionnaire.
        /// </summary>
        /// <returns>QuestionnaireResponseDTO.</returns>
        [HttpGet]
        [Route("questionnaire")]
        public ActionResult<QuestionnaireResponseDTO> GetAllQuestionnaire()
        {
            try
            {
                long agencyID = this.GetTenantID();
                QuestionnaireResponseDTO questionnaireResponse = this.lookupService.GetAllQuestionnaire(agencyID);
                return questionnaireResponse;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllQuestionnaire/GET : Getting item : Exception  : Exception occurred getting questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Questionnaire. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllCategories.
        /// </summary>
        /// <returns>CategoryResponseDTO.</returns>
        [HttpGet]
        [Route("category")]
        public ActionResult<CategoryResponseDTO> GetAllCategories()
        {
            try
            {
                long agencyID = this.GetTenantID();
                CategoryResponseDTO categories = this.lookupService.GetAllCategories(agencyID);
                return categories;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllCategories/GET : Getting item : Exception  : Exception occurred getting category. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Categories. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllLeads.
        /// </summary>
        /// <returns>HelperLookupResponseDTO.</returns>
        [HttpGet]
        [Route("collaboration-leads")]
        public ActionResult<HelperLookupResponseDTO> GetAllLeads()
        {
            try
            {
                long agencyID = this.GetTenantID();
                HelperLookupResponseDTO leads = this.lookupService.GetAllLeads(agencyID);
                return leads;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllLeads/GET : Getting item : Exception  : Exception occurred getting collaboration-leads. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Leads. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllCollaborationLevel.
        /// </summary>
        /// <returns>CollaborationLevelResponseDTO.</returns>
        [HttpGet]
        [Route("collaboration-levels")]
        public ActionResult<CollaborationLevelResponseDTO> GetAllLevels()
        {
            try
            {
                long agencyID = this.GetTenantID();
                CollaborationLevelResponseDTO levels = this.lookupService.GetAllLevels(agencyID);
                return levels;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllLevels/GET : Getting item : Exception  : Exception occurred getting collaboration-levels. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving collaboration levels. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllTherapyTypes.
        /// </summary>
        /// <returns>TherapyTypesResponseDTO.</returns>
        [HttpGet]
        [Route("collaboration-types")]
        public ActionResult<TherapyTypesResponseDTO> GetAllTherapyTypes()
        {
            try
            {
                long agencyID = this.GetTenantID();
                TherapyTypesResponseDTO therapyTypes = this.lookupService.GetAllTherapyTypes(agencyID);
                return therapyTypes;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllTherapyTypes/GET : Getting item : Exception  : Exception occurred getting collaboration-types. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving collaboration-types. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllResponse.
        /// </summary>
        /// <returns>ItemResponseLookupDTO.</returns>
        [HttpGet]
        [Route("response")]
        public ActionResult<ItemResponseLookupDTO> GetAllResponse()
        {
            try
            {
                ItemResponseLookupDTO therapyTypes = this.lookupService.GetAllResponse();
                return therapyTypes;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllResponse/GET : Getting item : Exception  : Exception occurred getting Response. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Response. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get all HelperTitle.
        /// </summary>
        /// <returns>ActionResult.<HelperTitleResponseDTO>.</returns>
        [HttpGet]
        [Route("helper-title")]
        public ActionResult<HelperTitleResponseDTO> GetAllHelperTitle()
        {
            try
            {
                long agencyID = this.GetTenantID();
                HelperTitleResponseDTO helperTitleResponseDTO = this.lookupService.GetAllHelperTitle(agencyID);
                return helperTitleResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllHelperTitle/GET : Getting item : Exception  : Exception occurred getting all helper title. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving all helper title. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetSharingPolicy.
        /// </summary>
        /// <returns>SharingPolicyResponseDTO.</returns>
        [HttpGet]
        [Route("sharingunits")]
        public ActionResult<SharingPolicyResponseDTO> SharingPolicy()
        {
            try
            {
                var response = this.lookupService.GetSharingPolicyList();
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"SharingPolicy/GET : Getting item : Exception  : Exception occurred Getting SharingPolicy List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving SharingPolicy List. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AgencyCollaboration.
        /// </summary>
        /// <returns>CollaborationsResponseDTO.</returns>
        [HttpGet]
        [Route("AgencyCollaboration/{id}")]
        public ActionResult<CollaborationsResponseDTO> AgencyCollaboration(int id)
        {
            try
            {
                long agencyID = this.GetTenantID();
                var response = this.lookupService.GetAgencyCollaboration(agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"Get AgencyCollaboration/GET : Getting item : Exception  : Exception occurred Getting AgencyCollaboration. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving AgencyCollaboration. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllAssessmentReason.
        /// </summary>
        /// <returns>AssessmentReasonLookupResponseDTO.</returns>
        [HttpGet]
        [Route("assessment-reason")]
        public ActionResult<AssessmentReasonLookupResponseDTO> GetAllAssessmentReason()
        {
            try
            {
                AssessmentReasonLookupResponseDTO assessmentReasonLookupResponseDTO = this.lookupService.GetAllAssessmentReason();
                return assessmentReasonLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllAssessmentReason/GET : Getting item : Exception  : Exception occurred Getting All Assessment Reason. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Assessment Reason. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllVoiceType.
        /// </summary>
        /// <returns>VoiceTypeLookupResponseDTO.</returns>
        [HttpGet]
        [Route("voice-type-all")]
        public ActionResult<VoiceTypeLookupResponseDTO> GetAllVoiceType()
        {
            try
            {
                VoiceTypeLookupResponseDTO voiceTypeLookupResponseDTO = this.lookupService.GetAllVoiceType();
                return voiceTypeLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllVoiceType/GET : Getting item : Exception  : Exception occurred Getting All VoiceType List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving VoiceType. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllNotificationType.
        /// </summary>
        /// <returns>NotificationTypeListResponseDTO.</returns>
        [HttpGet]
        [Route("notification-type")]
        public ActionResult<NotificationTypeListResponseDTO> GetAllNotificationType()
        {
            try
            {
                NotificationTypeListResponseDTO notificationTypeListResponseDTO = this.lookupService.GetAllNotificationType();
                return notificationTypeListResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllNotificationType/GET : Getting item : Exception  : Exception occurred Getting All Notification Type List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving notification type list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllManager.
        /// </summary>
        /// <returns>ManagerLookupResponseDTO.</returns>
        [HttpGet]
        [Route("managers")]
        public ActionResult<ManagerLookupResponseDTO> GetAllManager()
        {
            try
            {
                long agencyID = 0;
                agencyID = this.GetTenantID();

                ManagerLookupResponseDTO managerLookupResponseDTO = this.lookupService.GetAllManager(agencyID);
                return managerLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllManager/GET : Getting item : Exception  : Exception occurred Getting All Manager List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving manager list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <returns>ActionResult.<CountryStateResponseDTO>.</returns>
        [HttpGet]
        [Route("countries")]
        public ActionResult<CountryLookupResponseDTO> GetAllCountries()
        {
            try
            {
                CountryLookupResponseDTO countryLookupResponseDTO = this.lookupService.GetAllCountries();
                return countryLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllCountries/GET : Exception occurred getting All Countries. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving All Countries. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetTimePeriodDetails.
        /// </summary>
        /// <param name="daysInEpisode">daysInEpisode.</param>
        /// <returns>TimeFrameResponseDTO.</returns>
        [HttpGet]
        [Route("timeperiod/{daysInEpisode}")]
        public ActionResult<TimeFrameResponseDTO> GetTimePeriodDetails(int daysInEpisode)
        {
            try
            {
                TimeFrameResponseDTO timeFrameResponseDTO = this.lookupService.GetTimeFrameDetails(daysInEpisode);
                return timeFrameResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetTimePeriodDetails/GET : Exception occurred getting Time Period Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Time Period Details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllAssessments.
        /// </summary>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <returns>PersonSupportLookupResponseDTO.</returns>
        [HttpGet]
        [Route("get-assessments/{personQuestionnaireID}")]
        public ActionResult<AssessmentsResponseDTO> GetAllAssessments(int personQuestionnaireID)
        {
            try
            {
                var response = this.lookupService.GetAllAssessments(personQuestionnaireID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllAssessments/GET : Getting item : Exception  : Exception occurred getting all Assessment Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Assessment Details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllAssessments.
        /// </summary>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <param name="personCollaborationID">personCollaborationID.</param>
        /// <param name="voiceTypeID">voiceTypeID.</param>
        /// <param name="voiceTypeFKID">voiceTypeFKID.</param>
        /// <returns>AssessmentsResponseDTO.</returns>
        [HttpGet]
        [Route("assessments-in-collaboration/{personQuestionnaireID}/{personCollaborationID}/{voiceTypeID}/{voiceTypeFKID}")]
        public ActionResult<AssessmentsResponseDTO> GetAllAssessments(long personQuestionnaireID, long personCollaborationID, int voiceTypeID, long voiceTypeFKID)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                var response = this.lookupService.GetAllAssessments(personQuestionnaireID, personCollaborationID, voiceTypeID, voiceTypeFKID, userTokenDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllAssessments/GET : Getting item : Exception  : Exception occurred getting Assessment Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Assessment Details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllIdentifiedGender.
        /// </summary>
        /// <returns>IdentifiedGenderResponseDTO.</returns>
        [HttpGet]
        [Route("identified-gender")]
        public ActionResult<IdentifiedGenderResponseDTO> GetAllIdentifiedGender()
        {
            try
            {
                long agencyID = this.GetTenantID();
                IdentifiedGenderResponseDTO identifiedGenderResponseDTO = this.lookupService.GetAllIdentifiedGender(agencyID);
                return identifiedGenderResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllIdentifiedGender/GET : Exception occurred getting identified gender. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving identified gender. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllAgencySharingPolicy.
        /// </summary>
        /// <returns>AgencySharingPolicyResponseDTO.</returns>
        [HttpGet]
        [Route("agencysharingpolicy")]
        public ActionResult<AgencySharingPolicyResponseDTO> GetAllAgencySharingPolicy()
        {
            try
            {
                AgencySharingPolicyResponseDTO agencySharingPolicyResponseDTO = this.lookupService.GetAllAgencySharingPolicy();
                return agencySharingPolicyResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllAgencySharingPolicy/GET : Exception occurred getting AgencySharingPolicy. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving GetAllAgencySharingPolicy. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllAgencyForSharing.
        /// </summary>
        /// <returns>AgencyLookupResponseDTO.</returns>
        [HttpGet]
        [Route("agencyforsharing")]
        public ActionResult<AgencyLookupResponseDTO> GetAllAgencyForSharing()
        {
            try
            {
                AgencyLookupResponseDTO agencySharingPolicyResponseDTO = this.lookupService.GetAllAgencyForSharing();
                return agencySharingPolicyResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllAgencyForSharing/GET : Exception occurred getting agency for sharing. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving agency for sharing. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllCollaborationSharingPolicy.
        /// </summary>
        /// <returns>CollaborationSharingPolicyResponseDTO.</returns>
        [HttpGet]
        [Route("collaborationsharingpolicy")]
        public ActionResult<CollaborationSharingPolicyResponseDTO> GetAllCollaborationSharingPolicy()
        {
            try
            {
                CollaborationSharingPolicyResponseDTO collaborationSharingPolicyResponseDTO = this.lookupService.GetAllCollaborationSharingPolicy();
                return collaborationSharingPolicyResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllCollaborationSharingPolicy/GET : Exception occurred getting Collaboration SharingPolicy. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving GetAllCollaborationSharingPolicy. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetConfigurationValueByName.
        /// If AgenyID is 0 result will have the applicationLevel configurationValue.
        /// </summary>
        /// <returns>ConfigurationResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("configurationvalue/{key}/{agencyID}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ConfigurationResponseDTO> GetConfigurationValueByName(string key, long agencyID)
        {
            try
            {
                ConfigurationResponseDTO configurationResponseDTO = this.lookupService.GetConfigurationValueByName(key, agencyID);
                return configurationResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetConfigurationValueByName/GET : Exception occurred getting Configuration value. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Configuration Value By Name. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllConfigurationsForAgency.
        /// </summary>
        /// <returns>AllConfigurationsResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("configurations/{agencyID}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<AllConfigurationsResponseDTO> GetAllConfigurationsForAgency(long agencyID)
        {
            try
            {
                AllConfigurationsResponseDTO configurationResponseDTO = this.lookupService.GetAllConfigurationsForAgency(agencyID);
                return configurationResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllConfigurationsForAgency/GET : Exception occurred getting all Configurations for Agency. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving All Configurations For Agency. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllLookupsForEHRAgency.
        /// Gender/RaceEthinicity/Language/SupportType/ExternalHelper.
        /// </summary>
        /// <returns>EHRLookupResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("lookups/{agencyID}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<EHRLookupResponseDTO> GetAllLookupsForEHRAgency(long agencyID)
        {
            try
            {
                EHRLookupResponseDTO eHRLookupResponseDTO = this.lookupService.GetAllLookupsForEHRAgency(agencyID);
                return eHRLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllLookupsForEHRAgency/GET : Exception occurred getting all Lookups for EHR Agency. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving GetAllLookupsForEHRAgency. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetEmailDetails.
        /// </summary>
        /// <returns>EmailDetailsResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("emaildetails")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<EmailDetailsResponseDTO> GetEmailDetails()
        {
            try
            {
                var response = this.lookupService.GetEmailDetails();
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetEmailDetails/get : Adding item : Exception  : Exception occurred GetEmailDetails. {ex.Message}");
                return this.HandleException(ex, "An error occurred GetEmailDetails. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateEmailDetails.
        /// </summary>
        /// <param name="emailDetails">emailDetails.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPut]
        [Route("emaildetails")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> UpdateEmailDetails([FromBody] List<EmailDetailsDTO> emailDetails)
        {
            try
            {
                var response = this.lookupService.UpdateEmailDetails(emailDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateEmailDetails/put : Updating item : Exception  : Exception occurred UpdateEmailDetails. {ex.Message}");
                return this.HandleException(ex, "An error occurred UpdateEmailDetails. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddEmailDetails.
        /// </summary>
        /// <param name="emailDetails">emailDetails.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("emaildetails")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> AddEmailDetails([FromBody] List<EmailDetailsDTO> emailDetails)
        {
            try
            {
                var response = this.lookupService.AddEmailDetails(emailDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddEmailDetails/POST : Adding item : Exception  : Exception occurred AddEmailDetails. {ex.Message}");
                return this.HandleException(ex, "An error occurred AddEmailDetails. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllAssessmentReasonForNotification.
        /// </summary>
        /// <returns>AssessmentReasonLookupResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("assessment-reason-all")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<AssessmentReasonLookupResponseDTO> GetAllAssessmentReasonForNotification()
        {
            try
            {
                AssessmentReasonLookupResponseDTO assessmentReasonLookupResponseDTO = this.lookupService.GetAllAssessmentReason();
                return assessmentReasonLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllAssessmentReasonForNotification/GET : Getting item : Exception  : Exception occurred Getting All Assessment Reason fro notification. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Assessment Reason for notification. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllQuestionnaireReminderType.
        /// </summary>
        /// <returns>QuestionnaireReminderTypeResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("questionnaire-reminder-type")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<QuestionnaireReminderTypeResponseDTO> GetAllQuestionnaireReminderType()
        {
            try
            {
                QuestionnaireReminderTypeResponseDTO questionnaireReminderTypeResponseDTO = this.lookupService.GetAllQuestionnaireReminderType();
                return questionnaireReminderTypeResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllQuestionnaireReminderType/GET : Getting item : Exception  : Exception occurred Getting All Questionnaire Reminder Type. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Questionnaire Reminder Type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetIdentificationTypeDetailsByName.
        /// </summary>
        /// <param name="importParameterDTO">importParameterDTO.</param>
        /// <returns>IdentificationTypesResponseDTO.</returns>
        [HttpPost]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("identification-types-byname")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<IdentificationTypesResponseDTO> GetIdentificationTypeDetailsByName(ImportParameterDTO importParameterDTO)
        {
            try
            {
                IdentificationTypesResponseDTO response = this.lookupService.GetIdentificationTypeDetailsByName(importParameterDTO.JsonData, importParameterDTO.agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetIdentificationTypeDetailsByName/POST : IdentificationType Details By Name : Exception  : Exception occurred while fetching identification type by name. {ex.Message}");
                return this.HandleException(ex, ex.InnerException + ". An error occurred while fetching identification type by name. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetRaceEthnicityDetailsByName.
        /// </summary>
        /// <param name="importParameterDTO">importParameterDTO.</param>
        /// <returns>IdentificationTypesResponseDTO.</returns>
        [HttpPost]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("race-ethnicity-byname")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<RaceEthnicityResponseDTO> GetRaceEthnicityDetailsByName(ImportParameterDTO importParameterDTO)
        {
            try
            {
                RaceEthnicityResponseDTO response = this.lookupService.GetRaceEthnicityDetailsByName(importParameterDTO.JsonData, importParameterDTO.agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetRaceEthnicityDetailsByName/POST :RaceEthnicity Details By Name : Exception  : Exception occurred while fetching race ethnicity details by name. {ex.Message}");
                return this.HandleException(ex, ex.InnerException + ". An error occurred while fetching race ethnicity details by name. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetIdentifiedGenderDetailsByName.
        /// </summary>
        /// <returns>IdentifiedGenderResponseDTO.</returns>
        [HttpGet]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("get-all-identified-gender")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<IdentifiedGenderResponseDTO> GetIdentifiedGenderDetailsByName(long agencyID)
        {
            try
            {
                IdentifiedGenderResponseDTO identifiedGenderResponseDTO = this.lookupService.GetAllIdentifiedGender(agencyID);
                return identifiedGenderResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetIdentifiedGenderDetailsByName/GET : Exception occurred getting identified gender by name. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving identified gender by name. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllLookupsForAssessmentUpload.
        /// VoiceTypes/AssessmentReasons/Helpers/AssessmentStatus.
        /// </summary>
        /// <returns>EHRLookupResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("import-assessment-lookups/{agencyID}/{questionnaireID}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<EHRLookupResponseDTO> GetAllLookupsForAssessmentUpload(long agencyID, int questionnaireID)
        {
            try
            {
                EHRLookupResponseDTO eHRLookupResponseDTO = this.lookupService.GetAllLookupsForAssessmentUpload(agencyID, questionnaireID);
                return eHRLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllLookupsForAssessmentUpload/GET : Exception occurred getting all Lookups for Assessment Upload. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving GetAllLookupsForAssessmentUpload. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetNotificationType.
        /// </summary>
        /// <param name="notificationType">notificationType.</param>
        /// <returns>NotificationTypeListResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("notification-type-byname/{notificationType}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<NotificationTypeListResponseDTO> GetNotificationType(string notificationType)
        {
            try
            {
                var response = this.lookupService.GetNotificationType(notificationType);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetNotificationType/GET : Exception occurred GetNotificationType. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving GetNotificationType. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetNotificationLevel.
        /// </summary>
        /// <param name="notificationlevelId">notificationlevelId.</param>
        /// <returns>NotificationLevelResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("notification-level/{notificationlevelId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<NotificationLevelResponseDTO> GetNotificationLevel(int notificationlevelId)
        {
            try
            {
                var response = this.lookupService.GetNotificationLevel(notificationlevelId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetNotificationLevel/GET : Exception occurred GetNotificationLevel. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving GetNotificationLevel. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetResponse.
        /// </summary>
        /// <param name="responseId">responseId</param>
        /// <returns>ResponseDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("response/{responseId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ResponseDetailsDTO> GetResponse(int responseId)
        {
            try
            {
                var response = this.lookupService.GetResponse(responseId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"GetResponse/GET : GET item : Exception  : Exception occurred while GetResponse. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetResponse. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetBackgroundProcessLog.
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>ResponseDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("background-process/{name}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<BackgroundProcessResponseDTO> GetBackgroundProcessLog(string name)
        {
            try
            {
                var response = this.lookupService.GetBackgroundProcessLog(name);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetBackgroundProcessLog/GET : GET item : Exception  : Exception occurred while GetBackgroundProcessLog. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetBackgroundProcessLog. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddBackgroundProcessLog.
        /// </summary>
        /// <param name="backgroundProcessLog">backgroundProcessLog</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("background-process")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> AddBackgroundProcessLog([FromBody] BackgroundProcessLogDTO backgroundProcessLog)
        {
            try
            {
                var response = this.lookupService.AddBackgroundProcessLog(backgroundProcessLog);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddBackgroundProcessLog/POST : POST item : Exception  : Exception occurred while AddBackgroundProcessLog. {ex.Message}");
                return this.HandleException(ex, "An error occurred while AddBackgroundProcessLog. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateBackgroundProcessLog.
        /// </summary>
        /// <param name="backgroundProcessLog">backgroundProcessLog</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPut]
        [Route("background-process")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> UpdateBackgroundProcessLog([FromBody] BackgroundProcessLogDTO backgroundProcessLog)
        {
            try
            {
                var response = this.lookupService.UpdateBackgroundProcessLog(backgroundProcessLog);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateBackgroundProcessLog/Put : put item : Exception  : Exception occurred while UpdateBackgroundProcessLog. {ex.Message}");
                return this.HandleException(ex, "An error occurred while UpdateBackgroundProcessLog. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetRolesDetailsByName.
        /// </summary>
        /// <returns>RoleLookupResponseDTO.</returns>
        [HttpGet]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("roles-byname")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<RoleLookupResponseDTO> GetRolesDetailsByName()
        {
            try
            {
                RoleLookupResponseDTO roleLookupResponseResponseDTO = this.lookupService.GetAllRolesLookup();
                return roleLookupResponseResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetRolesDetailsByName/GET : Getting item : Exception  : Exception occurred while getting roles details by name. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving roles details by name. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllItemResponseBehavior.
        /// </summary>
        /// <returns>ItemResponseBehaviorResponseDTO.</returns>
        [HttpGet]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("item-response-behavior-list")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ItemResponseBehaviorResponseDTO> GetAllItemResponseBehaviorForDashboard()
        {
            try
            {
                ItemResponseBehaviorResponseDTO itemResponseBehavior = this.lookupService.GetAllItemResponseBehavior();
                return itemResponseBehavior;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllItemResponseBehaviorForDashboard/GET : Getting item : Exception  : Exception occurred getting Item Response Behavior.{ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Item Response Behavior. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllItemResponseType.
        /// </summary>
        /// <returns>ItemResponseTypeDTO.</returns>
        [HttpGet]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("item-response-type-list")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ItemResponseTypeResponseDTO> GetAllItemResponseType()
        {
            try
            {
                ItemResponseTypeResponseDTO response = this.lookupService.GetAllItemResponseType();
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllItemResponseType/GET : Getting item : Exception  : Exception occurred getting GetAllItemResponseType.{ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving GetAllItemResponseType. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllCountriesCodes.
        /// </summary>
        /// <returns>CountryLookupResponseDTO.</returns>
        [HttpGet]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("country-code")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CountryLookupResponseDTO> GetAllCountriesCodes()
        {
            try
            {
                CountryLookupResponseDTO response = this.lookupService.GetAllCountries();
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllCountriesCodes/GET : Getting item : Exception  : Exception occurred getting GetAllCountriesCodes.{ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving GetAllCountriesCodes. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetCategoryAndItemforSkipLogic.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>CategoryAndItemForSkipLogicResponseDTO.</returns>
        [HttpGet]
        [Route("category-item/{questionnaireId}")]
        public ActionResult<CategoryAndItemForSkipLogicResponseDTO> GetCategoryAndItemforSkipLogic(int questionnaireId)
        {
            try
            {
                CategoryAndItemForSkipLogicResponseDTO response = this.lookupService.GetCategoryAndItemforSkipLogic(questionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetCategoryAndItemforSkipLogic/GET : Getting item : Exception  : Exception occurred getting GetCategoryAndItemforSkipLogic.{ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving GetCategoryAndItemforSkipLogic. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllActionType.
        /// </summary>
        /// <returns>ActionTypeResponseDTO.</returns>
        [HttpGet]
        [Route("action-type")]
        public ActionResult<ActionTypeResponseDTO> GetAllActionType()
        {
            try
            {
                ActionTypeResponseDTO response = this.lookupService.GetAllActionType();
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllActionType/GET : Getting item : Exception  : Exception occurred getting GetAllActionType.{ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving GetAllActionType. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetLookupsForRegularReminderSchedules.
        /// </summary>
        /// <returns>RegularReminderScheduleLookupDTO.</returns>
        [HttpGet]
        [Route("regular-reminder-lookups")]
        public ActionResult<RegularReminderScheduleLookupDTO> GetLookupsForRegularReminderSchedules()
        {
            try
            {
                RegularReminderScheduleLookupDTO response = this.lookupService.GetLookupsForRegularReminderSchedules();
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetLookupsForRegularReminderSchedules/GET : Getting item : Exception  : Exception occurred getting GetLookupsForRegularReminderSchedules.{ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving GetLookupsForRegularReminderSchedules. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllResponseValueTypeLookup.
        /// </summary>
        /// <returns>ResponseValueTypeResponseDTO.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("response-value-type")]
        public ActionResult<ResponseValueTypeResponseDTO> GetAllResponseValueTypeLookup()
        {
            try
            {
                ResponseValueTypeResponseDTO lookupResponseResponseDTO = this.lookupService.GetAllResponseValueTypeLookup();
                return lookupResponseResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllResponseValueTypeLookup/GET : Getting item : Exception  : Exception occurred getting ResponseValueType lookup. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving ResponseValueType lookup. Please try again later or contact support.");
            }
        }
    }
}
