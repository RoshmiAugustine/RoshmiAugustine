// -----------------------------------------------------------------------
// <copyright file="HelperDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class HelperDetailsDTO
    {
        public Guid? HelperIndex { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? HelperTitleID { get; set; }

        [Required]
        public int RoleId { get; set; }

        public int UpdateUserID { get; set; }

        [Required]
        public long AgencyID { get; set; }

        public int? SupervisorHelperID { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public int? StateId { get; set; }

        public string Zip { get; set; }

        [Required]
        public int? ReviewerID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string HelperExternalID { get; set; }
        public bool IsEmailReminderAlerts { get; set; }
        public int? CountryId { get; set; }
        [DefaultValue(true)]
        public bool SendSignUpMail { get; set; }
    }
}
