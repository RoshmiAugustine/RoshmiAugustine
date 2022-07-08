// -----------------------------------------------------------------------
// <copyright file="PersonSupport.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonSupport : BaseEntity
    {
        public int PersonSupportID { get; set; }
        public long PersonID { get; set; }
        public int SupportTypeID { get; set; }
        public bool IsCurrent { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Email { get; set; }
        public string PhoneCode { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsRemoved { get; set; }
        public int UpdateUserID { get; set; }
        public bool TextPermission { get; set; }
        public bool EmailPermission { get; set; }
        public DateTime? SMSConsentStoppedON { get; set; }


        public DateTime UpdateDate { get; set; }

        public Person Person { get; set; }
        public SupportType SupportType { get; set; }
        public User UpdateUser { get; set; }
        public string UniversalID { get; set; }


    }
}
