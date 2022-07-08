using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireNotifyRiskRuleRepository
    {
        /// <summary>
        /// GetQuestionnaireNotifyRiskRule.
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskRuleID">QuestionnaireNotifyRiskRuleID.</param>
        /// <returns>QuestionnaireNotifyRiskRule</returns>
        Task<QuestionnaireNotifyRiskRule> GetQuestionnaireNotifyRiskRule(int questionnaireWindowID);

        /// <summary>
        /// GetQuestionnaireNotifyRiskRuleByScheduleID.
        /// </summary>
        /// <param name="scheduleID">scheduleID.</param>
        /// <returns>QuestionnaireNotifyRiskRule
        /// </returns>
        List<QuestionnaireNotifyRiskRule> GetQuestionnaireNotifyRiskRuleByScheduleID(int scheduleID);

        /// <summary>
        /// UpdateQuestionnaireNotifyRiskRule.
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskRule">QuestionnaireNotifyRiskRule.</param>
        /// <returns>QuestionnaireNotifyRiskRuleDTO.</returns>
        QuestionnaireNotifyRiskRuleDTO UpdateQuestionnaireNotifyRiskRule(QuestionnaireNotifyRiskRule questionnaireWindow);

        /// <summary>
        /// AddQuestionnaireNotifyRiskRule.
        /// </summary>
        /// <param name="questionnaireNotifyRiskRuleDTO">QuestionnaireNotifyRiskRuleDTO.</param>
        /// <returns>long</returns>
        int AddQuestionnaireNotifyRiskRule(QuestionnaireNotifyRiskRuleDTO questionnaireNotifyRiskRuleDTO);

        /// <summary>
        /// GetQuestionnaireNotifyRiskRuleBySchedule
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireNotifyRiskRulesDTO</returns>
        List<QuestionnaireNotifyRiskRulesDTO> GetQuestionnaireNotifyRiskRuleBySchedule(int questionnaireNotifyRiskScheduleID);

        /// <summary>
        /// CloneQuestionnaireNotifyRiskRule
        /// </summary>
        /// <param name="questionnaireItemDTO"></param>
        /// <returns>QuestionnaireNotifyRiskRulesDTO</returns>
        void CloneQuestionnaireNotifyRiskRule(List<QuestionnaireNotifyRiskRulesDTO> questionnaireNotifyRiskRuleDTO);
    }
}
