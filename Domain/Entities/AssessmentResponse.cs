// -----------------------------------------------------------------------
// <copyright file="AssessmentResponse.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class AssessmentResponse : BaseEntity
    {
        public int AssessmentResponseID { get; set; }
        public int? AssessmentID { get; set; }
        public int? PersonSupportID { get; set; }
        public int? ResponseID { get; set; }
        public int? ItemResponseBehaviorID { get; set; }
        public bool IsRequiredConfidential { get; set; }
        public bool? IsPersonRequestedConfidential { get; set; }
        public bool? IsOtherConfidential { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int? QuestionnaireItemID { get; set; }
        public bool IsCloned { get; set; }
        public string CaregiverCategory { get; set; }
        public int? Priority { get; set; }
        public Guid AssessmentResponseGuid { get; set; }
        /// <summary>
        /// Holds the value for response value types like slider,text,date,checkbox etc
        /// For response value as List it stores the label from response table.
        /// </summary>
        public string Value { get; set; }
        public int? ItemID { get; set; }
        public int? GroupNumber { get; set; }
        public int? ParentAssessmentResponseID { get; set; }


        public Assessment Assessment { get; set; }
        public PersonSupport PersonSupport { get; set; }
        public Response Response { get; set; }
        public ItemResponseBehavior ItemResponseBehavior { get; set; }
        public User UpdateUser { get; set; }
        public QuestionnaireItem QuestionnaireItem { get; set; }
        public Item Item { get; set; }
        public AssessmentResponse ParentAssessmentResponse { get; set; }
    }

}
