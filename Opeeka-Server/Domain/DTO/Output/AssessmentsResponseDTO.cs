// -----------------------------------------------------------------------
// <copyright file="AssessmentsResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class AssessmentsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<AssessmentsDTO> AssessmentList { get; set; }
    }

    public class BulkAddAssessmentResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public string AssessmentIDDetails { get; set; }
        public string AssessmentEmailLinkDetails { get; set; }
    }
}
