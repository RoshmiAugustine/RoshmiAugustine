// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireSchedule.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonQuestionnaireSchedule : BaseEntity
    {
        public long PersonQuestionnaireScheduleID { get; set; }
        public Guid? PersonQuestionnaireScheduleIndex { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public DateTime WindowDueDate { get; set; }
        public int QuestionnaireWindowID { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? WindowOpenDate { get; set; }
        public DateTime? WindowCloseDate { get; set; }
        public int OccurrenceCounter { get; set; }
        public PersonQuestionnaire PersonQuestionnaire { get; set; }
        public QuestionnaireWindow QuestionnaireWindow { get; set; }
    }
}
