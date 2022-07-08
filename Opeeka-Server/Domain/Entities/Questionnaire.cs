// -----------------------------------------------------------------------
// <copyright file="Questionnaire.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.Entities
{
    public class Questionnaire : BaseEntity
    {
        public int QuestionnaireID { get; set; }
        public int InstrumentID { get; set; }
        public long? AgencyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Abbrev { get; set; }
        public string? ReminderScheduleName { get; set; }
        public string RequiredConfidentialityLanguage { get; set; }
        public string? PersonRequestedConfidentialityLanguage { get; set; }
        public string? OtherConfidentialityLanguage { get; set; }
        public bool IsPubllished { get; set; }
        public int? ParentQuestionnaireID { get; set; }
        public bool? IsBaseQuestionnaire { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsRemoved { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public int OwnerUserID { get; set; }
        public bool IsEmailRemindersHelpers { get; set; } = false;
        public bool IsAlertsHelpersManagers { get; set; } = false;
        public bool HasSkipLogic { get; set; } = false;
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public bool HasDefaultResponseRule { get; set; } = false;

        public bool HasFormView { get; set; } = false;
        public bool IsEmailInviteToCompleteReminders { get; set; } = false;
        public string? InviteToCompleteReceiverIds { get; set; }

        public Agency Agency { get; set; }
        public Instrument Instrument { get; set; }
        public User UpdateUser { get; set; }
        public Questionnaire ParentQuestionnaire { get; set; }
    }
}
