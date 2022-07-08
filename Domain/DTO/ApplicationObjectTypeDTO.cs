

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class ApplicationObjectTypeDTO
    {
        public int ApplicationObjectId { get; set; }
        public int ApplicationObjectTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateUserID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ApplicationObjectTypeName { get; set; }
    }
}