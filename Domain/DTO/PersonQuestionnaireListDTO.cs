// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireListDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonQuestionnaireListDTO
    {
        public int QuestionnaireID { get; set; }
        public int InstrumentID { get; set; }
        public long AgencyID { get; set; }
        public string QuestionnaireName { get; set; }
        public string QuestionnaireAbbrev { get; set; }
        public string NotificationScheduleName { get; set; }
        public string ReminderScheduleName { get; set; }
        public bool IsBaseQuestionnaire { get; set; }
        public string InstrumentName { get; set; }
        public string InstrumentAbbrev { get; set; }
        public int TotalCount { get; set; }
        public string Status { get; set; }
        public System.DateTime? QuestionnaireEndDate { get; set; }
        public string InstrumentUrl { get; set; }
    }
}
