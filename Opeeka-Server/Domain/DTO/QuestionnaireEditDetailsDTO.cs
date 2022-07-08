// -----------------------------------------------------------------------
// <copyright file="QuestionnaireEditDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireEditDetailsDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a QuestionnaireID")]
        public int QuestionnaireID { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a UpdatedUserID")]
        public int UpdateUserID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool HasFormView { get; set; }
    }
}
