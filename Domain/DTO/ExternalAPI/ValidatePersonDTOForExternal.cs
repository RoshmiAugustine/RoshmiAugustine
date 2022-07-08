using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{
    public class ValidatePersonDTOForExternal
    {
       
        public Guid PersonIndex { get; set; }
        public long PersonAddressID { get; set; }

        public long PersonID { get; set; }

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

        public string Zip { get; set; }
        public string Phone2 { get; set; }

        public int LanguageID { get; set; }

        public long AgencyID { get; set; }

        public long AddressID { get; set; }

        public int? CountryStateId { get; set; }

        public int? CountryId { get; set; }

        public List<PersonIdentificationDTO> PersonIdentifications { get; set; }

        public List<PersonRaceEthnicityDTO> PersonRaceEthnicities { get; set; }
        public List<PersonHelperDTO> PersonHelpers { get; set; }

        public List<PersonSupportDTO> PersonSupports { get; set; }
        public List<PersonCollaborationDTO> PersonCollaborations { get; set; }

    }
}
