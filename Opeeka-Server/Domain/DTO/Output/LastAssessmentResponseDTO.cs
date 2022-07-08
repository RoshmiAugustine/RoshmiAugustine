// -----------------------------------------------------------------------
// <copyright file="LastAssessmentResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class LastAssessmentResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public AssessmentDTO AssessmentData { get; set; }
    }
}
