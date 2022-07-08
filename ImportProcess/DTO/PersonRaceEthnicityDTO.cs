using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class PersonRaceEthnicityDTO
    {
        public long PersonRaceEthnicityID { get; set; }
        public long PersonID { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Please enter a RaceEthnicity ID")]

        public int RaceEthnicityID { get; set; }

        public bool IsRemoved { get; set; }
        public Guid PersonIndexGuid { get; set; }
    }
}
