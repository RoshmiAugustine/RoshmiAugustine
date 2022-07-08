using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Entities
{
    public class ImportType : BaseEntity
    {
        public int ImportTypeID { get; set; }
        public string Name { get; set; }
        public string TemplateJson { get; set; }
        public string TemplateURL { get; set; }
        public bool IsRemoved { get; set; }
        public int ListOrder { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
