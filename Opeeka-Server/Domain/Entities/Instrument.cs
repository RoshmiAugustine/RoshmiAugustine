// -----------------------------------------------------------------------
// <copyright file="Instrument.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.Entities
{
    public class Instrument : BaseEntity
    {

        public int InstrumentID { get; set; }
        public String Name { get; set; }
        public String? Abbrev { get; set; }
        public String? Description { get; set; }
        public String Instructions { get; set; }
        public String? Authors { get; set; }
        public String Source { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public String? InstrumentUrl { get; set; }
        public String? IconUrl { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public User UpdateUser { get; set; }
    }
}
