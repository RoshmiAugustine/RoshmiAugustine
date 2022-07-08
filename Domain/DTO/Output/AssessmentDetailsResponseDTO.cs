// -----------------------------------------------------------------------
// <copyright file="AssessmentDetailsResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class AssessmentDetailsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }
        public int PageNumber { get; set; }

        public List<AssessmentDetailsDTO> AssessmentDetails { get; set; }

    }
    public class AssessmentPageNumberDetailsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

    }
}
