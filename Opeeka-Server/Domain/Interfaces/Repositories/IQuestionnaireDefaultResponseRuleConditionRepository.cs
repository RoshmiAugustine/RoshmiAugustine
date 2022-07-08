using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireDefaultResponseRuleConditionRepository
    {
        /// <summary>
        /// To Get GetQuestionnaireDefaultResponseCondition.
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRuleID">QuestionnaireDefaultResponseRuleID.</param>
        /// <returns>QuestionnaireDefaultResponseRuleConditionDTO.</returns>
        public List<QuestionnaireDefaultResponseRuleConditionDTO> GetQuestionnaireDefaultResponseCondition(int QuestionnaireDefaultResponseRuleID);

        /// <summary>
        /// CloneQuestionnaireDefaultResponseRuleCondition.
        /// </summary>
        /// <param name="questionnaireDefaultResponseRuleConditionDTOToClone">questionnaireDefaultResponseRuleConditionDTOToClone.</param>
        void CloneQuestionnaireDefaultResponseRuleCondition(List<QuestionnaireDefaultResponseRuleConditionDTO> questionnaireDefaultResponseRuleConditionDTOToClone);

        /// <summary>
        /// Update Questionnaire Skip Logic Rule Condition
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRuleCondition"></param>
        /// <returns>QuestionnaireDefaultResponseRuleCondition List</returns>
        List<QuestionnaireDefaultResponseRuleCondition> UpdateQuestionnaireDefaultResponseRuleCondition(List<QuestionnaireDefaultResponseRuleCondition> questionnaireDefaultResponseRule);

        /// <summary>
        /// AddBulkQuestionnaireDefaultResponseRuleCondition
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRuleCondition"></param>
        /// <returns></returns>
        void AddBulkQuestionnaireDefaultResponseRuleCondition(List<QuestionnaireDefaultResponseRuleCondition> QuestionnaireDefaultResponseRuleCondition);
    }
}
