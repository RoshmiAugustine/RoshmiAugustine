// -----------------------------------------------------------------------
// <copyright file="ItemDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class ItemDetailsDTO
    {
        public long Time { get; set; }
        public int AssessmentId { get; set; }
        public DateTime DateTaken { get; set; }
        public string Period { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public int ItemId { get; set; }
        public decimal? Value { get; set; }
        public string Item { get; set; }
        public string Notes { get; set; }
        public string Rgb { get; set; }
        public string ResponseLable { get; set; }
        public int ItemResponseBehaviorID { get; set; }
        public int ResponseValueTypeID { get; set; }
        public decimal minRange { get; set; }
        public int maxRange { get; set; }
        public string AssessmentResponseValue { get; set; }

    }
}
