// -----------------------------------------------------------------------
// <copyright file="PersonIdentificationDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonIdentificationDTO
    {
        public long PersonIdentificationID { get; set; }
        public long PersonID { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Please enter a Identification Type ID")]
        public int IdentificationTypeID { get; set; }

        //[Required]
        public string IdentificationNumber { get; set; }
        public bool IsRemoved { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Updated User ID")]
        public int UpdateUserID { get; set; }
        public Guid PersonIndexGuid { get; set; }
    }
}
