using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class ActionTypeDTO
    {
        public int ActionTypeID { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
    }
}
