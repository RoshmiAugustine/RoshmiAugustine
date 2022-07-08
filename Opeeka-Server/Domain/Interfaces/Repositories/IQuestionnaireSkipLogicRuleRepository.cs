using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireSkipLogicRuleRepository
    {
        /// <summary>
        /// To Get GetQuestionnaireSkipLogic.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireSkipLogicRule.</returns>
        public List<QuestionnaireSkipLogicRuleDTO> GetQuestionnaireSkipLogic(int questionnaireID);

        /// <summary>
        /// To Get GetQuestionnaireSkipLogicRule.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireSkipLogicRule.</returns>
        public List<QuestionnaireSkipLogicRuleDTO> GetQuestionnaireSkipLogicRule(int questionnaireID);

        /// <summary>
        /// CloneQuestionnaireSkipLogicRule.
        /// </summary>
        /// <param name="objQuestionnaireSkipLogicRuleDTO">objQuestionnaireSkipLogicRuleDTO.</param>
        /// <returns></returns>
        QuestionnaireSkipLogicRule CloneQuestionnaireSkipLogicRule(QuestionnaireSkipLogicRuleDTO objQuestionnaireSkipLogicRuleDTO);

        /// <summary>
        /// Update Questionnaire Skip Logic Rule
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRule"></param>
        /// <returns>QuestionnaireSkipLogicRule List</returns>
        List<QuestionnaireSkipLogicRule> UpdateBulkQuestionnaireSkipLogicRule(List<QuestionnaireSkipLogicRule> questionnaireSkipLogicRule);


        /// <summary>
        /// UpdateQuestionnaireSkipLogicRule.
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRule">QuestionnaireSkipLogicRule.</param>
        /// <returns>QuestionnaireSkipLogicRule</returns>
        QuestionnaireSkipLogicRule UpdateQuestionnaireSkipLogicRule(QuestionnaireSkipLogicRule QuestionnaireSkipLogicRule);

        /// <summary>
        /// AddQuestionnaireSkipLogicRule
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRule"></param>
        /// <returns>QuestionnaireSkipLogicRule</returns>
        QuestionnaireSkipLogicRule AddQuestionnaireSkipLogicRule(QuestionnaireSkipLogicRule QuestionnaireSkipLogicRule);
    }
}
