using System;
using System.Collections.Generic;
using System.Text;
using Opeeka.PICS.Domain.DTO;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IQuestionnaireRegularReminderRecurrenceRepository.
    /// </summary>
    public interface IQuestionnaireRegularReminderRecurrenceRepository
    {
        QuestionnaireRegularReminderRecurrenceDTO GetQuestionnaireRegularReminderRecurrence(int questionnaireId);
        int AddQuestionnaireReminderRecurrence(QuestionnaireRegularReminderRecurrenceDTO regularReminderRecurrence);
        List<QuestionnaireRegularReminderRecurrenceDTO> GetRegularReminderSettingsForQuestionnaires(List<int> questionnaireIds);
        int UpdateQuestionnaireReminderRecurrence(QuestionnaireRegularReminderRecurrenceDTO existingRule);
    }
    /// <summary>
    /// IQuestionnaireRegularReminderTimeRuleRepository.
    /// </summary>
    public interface IQuestionnaireRegularReminderTimeRuleRepository
    {
        List<QuestionnaireRegularReminderTimeRuleDTO> GetQuestionnaireRegularReminderTimeRule(int questionnaireId);
        void AddReminderTimeRule(List<QuestionnaireRegularReminderTimeRuleDTO> regularReminderTimeRuleDTO);
        List<QuestionnaireRegularReminderTimeRuleDTO> GetRegularReminderTimeRuleForQuestionnaires(List<int> questionnaireIds);
        void UpdateReminderTimeRule(List<QuestionnaireRegularReminderTimeRuleDTO> regularRminderTimeRuleDTO);
    }
}
