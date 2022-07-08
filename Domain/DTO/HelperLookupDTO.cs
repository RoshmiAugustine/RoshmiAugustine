// -----------------------------------------------------------------------
// <copyright file="HelperLookupDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class HelperLookupDTO
    {
        public int HelperID { get; set; }
        public Guid? HelperIndex { get; set; }
        public string HelperName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
