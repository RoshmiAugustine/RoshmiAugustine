using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireDefaultResponseRuleRepository
    {
        /// <summary>
        /// To Get GetQuestionnaireDefaultResponse.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireDefaultResponseRule.</returns>
        public List<QuestionnaireDefaultResponseRuleDTO> GetQuestionnaireDefaultResponse(int questionnaireID);

        /// <summary>
        /// To Get GetQuestionnaireDefaultResponseRule.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireDefaultResponseRule.</returns>
        public List<QuestionnaireDefaultResponseRuleDTO> GetQuestionnaireDefaultResponseRule(int questionnaireID);

        /// <summary>
        /// CloneQuestionnaireDefaultResponseRule.
        /// </summary>
        /// <param name="objQuestionnaireDefaultResponseRuleDTO">objQuestionnaireDefaultResponseRuleDTO.</param>
        /// <returns></returns>
        QuestionnaireDefaultResponseRule CloneQuestionnaireDefaultResponseRule(QuestionnaireDefaultResponseRuleDTO objQuestionnaireDefaultResponseRuleDTO);

        /// <summary>
        /// Update Questionnaire Skip Logic Rule
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRule"></param>
        /// <returns>QuestionnaireDefaultResponseRule List</returns>
        List<QuestionnaireDefaultResponseRule> UpdateBulkQuestionnaireDefaultResponseRule(List<QuestionnaireDefaultResponseRule> questionnaireDefaultResponseRule);


        /// <summary>
        /// UpdateQuestionnaireDefaultResponseRule.
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRule">QuestionnaireDefaultResponseRule.</param>
        /// <returns>QuestionnaireDefaultResponseRule</returns>
        QuestionnaireDefaultResponseRule UpdateQuestionnaireDefaultResponseRule(QuestionnaireDefaultResponseRule QuestionnaireDefaultResponseRule);

        /// <summary>
        /// AddQuestionnaireDefaultResponseRule
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRule"></param>
        /// <returns>QuestionnaireDefaultResponseRule</returns>
        QuestionnaireDefaultResponseRule AddQuestionnaireDefaultResponseRule(QuestionnaireDefaultResponseRule QuestionnaireDefaultResponseRule);
    }
}
