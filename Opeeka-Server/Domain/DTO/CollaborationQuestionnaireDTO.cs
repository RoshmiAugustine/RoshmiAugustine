// -----------------------------------------------------------------------
// <copyright file="CollaborationQuestionnaireDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class CollaborationQuestionnaireDTO
    {
        public int CollaborationQuestionnaireID { get; set; }

        [Required]
        public int CollaborationID { get; set; }

        public int QuestionnaireID { get; set; }

        public bool IsMandatory { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsRemoved { get; set; }

        [DefaultValue(true)]
        public bool IsReminderOn { get; set; }
    }
}
