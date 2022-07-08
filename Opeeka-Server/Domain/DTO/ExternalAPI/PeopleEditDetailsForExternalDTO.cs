// -----------------------------------------------------------------------
// <copyright file="PeopleEditDetailsForExternalDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class PeopleEditDetailsForExternalDTO
    {
        [Required]
        public Guid PersonIndex { get; set; }
        public long PersonAddressID { get; set; }

        [Required]
        [RegularExpression(PCISEnum.InputDTOValidationPattern.Name, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateFirstName)]
        public string FirstName { get; set; }

        [RegularExpression(PCISEnum.InputDTOValidationPattern.Name, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateLastName)]
        public string MiddleName { get; set; }

        [Required]
        [RegularExpression(PCISEnum.InputDTOValidationPattern.Name, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateMiddleName)]
        public string LastName { get; set; }

        public string Suffix { get; set; }

        public int? PrimaryLanguageID { get; set; }

        public int? PreferredLanguageID { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid GenderId.")]
        public int GenderID { get; set; }

        public int? SexualityID { get; set; }

        public int? BiologicalSexID { get; set; }

        public string Email { get; set; }

        public string Phone1Code { get; set; }

        public string Phone1 { get; set; }

        public string Phone2Code { get; set; }

        public bool IsPreferred { get; set; }

        public string Phone2 { get; set; }

        public Guid addressIndex { get; set; }

        public bool IsPrimary { get; set; }

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

        public List<PersonEditIdentificationForExternalDTO> PersonEditIdentificationDTO { get; set; }

        [Required]
        public List<PersonEditHelperForExternalDTO> PersonEditHelperDTO { get; set; }

        [Required]
        public List<PersonEditCollaborationForExternalDTO> PersonEditCollaborationDTO { get; set; }

        [Required]
        public List<PersonEditRaceEthnicityForExternalDTO> PersonEditRaceEthnicityDTO { get; set; }
        public List<PersonEditSupportForExternalDTO> PersonEditSupportDTO { get; set; }
    }

    public class PersonEditSupportForExternalDTO
    {

        public int PersonSupportID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid SupportTypeID")]
        public int SupportTypeID { get; set; }

        [Required]
        [RegularExpression(PCISEnum.InputDTOValidationPattern.Name, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateFirstName)]
        public string FirstName { get; set; }
        [RegularExpression(PCISEnum.InputDTOValidationPattern.Name, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateMiddleName)]
        public string MiddleName { get; set; }
        [Required]
        [RegularExpression(PCISEnum.InputDTOValidationPattern.Name, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateLastName)]
        public string LastName { get; set; }

        public string Suffix { get; set; }

        public string Email { get; set; }

        public string PhoneCode { get; set; }

        public string Phone { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string UniversalID { get; set; }
        public bool TextPermission { get; set; }
        public bool EmailPermission { get; set; }

    }

    public class PersonEditRaceEthnicityForExternalDTO
    {
        public long PersonRaceEthnicityID { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please enter a RaceEthnicity ID")]
        public int RaceEthnicityID { get; set; }
    }

    public class PersonEditCollaborationForExternalDTO
    {

        public long PersonCollaborationID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Collaboration ID")]
        public int CollaborationID { get; set; }

        [Required]
        public DateTime EnrollDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public bool IsPrimary { get; set; }
    }

    public class PersonEditHelperForExternalDTO
    {
        public long PersonHelperID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Helper ID")]
        public int HelperID { get; set; }
        [Required]
        public bool IsLead { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public int? CollaborationID { get; set; }
    }

    public class PersonEditIdentificationForExternalDTO
    {
        public long PersonIdentificationID { get; set; }
        [Required]
        public int IdentificationTypeID { get; set; }
        [Required]
        public string IdentificationNumber { get; set; }

    }

    public class CRUDResponseDTOForExternalPersoneEdit : PersonRelatedReturnDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public Guid Index { get; set; }

        public long Id { get; set; }
    }
}
