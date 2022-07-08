using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireDefaultResponseRuleActionRepository
    {
        /// <summary>
        /// To Get GetQuestionnaireDefaultResponseAction.
        /// </summary>
        /// <param name="questionnaireID">questionnaireDefaultResponseRuleID.</param>
        /// <returns>QuestionnaireDefaultResponseRuleActionDTO.</returns>
        public List<QuestionnaireDefaultResponseRuleActionDTO> GetQuestionnaireDefaultResponseAction(int questionnaireDefaultResponseRuleID);

        /// <summary>
        /// CloneQuestionnaireDefaultResponseRuleAction.
        /// </summary>
        /// <param name="questionnaireDefaultResponseRuleActionDTOToClone">questionnaireDefaultResponseRuleActionDTOToClone.</param>
        void CloneQuestionnaireDefaultResponseRuleAction(List<QuestionnaireDefaultResponseRuleActionDTO> questionnaireDefaultResponseRuleActionDTOToClone);

        /// <summary>
        /// UpdateQuestionnaireDefaultResponseRuleAction
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRuleAction"></param>
        /// <returns>QuestionnaireDefaultResponseRuleAction List</returns>
        List<QuestionnaireDefaultResponseRuleAction> UpdateQuestionnaireDefaultResponseRuleAction(List<QuestionnaireDefaultResponseRuleAction> QuestionnaireDefaultResponseRuleAction);

        /// <summary>
        /// AddBulkQuestionnaireDefaultResponseRuleAction
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRuleAction"></param>
        /// <returns></returns>
        void AddBulkQuestionnaireDefaultResponseRuleAction(List<QuestionnaireDefaultResponseRuleAction> QuestionnaireDefaultResponseRuleAction);
    }
}
