// -----------------------------------------------------------------------
// <copyright file="SystemRoleController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.ExternalAPI;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// SystemRole Controller.
    /// </summary>
    [Route("api")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "api-external")]
    public class ExternalApiController : BaseController
    {
        private readonly IPersonService personService;
        private readonly ISystemRoleService systemRoleService;
        private readonly IQuestionnaireService questionnaireService;
        private readonly IOptionsService optionsService;
        private readonly IHelperService helperService;
        private readonly ILookupService lookupService;
        private readonly ILanguageService languageService;
        private readonly IAssessmentService assessmentService;
        private readonly ICollaborationService collaborationService;
        private readonly ILogger<ExternalApiController> logger;

        /// <summary>
        ///  Initializes a new instance of the <see cref="ExternalApiController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="personService">personService.</param>
        /// <param name="questionnaireService">questionnaireService.</param>
        /// <param name="optionsService">optionsService.</param>
        public ExternalApiController(ILogger<ExternalApiController> logger, ICollaborationService collaborationService, IAssessmentService assessmentService, ILanguageService languageService, ILookupService lookupService, IHelperService helperService,ISystemRoleService systemRoleService, IPersonService personService, IQuestionnaireService questionnaireService, IOptionsService optionsService)
        {
            this.systemRoleService = systemRoleService;
            this.personService = personService;
            this.optionsService = optionsService;
            this.lookupService = lookupService;
            this.questionnaireService = questionnaireService;
            this.helperService = helperService;
            this.languageService = languageService;
            this.assessmentService = assessmentService;
            this.collaborationService = collaborationService;
            this.logger = logger;
        }

        /// <summary>
        /// GetAllHelperDetailsForExternal
        /// </summary>
        /// <param name="helperSearchInputDTO">helperSearchInputDTO.</param>
        /// <returns>GetHelperResponseDTOForExternal</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPost]
        [Route("helper/list-external")]
        public ActionResult<GetHelperResponseDTOForExternal> GetAllHelperDetailsForExternal(HelperSearchInputDTO helperSearchInputDTO)
        {
            try
            {
                LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
                loggedInUserDTO.AgencyId = this.GetTenantID();

                GetHelperResponseDTOForExternal response = this.helperService.GetAllHelperDetailsForExternal(helperSearchInputDTO, loggedInUserDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllHelperDetails/POST : Getting item : Exception  : Exception occurred getting helper details. {ex.Message}");
                return this.HandleException(ex, "An error occurred getting helper details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// SaveHelperDetailsForExternal.
        /// </summary>
        /// <param name="helperInputData">helperInputData</param>
        /// <returns>CRUDResponseDTOForExternal.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPost]
        [Route("helper/add-external")]
        public ActionResult<CRUDResponseDTOForExternal> SaveHelperDetailsForExternal(HelperDetailsInputDTO helperInputData)
        {
            try
            {
                if (helperInputData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        helperInputData.UpdateUserID = this.GetUserID();
                        helperInputData.AgencyID = this.GetTenantID();
                        var response = this.helperService.SaveHelperDetailsForExternal(helperInputData);
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
                this.logger.LogError(MyLogEvents.InsertItem, $"SaveHelperDetailsForExternal/POST : Adding item : Exception  : Exception occurred Adding helper Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding helper. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateHelperDetailsForExternal.
        /// </summary>
        /// <param name="helperInputData">helperInputData</param>
        /// <returns>CRUDResponseDTOForExternal.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPut]
        [Route("helper/update-external")]
        public ActionResult<CRUDResponseDTOForExternal> UpdateHelperDetailsForExternal(HelperDetailsEditInputDTO helperInputData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    helperInputData.UpdateUserID = this.GetUserID();
                    helperInputData.AgencyID = this.GetTenantID();
                    var result = this.helperService.UpdateHelperDetailsForExternal(helperInputData);
                    return result.Result;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateHelperDetails/PUT : Updating item : Exception  : Exception occurred updating helper details. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating helper details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get the Collaboration Tag Types list paginated for external.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>CollaborationTagTypeListResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("collaboration-tag-type-external/{pageNumber}/{pageSize}")]
        public ActionResult<CollaborationTagTypeListResponseDTO> GetCollaborationTagTypeListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetCollaborationTagTypeList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetCollaborationTagTypeListForExternal/Get : Listing collaboration tag type : Exception  : Exception occurred getting collaboration tag type. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving collaboration tag type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetTherapyTypeListForExternal.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>Action result.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("therapy-type-external/{pageNumber}/{pageSize}")]
        public ActionResult<TherapyTypesResponseDTO> GetTherapyTypeListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetTherapyTypeList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetTherapyTypeListForExternal/Get : Listing therapy type : Exception  : Exception occurred listing therapy type. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing therapy type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get the Helper title list paginated for external.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>HelperTitleListResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("helper-title-external/{pageNumber}/{pageSize}")]
        public ActionResult<HelperTitleResponseDTO> GetHelperTitleListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetHelperTitleList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetHelperTitleListForExternal/Get : Listing helper title : Exception  : Exception occurred getting helper title list. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving helper title list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetNotificationLevelListForExternal.
        /// </summary>
        /// <param name="pageNumber">page Number.</param>
        /// <param name="pageSize"> page Size.</param>
        /// <returns>NotificationLevelResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("notification-level-external/{pageNumber}/{pageSize}")]
        public ActionResult<NotificationLevelResponseDTO> GetNotificationLevelListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetNotificationLevelList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetNotificationLevelListForExternal/Get : Listing Notification Level : Exception  : Exception occurred listing notification level list. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing notification level list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetGenderListForExternal.
        /// </summary>
        /// <param name="pageNumber">page Number.</param>
        /// <param name="pageSize"> page Size.</param>
        /// <returns>GenderResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("gender-external/{pageNumber}/{pageSize}")]
        public ActionResult<GenderResponseDTO> GetGenderListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetGenderList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetGenderListForExternal/Get : Listing Gender : Exception  : Exception occurred getting gender list. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving gender list. Please try again later or contact support.");
            }
        }


        /// <summary>
        /// GetIdentificationTypeListForExternal.
        /// </summary>
        /// <param name="pageNumber">page Number.</param>
        /// <param name="pageSize"> page Size.</param>
        /// <returns>IdentificationTypeResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("identification-type-external/{pageNumber}/{pageSize}")]
        public ActionResult<IdentificationTypesResponseDTO> GetIdentificationTypeListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetIdentificationTypeList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetIdentificationTypeListForExternal/Get : Listing Identification Type : Exception  : Exception occurred listing identification type. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing identification type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetRaceEthnicityListForExternal.
        /// </summary>
        /// <param name="pageNumber">page Number.</param>
        /// <param name="pageSize"> page Size.</param>
        /// <returns>RaceEthnicityResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("race-ethnicity-external/{pageNumber}/{pageSize}")]
        public ActionResult<RaceEthnicityResponseDTO> GetRaceEthnicityListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetRaceEthnicityList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetRaceEthnicityListForExternal/Get : Listing Race Ethnicity : Exception  : Exception occurred listing race ethnicity. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing race ethnicity. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetLanguageListForExternal.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>LanguageListResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("language-external/{pageNumber}/{pageSize}")]
        public ActionResult<LanguageListResponseDTO> GetLanguageListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.languageService.GetLanguageList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetLanguageListForExternal/Get : Listing language : Exception  : Exception occurred getting language list. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving language list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetSupportTypeListForExternal.
        /// </summary>
        /// <param name="pageNumber">page Number.</param>
        /// <param name="pageSize"> page Size.</param>
        /// <returns>SupportTypeResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("support-type-external/{pageNumber}/{pageSize}")]
        public ActionResult<SupportTypeResponseDTO> GetSupportTypeListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetSupportTypeList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetSupportTypeListForExternal/Get : Listing Support Type : Exception  : Exception occurred listing support type. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing support type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetSexualityListForExternal.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>SexualityResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("sexuality-external/{pageNumber}/{pageSize}")]
        public ActionResult<SexualityResponseDTO> GetSexualityListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetSexualityList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetSexualityListForExternal/Get : Listing Sexuality : Exception  : Exception occurred listing sexuality. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing sexuality. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetIdentifiedGenderListForExternal.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>IdentifiedGenderResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("identified-gender-external/{pageNumber}/{pageSize}")]
        public ActionResult<IdentifiedGenderResponseDTO> GetIdentifiedGenderListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                IdentifiedGenderResponseDTO response = this.optionsService.GetIdentifiedGenderList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetIdentifiedGenderListForExternal/Get : Listing identified gender : Exception  : Exception occurred getting identified gender list. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving identified gender list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetCollaborationLevelListForExternal.
        /// </summary>
        /// <param name="pageNumber">page Number.</param>
        /// <param name="pageSize"> page Size.</param>
        /// <returns>CollaborationLevelResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("collaboration-level-external/{pageNumber}/{pageSize}")]
        public ActionResult<CollaborationLevelResponseDTO> GetCollaborationLevelListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetCollaborationLevelList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetCollaborationLevelListForExternal/Get : Listing Collaboration Level : Exception  : Exception occurred getting collaboration level list. {ex.Message}");
                return this.HandleException(ex, "An error occurred rgetting collaboration level list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllCountryStateForExternal.
        /// </summary>
        /// <returns>ActionResult.<CountryStateResponseDTO>.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("country-states-external")]
        public ActionResult<CountryStateResponseDTO> GetAllCountryStateForExternal()
        {
            try
            {
                CountryStateResponseDTO countryStateResponseDTO = this.lookupService.GetAllCountryState();
                return countryStateResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllCountryStateForExternal/GET : Getting country state : Exception  : Exception occurred getting country state. {ex.Message}");
                return this.HandleException(ex, "An error occurred while retrieving country state. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <returns>ActionResult.<CountryStateResponseDTO>.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("countries-external")]
        public ActionResult<CountryLookupResponseDTO> GetAllCountriesForExternal()
        {
            try
            {
                CountryLookupResponseDTO countryLookupResponseDTO = this.lookupService.GetAllCountries();
                return countryLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllCountriesForExternal/GET : Exception occurred getting All Countries. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving All Countries. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetSystemRoleListForExternal.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>SystemRoleListResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpGet]
        [Route("system-role-external/{pageNumber}/{pageSize}")]
        public ActionResult<SystemRoleListResponseDTO> GetSystemRoleListForExternal(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.systemRoleService.GetSystemRoleList(pageNumber, pageSize);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetSystemRoleListForExternal/Get : GET item : Exception  : Exception occurred getting system role list. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving system role list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionnaireListForExternal.
        /// </summary>
        /// <param name="questionnaireSearchDTO">questionnaireSearchDTO.</param>
        /// <returns>QuestionnaireListResponseDTO.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPost]
        [Route("questionnaires-external")]
        public ActionResult<QuestionnaireListResponseDTO> GetQuestionnaireListForExternal([FromBody] QuestionnaireSearchInputDTO questionnaireSearchDTO)
        {
            try
            {
                LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
                loggedInUserDTO.AgencyId = this.GetTenantID();
                var response = this.questionnaireService.GetQuestionnaireListForExternal(questionnaireSearchDTO, loggedInUserDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireListForExternal/post : get item : Exception  : Exception occurred while getting questionnaire list. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting questionnaire list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPeopleDetailsListForExternal.
        /// </summary>
        /// <param name="personSearchInputDTO">personSearchInputDTO.</param>
        /// <returns>PeopleDetailsResponseDTOForExternal.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPost]
        [Route("person/list-external")]
        public ActionResult<PeopleDetailsResponseDTOForExternal> GetPeopleDetailsListForExternal(PersonSearchInputDTO personSearchInputDTO)
        {
            try
            {
                LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
                loggedInUserDTO.AgencyId = this.GetTenantID();

                var response = this.personService.GetPeopleDetailsListForExternal(personSearchInputDTO, loggedInUserDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPeopleDetailsListForExternal/GET : Getting item : Exception  : Exception occurred Get PeopleDetails as List ForExternal. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving PeopleDetails List ForExternal. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdatePersonForExternal.
        /// </summary>
        /// <param name="personEditInputDTO">personEditInputDTO.</param>
        /// <returns>CRUDResponseDTOForExternalPerson.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPut]
        [Route("person-external")]
        public ActionResult<CRUDResponseDTOForExternalPersoneEdit> UpdatePersonForExternal(PeopleEditDetailsForExternalDTO personEditInputDTO)
        {
            try
            {
                LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
                loggedInUserDTO.AgencyId = this.GetTenantID();
                loggedInUserDTO.UserId = this.GetUserID();

                var response = this.personService.UpdatePersonForExternal(personEditInputDTO, loggedInUserDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdatePersonForExternal/GET : Getting item : Exception  : Exception occurred Update PeopleDetails as List ForExternal. {ex.Message}");
                return this.HandleException(ex, "An error occurred Update PeopleDetails ForExternal. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Adding person details from external api.
        /// </summary>
        /// <param name="personAddInputDTO">personAddInputDTO.</param>
        /// <returns>CRUDResponseDTOForExternalPersoneAdd.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPost]
        [Route("person-external")]
        public ActionResult<CRUDResponseDTOForExternalPersoneAdd> AddPersonForExternal(PeopleAddDetailsForExternalDTO personAddInputDTO)
        {
            try
            {
                LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
                loggedInUserDTO.AgencyId = this.GetTenantID();
                loggedInUserDTO.UserId = this.GetUserID();

                var response = this.personService.AddPersonForExternal(personAddInputDTO, loggedInUserDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddPersonForExternal/POST : Getting item : Exception  : Exception occurred Add PeopleDetails as List ForExternal. {ex.Message}");
                return this.HandleException(ex, "An error occurred Add PeopleDetails ForExternal. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAssessmentDetailsListsForExternal.
        /// </summary>
        /// <param name="assessmentSearchInputDTO">assessmentSearchInputDTO.</param>
        /// <returns>AssessmentDetailsResponseDTOForExternal.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPost]
        [Route("assessment/list-external")]
        public ActionResult<AssessmentDetailsResponseDTOForExternal> GetAssessmentDetailsListsForExternal(AssessmentSearchInputDTO assessmentSearchInputDTO)
        {
            try
            {
                LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
                loggedInUserDTO.AgencyId = this.GetTenantID();

                var response = this.assessmentService.GetAssessmentDetailsListsForExternal(assessmentSearchInputDTO, loggedInUserDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAssessmentDetailsListsForExternal/GET : Getting item : Exception  : Exception occurred Get Assessment Details as List ForExternal. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Assessment Details List ForExternal. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetCollaborationDetailsListForExternal.
        /// </summary>
        /// <param name="collaborationSearchInputDTO">collaborationSearchInputDTO.</param>
        /// <returns>CollaborationResponseDTOForExternal.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPost]
        [Route("collaboration/list-external")]
        public ActionResult<CollaborationResponseDTOForExternal> GetCollaborationDetailsListForExternal(CollaborationSearchInputDTO collaborationSearchInputDTO)
        {
            try
            {
                LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
                loggedInUserDTO.AgencyId = this.GetTenantID();

                var response = this.collaborationService.GetCollaborationDetailsListForExternal(collaborationSearchInputDTO, loggedInUserDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetCollaborationDetailsListForExternal/Post : Getting item : Exception  : Exception occurred Get Collaboration as List ForExternal. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Collaboration List ForExternal. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Function to add external collabration details
        /// </summary>
        /// <param name="collaborationDetailsDTO">collaborationDetailsDTO.</param>
        /// <returns>AddCollaborationResponseDTOForExternal.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPost]
        [Route("collaboration-external")]
        public ActionResult<AddCollaborationResponseDTOForExternal> AddCollaborationDetailsForExternal([FromBody] CollabrationAddDTOForExternal collaborationDetailsDTO)
        {
            if (collaborationDetailsDTO is null)
            {
                throw new ArgumentNullException(nameof(collaborationDetailsDTO));
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    var agencyID = this.GetTenantID();
                    int updateUserID = 0;
                    var userID = this.GetUserID();
                    updateUserID = Convert.ToInt32(userID);
                    AddCollaborationResponseDTOForExternal returnData = this.collaborationService.AddCollabrationForExternal(collaborationDetailsDTO, agencyID, updateUserID);
                    return returnData;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddCollaborationDetailsForExternal/POST : Adding item : Exception  : Exception occurred Adding Collaboration Details For External. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding collaboration for external. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Edit collabration details.
        /// </summary>
        /// <param name="collaborationDetailsDTO">collaborationDetailsDTO.</param>
        /// <returns>CRUDCollaborationResponseDTOForExternal.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPut]
        [Route("collaboration-external")]
        public ActionResult<CRUDCollaborationResponseDTOForExternal> EditCollaborationDetailsForExternal([FromBody] CollabrationUpdateDTOForExternal collaborationDetailsDTO)
        {
            if (collaborationDetailsDTO is null)
            {
                throw new ArgumentNullException(nameof(collaborationDetailsDTO));
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    int updateUserID = 0;
                    var userID = this.GetUserID();
                    updateUserID = Convert.ToInt32(userID);
                    var agencyID = this.GetTenantID();
                    CRUDCollaborationResponseDTOForExternal returnData = this.collaborationService.UpdateCollabrationForExternal(collaborationDetailsDTO, updateUserID, agencyID);
                    return returnData;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"EditCollaborationDetailsForExternal/PUT : Updating item : Exception  : Exception occurred Editing Collaboration Details For External. {ex.Message}");
                return this.HandleException(ex, "An error occurred while editing collaboration for external. Please try again later or contact support.");
            }
        }


        /// <summary>
        /// Function to remove collabration details for external.
        /// </summary>
        /// <param name="collabrationIndex">collabrationIndex.</param>
        /// <returns>CRUDCollaborationResponseDTOForExternal.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpDelete]
        [Route("collaboration-external/{collabrationIndex}")]
        public ActionResult<CRUDCollaborationResponseDTOForExternal> DeleteCollaborationDetailsForExternal(Guid collabrationIndex)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    int updateUserID = this.GetUserID();
                    long agencyID = this.GetTenantID();
                    var response = this.collaborationService.DeleteCollaborationDetail(collabrationIndex, updateUserID, agencyID);
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"DeleteCollaborationDetailsForExternal/DELETE : Deleting item : Exception  : Exception occurred Deleting Collaboration Details For External. {ex.Message}");
                return this.HandleException(ex, "An error occurred while deleting collaboration for external. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddPersonHelperForExternal.
        /// </summary>
        /// <param name="personHelperDetailsDTO">personHelperDetailsDTO.</param>
        /// <returns>AddPersonHelperResponseDTOForExternal.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPost]
        [Route("person/add-helper")]
        public ActionResult<AddPersonHelperResponseDTOForExternal> AddPersonHelperForExternal([FromBody] PersonHelperAddDTOForExternal personHelperDetailsDTO)
        {
            if (personHelperDetailsDTO is null)
            {
                throw new ArgumentNullException(nameof(personHelperDetailsDTO));
            }

            try
            {

                if (this.ModelState.IsValid)
                {
                    var response = this.personService.SavePersonHelperDetailsForExternal(personHelperDetailsDTO, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }

            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddPersonHelperForExternal/POST : Adding item : Exception  : Exception occurred Adding person helper Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding personhelper. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// ExpirePersonHelperDetailsForExternal.
        /// </summary>
        /// <param name="expirePersonHelperDetailsDTO"></param>
        /// <returns>ExpirePersonHelperResponseDTOForExternal</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPut]
        [Route("person/expire-helper")]
        public ActionResult<ExpirePersonHelperResponseDTOForExternal> ExpirePersonHelperDetailsForExternal([FromBody] ExpirePersonHelperAddDTOForExternal expirePersonHelperDetailsDTO)
        {
            if (expirePersonHelperDetailsDTO is null)
            {
                throw new ArgumentNullException(nameof(expirePersonHelperDetailsDTO));
            }

            try
            {

                if (this.ModelState.IsValid)
                {
                    var response = this.personService.ExpirePersonHelperDetailsForExternal(expirePersonHelperDetailsDTO);
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }

            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"ExpirePersonHelperDetailsForExternal/POST : Adding item : Exception  : Exception occurred Expire person helper Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while expire personhelper. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddPersonCollaborationForExternal.
        /// </summary>
        /// <param name="personCollaborationDetailsDTO"></param>
        /// <returns>AddPersonCollaborationResponseDTOForExternal.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPost]
        [Route("person/add-collaboration")]
        public ActionResult<AddPersonCollaborationResponseDTOForExternal> AddPersonCollaborationForExternal([FromBody] PersonCollaborationAddDTOForExternal personCollaborationDetailsDTO)
        {
            if (personCollaborationDetailsDTO is null)
            {
                throw new ArgumentNullException(nameof(personCollaborationDetailsDTO));
            }

            try
            {

                if (this.ModelState.IsValid)
                {
                    var response = this.personService.SavePersonCollaborationDetailsForExternal(personCollaborationDetailsDTO, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }

            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddPersonCollaborationForExternal/POST : Adding item : Exception  : Exception occurred Adding person collaboration Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding personcollaboration. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// ExpirePersonCollaborationDetailsForExternal.
        /// </summary>
        /// <param name="expirePersonCollaborationDetailsDTO"></param>
        /// <returns>ExpirePersonCollaborationResponseDTOForExternal</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPut]
        [Route("person/expire-collaboration")]
        public ActionResult<ExpirePersonCollaborationResponseDTOForExternal> ExpirePersonCollaborationDetailsForExternal([FromBody] ExpirePersonCollaborationAddDTOForExternal expirePersonCollaborationDetailsDTO)
        {
            if (expirePersonCollaborationDetailsDTO is null)
            {
                throw new ArgumentNullException(nameof(expirePersonCollaborationDetailsDTO));
            }

            try
            {

                if (this.ModelState.IsValid)
                {
                    var response = this.personService.ExpirePersonCollaborationDetailsForExternal(expirePersonCollaborationDetailsDTO);
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }

            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"ExpirePersonCollaborationDetailsForExternal/POST : Adding item : Exception  : Exception occurred Expire PersonCollaboration Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while expire PersonCollaboration. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Add PersonSupport Details ForExternal.
        /// </summary>
        /// <param name="addPersonSupportInputDTO">addPersonSupportInputDTO.</param>
        /// <returns>CRUDResponseDTOForExternalPersonSupport.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPost]
        [Route("person/support-external")]
        public ActionResult<CRUDResponseDTOForExternalPersonSupport> AddPersonSupportDetailsForExternal([FromBody] AddPersonSupportDTOForExternal addPersonSupportInputDTO)
        {
            try
            {
                LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
                loggedInUserDTO.AgencyId = this.GetTenantID();
                loggedInUserDTO.UserId = this.GetUserID();

                var response = this.personService.AddPersonSupportDetailsForExternal(addPersonSupportInputDTO, loggedInUserDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddPersonSupportDetailsForExternal/POST : Adding item : Exception  : Exception occurred AddPersonSupportDetailsForExternal. {ex.Message}");
                return this.HandleException(ex, "An error occurred while AddPersonSupportDetailsForExternal. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Edit/Update PersonSupport Details For External.
        /// </summary>
        /// <param name="editPersonSupportInputDTO">editPersonSupportInputDTO.</param>
        /// <returns>CRUDResponseDTOForExternalPersonSupport.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPut]
        [Route("person/support-external")]
        public ActionResult<CRUDResponseDTOForExternalPersonSupport> EditPersonSupportDetailsForExternal([FromBody] EditPersonSupportDTOForExternal editPersonSupportInputDTO)
        {
            try
            {
                LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
                loggedInUserDTO.AgencyId = this.GetTenantID();
                loggedInUserDTO.UserId = this.GetUserID();

                var response = this.personService.EditPersonSupportDetailsForExternal(editPersonSupportInputDTO, loggedInUserDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"EditPersonSupportDetailsForExternal/POST : Adding item : Exception  : Exception occurred EditPersonSupportDetailsForExternal. {ex.Message}");
                return this.HandleException(ex, "An error occurred while EditPersonSupportDetailsForExternal. Please try again later or contact support.");
            }
        }


        /// <summary>
        /// Deactivate/Expire/Remove PersonSupport For External.
        /// </summary>
        /// <param name="expirePersonSupportInputDTO">expirePersonSupportInputDTO.</param>
        /// <returns>CRUDResponseDTOForExternalPersonSupport.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPut]
        [Route("person/expire-support-external")]
        public ActionResult<CRUDResponseDTOForExternalPersonSupport> ExpirePersonSupportForExternal([FromBody] ExpirePersonSupportDTOForExternal expirePersonSupportInputDTO)
        {
            try
            {
                LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
                loggedInUserDTO.AgencyId = this.GetTenantID();
                loggedInUserDTO.UserId = this.GetUserID();

                var response = this.personService.ExpirePersonSupportForExternal(expirePersonSupportInputDTO, loggedInUserDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"ExpirePersonSupportForExternal/POST : Adding item : Exception  : Exception occurred ExpirePersonSupportForExternal. {ex.Message}");
                return this.HandleException(ex, "An error occurred ExpirePersonSupportForExternal. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPeopleSupportDetailsListForExternal.
        /// </summary>
        /// <param name="personSupportSearchInputDTO">personSupportSearchInputDTO.</param>
        /// <returns>PersonSupportResponseDTOForExternal.</returns>
        [Authorize(Policy = "APIUserPermission")]
        [HttpPost]
        [Route("person/support/list-external")]
        public ActionResult<PersonSupportResponseDTOForExternal> GetPeopleSupportDetailsListForExternal(PersonSupportSearchInputDTO personSupportSearchInputDTO)
        {
            try
            {
                LoggedInUserDTO loggedInUserDTO = new LoggedInUserDTO();
                loggedInUserDTO.AgencyId = this.GetTenantID();

                var response = this.personService.GetPeopleSupportListForExternal(personSupportSearchInputDTO, loggedInUserDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPeopleDetailsListForExternal/GET : Getting item : Exception  : Exception occurred Get PeopleDetails as List ForExternal. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving PeopleDetails List ForExternal. Please try again later or contact support.");
            }
        }
    }
}
