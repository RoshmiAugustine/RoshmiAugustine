// -----------------------------------------------------------------------
// <copyright file="QuestionnaireItem.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.Entities
{
    public class QuestionnaireItem : BaseEntity
    {
        public int QuestionnaireItemID { get; set; }
        public Guid QuestionnaireItemIndex { get; set; }
        public int QuestionnaireID { get; set; }
        public int CategoryID { get; set; }
        public int ItemID { get; set; }
        public bool IsOptional { get; set; }
        public bool CanOverrideLowerResponseBehavior { get; set; }
        public bool CanOverrideMedianResponseBehavior { get; set; }
        public bool CanOverrideUpperResponseBehavior { get; set; }
        public int? LowerItemResponseBehaviorID { get; set; }
        public int? MedianItemResponseBehaviorID { get; set; }
        public int? UpperItemResponseBehaviorID { get; set; }
        public bool IsActive { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int? LowerResponseValue { get; set; }
        public int? UpperResponseValue { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ClonedQuestionnaireItemId { get; set; }
        public bool? DefaultRequiredConfidentiality { get; set; }
        public bool? DefaultPersonRequestedConfidentiality { get; set; }
        public bool? DefaultOtherConfidentiality { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Category Category { get; set; }
        public Item Item { get; set; }
        public ItemResponseBehavior LowerItemResponseBehavior { get; set; }
        public ItemResponseBehavior MedianItemResponseBehavior { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public User UpdateUser { get; set; }
        public ItemResponseBehavior UpperItemResponseBehavior { get; set; }

    }
}
