// -----------------------------------------------------------------------
// <copyright file="ResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AlertAutoResolveProcess.DTO
{
    public class ResponseDTO
    {
        public int ResponseID { get; set; }
        public int ItemID { get; set; }
        public int? BackgroundColorPaletteID { get; set; }
        public string Label { get; set; }
        public string KeyCodes { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public int? MaxRangeValue { get; set; }
        public int ListOrder { get; set; }
        public int? DefaultItemResponseBehaviorID { get; set; }
        public bool IsItemResponseBehaviorDisabled { get; set; }
    }
}
