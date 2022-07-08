// -----------------------------------------------------------------------
// <copyright file="Person.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class Person : BaseEntity
    {
        public long PersonID { get; set; }
        public Guid PersonIndex { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }  
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public int? PrimaryLanguageID { get; set; }
        public int? PreferredLanguageID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? GenderID { get; set; }
        public int? SexualityID { get; set; }
        public int? BiologicalSexID { get; set; }
        public string Email { get; set; }
        public string Phone1Code { get; set; }
        public string Phone2Code { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public bool? IsActive { get; set; }
        public int PersonScreeningStatusID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsRemoved { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public long AgencyID { get; set; }
        public bool TextPermission { get; set; }
        public bool EmailPermission { get; set; }
        public DateTime? SMSConsentStoppedON { get; set; }


        public Agency Agency { get; set; }
        public Gender BiologicalSex { get; set; }
        public PersonScreeningStatus PersonScreeningStatus { get; set; }
        public Language PreferredLanguage { get; set; }
        public Language PrimaryLanguage { get; set; }
        public Sexuality Sexuality { get; set; }
        public User UpdateUser { get; set; }
        public string UniversalID { get; set; }

    }
}
