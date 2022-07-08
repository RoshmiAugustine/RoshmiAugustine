using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
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

    public class Response
    {
        public int ResponseId { get; set; }
        public string Label { get; set; }
        public string DropdownChar { get; set; }
        public string KeyCodes { get; set; }
        public decimal Value { get; set; }
        public int ListOrder { get; set; }
    }
}
