// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireDataDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonQuestionnaireDataDTO
    {
        public int QuestionnaireID { get; set; }
        public int InstrumentID { get; set; }
        public long AgencyID { get; set; }
        public string Name { get; set; }
        public string QuestionnaireName { get; set; }
        public string QuestionnaireAbbrev { get; set; }
        public string ReminderScheduleName { get; set; }
        public string NotificationScheduleName { get; set; }
        public string InstrumentName { get; set; }
        public string InstrumentAbbrev { get; set; }
        public string Status { get; set; }
        public bool IsBaseQuestionnaire { get; set; }
        public long PersonID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalCount { get; set; }
    }
}
