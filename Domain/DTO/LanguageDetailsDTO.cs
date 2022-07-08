// -----------------------------------------------------------------------
// <copyright file="LanguageDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class LanguageDetailsDTO
    {
        public int LanguageID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Abbrev { get; set; }

        public string Description { get; set; }

        [Required]
        public int ListOrder { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter a valid UpdateUserID")]
        public int UpdateUserID { get; set; }

        [Required]
        public Int64 AgencyID { get; set; }

    }
}
