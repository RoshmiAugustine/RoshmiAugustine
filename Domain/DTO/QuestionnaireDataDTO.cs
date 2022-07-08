// -----------------------------------------------------------------------
// <copyright file="QuestionnaireDataDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireDataDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid QuestionnaireID")]
        public int QuestionnaireID { get; set; }

        public string Questionnaire { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool IsMandatory { get; set; }
        [DefaultValue(true)]
        public bool IsReminderOn { get; set; }
        public int CollaborationQuestionnaireID { get; set; }
    }
}
