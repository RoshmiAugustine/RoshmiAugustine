

using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class ApplicationObjectDTO
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
        public List<string> OperationTypes { get; set; }
        public int OperationTypeID { get; set; }
        public string OperationTypeName { get; set; }
        public int SystemRoleID { get; set; }

        public List<ApplicationObjectDTO> GetPermissions()
        {
            List<ApplicationObjectDTO> permissions = new List<ApplicationObjectDTO>();

            permissions.Add(new ApplicationObjectDTO { ApplicationObjectId = 1, Action = "GetData", Controller = "AccountController" });
            return permissions;
        }
    }

}