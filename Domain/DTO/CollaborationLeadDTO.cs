// -----------------------------------------------------------------------
// <copyright file="CollaborationLeadDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class CollaborationLeadDTO
    {
        public int CollaborationLeadHistoryID { get; set; }

        public int CollaborationLeadUserID { get; set; }

        public string CollaborationLead { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
