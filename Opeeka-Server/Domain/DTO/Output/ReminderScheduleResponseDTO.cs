// -----------------------------------------------------------------------
// <copyright file="ReminderScheduleResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class ReminderScheduleResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public ReminderScheduleDetailsDTO Questionnaire { get; set; }
    }
}
