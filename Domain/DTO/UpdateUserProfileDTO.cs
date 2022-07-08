using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class UpdateUserProfileDTO
    {
        public string Name { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public int HelperTitleID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Guid HelperIndex { get; set; }
    }
}
