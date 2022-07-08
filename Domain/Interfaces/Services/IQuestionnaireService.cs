// -----------------------------------------------------------------------
// <copyright file="IQuestionnaireService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IQuestionnaireService
    {
        /// <summary>
        /// GetQuestionnaireList.
        /// </summary>
        /// <param name="QuestionnaireSearchDTO">questionnaireSearchDTO.</param>
        /// <returns>QuestionnaireListResponseDTO.</returns> 
        QuestionnaireListResponseDTO GetQuestionnaireList(QuestionnaireSearchDTO questionnaireSearchDTO);

        /// <summary>
        /// GetQuestionnaireDetails.
        /// </summary>
        /// <param name="questionnaireIndex">questionnaireIndex.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>QuestionnaireDetailsResponseDTO.</returns>
        QuestionnaireDetailsResponseDTO GetQuestionnaireDetails(QuestionnaireDetailsSearchDTO detailsSearchDTO);

        /// <summary>
        /// GetNotificationDetails.
        /// </summary>
        /// <param name="id">questionnaireid.</param>
        /// <returns>NotificationDetailsResponseDTO.</returns>
        NotificationDetailsResponseDTO GetNotificationDetails(int id);

        /// <summary>
        /// UpdateQuestionaireDetails
        /// </summary>
        /// <param name="id">questionnaireEditDetailsDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateQuestionaireDetails(QuestionnaireEditDetailsDTO questionnaireEditDetailsDTO);

        /// <summary>
        /// GetReminderSchedule.
        /// </summary>
        /// <param name="id">questionnaireID.</param>
        /// <returns>NotificationDetailsResponseDTO.</returns>
        ReminderScheduleResponseDTO GetReminderSchedule(int questionnaireID);

        /// UpdateSchedule.
        /// </summary>
        /// <param name="scheduleParameter">ScheduleParameterDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateSchedule(ScheduleParameterDTO scheduleParameter);

        /// <summary>
        /// GetQuestionnaireDefaultResponseRule.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnaireDefaultResponseRuleDetailsDTO.</returns>
        QuestionnaireDefaultResponseRuleDetailsDTO GetQuestionnaireDefaultResponseRule(int questionnaireId);

        /// <summary>
        /// UpdateQuestionnaire
        /// </summary>
        /// <param name="questionnaireData"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO UpdateQuestionnaire(QuestionnaireInputDTO questionnaireData);

        /// <summary>
        /// GetPersonQuestionnaireList
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>PersonQuestionnaireListResponseDTO</returns>
        PersonQuestionnaireListResponseDTO GetPersonQuestionnaireList(Guid personIndex, int? questionnaireID, int pageNumber, int pageSize, UserTokenDetails userTokenDetails);

        /// <summary>
        /// CloneQuestionnaire
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <param name="agencyID"></param>
        /// <param name="updateUserID"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO CloneQuestionnaire(int questionnaireID, long agencyID, int updateUserID);

        /// <summary>
        /// Delete a questionnaire.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <param name="userID">userID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO DeleteQuestionnaire(int questionnaireID, int userID);

        /// <summary>
        /// GetQuestionnaireWindowsByQuestionnaire
        /// </summary>
        /// <param name="questionnaireId">questionnaireId</param>
        /// <returns>QuestionnaireWindowDetailsDTO</returns>
        QuestionnaireWindowDetailsDTO GetQuestionnaireWindowsByQuestionnaire(int questionnaireId);

        /// <summary>
        /// GetQuestionnaireReminderRulesByQuestionnaire.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnaireReminderRuleDetailsDTO.</returns>
        QuestionnaireReminderRuleDetailsDTO GetQuestionnaireReminderRulesByQuestionnaire(int questionnaireId);

        /// <summary>
        /// AddBulkNotifyReminder.
        /// </summary>
        /// <param name="notifyReminders">notifyReminders.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddBulkNotifyReminder(List<NotifyReminderDTO> notifyReminders);

        /// <summary>
        /// UpdateNotifications.
        /// </summary>
        /// <param name="GetNotifyReminderInput">GetNotifyReminderInput.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateNotifications(GetNotifyReminderInputDTO GetNotifyReminderInput);
        /// <summary>
        /// GetQuestionnaireRiskRule.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireNotifyRiskRulesDetailsDTO.</returns>
        QuestionnaireNotifyRiskRulesDetailsDTO GetQuestionnaireRiskRule(int questionnaireID);


        /// <summary>
        /// GetQuestionnaireNotifyRiskRuleConditionByRuleID.
        /// </summary>
        /// <param name="ruleId">ruleId.</param>
        /// <returns>QuestionnaireNotifyRiskRulesConditionDetailsDTO.</returns>
        QuestionnaireNotifyRiskRulesConditionDetailsDTO GetQuestionnaireNotifyRiskRuleConditionByRuleID(int ruleId);

        /// <summary>
        /// AddNotifyRisk.
        /// </summary>
        /// <param name="notifyRisk">notifyRisk.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddNotifyRisk(NotifyRiskDTO notifyRisk);

        /// <summary>
        /// AddNotifyRiskValues.
        /// </summary>
        /// <param name="notifyRiskValues">notifyRiskValues.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddNotifyRiskValues(List<NotifyRiskValueDTO> notifyRiskValues);
        /// <summary>
        /// GetNotificationStatus.
        /// </summary>
        /// <param name="status">status.</param>
        /// <returns>NotificationResolutionStatusDetailsDTO.</returns>
        NotificationResolutionStatusDetailsDTO GetNotificationStatus(string status);
        /// <summary>
        /// GetNotificationStatus.
        /// </summary>
        /// <param name="notifyReminderIds">notifyReminderIds.</param>
        /// <returns>NotifyReminderDetailsDTO.</returns>
        NotifyReminderDetailsDTO GetNotifyReminderByIds(List<int> notifyReminderIds);
        /// <summary>
        /// UpdateBulkNotifyReminder.
        /// </summary>
        /// <param name="notifyReminders">notifyReminders.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateBulkNotifyReminder(List<NotifyReminderDTO> notifyReminders);

        /// <summary>
        /// GetQuestionnaireItems.
        /// </summary>
        /// <param name="QuestionnaireId">QuestionnaireId.</param>
        /// <returns>QuestionnaireItemsResponseDTO.</returns>
        QuestionnaireItemsResponseDTO GetQuestionnaireItems(int QuestionnaireId);

        /// <summary>
        /// GetQuestionnaire.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>QuestionnairesResponseDTO</returns>
        QuestionnairesResponseDTO GetQuestionnaire(int questionnaireId);
        QuestionnairesResponseDTO GetQuestionnaireDetailsbyIds(List<int> questionnaireIds);
        /// <summary>
        /// GetQuestionnaireListForExternal.
        /// </summary>
        /// <param name="questionnaireSearchDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        QuestionnaireListResponseDTO GetQuestionnaireListForExternal(QuestionnaireSearchInputDTO questionnaireSearchDTO, LoggedInUserDTO loggedInUserDTO);

        /// <summary>
        /// GetUnSelectedQuestionnaireWindowsByQuestionnaire.
        /// </summary>
        /// <param name="questionnaireId"></param>
        /// <returns></returns>
        QuestionnaireWindowDetailsDTO GetUnSelectedQuestionnaireWindowsByQuestionnaire(int questionnaireId);
        /// <summary>
        /// GetReminderRulesByQuestionnaires - By list of questionnaireIDs
        /// </summary>
        /// <param name="list_questionnaireIds"></param>
        /// <returns></returns>
        QuestionnaireReminderRuleDetailsDTO GetReminderRulesByQuestionnaires(List<int> list_questionnaireIds);
        /// <summary>
        /// GetRegularReminderSettingsForQuestionnaires.
        /// </summary>
        /// <param name="questionnaireIds"></param>
        /// <returns></returns>
        QuestionnaireRegularReminderSettingsDTO GetRegularReminderSettingsForQuestionnaires(List<int> questionnaireIds);
        /// <summary>
        /// GetReminderDetailsForInviteToCompleteTrigger.
        /// </summary>
        /// <returns></returns>
        RemindersForInviteToCompleteResponseDTO GetReminderDetailsForInviteToCompleteTrigger();
    }
}
