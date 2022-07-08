// -----------------------------------------------------------------------
// <copyright file="ItemResponseType.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace DashboardMatrixCalculationProcess.DTO
{
    public class MetricsInputDTO
    {
        public long personId { get; set; }
       
        public List<int> itemIds { get; set; }
        public int AssessmentID { get; set; }
    }
}
