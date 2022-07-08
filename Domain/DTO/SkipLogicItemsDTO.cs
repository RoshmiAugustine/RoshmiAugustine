using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class SkipLogicItemsDTO
    {
        public int? QuestionnaireItemID { get; set; }
        public int? CategoryID { get; set; }
        public int ActionTypeID { get; set; }
        public int? ItemID { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public int? ParentItemID { get; set; }
        public string ParentName { get; set; }
    }
}
