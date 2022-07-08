// -----------------------------------------------------------------------
// <copyright file="AddPersonQuestionnaireScheduleResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace FutureReminderScheduleProcess.DTO
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
        public AddBulkPersonQuestionnaireScheduleResponseDTO result { get; set; }
    }
    public class AddBulkPersonQuestionnaireScheduleResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public string PersonQuestionnaireSchedule { get; set; }
    }
}
