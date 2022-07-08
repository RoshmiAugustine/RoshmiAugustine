// -----------------------------------------------------------------------
// <copyright file="AuditEntry.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System.Linq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Infrastructure.Data;

namespace Opeeka.PICS.Infrastructure.Audit
{

    public class AuditEntry
    {
        private readonly OpeekaDBContext _context;

        public AuditEntry(EntityEntry entry, OpeekaDBContext context)
        {
            Entry = entry;
            _context = context;
        }

        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public string EntityState { get; set; }
        public string Tenant { get; set; }

        // public string AuditUser { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> ReferenceKeyValues { get; set; } = new Dictionary<string, object>();

        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> DeletingValues { get; } = new Dictionary<string, object>();

        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();
        //public List<PropertyInfo> AuditUser { get; set; } = new List<PropertyInfo>();
        public string AuditUser { get; set; }
        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public AuditDetails ToAudit()
        {
            var audit = new AuditDetails
            {
                TableName = TableName,
                DateTime = DateTime.UtcNow,
                KeyValues = JsonConvert.SerializeObject(KeyValues),
                OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),
                //AuditUser = AuditUser.Count > 0 && NewValues.Count > 0 ? Convert.ToString(NewValues[AuditUser[0].Name]) : AuditUser.Count > 0 && DeletingValues.Count > 0 ? Convert.ToString(DeletingValues[AuditUser[0].Name]) : "N/A",
                AuditUser = AuditUser,
                ReferenceKeyValues = ReferenceKeyValues.Count == 0 ? null : JsonConvert.SerializeObject(ReferenceKeyValues),
                EntityState = EntityState,
                Tenant = Tenant
            };
            return audit;
        }
    }
}
