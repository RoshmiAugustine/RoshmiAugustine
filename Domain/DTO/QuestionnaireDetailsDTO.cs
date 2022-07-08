// -----------------------------------------------------------------------
// <copyright file="QuestionnaireDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireDetailsDTO
    {
        public int TotalCount { get; set; }
        public int QuestionnaireItemID { get; set; }
        public string CatagoryAbbrev { get; set; }
        public string ItemName { get; set; }
        public int ListOrder { get; set; }
        public int Property { get; set; }
        public string PropertyValue { get; set; }
        public int CategoryID { get; set; }
        public bool MinOption { get; set; }
        public int MinDefault { get; set; }
        public decimal MinDefaultValue { get; set; }
        public bool MaxOption { get; set; }
        public string MinThresholdValue { get; set; }
        public int MinThreshold { get; set; }
        public decimal MaxDefaultValue { get; set; }
        public int MaxDefault { get; set; }
        public int MaxThreshold { get; set; }
        public string MaxThresholdValue { get; set; }
        public int AltDefault { get; set; }
        public bool AltOption { get; set; }
        public string AltDefaultValue { get; set; }
        public bool Focus { get; set; }
        public int ItemID { get; set; }
        public int LowerResponseValue { get; set; }
        public int UpperResponseValue { get; set; }
        public int ResponseValueTypeID { get; set; }
        public decimal MinRange { get; set; }
        public int MaxRange { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
