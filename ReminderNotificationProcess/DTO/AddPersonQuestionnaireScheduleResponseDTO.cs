// -----------------------------------------------------------------------
// <copyright file="AddPersonQuestionnaireScheduleResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace ReminderNotificationProcess.DTO
{
    public class AddPersonQuestionnaireSchedule
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public long PersonQuestionnaireScheduleID { get; set; }
    }
    public class AddPersonQuestionnaireScheduleResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public AddPersonQuestionnaireSchedule result { get; set; }
    }
    public class AddBulkPersonQuestionnaireScheduleResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public BulkPersonQuestionnaireScheduleDTO result { get; set; }
    }
    public class BulkPersonQuestionnaireScheduleDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public string PersonQuestionnaireSchedule { get; set; }
    }
}
