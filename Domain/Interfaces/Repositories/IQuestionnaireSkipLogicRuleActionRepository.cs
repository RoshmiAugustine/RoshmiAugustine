using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireSkipLogicRuleActionRepository
    {
        /// <summary>
        /// To Get GetQuestionnaireSkipLogicAction.
        /// </summary>
        /// <param name="questionnaireID">questionnaireSkipLogicRuleID.</param>
        /// <returns>QuestionnaireSkipLogicRuleActionDTO.</returns>
        public List<QuestionnaireSkipLogicRuleActionDTO> GetQuestionnaireSkipLogicAction(int questionnaireSkipLogicRuleID);

        /// <summary>
        /// CloneQuestionnaireSkipLogicRuleAction.
        /// </summary>
        /// <param name="questionnaireSkipLogicRuleActionDTOToClone">questionnaireSkipLogicRuleActionDTOToClone.</param>
        void CloneQuestionnaireSkipLogicRuleAction(List<QuestionnaireSkipLogicRuleActionDTO> questionnaireSkipLogicRuleActionDTOToClone);

        /// <summary>
        /// UpdateQuestionnaireSkipLogicRuleAction
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRuleAction"></param>
        /// <returns>QuestionnaireSkipLogicRuleAction List</returns>
        List<QuestionnaireSkipLogicRuleAction> UpdateQuestionnaireSkipLogicRuleAction(List<QuestionnaireSkipLogicRuleAction> QuestionnaireSkipLogicRuleAction);

        /// <summary>
        /// AddBulkQuestionnaireSkipLogicRuleAction
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRuleAction"></param>
        /// <returns></returns>
        void AddBulkQuestionnaireSkipLogicRuleAction(List<QuestionnaireSkipLogicRuleAction> QuestionnaireSkipLogicRuleAction);
    }
}
