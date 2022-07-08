// -----------------------------------------------------------------------
// <copyright file="PeopleEditDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class PeopleEditDetailsDTO
    {
        [Required]
        public Guid PersonIndex { get; set; }
        public long PersonAddressID { get; set; }


        public long PersonID { get; set; }


        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Suffix { get; set; }

        public int? PrimaryLanguageID { get; set; }

        public int? PreferredLanguageID { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int GenderID { get; set; }

        public int? SexualityID { get; set; }

        public int? BiologicalSexID { get; set; }

        public string Email { get; set; }

        public string Phone1Code { get; set; }

        public string Phone2Code { get; set; }

        public string Phone1 { get; set; }

        public bool IsPreferred { get; set; }

        public string Phone2 { get; set; }

        public bool IsActive { get; set; }

        public int LanguageID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Person Screening Status ID")]
        public int PersonScreeningStatusID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsRemoved { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Update User ID")]

        public int UpdateUserID { get; set; }

        public DateTime UpdateDate { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please enter a Agency ID")]

        public long AgencyID { get; set; }


        public long AddressID { get; set; }

        public bool IsPrimary { get; set; }

        public Guid AddressIndex { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public int? CountryStateId { get; set; }

        public string Zip { get; set; }

        public string Zip4 { get; set; }
        public string UniversalID { get; set; }

        public int? CountryId { get; set; }
        public bool TextPermission { get; set; }
        public bool EmailPermission { get; set; }

        public List<PersonEditIdentificationDTO> PersonEditIdentificationDTO { get; set; }

        public List<PersonEditHelperDTO> PersonEditHelperDTO { get; set; }
        public List<PersonEditCollaborationDTO> PersonEditCollaborationDTO { get; set; }

        public List<PersonEditRaceEthnicityDTO> PersonEditRaceEthnicityDTO { get; set; }
        public List<PersonEditSupportDTO> PersonEditSupportDTO { get; set; }
    }
}
