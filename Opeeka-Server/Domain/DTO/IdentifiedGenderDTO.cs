// -----------------------------------------------------------------------
// <copyright file="IdentifiedGenderDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class IdentifiedGenderDTO
    {
        public int IdentifiedGenderID { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public string Description { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public long AgencyID { get; set; }
    }
}
