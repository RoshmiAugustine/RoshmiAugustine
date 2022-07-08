﻿// -----------------------------------------------------------------------
// <copyright file="QuestionnaireWindowDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireWindowDTO
    {
        public int QuestionnaireWindowID { get; set; }
        public int QuestionnaireID { get; set; }
        public int AssessmentReasonID { get; set; }
        public int DueDateOffsetDays { get; set; }
        public int? WindowOpenOffsetDays { get; set; }
        public char? OpenOffsetTypeID { get; set; }
        public int? WindowCloseOffsetDays { get; set; }
        public char? CloseOffsetTypeID { get; set; }
        public bool CanRepeat { get; set; }
        public int? RepeatIntervalDays { get; set; }
        public bool IsSelected { get; set; }
        public string AssessmentName { get; set; }
        public int ListOrder { get; set; }
        public QuestionnaireRegularReminderRecurrenceDTO questionnaireRegularReminderRecurrence { get; set; }
    }
}