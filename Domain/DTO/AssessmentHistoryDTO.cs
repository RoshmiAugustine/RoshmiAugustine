﻿// -----------------------------------------------------------------------
// <copyright file="AssessmentHistoryDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class AssessmentHistoryDTO
    {
        public int AssessmentReviewHistoryID { get; set; }
        public DateTime? RecordedDate { get; set; }
        public int StatusFrom { get; set; }
        public int StatusTo { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
    }
}
