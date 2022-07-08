// -----------------------------------------------------------------------
// <copyright file="QuestionnaireDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireDTO
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
        public string Description { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsUsed { get; set; }
        public bool HasSkipLogic { get; set; }
        public bool HasDefaultResponseRule { get; set; }
        public bool HasFormView { get; set; }
    }
}
