// -----------------------------------------------------------------------
// <copyright file="ItemResponseBehaviorDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace DashboardMatrixCalculationProcess.DTO
{
    public class ItemResponseBehaviorDTO
    {
        public int ItemResponseBehaviorID { get; set; }
        public int ItemResponseTypeID { get; set; }
        public string Name { get; set; }
        public string? Abbrev { get; set; }
        public string? Description { get; set; }
        public int ListOrder { get; set; }
    }
}
