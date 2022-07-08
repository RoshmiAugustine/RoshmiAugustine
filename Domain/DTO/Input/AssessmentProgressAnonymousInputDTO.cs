// -----------------------------------------------------------------------
// <copyright file="AssessmentProgressAnonymousInputDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class AssessmentProgressAnonymousInputDTO
    {
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter a valid User")]
        public int UpdateUserID { get; set; }

        public List<AssessmentProgressInputDTO> assessmentProgressData { get; set; }
    }
}
