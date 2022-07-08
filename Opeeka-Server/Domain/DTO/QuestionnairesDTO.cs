// -----------------------------------------------------------------------
// <copyright file="QuestionnairesDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnairesDTO
    {

        public int QuestionnaireID { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a InstrumentID")]
        public int InstrumentID { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a AgencyID")]
        public long? AgencyID { get; set; }
        [Required]
        public string Description { get; set; }
        public string Abbrev { get; set; }
        public string ReminderScheduleName { get; set; }
        public bool IsEmailRemindersHelpers { get; set; }
        public bool IsAlertsHelpersManagers { get; set; }
        [Required]
        public string RequiredConfidentialityLanguage { get; set; }
        public string PersonRequestedConfidentialityLanguage { get; set; }
        public string OtherConfidentialityLanguage { get; set; }
        [Required]
        public bool IsPubllished { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a ParentQuestionnaireID")]
        public int? ParentQuestionnaireID { get; set; }
        [Required]
        public bool IsBaseQuestionnaire { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public bool IsRemoved { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a UpdateUserID")]
        public int UpdateUserID { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        public int OwnerUserID { get; set; }
        public bool HasSkipLogic { get; set; }
        public bool HasDefaultResponseRule { get; set; }

        public bool HasFormView { get; set; }
        public bool IsEmailInviteToCompleteReminders { get; set; }
        public string InviteToCompleteReceiverIds { get; set; }

    }
}
