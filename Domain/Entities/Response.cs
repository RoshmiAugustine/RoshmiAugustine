// -----------------------------------------------------------------------
// <copyright file="Response.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.Entities
{
    public class Response : BaseEntity
    {
        public int ResponseID { get; set; }
        public Guid ResponseIndex { get; set; }
        public int ItemID { get; set; }
        public int? BackgroundColorPaletteID { get; set; }
        public string Label { get; set; }
        public string? KeyCodes { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int? MaxRangeValue { get; set; }
        public int ListOrder { get; set; }
        public int? DefaultItemResponseBehaviorID { get; set; }
        public bool IsItemResponseBehaviorDisabled { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public bool DisplayChildItem { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public User UpdateUser { get; set; }
    }
}
