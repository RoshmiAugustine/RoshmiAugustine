using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{
    public class PersonHelperAddDTOForExternal
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter a PersonID")]
        public long PersonID { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a HelperID")]
        public int HelperID { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        public bool IsLead { get; set; }

        public int? CollaborationID { get; set; }
     
    }

    public class ExpirePersonHelperAddDTOForExternal
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a PersonHelperID")]
        public int PersonHelperID { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }
    }
    public class AddPersonHelperResponseDTOForExternal
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public long Id { get; set; }

    }
    public class ExpirePersonHelperResponseDTOForExternal
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }


    }
}
