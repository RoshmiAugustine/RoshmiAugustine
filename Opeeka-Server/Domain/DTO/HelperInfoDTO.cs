// -----------------------------------------------------------------------
// <copyright file="HelperInfoDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class HelperInfoDTO
    {
        public int HelperID { get; set; }

        public int? SupervisorHelperID { get; set; }

        public Guid HelperIndex { get; set; }
        public Guid? SupervisorIndex { get; set; }


        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int? HelperTitleID { get; set; }
        public string HelperTitle { get; set; }

        public int RoleId { get; set; }

        public long AgencyId { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public long StateId { get; set; }

        public string Zip { get; set; }

        public string Role { get; set; }

        public string Agency { get; set; }

        public string Supervisor { get; set; }

        public string State { get; set; }
        public int? CountryStateId { get; set; }
        public int? SystemRoleID { get; set; }
        public int? ReviewerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string HelperExternalID { get; set; }
        public int UserId { get; set; }
        public bool IsEmailReminderAlerts { get; set; }
        public int? CountryID { get; set; }
        public string Country { get; set; }

    }
}

