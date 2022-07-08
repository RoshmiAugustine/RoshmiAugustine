// -----------------------------------------------------------------------
// <copyright file="QuestionnaireController.cs" company="Naico ITS">
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
    using Opeeka.PICS.Domain.DTO.Input;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// ConsumerController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class QuestionnaireController : BaseController
    {
        /// Initializes a new instance of the <see cref="questionnaireService"/> class.
        private readonly IQuestionnaireService questionnaireService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<PersonController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="questionnaireService">questionnaireService.</param>
        public QuestionnaireController(ILogger<PersonController> logger, IQuestionnaireService questionnaireService)
        {
            this.questionnaireService = questionnaireService;
            this.logger = logger;
        }

        /// <summary>
        /// GetQuestionnaireList.
        /// </summary>
        /// <param name="questionnaireSearchDTO">questionnaireSearchDTO.</param>
        /// <returns>QuestionnaireListResponseDTO.</returns>
        [HttpPost]
        [Route("questionnaires")]
        public ActionResult<QuestionnaireListResponseDTO> GetQuestionnaireList([FromBody] QuestionnaireSearchDTO questionnaireSearchDTO)
        {
            try
            {
                questionnaireSearchDTO.AgencyId = this.GetTenantID();
                var response = this.questionnaireService.GetQuestionnaireList(questionnaireSearchDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireList/post : get item : Exception  : Exception occurred while getting questionnaire list. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting questionnaire list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// QuestionnaireDetails.
        /// </summary>
        /// <param name="detailsSearchDTO">pageSize.</param>
        /// <returns>QuestionnaireDetailsResponseDTO.</returns>
        [HttpPost]
        [Route("questionnaire-Details")]
        public ActionResult<QuestionnaireDetailsResponseDTO> GetQuestionnaireDetails([FromBody] QuestionnaireDetailsSearchDTO detailsSearchDTO)
        {
            try
            {
                var response = this.questionnaireService.GetQuestionnaireDetails(detailsSearchDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireDetails/POST : Getting item : Exception  : Exception occurred getting questionnaire details. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Questionnaire Details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetNotificationDetails.
        /// </summary>
        /// <param name="id">questionnaireid.</param>
        /// <returns>NotificationDetailsResponseDTO.</returns>
        [HttpGet]
        [Route("notification-Details/{id}")]
        public ActionResult<NotificationDetailsResponseDTO> GetNotificationDetails(int id)
        {
            try
            {
                var response = this.questionnaireService.GetNotificationDetails(id);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetNotificationDetails/GET : Getting item : Exception  : Exception occurred getting Notification Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving notification Details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateQuestionaireDetails.
        /// </summary>
        /// <param name="questionnaireEditDetailsDTO">questionnaireEditDetailsDTO.</param>
        /// <returns>NotificationDetailsResponseDTO.</returns>
        [HttpPut]
        [Route("questionnaire")]
        public ActionResult<CRUDResponseDTO> UpdateQuestionaire([FromBody] QuestionnaireEditDetailsDTO questionnaireEditDetailsDTO)
        {
            try
            {
                var response = this.questionnaireService.UpdateQuestionaireDetails(questionnaireEditDetailsDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateQuestionaireDetails/put : Updating item : Exception  : Exception occurred while updating Questionnaire Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating Questionnaire Details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetReminderSchedule.
        /// </summary>
        /// <param name="id">questionnaireID.</param>
        /// <returns>NotificationDetailsResponseDTO.</returns>
        [HttpGet]
        [Route("reminder-schedule-Details/{id}")]
        public ActionResult<ReminderScheduleResponseDTO> GetReminderSchedule(int id)
        {
            try
            {
                var response = this.questionnaireService.GetReminderSchedule(id);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetReminderSchedule/GET : Getting item : Exception  : Exception occurred getting Reminder Schedule. {ex.Message}");
                return this.HandleException(ex, "An error occurred Getting ReminderSchedule Details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateQuestionnaire.
        /// </summary>
        /// <param name="questionnaireData">questionnaireData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPut]
        [Route("questionnaire-details")]
        public ActionResult<CRUDResponseDTO> UpdateQuestionnaire([FromBody] QuestionnaireInputDTO questionnaireData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.questionnaireService.UpdateQuestionnaire(questionnaireData);
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateQuestionnaire/Put : Updating Questionnaire : Exception  : Exception occurred updating questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating questionnaire. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateSchedule.
        /// </summary>
        /// <param name="scheduleParameter">ScheduleParameterDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPut]
        [Route("schedule")]
        public ActionResult<CRUDResponseDTO> UpdateSchedule([FromBody] ScheduleParameterDTO scheduleParameter)
        {
            try
            {
                int updateUserID = 0;
                updateUserID = this.GetUserID();
                scheduleParameter.UpdatedUserID = updateUserID;
                var response = this.questionnaireService.UpdateSchedule(scheduleParameter);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateSchedule/PUT : Updating item : Exception  : Exception occurred Updating schedule Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred Updating schedule Details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionnaireDefaultResponseRule.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnaireDefaultResponseRuleDetailsDTO.</returns>
        [HttpGet]
        [Route("questionnaire-default-response/{questionnaireId}")]
        public ActionResult<QuestionnaireDefaultResponseRuleDetailsDTO> GetQuestionnaireDefaultResponseRule(int questionnaireId)
        {
            try
            {
                var response = this.questionnaireService.GetQuestionnaireDefaultResponseRule(questionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireDefaultResponseRule/Get : Get item : Exception  : Exception occurred while GetQuestionnaireDefaultResponseRule. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetQuestionnaireDefaultResponseRule. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireList.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>PersonQuestionnaireListResponseDTO.</returns>
        [HttpGet]
        [Route("person-questionnaires/{personIndex}/{questionnaireID}/{pageNumber}/{pageSize}")]
        public ActionResult<PersonQuestionnaireListResponseDTO> GetPersonQuestionnaireList(Guid personIndex, int? questionnaireID, int pageNumber, int pageSize)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                var response = this.questionnaireService.GetPersonQuestionnaireList(personIndex, questionnaireID, pageNumber, pageSize, userTokenDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetPersonQuestionnaireList/Get : Listing Persons Questionnaire : Exception  : Exception occurred listing person questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving person questionnaire. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// CloneQuestionnaire.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("questionnaire/{questionnaireID}")]
        public ActionResult<CRUDResponseDTO> CloneQuestionnaire(int questionnaireID)
        {
            try
            {
                var response = this.questionnaireService.CloneQuestionnaire(questionnaireID, this.GetTenantID(), this.GetUserID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"CloneQuestionnaire/Post : Cloning Questionnaire : Exception  : Exception occurred cloning questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred cloning questionnaire. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// DeleteQuestionnaire.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpDelete]
        [Route("questionnaire/{questionnaireID}")]
        public ActionResult<CRUDResponseDTO> DeleteQuestionnaire(int questionnaireID)
        {
            try
            {
                CRUDResponseDTO result = this.questionnaireService.DeleteQuestionnaire(questionnaireID, this.GetUserID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"Questionnaire/Delete : Deleting Questionnaire : Exception  : Exception occurred while deleting questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred  while deleting Questionnaire. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionnaireWindowsByQuestionnaire.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnaireWindowDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("questionnaire-window/{questionnaireId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<QuestionnaireWindowDetailsDTO> GetQuestionnaireWindowsByQuestionnaire(int questionnaireId)
        {
            try
            {
                var response = this.questionnaireService.GetQuestionnaireWindowsByQuestionnaire(questionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireWindowsByQuestionnaire/GET : Getting item : Exception  : Exception occurred while recieving Questionnaire Windows By Questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving Questionnaire Windows By Questionnaire. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionnaireReminderRulesByQuestionnaire.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnaireReminderRuleDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("questionnaire-reminder-rule/{questionnaireId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<QuestionnaireReminderRuleDetailsDTO> GetQuestionnaireReminderRulesByQuestionnaire(int questionnaireId)
        {
            try
            {
                var response = this.questionnaireService.GetQuestionnaireReminderRulesByQuestionnaire(questionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireReminderRulesByQuestionnaire/GET : Getting item : Exception  : Exception occurred while recieving Questionnaire ReminderRule By Questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving Questionnaire ReminderRule By Questionnaire. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddBulkNotifyReminder.
        /// </summary>
        /// <param name="notifyReminders">notifyReminders.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("notify-reminder")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> AddBulkNotifyReminder([FromBody] List<NotifyReminderDTO> notifyReminders)
        {
            try
            {
                var response = this.questionnaireService.AddBulkNotifyReminder(notifyReminders);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddBulkNotifyReminder/Post : POSt item : Exception  : Exception occurred while insert notifyReminders. {ex.Message}");
                return this.HandleException(ex, "An error occurred while Insert notifyReminders. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateNotifications.
        /// </summary>
        /// <param name="getNotifyReminderInput">getNotifyReminderInput.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("notification-update")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> UpdateNotifications([FromBody] GetNotifyReminderInputDTO getNotifyReminderInput)
        {
            try
            {
                var response = this.questionnaireService.UpdateNotifications(getNotifyReminderInput);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"UpdateNotifications/Post : POST item : Exception  : Exception occurred while updating notifications. {ex.Message}");
                return this.HandleException(ex, "An error occurred while updating notifications. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionnaireRiskRule.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireNotifyRiskRulesDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("risk-rule/{questionnaireId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<QuestionnaireNotifyRiskRulesDetailsDTO> GetQuestionnaireRiskRule(int questionnaireID)
        {
            try
            {
                var response = this.questionnaireService.GetQuestionnaireRiskRule(questionnaireID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireRiskRule/Get : Get item : Exception  : Exception occurred getting QuestionnaireRiskRule. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting QuestionnaireRiskRule. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionnaireNotifyRiskRuleConditionByRuleID.
        /// </summary>
        /// <param name="ruleId">ruleId.</param>
        /// <returns>QuestionnaireNotifyRiskRulesConditionDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("risk-rule-condition/{ruleId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<QuestionnaireNotifyRiskRulesConditionDetailsDTO> GetQuestionnaireNotifyRiskRuleConditionByRuleID(int ruleId)
        {
            try
            {
                var response = this.questionnaireService.GetQuestionnaireNotifyRiskRuleConditionByRuleID(ruleId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireNotifyRiskRuleConditionByRuleID/Get : Get item : Exception  : Exception occurred Getting QuestionnaireNotifyRiskRuleConditionByRuleID. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting QuestionnaireNotifyRiskRuleConditionByRuleID. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddNotifyRisk.
        /// </summary>
        /// <param name="notifyRisk">notifyRisk.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("notify-risk")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> AddNotifyRisk([FromBody] NotifyRiskDTO notifyRisk)
        {
            try
            {
                var response = this.questionnaireService.AddNotifyRisk(notifyRisk);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddNotifyRisk/Post : POST item : Exception  : Exception occurred while inserting NotifyRisk. {ex.Message}");
                return this.HandleException(ex, "An error occurred while Inserting NotifyRisk. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddNotifyRiskValues.
        /// </summary>
        /// <param name="notifyRiskValues">notifyRiskValues.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("notify-risk-values")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> AddNotifyRiskValues([FromBody] List<NotifyRiskValueDTO> notifyRiskValues)
        {
            try
            {
                var response = this.questionnaireService.AddNotifyRiskValues(notifyRiskValues);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddNotifyRiskValues/Post : POST item : Exception  : Exception occurred while inserting notify risk values. {ex.Message}");
                return this.HandleException(ex, "An error occurred while inserting notify risk values. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetNotificationStatus.
        /// </summary>
        /// <param name="status">status.</param>
        /// <returns>NotificationResolutionStatusDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("notification-resolution-Status-byname /{status}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<NotificationResolutionStatusDetailsDTO> GetNotificationStatus(string status)
        {
            try
            {
                var response = this.questionnaireService.GetNotificationStatus(status);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetNotificationStatus/Get : Get item : Exception  : Exception occurred Getting NotificationStatus. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting NotificationStatus. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetNotificationStatus.
        /// </summary>
        /// <param name="notifyReminderIds">notifyReminderIds.</param>
        /// <returns>NotifyReminderDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("notify-reminder-byids")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<NotifyReminderDetailsDTO> GetNotifyReminderByIds([FromBody] List<int> notifyReminderIds)
        {
            try
            {
                var response = this.questionnaireService.GetNotifyReminderByIds(notifyReminderIds);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetNotifyReminderByIds/post : Post item : Exception  : Exception occurred getting notify reminder by id. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting notify reminder by id. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateBulkNotifyReminder.
        /// </summary>
        /// <param name="notifyReminders">notifyReminders.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPut]
        [Route("notify-reminder")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> UpdateBulkNotifyReminder([FromBody] List<NotifyReminderDTO> notifyReminders)
        {
            try
            {
                var response = this.questionnaireService.UpdateBulkNotifyReminder(notifyReminders);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateBulkNotifyReminder/Put : PUt item : Exception  : Exception occurred while updating BulkNotifyReminder. {ex.Message}");
                return this.HandleException(ex, "An error occurred while updating BulkNotifyReminder. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionnaireItems.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnaireItemsResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("questionnaire-item/{questionnaireId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<QuestionnaireItemsResponseDTO> GetQuestionnaireItems(int questionnaireId)
        {
            try
            {
                var response = this.questionnaireService.GetQuestionnaireItems(questionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireItems/Get : Get item : Exception  : Exception occurred while getting QuestionnaireItems. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting QuestionnaireItems. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionnaireDetailsById.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnairesResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("questionaire-details/{questionnaireId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<QuestionnairesResponseDTO> GetQuestionnaireDetailsById(int questionnaireId)
        {
            try
            {
                QuestionnairesResponseDTO response = this.questionnaireService.GetQuestionnaire(questionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireDetails/GET : Getting item : Exception  : Exception occurred while recieving Questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving Questionnaire. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionnaireDetailsList.
        /// </summary>
        /// <param name="questionnaireIds">questionnaireId list.</param>
        /// <returns>QuestionnairesResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("questionaire-details-byids")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<QuestionnairesResponseDTO> GetQuestionnaireDetailsList([FromBody] List<int> questionnaireIds)
        {
            try
            {
                QuestionnairesResponseDTO response = this.questionnaireService.GetQuestionnaireDetailsbyIds(questionnaireIds);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireDetails/Post : Getting item : Exception  : Exception occurred while recieving Questionnaire details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving Questionnaire details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetUnSelectedQuestionnaireWindowsByQuestionnaire.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnaireWindowDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("questionnaire-window-unselected/{questionnaireId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<QuestionnaireWindowDetailsDTO> GetUnSelectedQuestionnaireWindowsByQuestionnaire(int questionnaireId)
        {
            try
            {
                var response = this.questionnaireService.GetUnSelectedQuestionnaireWindowsByQuestionnaire(questionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetUnSelectedQuestionnaireWindowsByQuestionnaire/GET : Getting item : Exception  : Exception occurred while recieving Questionnaire Windows By Questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving Questionnaire Windows By Questionnaire. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetReminderRulesByQuestionnaireList.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnaireReminderRuleDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("questionnaire-reminder-rule-all")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<QuestionnaireReminderRuleDetailsDTO> GetReminderRulesByQuestionnaireList([FromBody] List<int> list_questionnaireIds)
        {
            try
            {
                var response = this.questionnaireService.GetReminderRulesByQuestionnaires(list_questionnaireIds);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetReminderRulesByQuestionnaires/GET : Getting item : Exception  : Exception occurred while recieving Questionnaire ReminderRule By Questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving Questionnaire ReminderRule By Questionnaire. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetRegularReminderSettingsForQuestionnaires.
        /// </summary>
        /// <param name="questionnaireIds">questionnaireIds.</param>
        /// <returns>QuestionnaireRegularReminderSettingsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("questionnaire-regular-reminder-settings")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<QuestionnaireRegularReminderSettingsDTO> GetRegularReminderSettingsForQuestionnaires(List<int> questionnaireIds)
        {
            try
            {
                var response = this.questionnaireService.GetRegularReminderSettingsForQuestionnaires(questionnaireIds);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetRegularReminderSettingsForQuestionnaires/GET : Getting item : Exception  : Exception occurred while recieving Questionnaire RegularReminder Recurrance Settings By list of Questionnaire. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving  Questionnaire RegularReminder Recurrance Settings By list of Questionnaire. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetReminderDetailsForInviteToCompleteTriggering.
        /// </summary>
        /// <returns>ReminderInviteToCompleteResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("invitetocomplete-regular-reminder-details")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<RemindersForInviteToCompleteResponseDTO> GetReminderDetailsForInviteToComplete()
        {
            try
            {
                var response = this.questionnaireService.GetReminderDetailsForInviteToCompleteTrigger();
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetReminderDetailsForInviteToComplete/GET : Getting item : Exception  : Exception occurred while GetReminderDetailsForInviteToComplete. {ex.Message}");
                return this.HandleException(ex, "An error occurred while recieving GetReminderDetailsForInviteToComplete By list of Questionnaire. Please try again later or contact support.");
            }
        }
    }
}