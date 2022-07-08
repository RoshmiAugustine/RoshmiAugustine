using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireNotifyRiskScheduleRepository
    {
        /// <summary>
        /// GetNotificationDetails.
        /// </summary>
        /// <param name="id">questionnaireid.</param>
        /// <returns>NotificationDetailsResponseDTO.</returns>
        List<NotificationDetailsDTO> GetNotificationDetails(int id);

        /// <summary>
        /// GetQuestionnaireNotifyRiskSchedule.
        /// </summary>
        /// <param name="questionnaireNotifyRiskScheduleID">QuestionnaireNotifyRiskScheduleID.</param>
        /// <returns>QuestionnaireNotifyRiskSchedule</returns>
        Task<QuestionnaireNotifyRiskSchedule> GetQuestionnaireNotifyRiskSchedule(int questionnaireNotifyRiskScheduleID);

        /// <summary>
        /// UpdateQuestionnaireNotifyRiskSchedule.
        /// </summary>
        /// <param name="questionnaireNotifyRiskSchedule">QuestionnaireNotifyRiskSchedule.</param>
        /// <returns>QuestionnaireNotifyRiskScheduleDTO.</returns>
        QuestionnaireNotifyRiskScheduleDTO UpdateQuestionnaireNotifyRiskSchedule(QuestionnaireNotifyRiskSchedule questionnaireNotifyRiskSchedule);

        /// <summary>
        /// AddQuestionnaireNotifyRiskSchedule.
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskScheduleDTO">QuestionnaireNotifyRiskScheduleDTO.</param>
        /// <returns>long</returns>
        long AddQuestionnaireNotifyRiskSchedule(QuestionnaireNotifyRiskScheduleDTO QuestionnaireNotifyRiskScheduleDTO);

        /// <summary>
        /// GetQuestionnaireNotifyRiskScheduleByQuestionnaire
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireNotifyRiskSchedulesDTO</returns>
        Task<QuestionnaireNotifyRiskSchedulesDTO> GetQuestionnaireNotifyRiskScheduleByQuestionnaire(int questionnaireID);

        /// <summary>
        /// GetQuestionnaireNotifyRiskScheduleByQuestionnaireID
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireNotifyRiskSchedule</returns>
        Task<QuestionnaireNotifyRiskSchedule> GetQuestionnaireNotifyRiskScheduleByQuestionnaireID(int questionnaireID);

        /// <summary>
        /// CloneQuestionnaireNotifyRiskSchedule
        /// </summary>
        /// <param name="questionnaireItemDTO"></param>
        /// <returns>QuestionnaireNotifyRiskSchedulesDTO</returns>
        QuestionnaireNotifyRiskSchedulesDTO CloneQuestionnaireNotifyRiskSchedule(QuestionnaireNotifyRiskSchedulesDTO QuestionnaireNotifyRiskScheduleDTO);
    }
}
