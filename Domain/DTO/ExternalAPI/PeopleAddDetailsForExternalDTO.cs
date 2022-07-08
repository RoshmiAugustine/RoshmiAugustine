using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{
    public class PeopleAddDetailsForExternalDTO
    {
        [Required]
        [RegularExpression(PCISEnum.InputDTOValidationPattern.Name, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateFirstName)]
        public string FirstName { get; set; }

        [RegularExpression(PCISEnum.InputDTOValidationPattern.Name, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateMiddleName)]
        public string MiddleName { get; set; }

        [Required]
        [RegularExpression(PCISEnum.InputDTOValidationPattern.Name, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateLastName)]
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

        public string Phone2 { get; set; }

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

        public List<PersonAddIdentificationForExternalDTO> PersonIdentifications { get; set; }

        [Required]
        public List<PersonAddRaceEthnicityForExternalDTO> PersonRaceEthnicities { get; set; }

        [Required]
        public List<PersonAddHelperForExternalDTO> PersonHelpers { get; set; }

        public List<PersonAddSupportForExternalDTO> PersonSupports { get; set; }
        
        [Required]
        public List<PersonAddCollaborationForExternalDTO> PersonCollaborations { get; set; }
    }

    public class PersonAddIdentificationForExternalDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Identification Type ID")]
        public int IdentificationTypeID { get; set; }

        [Required]
        public string IdentificationNumber { get; set; }
    }

    public class PersonAddRaceEthnicityForExternalDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a RaceEthnicity ID")]

        public int RaceEthnicityID { get; set; }
    }

    public class PersonAddHelperForExternalDTO
    {
        [Range(1, long.MaxValue, ErrorMessage = "Please enter a Helper ID")]
        public int HelperID { get; set; }
        [Required]
        public bool IsLead { get; set; }
       
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public int? CollaborationID { get; set; }
    }

    public class PersonAddSupportForExternalDTO
    {
        [Range(1, int.MaxValue , ErrorMessage = "Please enter a valid SupportTypeID")]
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

        [Required]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// If null then Support.Iscurrent = true;
        /// If a future date then Support.Iscurrent = true;
        /// If a date less than today then Support.Iscurrent = false;
        /// </summary>
        public DateTime? EndDate { get; set; }
        public bool TextPermission { get; set; }
        public bool EmailPermission { get; set; }


    }

    public class PersonAddCollaborationForExternalDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Collaboration ID")]
        public int CollaborationID { get; set; }

        [Required]
        public DateTime EnrollDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public bool IsPrimary { get; set; }
    }

    public class CRUDResponseDTOForExternalPersoneAdd : PersonRelatedReturnDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
      
        public Guid Index { get; set; }

        public long Id { get; set; }

    }
}
