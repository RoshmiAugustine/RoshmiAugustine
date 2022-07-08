// -----------------------------------------------------------------------
// <copyright file="AssessmentValuesDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class AssessmentValuesDTO
    {
        public int AssessmentID { get; set; }
        public int AssessmentResponseID { get; set; }
        public int? PersonSupportID { get; set; }
        public int ResponseId { get; set; }
        public string BehaviorName { get; set; }
        public bool IsRequiredConfidential { get; set; }
        public long PersonID { get; set; }
        public int ItemResponseBehaviorID { get; set; }
        public int ItemId { get; set; }
        public string KeyCodes { get; set; }
        public decimal Value { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public bool IsPersonRequestedConfidential { get; set; }
        public int QuestionnaireItemID { get; set; }
        public bool IsCloned { get; set; }
        public bool IsOtherConfidential { get; set; }
        public int CategoryID { get; set; }
        public DateTime Date { get; set; }
        public string CaregiverCategory { get; set; }
        public int VoicetypeID { get; set; }
        public string ColorPalette { get; set; }
        /// <summary>
        /// Holds the value for response value types like slider,text,date,checkbox etc
        /// For response value as List it stores the label from response table.
        /// </summary>
        public string AssessmentResponseValue { get; set; }
        public string ChildItemResponses { get; set; }
        public string Attachments { get; set; }
        
        public bool Isconfidential {
            get
            {
                if (IsRequiredConfidential || IsPersonRequestedConfidential || IsOtherConfidential)
                {
                    return true;
                }
                return false;
            }
        }
        public int ResponseValueTypeId { get; set; }
    }
}
