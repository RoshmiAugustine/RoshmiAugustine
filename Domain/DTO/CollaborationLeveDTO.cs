// -----------------------------------------------------------------------
// <copyright file="CollaborationLeveDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class CollaborationLevelDTO
    {
        public int CollaborationLevelID { get; set; }
        public string Name { get; set; }

        public string Abbrev { get; set; }

        public int ListOrder { get; set; }

        public Int64 AgencyID { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid UpdateUserID")]
        public int UpdateUserID { get; set; }

        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
