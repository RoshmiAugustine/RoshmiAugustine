// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonQuestionnaireDTO
    {
        public long PersonQuestionnaireID { get; set; }
        public long PersonID { get; set; }
        public int QuestionnaireID { get; set; }
        public int? CollaborationID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDueDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public long UpdateUserID { get; set; }
    }
}
