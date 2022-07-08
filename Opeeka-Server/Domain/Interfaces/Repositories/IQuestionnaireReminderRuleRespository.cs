using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireReminderRuleRespository
    {
        /// <summary>
        /// GetQuestionnaireReminderRule.
        /// </summary>
        /// <param name="questionnaireReminderRuleID">questionnaireReminderRuleID.</param>
        /// <returns>QuestionnaireReminderRule</returns>
        Task<QuestionnaireReminderRule> GetQuestionnaireReminderRule(int questionnaireReminderRuleID);

        /// <summary>
        /// UpdateQuestionnaireReminderRule.
        /// </summary>
        /// <param name="questionnaireReminderRule">QuestionnaireReminderRule.</param>
        /// <returns>QuestionnaireReminderRuleDTO.</returns>
        QuestionnaireReminderRuleDTO UpdateQuestionnaireReminderRule(QuestionnaireReminderRule questionnaireReminderRule);

        /// <summary>
        /// AddQuestionnaireReminderRule.
        /// </summary>
        /// <param name="QuestionnaireReminderRuleDTO">QuestionnaireReminderRuleDTO.</param>
        /// <returns>long</returns>
        long AddQuestionnaireReminderRule(QuestionnaireReminderRuleDTO QuestionnaireReminderRuleDTO);

        /// <summary>
        /// GetQuestionnaireReminderRulesByQuestionnaire
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireReminderRulesDTO</returns>
        List<QuestionnaireReminderRulesDTO> GetQuestionnaireReminderRulesByQuestionnaire(int questionnaireID);

        /// <summary>
        /// CloneQuestionnaireReminderRule
        /// </summary>
        /// <param name="questionnaireItemDTO"></param>
        /// <returns>QuestionnaireReminderRulesDTO</returns>
        void CloneQuestionnaireReminderRule(List<QuestionnaireReminderRulesDTO> questionnaireReminderRuleDTO);
        /// <summary>
        /// GetAllReminderRulesByQuestionnaires- list of questionnaireIDs
        /// </summary>
        /// <param name="list_questionnaireIds"></param>
        /// <returns></returns>
        List<QuestionnaireReminderRulesDTO> GetAllReminderRulesByQuestionnaires(List<int> list_questionnaireIds);
        void AddBulkQuestionnaireReminderRule(List<QuestionnaireReminderRuleDTO> lst_Add);
        void UpdateBulkQuestionnaireReminderRule(List<QuestionnaireReminderRule> lst_Edit);
        /// <summary>
        /// GetQuestionnaireReminderRule
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireReminderRulesDTO</returns>
        List<QuestionnaireReminderRule> GetAllQuestionnaireReminderRules(int questionnaireID);
    }
}
