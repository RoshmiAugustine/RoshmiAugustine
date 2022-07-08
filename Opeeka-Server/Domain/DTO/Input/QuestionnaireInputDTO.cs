// -----------------------------------------------------------------------
// <copyright file="QuestionnaireInputDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class QuestionnaireInputDTO
    {
        public int QuestionnaireItemID { get; set; }
        public int? LowerItemResponseBehaviorID { get; set; }
        public int? MedianItemResponseBehaviorID { get; set; }
        public int? UpperItemResponseBehaviorID { get; set; }
        [Required]
        [Range(1, Int64.MaxValue, ErrorMessage = "Please enter a valid UpdateUserID")]
        public int UpdateUserID { get; set; }
        public int? LowerResponseValue { get; set; }
        public int? UpperResponseValue { get; set; }
        public bool MinOption { get; set; }
        public bool MaxOption { get; set; }
        public bool AltOption { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}