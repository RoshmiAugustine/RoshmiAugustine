// -----------------------------------------------------------------------
// <copyright file="AssessmentEmailLinkDetailsResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class AssessmentEmailLinkDetailsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public AssessmentEmailLinkDetailsOutputDTO AssessmentEmailLinkDetails { get; set; }
    }
}
