using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{
    public class ValidateHelperDTOForExternal
    {
        public int? HelperTitleID { get; set; }

        public int RoleId { get; set; }

        public string Email { get; set; }

        public int UpdateUserID { get; set; }

        public long AgencyID { get; set; }

        public int? SupervisorHelperID { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }


        public int? StateId { get; set; }


        public int? ReviewerID { get; set; }

        public string HelperExternalID { get; set; }

        public int? CountryId { get; set; }

    }
}
