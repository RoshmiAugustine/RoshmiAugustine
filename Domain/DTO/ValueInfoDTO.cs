// -----------------------------------------------------------------------
// <copyright file="ValueInfoDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class ValueInfoDTO
    {
        public string KeyCodes { get; set; }
        public string ResponseID { get; set; }
        public string Title { get; set; }
        public string DropdownChar { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public int ListOrder { get; set; }
        public string BackgroundColor { get; set; }
        public bool AutoExpand { get; set; }
        public int MaxRangeValue { get; set; }
    }
}
