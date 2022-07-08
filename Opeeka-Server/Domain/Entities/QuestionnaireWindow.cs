// -----------------------------------------------------------------------
// <copyright file="QuestionnaireWindow.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class QuestionnaireWindow : BaseEntity
    {
        public int QuestionnaireWindowID { get; set; }
        public int QuestionnaireID { get; set; }
        public int AssessmentReasonID { get; set; }
        public int? DueDateOffsetDays { get; set; }
        public int? WindowOpenOffsetDays { get; set; }
        public char? OpenOffsetTypeID { get; set; }
        public int? WindowCloseOffsetDays { get; set; }
        public char? CloseOffsetTypeID { get; set; }
        public bool CanRepeat { get; set; }
        public int? RepeatIntervalDays { get; set; }
        public bool IsSelected { get; set; }
        public DateTime UpdateDate { get; set; }


        public AssessmentReason AssessmentReason { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public OffsetType OpenOffsetType { get; set; }
        public OffsetType CloseOffsetType { get; set; }
    }
}
