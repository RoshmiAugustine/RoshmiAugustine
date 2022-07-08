// -----------------------------------------------------------------------
// <copyright file="CollaborationTagDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class CollaborationTagDTO
    {
        public int CollaborationTagID { get; set; }

        public int CollaborationID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a collaboration tag type")]
        public int CollaborationTagTypeID { get; set; }

        public bool IsRemoved { get; set; }
    }
}
