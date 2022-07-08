// -----------------------------------------------------------------------
// <copyright file="AssessmentReasonLookupResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class AssessmentReasonLookupResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<AssessmentReasonLookupDTO> AssessmentReasonLookup { get; set; }
    }
}
