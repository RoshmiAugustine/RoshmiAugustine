using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class RoleLookupDTO
    {
        public int SystemRoleID { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public bool IsExternal { get; set; }

    }
}
