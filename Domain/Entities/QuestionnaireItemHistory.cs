// -----------------------------------------------------------------------
// <copyright file="QuestionnaireItemHistory.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class QuestionnaireItemHistory : BaseEntity
    {
        public int QuestionnaireItemHistoryID { get; set; }
        public int QuestionnaireItemID { get; set; }
        public DateTime InactiveStartDate { get; set; }
        public DateTime? InactiveEndDate { get; set; }

        public QuestionnaireItem QuestionnaireItem { get; set; }
    }
}
