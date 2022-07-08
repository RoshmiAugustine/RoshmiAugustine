using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireNotifyRiskRuleConditionRepository
    {

        /// <summary>
        /// GetQuestionnaireNotifyRiskRuleCondition.
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskRuleConditionID">QuestionnaireNotifyRiskRuleConditionID.</param>
        /// <returns>QuestionnaireNotifyRiskRuleCondition</returns>
        Task<QuestionnaireNotifyRiskRuleCondition> GetQuestionnaireNotifyRiskRuleCondition(int questionnaireWindowID);

        /// <summary>
        /// GetQuestionnaireNotifyRiskRuleConditionByRuleID.
        /// </summary>
        /// <param name="ruleID"Id of rule></param>
        /// <returns>QuestionnaireNotifyRiskRuleCondition.</returns>
        List<QuestionnaireNotifyRiskRuleCondition> GetQuestionnaireNotifyRiskRuleConditionByRuleID(int ruleID);

        /// <summary>
        /// UpdateQuestionnaireNotifyRiskRuleCondition.
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskRuleCondition">QuestionnaireNotifyRiskRuleCondition.</param>
        /// <returns>QuestionnaireNotifyRiskRuleConditionDTO.</returns>
        QuestionnaireNotifyRiskRuleConditionDTO UpdateQuestionnaireNotifyRiskRuleCondition(QuestionnaireNotifyRiskRuleCondition questionnaireWindow);

        /// <summary>
        /// AddQuestionnaireNotifyRiskRuleCondition.
        /// </summary>
        /// <param name="questionnaireNotifyRiskRuleConditionDTO">QuestionnaireNotifyRiskRuleConditionDTO.</param>
        /// <returns>long</returns>
        long AddQuestionnaireNotifyRiskRuleCondition(QuestionnaireNotifyRiskRuleConditionDTO questionnaireNotifyRiskRuleConditionDTO);

        /// <summary>
        /// GetQuestionnaireNotifyRiskRuleConditionByRiskRule
        /// </summary>
        /// <param name="questionnaireNotifyRiskRuleID"></param>
        /// <returns>QuestionnaireNotifyRiskRuleConditionsDTO</returns>
        List<QuestionnaireNotifyRiskRuleConditionsDTO> GetQuestionnaireNotifyRiskRuleConditionByRiskRule(int questionnaireNotifyRiskRuleID);

        /// <summary>
        /// CloneQuestionnaireNotifyRiskRuleCondition
        /// </summary>
        /// <param name="questionnaireNotifyRiskRuleConditionDTO"></param>
        /// <returns>QuestionnaireNotifyRiskRuleConditionsDTO</returns>
        void CloneQuestionnaireNotifyRiskRuleCondition(List<QuestionnaireNotifyRiskRuleConditionsDTO> questionnaireNotifyRiskRuleConditionDTO);

        /// <summary>
        /// GetQuestionnaireNotifyRiskRuleCondition.
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskRuleConditionID">QuestionnaireNotifyRiskRuleConditionID.</param>
        /// <returns>QuestionnaireNotifyRiskRuleCondition</returns>
        Task<QuestionnaireNotifyRiskRuleCondition> GetQuestionnaireNotifyRiskRuleConditionByQuestionnaireItemID(int questionnaireItemID);

        List<QuestionnaireRiskItemDetailsDTO> GetRiskItemDetails(int notificationLogID);
    }
}
