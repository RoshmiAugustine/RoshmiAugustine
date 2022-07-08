using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{
    public class PersonSearchInputDTO : Paginate
    {
        public PersonSearchFields SearchFields { get; set; }
    }
    public class Paginate
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid page number.")]
        public int PageNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid page size.")]
        public int PageSize { get; set; }
    }
    public class PersonSearchFields
    {
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public string Name { get; set; }
        public string ExternalId { get; set; }
        public Guid? PersonIndex { get; set; }
        public long? PersonId { get; set; }
    }

    public class LoggedInUserDTO
    {
        public long AgencyId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public int HelperId { get; set; }
    }

    public class PeopleProfileDetailsDTO
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

        public int GenderID { get; set; }

        public int? SexualityID { get; set; }

        public int? BiologicalSexID { get; set; }

        public string Email { get; set; }

        public string Phone1Code { get; set; }

        public string Phone2Code { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public bool IsActive { get; set; }

        public int PersonScreeningStatusID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsRemoved { get; set; }

        public int UpdateUserID { get; set; }

        public DateTime UpdateDate { get; set; }

        public long AgencyID { get; set; }

        public long? PersonAddressID { get; set; }

        public long AddressID { get; set; }

        public Guid? AddressIndex { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public int? CountryStateId { get; set; }

        public string Zip { get; set; }

        public string Zip4 { get; set; }

        public string UniversalID { get; set; }
        public int? CountryId { get; set; }

        public string PersonIdentifications { get; set; }
        public string PersonRaceEthnicitys { get; set; }
        public string PersonSupports { get; set; }
        public string PersonHelpers { get; set; }
        public string PersonCollaborations { get; set; }

        public int TotalCount { get; set; }
    }

    public class PeopleDetailsResponseDTOForExternal
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }
        public List<PeopleProfileDetailsDTO> PeopleDataList { get; set; }
    }
}
