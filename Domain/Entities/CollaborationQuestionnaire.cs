// -----------------------------------------------------------------------
// <copyright file="CollaborationQuestionnaire.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace Opeeka.PICS.Domain.Entities
{
    public class CollaborationQuestionnaire : BaseEntity
    {
        public int CollaborationQuestionnaireID { get; set; }
        public int CollaborationID { get; set; }
        public int QuestionnaireID { get; set; }
        public bool IsMandatory { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsRemoved { get; set; }
        [DefaultValue(true)]
        public bool IsReminderOn { get; set; }

        public Collaboration Collaboration { get; set; }
        public Questionnaire Questionnaire { get; set; }
    }
}
