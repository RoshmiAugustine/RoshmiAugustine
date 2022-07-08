// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaire.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonQuestionnaire : BaseEntity
    {
        public long PersonQuestionnaireID { get; set; }
        public long PersonID { get; set; }
        public int QuestionnaireID { get; set; }
        public int? CollaborationID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDueDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }

        public Collaboration Collaboration { get; set; }
        public Person Person { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public User UpdateUser { get; set; }

    }
}
