using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EHRProcess.DTO
{
    public class PeopleDetailsDTO
    {
        public long PersonID { get; set; }

        public Guid PersonIndex { get; set; }

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

        [Range(1, long.MaxValue, ErrorMessage = "Please enter a Person Screening Status ID")]
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

        public long? PersonAddressID { get; set; }

        public long AddressID { get; set; }

        public bool IsPrimary { get; set; }

        public Guid AddressIndex { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Please enter a Country State Id")]

        public int? CountryStateId { get; set; }

        public string Zip { get; set; }

        public string Zip4 { get; set; }

        public string UniversalID { get; set; }
        public List<PersonIdentificationDTO> PersonIdentifications { get; set; }

        public List<PersonRaceEthnicityDTO> PersonRaceEthnicities { get; set; }
        public List<PersonHelperDTO> PersonHelpers { get; set; }

        public List<PersonSupportDTO> PersonSupports { get; set; }
        public List<PersonCollaborationDTO> PersonCollaborations { get; set; }

    }


    public class PersonIdentificationDTO
    {
        public long PersonIdentificationID { get; set; }
        public long PersonID { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Please enter a Identification Type ID")]
        public int IdentificationTypeID { get; set; }

        //[Required]
        public string IdentificationNumber { get; set; }
        public bool IsRemoved { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Updated User ID")]
        public int UpdateUserID { get; set; }
    }
    public class PersonRaceEthnicityDTO
    {
        public long PersonRaceEthnicityID { get; set; }
        public long PersonID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a RaceEthnicity ID")]

        public int RaceEthnicityID { get; set; }

        public bool IsRemoved { get; set; }
    }

    public class PersonHelperDTO
    {
        public long PersonHelperID { get; set; }
        public long PersonID { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please enter a Helper ID")]
        public int HelperID { get; set; }
        [Required]
        public bool IsLead { get; set; }
        [Required]
        public bool IsCurrent { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool IsRemoved { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a UpdateUserID ")]
        public int UpdateUserID { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
    }

    public class PersonSupportDTO
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

        public DateTime UpdateDate { get; set; }
        public string UniversalID { get; set; }

    }


    public class PersonCollaborationDTO
    {
        public long PersonCollaborationID { get; set; }
        public long PersonID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Collaboration ID")]
        public int CollaborationID { get; set; }

        [Required]
        public DateTime EnrollDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public bool IsPrimary { get; set; }

        [Required]
        public bool IsCurrent { get; set; }


        public bool IsRemoved { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enterUpdateUserID")]
        public int UpdateUserID { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }
    }
}
