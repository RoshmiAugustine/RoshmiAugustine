using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class PeopleIdentifierDTO
    {
        public int IdentificationTypeID { get; set; }
        public string IdentifierType { get; set; }
        public string IdentifierID { get; set; }
        public long PersonIdentificationID { get; set; }
    }
}
