// -----------------------------------------------------------------------
// <copyright file="CountryState.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class CountryState : BaseEntity
    {

        public int CountryStateID { get; set; }

        public int CountryID { get; set; }

        public String Name { get; set; }

        public String? Abbrev { get; set; }

        public String? Description { get; set; }

        public int ListOrder { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime UpdateDate { get; set; }

        public int UpdateUserID { get; set; }

        public Country Country { get; set; }
        public User UpdateUser { get; set; }
    }
}
