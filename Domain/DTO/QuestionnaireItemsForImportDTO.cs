using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireItemsForImportDTO
    {
        public int QuestionnaireItemID { get; set; }
        public int ItemId { get; set; }
        public string QuestionnaireItemName { get; set; }
        public int ListOrder { get; set; }
        public bool EditableMin { get; set; }
        public bool Editable { get; set; }
        public bool EditableMax { get; set; }
        public string MinTypeInfo { get; set; }
        public string DefaultTypeInfo { get; set; }
        public string MaxTypeInfo { get; set; }
        public int MinItemResponseBehaviorID { get; set; }
        public int DefaultItemResponseBehaviorID { get; set; }
        public int MaxItemResponseBehaviorID { get; set; }
        public int MinThreshold { get; set; }
        public int MaxThreshold { get; set; }
        public string Property { get; set; }
        public bool IsOptional { get; set; }
        public string Responses { get; set; }

    }
}
