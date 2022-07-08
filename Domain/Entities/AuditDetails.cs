// -----------------------------------------------------------------------
// <copyright file="AuditDetails.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class AuditDetails : BaseEntity
    {
        public string TableName { get; set; }
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AuditUser { get; set; }
        public string ReferenceKeyValues { get; set; }
        public string EntityState { get; set; }
        public string Tenant { get; set; }
    }
}
