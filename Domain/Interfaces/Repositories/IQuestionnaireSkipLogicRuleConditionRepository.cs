using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireSkipLogicRuleConditionRepository
    {
        /// <summary>
        /// To Get GetQuestionnaireSkipLogicCondition.
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRuleID">QuestionnaireSkipLogicRuleID.</param>
        /// <returns>QuestionnaireSkipLogicRuleConditionDTO.</returns>
        public List<QuestionnaireSkipLogicRuleConditionDTO> GetQuestionnaireSkipLogicCondition(int QuestionnaireSkipLogicRuleID);

        /// <summary>
        /// CloneQuestionnaireSkipLogicRuleCondition.
        /// </summary>
        /// <param name="questionnaireSkipLogicRuleConditionDTOToClone">questionnaireSkipLogicRuleConditionDTOToClone.</param>
        void CloneQuestionnaireSkipLogicRuleCondition(List<QuestionnaireSkipLogicRuleConditionDTO> questionnaireSkipLogicRuleConditionDTOToClone);

        /// <summary>
        /// Update Questionnaire Skip Logic Rule Condition
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRuleCondition"></param>
        /// <returns>QuestionnaireSkipLogicRuleCondition List</returns>
        List<QuestionnaireSkipLogicRuleCondition> UpdateQuestionnaireSkipLogicRuleCondition(List<QuestionnaireSkipLogicRuleCondition> questionnaireSkipLogicRule);

        /// <summary>
        /// AddBulkQuestionnaireSkipLogicRuleCondition
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRuleCondition"></param>
        /// <returns></returns>
        void AddBulkQuestionnaireSkipLogicRuleCondition(List<QuestionnaireSkipLogicRuleCondition> QuestionnaireSkipLogicRuleCondition);
    }
}
