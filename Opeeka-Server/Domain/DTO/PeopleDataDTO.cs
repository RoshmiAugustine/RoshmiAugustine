// -----------------------------------------------------------------------
// <copyright file="PeopleDataDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class PeopleDataDTO
    {

        public long PersonID { get; set; }
        public Guid PersonIndex { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Phone1Code { get; set; }
        public string Phone2Code { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public long PersonAddressID { get; set; }
        public string City { get; set; }
        public int? CountryStateID { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int IdentifiedGenderID { get; set; }
        public string IdentifiedGender { get; set; }
        public int? BioGenderID { get; set; }
        public string BioGender { get; set; }
        public int? SexualityID { get; set; }
        public string Sexuality { get; set; }
        public int? PrimaryLanugageID { get; set; }
        public string PrimaryLanugage { get; set; }
        public int? PreferredLanugageID { get; set; }
        public string PreferredLanugage { get; set; }
        public long AddressID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CountryID { get; set; }
        public string Country { get; set; }

        public List<PeopleIdentifierDTO> peopleIdentifier { get; set; }
        public List<PeopleRaceEthnicityDTO> peopleRaceEthnicity { get; set; }
        public List<PeopleSupportDTO> peopleSupport { get; set; }
        public List<PeopleHelperDTO> peopleHelper { get; set; }
        public List<PeopleCollaborationDTO> peopleCollaboration { get; set; }
        public int PersonScreeningStatusID { get; set; }
        public bool IsRemoved { get; set; }
        public bool IsActive { get; set; }
        public bool IsShared { get; set; }
        public long AgencyID { get; set; }
        public string AgencyName { get; set; }
        public string UniversalID { get; set; }
        public bool TextPermission { get; set; }
        public bool EmailPermission { get; set; }
        public bool IsHelperHasCollaboration { get; set; }
        public List<AuditPersonProfileDTO> LeadHelperHistory { get; set; }
        public List<AuditPersonProfileDTO> PrimaryCollaborationHistory { get; set; }
    }
}
