// -----------------------------------------------------------------------
// <copyright file="PeopleHelperDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class PeopleHelperDTO
    {
        public int HelperID { get; set; }
        public string HelperName { get; set; }
        public DateTime HelperStartDate { get; set; }
        public DateTime? HelperEndDate { get; set; }
        public long PersonHelperID { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsLead { get; set; }
        public int? CollaborationID { get; set; }
        public string CollaborationName { get; set; }
        public int UserID { get; set; }
    }
}
