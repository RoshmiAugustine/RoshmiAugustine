// -----------------------------------------------------------------------
// <copyright file="Helper.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public int PermissionID { get; set; }
        public int ApplicationObjectID { get; set; }
        public int OperationTypeID { get; set; }
        public string Description { get; set; }
        public int ListOrder { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public bool IsRemoved { get; set; }

        public ApplicationObject ApplicationObject { get; set; }
        public OperationType OperationType { get; set; }
        public User UpdateUser { get; set; }
    }
}
