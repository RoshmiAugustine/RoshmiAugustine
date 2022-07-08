// -----------------------------------------------------------------------
// <copyright file="QuestionnaireWindowsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace ReminderAutoResolveProcess.DTO
{
    public class QuestionnaireWindowsDTO
    {
        public int QuestionnaireWindowID { get; set; }
        public int QuestionnaireID { get; set; }
        public int AssessmentReasonID { get; set; }
        public int DueDateOffsetDays { get; set; }
        public int WindowOpenOffsetDays { get; set; }
        public int WindowCloseOffsetDays { get; set; }
        public bool CanRepeat { get; set; }
        public int? RepeatIntervalDays { get; set; }
        public bool IsSelected { get; set; }
    }
}
