// -----------------------------------------------------------------------
// <copyright file="Helper.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class ApplicationObject : BaseEntity
    {
        public int ApplicationObjectID { get; set; }
        public int ApplicationObjectTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }

        public ApplicationObjectType ApplicationObjectType { get; set; }
        public User UpdateUser { get; set; }
    }
}
