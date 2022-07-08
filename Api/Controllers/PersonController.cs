// -----------------------------------------------------------------------
// <copyright file="PersonController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.Infrastructure.Logging;

namespace Opeeka.PICS.Api.Controllers
{
    /// <summary>
    /// PersonController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class PersonController : BaseController
    {
        /// Initializes a new instance of the <see cref="personService"/> class.
        private readonly IPersonService personService;

        /// Initializes a new instance of the <see cref="personQuestionnaireMetricsService"/> class.
        private readonly IPersonQuestionnaireMetricsService personQuestionnaireMetricsService;

        /// Initializes a new instance of the <see cref="helperService"/> class.
        private readonly IHelperService helperService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<PersonController> logger;

        /// Initializes a new instance of the <see cref="lookupService"/> class.
        private readonly ILookupService lookupService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="personService">personService.</param>
        public PersonController(IPersonQuestionnaireMetricsService personQuestionnaireMetricsService, ILogger<PersonController> logger, IPersonService personService, IHelperService helperService, ILookupService lookupService)
        {
            this.lookupService = lookupService;
            this.personService = personService;
            this.helperService = helperService;
            this.personQuestionnaireMetricsService = personQuestionnaireMetricsService;
            this.logger = logger;
        }

        /// <summary>
        /// GetPeopleDetails.
        /// </summary>
        /// <param name="peopleIndex">peopleIndex.</param>
        /// <returns>PeopleDetailsResponseDTO.</returns>
        [HttpGet]
        [Route("person/{peopleIndex}")]
        public ActionResult<PeopleDetailsResponseDTO> GetPeopleDetails(Guid peopleIndex)
        {
            try
            {
                var agencyID = GetTenantID();
                var userID = GetUserID();
                var response = this.personService.GetPeopleDetails(peopleIndex, agencyID, userID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPeopleDetails/GET : Getting item : Exception  : Exception occurred getting people details. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving people detailst. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddPeopleDetails.
        /// </summary>
        /// <param name="peopleDetailsDTO">peopleDetailsDTO.</param>
        /// <returns>PeopleDetailsResponseDTO.</returns>
        [HttpPost]
        //[ServiceFilter(typeof(TransactionFilter))]
        [Route("person")]
        public ActionResult<AddPersonResponseDTO> AddPeopleDetails([FromBody] PeopleDetailsDTO peopleDetailsDTO)
        {
            try
            {
                peopleDetailsDTO.UpdateUserID = this.GetUserID();
                peopleDetailsDTO.AgencyID = this.GetTenantID();
                var response = this.personService.AddPeopleDetails(peopleDetailsDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddPeopleDetails/post : Adding item : Exception  : Exception occurred adding people details. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding people details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// RemovePeopleDetails.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpDelete]
        [Route("person/{personIndex}")]
        public ActionResult<CRUDResponseDTO> RemovePeopleDetails(Guid personIndex)
        {
            try
            {
                int updateUserID = this.GetUserID();
                long agencyID = this.GetTenantID();
                var response = this.personService.RemovePeopleDetails(personIndex, updateUserID, agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"RemovePeopleDetails/DELETE : Deleting item : Exception  : Exception occurred deleting people details. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting people details. Please try again later or contact support.");
            }
        }


        /// <summary>
        /// PersonQuestionaire.
        /// </summary>
        /// <param name="personQuestionnaireDetailsDTO">personQuestionnaireDetailsDTO.</param>
        /// <returns>Action Result CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("personQuestionnaire")]
        public ActionResult<CRUDResponseDTO> PersonQuestionnaire([FromBody] PersonQuestionnaireDetailsDTO personQuestionnaireDetailsDTO)
        {
            try
            {
                long agencyID = this.GetTenantID();
                var response = this.personService.AddPersonQuestionaire(personQuestionnaireDetailsDTO, this.GetUserID(), agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"PersonQuestionaire/post : Adding item : Exception  : Exception occurred adding person questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding person questionnaire. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPeopleDetails.
        /// </summary>
        /// <param name="peopleEditDetailsDTO">peopleDetailsDTO.</param>
        /// <returns>PeopleDetailsResponseDTO.</returns>
        [HttpPut]
        //[ServiceFilter(typeof(TransactionFilter))]
        [Route("person")]
        public ActionResult<CRUDResponseDTO> EditPeopleDetails([FromBody] PeopleEditDetailsDTO peopleEditDetailsDTO)
        {
            try
            {
                var userID = this.GetUserID();
                peopleEditDetailsDTO.UpdateUserID = userID;
                long agencyID = this.GetTenantID();
                var response = this.personService.EditPeopleDetails(peopleEditDetailsDTO, agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"EditPeopleDetails/PUT : Updating item : Exception  : Exception occurred updating people details. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating people details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetRiskNotificationList.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>RiskNotificationDetailsResponseDTO.</returns>
        [HttpGet]
        [Route("risk-notifications/{personIndex}")]
        public ActionResult<RiskNotificationDetailsResponseDTO> GetRiskNotificationList(Guid personIndex)
        {
            try
            {
                var response = this.personService.GetRiskNotificationList(personIndex);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetRiskNotificationList/Get : Listing Risk notifications : Exception  : Exception occurred listing Risk notifications. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing Risk notifications. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPastNotificationList.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>PastNotificationDetailsResponseDTO.</returns>
        [HttpGet]
        [Route("past-notifications/{personIndex}")]
        public ActionResult<PastNotificationDetailsResponseDTO> GetPastNotificationList(Guid personIndex)
        {
            try
            {
                var response = this.personService.GetPastNotificationList(personIndex);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetPastNotificationList/Get : Listing past notifications : Exception  : Exception occurred listing past notifications. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing past notifications. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllQuestionnaireForPerson.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>QuestionnairesForPersonDTO.</returns>
        [HttpGet]
        [Route("questionnaires-data/{personIndex}/{pageNumber}/{pageSize}")]
        public ActionResult<QuestionnairesForPersonDTO> GetAllQuestionnaireForPerson(Guid personIndex, int pageNumber, int pageSize)
        {
            try
            {
                QuestionnairesForPersonDTO questionnairesForPersonDTO = this.personService.GetAllQuestionnaireForPerson(this.GetTenantID(), personIndex, pageNumber, pageSize);
                return questionnairesForPersonDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllQuestionnaireForPerson/GET : Getting item : Exception  : Exception occurred Getting Questionnaire List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving questionnaire list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateNotificationStatus.
        /// </summary>
        /// <param name="notificationLogID">notificationLogID.</param>
        /// <param name="status">status.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPut]
        [Route("update-notification-status/{notificationLogID}/{status}")]
        public ActionResult<CRUDResponseDTO> UpdateNotificationStatus(int notificationLogID, string status)
        {
            try
            {
                var response = this.personService.UpdateNotificationStatus(notificationLogID, status, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateNotificationStatus/PUT : Updating item : Exception  : Exception occurred updating notification status. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating notification status. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllNotes.
        /// </summary>
        /// <param name="notificationLogID">notificationLogID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>PastNotesResponseDTO.</returns>
        [HttpGet]
        [Route("past-notes/{notificationLogID}/{pageNumber}/{pageSize}")]
        public ActionResult<PastNotesResponseDTO> GetAllPastNotes(int notificationLogID, int pageNumber, int pageSize)
        {
            try
            {
                PastNotesResponseDTO pastNotesResponse = this.personService.GetAllPastNotes(notificationLogID, pageNumber, pageSize);
                return pastNotesResponse;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllPastNotes/GET : Getting item : Exception  : Exception occurred getting all past notes. {ex.Message}");
                return this.HandleException(ex, "An error occurred getting all past notes. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddNotificationNote.
        /// </summary>
        /// <param name="notificationNoteDataDTO">notificationNoteDataDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPut]
        [Route("add-notification-note")]
        public ActionResult<CRUDResponseDTO> AddNotificationNote([FromBody] NotificationNoteDataDTO notificationNoteDataDTO)
        {
            try
            {
                int userID = 0;
                userID = this.GetUserID();
                var response = this.personService.AddNotificationNote(notificationNoteDataDTO, userID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddNotificationNote/PUT : Adding item : Exception  : Exception occurred inserting notification note. {ex.Message}");
                return this.HandleException(ex, "An error occurred inserting notification note. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonHelpingCount.
        /// </summary>
        /// <param name="helperIndex">helperIndex.</param>
        /// <returns>PersonHelping Count.</returns>
        [HttpGet]
        [Route("personhelping-count/{helperIndex?}")]
        public ActionResult<PersonHelpingResponseDTO> GetPersonHelpingCount(Guid? helperIndex)
        {
            try
            {
                int? helperID = null;
                bool isSameAsLoggedInUser = false;
                var userID = this.GetUserID();
                var userRole = this.GetRole();
                var agencyID = this.GetTenantID();

                if (helperIndex == null)
                {
                    var helperData = this.helperService.GetUserDetails(Convert.ToInt32(userID));
                    if (helperData != null && helperData.UserDetails != null)
                    {
                        helperID = helperData.UserDetails.HelperID;
                    }
                }
                else
                {
                    var helperInfo = this.helperService.GetHelperInfo((Guid)helperIndex,agencyID);
                    if (helperInfo != null && helperInfo.HelperDetails != null)
                    {
                        helperID = helperInfo.HelperDetails.HelperID;
                        List<string> role = new List<string>();
                        role.Add(helperInfo.HelperDetails.Role);
                        userRole = role;
                        if (helperInfo.HelperDetails.UserId == userID)
                        {
                            isSameAsLoggedInUser = true;
                        }
                    }
                    else
                    {
                        PersonHelpingResponseDTO emptyResponseDTO = new PersonHelpingResponseDTO();
                        emptyResponseDTO.PersonHelpingCount = 0;
                        emptyResponseDTO.LeadHelpingCount = 0;
                        emptyResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                        emptyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return emptyResponseDTO;
                    }
                }

                var response = this.personService.GetPersonHelpingCount(helperID, agencyID, userRole, isSameAsLoggedInUser);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonHelpingCount/GET : Get item : Exception  : Exception occurred getting person helper count. {ex.Message}");
                return this.HandleException(ex, "An error occurred getting person helper count. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllQuestionnaireWithCompletedAssessment For Reports.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="personCollaborationID">personCollaborationID.</param>
        /// <param name="voicetypeID">voicetypeID.</param>
        /// <param name="voiceTypeFKID">voicetypeFKID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>AssessedQuestionnairesForPersonDTO.</returns>
        [HttpGet]
        [Route("assessed-questionnaires/{personIndex}/{personCollaborationID}/{voicetypeID}/{voiceTypeFKID}/{pageNumber}/{pageSize}")]
        public ActionResult<AssessedQuestionnairesForPersonDTO> GetAllQuestionnaireWithCompletedAssessment(Guid personIndex, long personCollaborationID, int voicetypeID, long voiceTypeFKID, int pageNumber, int pageSize)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();

                AssessedQuestionnairesForPersonDTO assessedQuestionnairesForPersonDTO = this.personService.GetAllQuestionnaireWithCompletedAssessment(userTokenDetails, personIndex, pageNumber, pageSize, personCollaborationID, voicetypeID, voiceTypeFKID);
                return assessedQuestionnairesForPersonDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllQuestionnaireWithCompletedAssessment/GET : Getting item : Exception  : Exception occurred Getting Questionnaire List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving questionnaire list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonList.
        /// </summary>
        /// <param name="personSearchDTO">personSearchDTO.</param>
        /// <returns>GetPersonListDTO.</returns>
        [HttpPost]
        [Route("persons/list")]
        public ActionResult<GetPersonListDTO> GetPersonList([FromBody] PersonSearchDTO personSearchDTO)
        {
            try
            {
                int? helperID = null;
                bool isSameAsLoggedInUser = false;
                int userID = this.GetUserID();
                var userRole = this.GetRole();
                var agencyID = this.GetTenantID();

                personSearchDTO.userID = userID;
                if (personSearchDTO.helperIndex == null)
                {
                    var helperData = this.helperService.GetUserDetails(Convert.ToInt32(userID));
                    if (helperData != null && helperData.UserDetails != null)
                    {
                        helperID = helperData.UserDetails.HelperID;
                    }
                }
                else
                {
                    var helperInfo = this.helperService.GetHelperInfo((Guid)personSearchDTO.helperIndex, agencyID);
                    if (helperInfo != null && helperInfo.HelperDetails != null)
                    {
                        personSearchDTO.userID = helperInfo.HelperDetails.UserId;
                        helperID = helperInfo.HelperDetails.HelperID;
                        userRole = new List<string>() { helperInfo.HelperDetails.Role };
                        if (helperInfo.HelperDetails.UserId == userID)
                        {
                            isSameAsLoggedInUser = true;
                        }
                    }
                    else
                    {
                        GetPersonListDTO emptyResponseDTO = new GetPersonListDTO();
                        emptyResponseDTO.PersonList = null;
                        emptyResponseDTO.TotalCount = 0;
                        emptyResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                        emptyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return emptyResponseDTO;
                    }
                }

                personSearchDTO.isSameAsLoggedInUser = isSameAsLoggedInUser;
                personSearchDTO.helperID = helperID.HasValue ? helperID.Value : 0;
                personSearchDTO.userRole = userRole;
                personSearchDTO.agencyID = agencyID;
                var response = this.personService.GetPersonsListByHelperID(personSearchDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonList/POST : Getting item : Exception  : Exception occurred Getting person list. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving person list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonInitials.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>PersonInitialsResponseDTO.</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("person-initials/{personIndex}")]
        public ActionResult<PersonInitialsResponseDTO> GetPersonInitials(Guid personIndex)
        {
            try
            {
                var response = this.personService.GetPersonInitials(personIndex);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonInitials/GET : Getting item : Exception  : Exception occurred Getting person initials. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving person initials. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get the list of collaborations assigned to a person.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>PersonCollaborationResponseDTO.</returns>
        [HttpGet]
        [Route("person-collaborations/{personIndex}/{questionnaireID}")]
        public ActionResult<PersonCollaborationResponseDTO> GetPeopleCollaborationList(Guid personIndex, int questionnaireID)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                var response = this.personService.GetPeopleCollaborationList(personIndex, userTokenDetails, questionnaireID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPeopleCollaborationList/GET : Getting item : Exception  : Exception occurred getting person collaboration list. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving person collaboration list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get the list of collaborations assigned to a person in Reports.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="personQuestionaireID">personQuestionaireID.</param>
        /// <param name="voiceTypeID">voiceTypeID.</param>
        /// <returns>PersonCollaborationResponseDTO.</returns>
        [HttpGet]
        [Route("person-collaborations-reports/{personIndex}/{personQuestionaireID}/{voiceTypeID}")]
        public ActionResult<PersonCollaborationResponseDTO> GetPeopleCollaborationListForReport(Guid personIndex, long personQuestionaireID, int voiceTypeID)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                var response = this.personService.GetPeopleCollaborationListForReport(personIndex, personQuestionaireID, voiceTypeID, userTokenDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPeopleCollaborationListForReport/GET : Getting item : Exception  : Exception occurred getting person collaboration for report. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving person collaboration for report. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonDetails.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>PersonDetailsResponseDTO.</returns>
        [HttpGet]
        [Route("person-details/{personIndex}")]
        public ActionResult<PersonDetailsResponseDTO> GetPersonDetails(Guid personIndex)
        {
            try
            {
                var response = this.personService.GetPersonDetails(personIndex, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonDetails/GET : Get item : Exception  : Exception occurred getting person details. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving person details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPastNotificationList.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="notificationLogSearchDTO">notificationLogSearchDTO.</param>
        /// <returns>NotificationLogResponseDTO.</returns>
        [HttpPost]
        [Route("past-notification-list/{personIndex}")]
        public ActionResult<NotificationLogResponseDTO> GetPastNotifications(Guid personIndex, [FromBody] NotificationLogSearchDTO notificationLogSearchDTO)
        {
            try
            {
                int? helperID = null;
                var userID = this.GetUserID();
                var userRole = this.GetRole();
                var agencyID = this.GetTenantID();

                if (notificationLogSearchDTO.helperIndex == null)
                {
                    var helperData = this.helperService.GetUserDetails(Convert.ToInt32(userID));
                    if (helperData != null && helperData.UserDetails != null)
                    {
                        helperID = helperData.UserDetails.HelperID;
                    }
                }
                else
                {
                    var helperInfo = this.helperService.GetHelperInfo((Guid)notificationLogSearchDTO.helperIndex, agencyID);
                    if (helperInfo != null && helperInfo.HelperDetails != null)
                    {
                        helperID = helperInfo.HelperDetails.HelperID;
                        userRole = new List<string>() { helperInfo.HelperDetails.Role };
                    }
                    else
                    {
                        NotificationLogResponseDTO notificationLogDTO = new NotificationLogResponseDTO();
                        notificationLogDTO.NotificationLog = null;
                        notificationLogDTO.TotalCount = 0;
                        notificationLogDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                        notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return notificationLogDTO;
                    }
                }

                notificationLogSearchDTO.helperID = helperID.HasValue ? helperID.Value : 0;
                notificationLogSearchDTO.userRole = userRole;
                notificationLogSearchDTO.agencyID = agencyID;
                notificationLogSearchDTO.UserID = userID;
                var response = this.personService.GetPastNotifications(personIndex, notificationLogSearchDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetPastNotifications/POST : Listing past notifications : Exception  : Exception occurred listing past notifications. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing past notifications. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetCurrentNotificationList.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="notificationLogSearchDTO">notificationLogSearchDTO.</param>
        /// <returns>NotificationLogResponseDTO.</returns>
        [HttpPost]
        [Route("present-notification-list/{personIndex}")]
        public ActionResult<NotificationLogResponseDTO> GetPresentNotifications(Guid personIndex, [FromBody] NotificationLogSearchDTO notificationLogSearchDTO)
        {
            try
            {
                int? helperID = null;
                var userID = this.GetUserID();
                var userRole = this.GetRole();
                var agencyID = this.GetTenantID();

                if (notificationLogSearchDTO.helperIndex == null)
                {
                    var helperData = this.helperService.GetUserDetails(Convert.ToInt32(userID));
                    if (helperData != null && helperData.UserDetails != null)
                    {
                        helperID = helperData.UserDetails.HelperID;
                    }
                }
                else
                {
                    var helperInfo = this.helperService.GetHelperInfo((Guid)notificationLogSearchDTO.helperIndex, agencyID);
                    if (helperInfo != null && helperInfo.HelperDetails != null)
                    {
                        helperID = helperInfo.HelperDetails.HelperID;
                        userRole = new List<string>() { helperInfo.HelperDetails.Role };
                    }
                    else
                    {
                        NotificationLogResponseDTO notificationLogDTO = new NotificationLogResponseDTO();
                        notificationLogDTO.NotificationLog = null;
                        notificationLogDTO.TotalCount = 0;
                        notificationLogDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                        notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return notificationLogDTO;
                    }
                }

                notificationLogSearchDTO.helperID = helperID.HasValue ? helperID.Value : 0;
                notificationLogSearchDTO.userRole = userRole;
                notificationLogSearchDTO.agencyID = agencyID;
                notificationLogSearchDTO.UserID = userID;
                var response = this.personService.GetPresentNotifications(personIndex, notificationLogSearchDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetPresentNotifications/POST : Listing present notifications : Exception  : Exception occurred listing present notifications. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing present notifications. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPeopleSharingDetails.
        /// </summary>
        /// <param name="peopleIndex">peopleIndex.</param>
        /// <returns>PersonSharingDetailsDTO.</returns>
        [HttpGet]
        [Route("person-sharing-details/{peopleIndex}")]
        public ActionResult<PersonSharedDetailsResponseDTO> GetPersonSharingDetails(Guid peopleIndex)
        {
            try
            {
                var agencyID = this.GetTenantID();
                var response = this.personService.GetPersonSharingDetails(peopleIndex, agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonSharingDetails/GET : Getting person sharing details : Exception  : Exception occurred getting person sharing details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting person sharing details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// RemovePersonQuestionnaire.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpDelete]
        [Route("person-questionnaire/{personIndex}/{questionnaireID}")]
        public ActionResult<CRUDResponseDTO> RemovePersonQuestionnaire(Guid personIndex, int questionnaireID)
        {
            try
            {
                int updateUserID = this.GetUserID();
                var response = this.personService.RemovePersonQuestionnaire(personIndex, updateUserID, questionnaireID, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"RemovePersonQuestionnaire/delete : Deleting item : Exception  : Exception occurred deleting person questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting personQuestionnaire info. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpsertPerson.
        /// </summary>
        /// <param name="peopleDetailsDTO">List of peopleDetailsDTO. </param>
        /// <param name="isClosed">Records to usert are of open or closed status. </param>
        /// <returns>AddPersonResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-upsert/{isClosed}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> UpsertPerson([FromBody] List<PeopleDetailsDTO> peopleDetailsDTO, bool isClosed)
        {
            try
            {
                var response = this.personService.UpsertPerson(peopleDetailsDTO, isClosed);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"UpsertPerson/post : Adding item : Exception  : Exception occurred upserting Person. {ex.Message}");
                return this.HandleException(ex, "An error occurred upserting Person. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireById.
        /// </summary>
        /// <param name="personQuestionnaireId">personQuestionnaireId.</param>
        /// <returns>PersonQuestionnaireDetailsDTo.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("person-questionnaire/{personQuestionnaireId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<PersonQuestionnaireDetailsDTo> GetPersonQuestionnaireById(int personQuestionnaireId)
        {
            try
            {
                var response = this.personService.GetPersonQuestionnaireById(personQuestionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonQuestionnaireById/GET : Getting item : Exception  : Exception occurred while recieving person questionnaire details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving person questionnaire details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonQuestionaireByPersonQuestionaireID.
        /// </summary>
        /// <param name="personQuestionnaireId">personQuestionnaireId.</param>
        /// <returns>PersonQuestionnaireListDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("person-questionnaire-list/{personQuestionnaireId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<PersonQuestionnaireListDetailsDTO> GetPersonQuestionaireByPersonQuestionaireID(int personQuestionnaireId)
        {
            try
            {
                var response = this.personService.GetPersonQuestionaireByPersonQuestionaireID(personQuestionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonQuestionaireByPersonQuestionaireID/GET : Getting item : Exception  : Exception occurred while recieving person questionnaire details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving person questionnaire details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonCollaborationByPersonIdAndCollaborationId.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <param name="collaborationId">collaborationId.</param>
        /// <returns>PersonCollaborationDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("person-collaboration-for-reminder/{personId}/{collaborationId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<PersonCollaborationDetailsDTO> GetPersonCollaborationByPersonIdAndCollaborationId(long personId, int? collaborationId)
        {
            try
            {
                var response = this.personService.GetPersonCollaborationByPersonIdAndCollaborationId(personId, collaborationId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonCollaborationByPersonIdAndCollaborationId/GET : Getting item : Exception  : Exception occurred while recieving person collaboration details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving person collaboration details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireSchedule.
        /// </summary>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <param name="questionnaireWindowId">QuestionnaireWindowId.</param>
        /// <returns>PersonQuestionnaireScheduleDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("person-questionnaire-schedule/{personQuestionnaireId}/{questionnaireWindowId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<PersonQuestionnaireScheduleDetailsDTO> GetPersonQuestionnaireSchedule(long personQuestionnaireID, int questionnaireWindowId)
        {
            try
            {
                var response = this.personService.GetPersonQuestionnaireSchedule(personQuestionnaireID, questionnaireWindowId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonQuestionnaireSchedule/GET : Getting item : Exception  : Exception occurred while recieving person questionnaire Schedule. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving person questionnaire Schedule. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateBulkPersonQuestionnaireSchedule.
        /// </summary>
        /// <param name="personQuestionnaireSchedules">personQuestionnaireSchedules.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPut]
        [Route("person-questionnaire-schedule")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> UpdateBulkPersonQuestionnaireSchedule([FromBody] List<PersonQuestionnaireScheduleDTO> personQuestionnaireSchedules)
        {
            try
            {
                var response = this.personService.UpdateBulkPersonQuestionnaireSchedule(personQuestionnaireSchedules);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateBulkPersonQuestionnaireSchedule/PUT : Getting item : Exception  : Exception occurred while updating person questionnaire Schedule. {ex.Message}");
                return this.HandleException(ex, "An error occurred while updating person questionnaire Schedule. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddPersonQuestionnaireSchedule.
        /// </summary>
        /// <param name="personQuestionnaireSchedules">personQuestionnaireSchedules.</param>
        /// <returns>AddPersonQuestionnaireScheduleResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-questionnaire-schedule")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<AddPersonQuestionnaireScheduleResponseDTO> AddPersonQuestionnaireSchedule([FromBody] PersonQuestionnaireScheduleDTO personQuestionnaireSchedules)
        {
            try
            {
                var response = this.personService.AddPersonQuestionnaireSchedule(personQuestionnaireSchedules);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddPersonQuestionnaireSchedule/PUT : Adding item : Exception  : Exception occurred while Adding person questionnaire Schedule. {ex.Message}");
                return this.HandleException(ex, "An error occurred while Adding person questionnaire Schedule. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireScheduleForReminder.
        /// </summary>
        /// <param name="personQuestionnaireSchedule">personQuestionnaireSchedule.</param>
        /// <returns>PersonQuestionnaireScheduleDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-questionnaire-schedule-list")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<PersonQuestionnaireScheduleDetailsDTO> GetPersonQuestionnaireScheduleForReminder([FromBody] PersonQuestionnaireScheduleInputDTO personQuestionnaireSchedule)
        {
            try
            {
                var response = this.personService.GetPersonQuestionnaireScheduleForReminder(personQuestionnaireSchedule);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"GetPersonQuestionnaireScheduleForReminder/Post : GettiPostingng item : Exception  : Exception occurred while GetPersonQuestionnaireScheduleForReminder. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding PersonQuestionnaireScheduleForReminder. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// ImportPerson.
        /// </summary>
        /// <param name="peopleDetailsDTO">peopleDetailsDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("import-person")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> ImportPerson([FromBody] List<PeopleDetailsDTO> peopleDetailsDTO)
        {
            try
            {
                CRUDResponseDTO response = this.personService.ImportPerson(peopleDetailsDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"ImportPerson/POST : Adding item : Exception  : Exception occurred inporting Person. {ex.Message}");
                return this.HandleException(ex, "An error occurred importing Person. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonsAndHelpersByPersonIDListForAlert.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>PersonHelperEmailDetailDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("person-helper-bypersonId/{personId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<PersonHelperEmailDetailDTO> GetPersonsAndHelpersByPersonIDListForAlert(long personId)
        {
            try
            {
                var response = this.personService.GetPersonsAndHelpersByPersonIDListForAlert(personId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonsAndHelpersByPersonIDListForAlert/GET : Getting item : Exception  : Exception occurred while GetPersonsAndHelpersByPersonIDListForAlert. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetPersonsAndHelpersByPersonIDListForAlert. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonsAndHelpersByPersonIDListForAlert.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>PersonHelperEmailDetailDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-helper-bypersonId")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<PersonHelperEmailDetailDTO> GetPersonsAndHelpersByPersonIDList([FromBody] List<long> personId)
        {
            try
            {
                var response = this.personService.GetPersonsAndHelpersByPersonIDList(personId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonsAndHelpersByPersonIDList/Post : GettiPostingng item : Exception  : Exception occurred while GetPersonsAndHelpersByPersonIDList. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetPersonsAndHelpersByPersonIDList. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetActivePersons.
        /// </summary>
        /// <returns>ActivePersonResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("active-person")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ActivePersonResponseDTO> GetActivePersons()
        {
            try
            {
                var response = this.personService.GetActivePersons();
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetActivePersons/Get : Get item : Exception  : Exception occurred while getting ActivePersons. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting ActivePersons. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetActivePersonCollaboration.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>ActivePersonResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("active-person-collaboration")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ActivePersonResponseDTO> GetActivePersonCollaboration(List<long> personId)
        {
            try
            {
                var response = this.personService.GetActivePersonCollaboration(personId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetActivePersonCollaboration/POST : Get item : Exception  : Exception occurred while getting ActivePersonCollaboration. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting ActivePersonCollaboration. Please try again later or contact support.");
            }
        }
        /// <summary>
        /// GetVoiceTypeRelatedDetailsOfPerson.
        /// </summary>
        /// <param name="allPersonsToUpload">allPersonsToUpload.</param>
        /// <returns>EHRLookupResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-support-helper-details")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<EHRLookupResponseDTO> GetVoiceTypeRelatedDetailsOfPersonForImport(PersonIndexToUploadDTO allPersonsToUpload)
        {
            try
            {
                var response = this.personService.GetVoiceTypeRelatedDetailsOfPerson(allPersonsToUpload);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetVoiceTypeRelatedDetailsOfPersonForImport/POST : Exception occurred in GetVoiceTypeRelatedDetailsOfPerson. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving VoiceTypeRelatedDetailsOfPerson for Import. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateIsActiveForPerson.
        /// </summary>
        /// <param name="personIds">personIds.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPut]
        [Route("person-isactive-update")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> UpdateIsActiveForPerson([FromBody] List<long> personIds)
        {
            try
            {
                var response = this.personService.UpdateIsActiveForPerson(personIds);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateIsActiveForPerson/PUT : PUT item : Exception  : Exception occurred while Updating ActiveForPerson. {ex.Message}");
                return this.HandleException(ex, "An error occurred while Updating ActiveForPerson. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetNotifyReminderScheduledForToday.
        /// </summary>
        /// <param name="triggerTime">triggerTime.</param>
        /// <returns>ReminderNotificationScheduleResponse.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("notify-reminder-schedule")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ReminderNotificationScheduleResponse> GetNotifyReminderScheduledForToday(RemiderNotificationTriggerTimeDTO triggerTime)
        {
            try
            {
                var response = this.personService.GetNotifyReminderScheduledForToday(triggerTime.LastRunDatetime, triggerTime.CurrentRunDatetime);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetNotifyReminderScheduledForToday/Get : Get item : Exception  : Exception occurred while GetNotifyReminderScheduledForToday. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetNotifyReminderScheduledForToday. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetNotifyReminderScheduledCountForToday.
        /// </summary>
        /// <param name="triggerTime">triggerTime.</param>
        /// <returns>ReminderNotificationScheduleResponse.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("notify-reminder-schedule-count")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ReminderNotificationScheduleResponse> GetNotifyReminderScheduledCountForToday(RemiderNotificationTriggerTimeDTO triggerTime)
        {
            try
            {
                var response = this.personService.GetNotifyReminderScheduledCountForToday(triggerTime.LastRunDatetime, triggerTime.CurrentRunDatetime);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetNotifyReminderScheduledCountForToday/Get : Get item : Exception  : Exception occurred while GetNotifyReminderScheduledCountForToday. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetNotifyReminderScheduledCountForToday. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetDetailsByPersonQuestionScheduleList.
        /// </summary>
        /// <param name="personScheduleId">personScheduleId.</param>
        /// <returns>ReminderNotificationScheduleResponse.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-questionnire-schedule-byids")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ReminderNotificationScheduleResponse> GetDetailsByPersonQuestionScheduleList([FromBody]List<long> personScheduleId)
        {
            try
            {
                var response = this.personService.GetDetailsByPersonQuestionScheduleList(personScheduleId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetDetailsByPersonQuestionScheduleList/POST : Get item : Exception  : Exception occurred while getting details by Person QuestionSchedule List. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting details by Person QuestionSchedule List. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddPersonQuestionnaireMetrics.
        /// </summary>
        /// <param name="personQuestionnaireMetrics">personQuestionnaireMetrics.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-questionnaire-metrics")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> AddPersonQuestionnaireMetrics([FromBody] List<PersonQuestionnaireMetricsDTO> personQuestionnaireMetrics)
        {
            try
            {
                var response = this.personQuestionnaireMetricsService.AddPersonQuestionnaireMetrics(personQuestionnaireMetrics);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddPersonQuestionnaireMetrics/POST : POST item : Exception  : Exception occurred while adding person questionnaire metrics. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding person questionnaire metrics. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdatePersonQuestionnaireMetrics.
        /// </summary>
        /// <param name="personQuestionnaireMetrics">personQuestionnaireMetrics.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPut]
        [Route("person-questionnaire-metrics")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> UpdatePersonQuestionnaireMetrics([FromBody] List<PersonQuestionnaireMetricsDTO> personQuestionnaireMetrics)
        {
            try
            {
                var response = this.personQuestionnaireMetricsService.UpdatePersonQuestionnaireMetrics(personQuestionnaireMetrics);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdatePersonQuestionnaireMetrics/Put : put item : Exception  : Exception occurred while Updating PersonQuestionnaireMetrics. {ex.Message}");
                return this.HandleException(ex, "An error occurred while Updating PersonQuestionnaireMetrics. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireMetrics.
        /// </summary>
        /// <param name="metricsInput">metricsInput.</param>
        /// <returns>PersonQuestionnaireMetricsDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-questionnaire-metrics-list")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<PersonQuestionnaireMetricsDetailsDTO> GetPersonQuestionnaireMetrics([FromBody] DashboardMetricsInputDTO metricsInput)
        {
            try
            {
                var response = this.personQuestionnaireMetricsService.GetPersonQuestionnaireMetrics(metricsInput);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonQuestionnaireMetrics/post : get item : Exception  : Exception occurred while getting person questionnaire metrics. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting person questionnaire metrics. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAuditPersonProfileDetails.
        /// </summary>
        /// <param name="peopleIndex"></param>
        /// <param name="historyType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("audit-person-profile/{peopleIndex}/{historyType}")]
        public ActionResult<AuditPersonProfileResponseDTO> GetAuditPersonProfileDetails(Guid peopleIndex, string historyType)
        {
            try
            {
                var agencyID = GetTenantID();
                var response = this.personService.GetAuditPersonProfileDetails(peopleIndex, agencyID, historyType);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAuditPersonProfileDetails/GET : Getting item : Exception  : Exception occurred getting Audit PersonProfile details. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving people Audit PersonProfile details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// StopSMSInvitationSending,
        /// </summary>
        /// <param name="twilioRequest">twilioRequest.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded")]
        [Route("stop-sms-consent")]
        public IActionResult StopSMSInvitationSending([FromForm] TwilioRequest twilioRequest)
        {
            try
            {
                this.logger.LogInformation(MyLogEvents.TestItem, twilioRequest.From, twilioRequest.To, twilioRequest.Body);
                var response = this.personService.StopSMSInvitationSending(twilioRequest);
                this.logger.LogInformation(MyLogEvents.TestItem, response.ToString());
                return this.Content(response, "text/xml");
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"StopSMSInvitationSending/GET : Getting item : Exception  : Exception occurred while Stop SMSInvitation Sending. {ex.Message}");
                return this.HandleException(ex, $"An error occurred while Stop SMSInvitation Sending. Please try again later or contact support.{ex.Message}");
            }
        }

        /// <summary>
        /// GetPersonAssessmentMetricsInDetail.
        /// </summary>
        /// <param name="metricsInput">metricsInput.</param>
        /// <returns>PersonAssessmentMetricsDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-assessment-metrics-list")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<PersonAssessmentMetricsDetailsDTO> GetPersonAssessmentMetricsInDetail([FromBody] DashboardMetricsInputDTO metricsInput)
        {
            try
            {
                var response = this.personQuestionnaireMetricsService.GetPersonAssessmentMetricsInDetail(metricsInput);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetPersonAssessmentMetrics/post : get item : Exception  : Exception occurred while getting person assessment metrics. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting person assessment metrics. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddPersonQuestionnaireMetrics.
        /// </summary>
        /// <param name="personAssessmentMetrics">personQuestionnaireMetrics.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-assessment-metrics")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> AddPersonAssessmentMetrics([FromBody] List<PersonAssessmentMetricsDTO> personAssessmentMetrics)
        {
            try
            {
                var response = this.personQuestionnaireMetricsService.AddBulkPersonAssessmentMetrics(personAssessmentMetrics);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddPersonAssessmentMetrics/POST : POST item : Exception  : Exception occurred while adding person assessment metrics. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding person assessment metrics. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdatePersonAssessmentMetrics.
        /// </summary>
        /// <param name="personAssessmentMetrics">personQuestionnaireMetrics.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPut]
        [Route("person-assessment-metrics")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> UpdatePersonAssessmentMetrics([FromBody] List<PersonAssessmentMetricsDTO> personAssessmentMetrics)
        {
            try
            {
                var response = this.personQuestionnaireMetricsService.UpdateBulkPersonAssessmentMetrics(personAssessmentMetrics);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdatePersonAssessmentMetrics/Put : put item : Exception  : Exception occurred while Updating Person assessment Metrics. {ex.Message}");
                return this.HandleException(ex, "An error occurred while Updating Person assessment Metrics. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllPersonQuestionnairesRegularSchedule.
        /// </summary>
        /// <returns>PersonQuestionnairesRegularScheduleResponse.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-questionnaire-schedule-details")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<PersonQuestionnairesRegularScheduleResponse> GetAllPersonQuestionnairesRegularSchedule(List<long> lst_PersonQuestionnaireIds)
        {
            try
            {
                var response = this.personService.GetAllPersonQuestionnairesRegularSchedule(lst_PersonQuestionnaireIds);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllPersonQuestionnairesRegularSchedule/Get : Get item : Exception  : Exception occurred while GetAllPersonQuestionnairesRegularSchedule. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetAllPersonQuestionnairesRegularSchedule. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddBulkPersonQuestionnaireSchedule.
        /// </summary>
        /// <param name="personQuestionnaireSchedules">personQuestionnaireSchedules.</param>
        /// <returns>AddPersonQuestionnaireScheduleResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-questionnaire-schedule-bulk")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<AddBulkPersonQuestionnaireScheduleResponseDTO> AddBulkPersonQuestionnaireSchedule([FromBody] List<PersonQuestionnaireScheduleDTO> personQuestionnaireSchedules)
        {
            try
            {
                var response = this.personService.AddBulkPersonQuestionnaireSchedule(personQuestionnaireSchedules);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddBulkPersonQuestionnaireSchedule/POST : Adding item : Exception  : Exception occurred while Adding bulk person questionnaire Schedule. {ex.Message}");
                return this.HandleException(ex, "An error occurred while Adding bulk person questionnaire Schedule. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllPersonCollaborationForReminders.
        /// </summary>
        /// <param name="list_personCollaborationIds">list_personCollaborationIds</param>
        /// <returns>PersonCollaborationResponseDTO</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("person-collaboration-regular-schedules")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<PersonCollaborationResponseDTO> GetAllPersonCollaborationForReminders([FromBody] List<long> list_personCollaborationIds)
        {
            try
            {
                var response = this.personService.GetAllPersonCollaborationForReminders(list_personCollaborationIds);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllPersonCollaborationForReminders/GET : Getting item : Exception  : Exception occurred while GetAllPersonCollaborationForReminders. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetAllPersonCollaborationForReminders. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllPersonQuestionnairesToBeScheduled.
        /// </summary>
        /// <returns>PersonQuestionnairesRegularScheduleResponse.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("person-questionnaires-tobescheduled")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<PersonQuestionnairesRegularScheduleResponse> GetAllPersonQuestionnairesToBeScheduled()
        {
            try
            {
                var response = this.personService.GetAllPersonQuestionnairesToBeScheduled();
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllPersonQuestionnairesToBeScheduled/Get : Get item : Exception  : Exception occurred while GetAllPersonQuestionnairesToBeScheduled. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetAllPersonQuestionnairesToBeScheduled. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllSupportsForPersonAnonymously.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>PersonSupportLookupResponseDTO.</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("person-supports-anonymous/{personIndex}")]
        public ActionResult<PersonSupportLookupResponseDTO> GetAllSupportsForPersonAnonymously(Guid personIndex)
        {
            try
            {
                PersonSupportLookupResponseDTO personSupportLookupResponseDTO = this.lookupService.GetAllSupportsForPerson(personIndex);
                return personSupportLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllSupportsForPersonAnonymously/GET : Getting item : Exception  : Exception occurred Getting All Support For Person List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving person support list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetActiveVoiceTypes(active supports).
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>VoiceTypeResponseDTO.</returns>
        [HttpGet]
        [Route("active-voice-type/{personIndex}")]
        public ActionResult<VoiceTypeResponseDTO> GetActiveVoiceTypeInDetail(Guid personIndex)
        {
            try
            {
                VoiceTypeResponseDTO voiceTypeLookupResponseDTO = this.lookupService.GetActiveVoiceTypeInDetail(personIndex);
                return voiceTypeLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetActiveVoiceTypeInDetail/GET : Getting item : Exception  : Exception occurred Getting ActiveVoiceTypeInDetail. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving ActiveVoiceTypeInDetail. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetVoiceTypeForFilter.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="personQuestionaireID">personQuestionaireID.</param>
        /// <returns>VoiceTypeResponseDTO.</returns>
        [HttpGet]
        [Route("voice-type-filters/{personIndex}/{personQuestionaireID}")]
        public ActionResult<VoiceTypeResponseDTO> GetVoiceTypeForFilter(Guid personIndex, long personQuestionaireID)
        {
            try
            {
                var agencyID = this.GetTenantID();
                VoiceTypeResponseDTO voiceTypeLookupResponseDTO = this.lookupService.GetVoiceTypeForFilter(personIndex, personQuestionaireID, agencyID);
                return voiceTypeLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetVoiceTypeForFilter/GET : Getting item : Exception  : Exception occurred Getting Voice type filter List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving voice type filter list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllVoiceType for Reports.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="personQuestionaireID">personQuestionaireID.</param>
        /// <param name="personCollaborationID">personCollaborationID.</param>
        /// <returns>VoiceTypeResponseDTO.</returns>
        [HttpGet]
        [Route("voice-type-reports/{personIndex}/{personQuestionaireID}/{personCollaborationID}")]
        public ActionResult<VoiceTypeResponseDTO> GetAllVoiceTypeInDetail(Guid personIndex, long personQuestionaireID, long personCollaborationID)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                VoiceTypeResponseDTO voiceTypeLookupResponseDTO = this.lookupService.GetAllVoiceTypeInDetail(personIndex, personQuestionaireID, personCollaborationID, userTokenDetails);
                return voiceTypeLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllVoiceTypeInDetail/GET : Getting item : Exception  : Exception occurred Getting All Voice Type In Detail. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving voicetype in detail. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAllSupportsForPerson.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>RoleLookupResponseDTO.</returns>
        [HttpGet]
        [Route("person-supports/{personIndex}")]
        public ActionResult<PersonSupportLookupResponseDTO> GetAllSupportsForPerson(Guid personIndex)
        {
            try
            {
                PersonSupportLookupResponseDTO personSupportLookupResponseDTO = this.lookupService.GetAllSupportsForPerson(personIndex);
                return personSupportLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllSupportsForPerson/GET : Getting item : Exception  : Exception occurred Getting All Support for person. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving all person support. Please try again later or contact support.");
            }
        }
    }
}