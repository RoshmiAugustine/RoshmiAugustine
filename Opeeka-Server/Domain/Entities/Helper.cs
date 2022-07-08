// -----------------------------------------------------------------------
// <copyright file="Helper.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class Helper : BaseEntity
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

        public long AgencyID { get; set; }

        public int? HelperTitleID { get; set; }

        public string? Phone2 { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? SupervisorHelperID { get; set; }
        public int? ReviewerID { get; set; }
        public string HelperExternalID { get; set; }
        public bool IsEmailReminderAlerts { get; set; } = true;

        public Agency Agency { get; set; }
        public HelperTitle HelperTitle { get; set; }
        public Helper SupervisorHelper { get; set; }
        public Helper Reviewer { get; set; }
        public User UpdateUser { get; set; }
        public User User { get; set; }

    }
}
