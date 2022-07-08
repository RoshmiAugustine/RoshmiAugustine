// -----------------------------------------------------------------------
// <copyright file="AssessmentReasonLookupResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace ReminderNotificationProcess.DTO
{
    public class AssessmentReasonLookupResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<AssessmentReasonLookupDTO> AssessmentReasonLookup { get; set; }
    }
    public class AssessmentReasonLookupResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public AssessmentReasonLookupResponse result { get; set; }
    }
}
