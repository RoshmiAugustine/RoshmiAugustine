using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{
    public class PersonCollaborationAddDTOForExternal
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter a PersonID")]
        public long PersonID { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a HelperID")]
        public int CollaborationID { get; set; }

        [Required(ErrorMessage = "EnrollDate is required")]
        public DateTime EnrollDate { get; set; }

        public bool isPrimary { get; set; }


    }
    public class ExpirePersonCollaborationAddDTOForExternal
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a PersonCollaborationID")]
        public int PersonCollaborationID { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }
    }
    public class AddPersonCollaborationResponseDTOForExternal
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public long Id { get; set; }

    }
    public class ExpirePersonCollaborationResponseDTOForExternal
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }


    }
}
