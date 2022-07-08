using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireReminderTypeRepository
    {
        /// <summary>
        /// GetQuestionnaireReminderRule.
        /// </summary>
        /// <param name="questionnaireReminderRuleID">questionnaireReminderRuleID.</param>
        /// <returns>QuestionnaireReminderRule</returns>
        Task<QuestionnaireReminderType> GetQuestionnaireReminderType(int questionnaireReminderTypeID);

        /// <summary>
        /// GetAllQuestionnaireReminderType.
        /// </summary>
        /// <returns>QuestionnaireReminderType</returns>
        List<QuestionnaireReminderType> GetAllQuestionnaireReminderType();
    }
}
