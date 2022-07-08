// -----------------------------------------------------------------------
// <copyright file="NotificationNotesDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireRiskItemDetailsDTO
    {
        public int QuestionnaireItemID { get; set; }

        public string ComparisonOperator { get; set; }

        public decimal ComparisonValue { get; set; }

        public string JoiningOperator { get; set; }

        public int ItemID { get; set; }

        public string ItemName { get; set; }
        public int AssessmentID { get; set; }
        public int QuestionnaireID { get; set; }
        public Guid PersonIndex { get; set; }

    }
}
