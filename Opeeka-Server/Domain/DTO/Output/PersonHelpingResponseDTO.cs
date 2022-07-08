// -----------------------------------------------------------------------
// <copyright file="PersonHelpingResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonHelpingResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int PersonHelpingCount { get; set; }

        public int LeadHelpingCount { get; set; }

    }
}
