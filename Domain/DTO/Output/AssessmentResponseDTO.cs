// -----------------------------------------------------------------------
// <copyright file="AssessmentResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class AssessmentResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public AssessmentDTO AssessmentDetails { get; set; }
        public List<AssessmentValuesDTO> AssessmentValues { get; set; }
    }
}
