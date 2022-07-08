// -----------------------------------------------------------------------
// <copyright file="HelperDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class HelperDTO
    {
        public int HelperID { get; set; }

        public Guid HelperIndex { get; set; }

        public int UserID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime UpdateDate { get; set; }

        public int UpdateUserID { get; set; }

        public long AgencyId { get; set; }

        public int? HelperTitleID { get; set; }

        public string Phone2 { get; set; }

        public int? SupervisorHelperID { get; set; }
        public bool IsEmailReminderAlerts { get; set; }

        public int? ReviewerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string HelperExternalID { get; set; }
    }
}
