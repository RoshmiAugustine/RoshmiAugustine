// -----------------------------------------------------------------------
// <copyright file="PastNotesResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class PastNotesResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<NotificationNotesDTO> NotificationNotes { get; set; }
        public List<QuestionnaireRiskItemDetailsDTO> QuestionnaireRiskItemDetails { get; set; }
        public List<NotificationLogDTO> ReminderList { get; set; }


    }
}
