using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{
    public class PersonRelatedReturnDTO
    {
        public List<PeopleSupportExternalDTO> personSupport { get; set; }
        public List<PeopleHelperExternalDTO> personHelper { get; set; }
        public List<PeopleCollaborationExternalDTO> personCollaboration { get; set; }
    }
    public class PeopleHelperExternalDTO
    {
        public int HelperID { get; set; }
        public long PersonHelperID { get; set; }
    }
    public class PeopleSupportExternalDTO
    {
        public int PersonSupportID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
    public class PeopleCollaborationExternalDTO
    {
        public int CollaborationID { get; set; }
        public long PersonCollaborationID { get; set; }
    }
}
