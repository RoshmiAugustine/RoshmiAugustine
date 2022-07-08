// -----------------------------------------------------------------------
// <copyright file="GetHelperDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class GetHelperDTO
    {
        [Range(1, long.MaxValue, ErrorMessage = "Please enter a valid UserID")]
        public int UserID { get; set; }

        [Range(1, 1000, ErrorMessage = "Please enter a valid PageSize")]
        public int PageSize { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid PageNumber")]
        public int PageNumber { get; set; }


    }
}
