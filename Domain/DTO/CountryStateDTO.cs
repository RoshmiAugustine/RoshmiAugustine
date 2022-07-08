// -----------------------------------------------------------------------
// <copyright file="CountryStateDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
namespace Opeeka.PICS.Domain.DTO
{
    public class CountryStateDTO
    {

        public int CountryStateID { get; set; }

        public int CountryID { get; set; }

        public String Name { get; set; }

        public String Abbrev { get; set; }

        public String Description { get; set; }

        public int ListOrder { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime UpdateDate { get; set; }

        public int UpdateUserId { get; set; }
    }
}



