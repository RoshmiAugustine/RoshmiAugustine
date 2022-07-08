﻿// -----------------------------------------------------------------------
// <copyright file="AuditHelper.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Entities.Attributes;
using Opeeka.PICS.Infrastructure.Data;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Audit
{

    public class AuditHelper
    {
        private readonly OpeekaDBContext _context;
        private readonly IHttpContextAccessor _accessor;
        public AuditHelper(OpeekaDBContext context, IConfiguration configuration, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }
        public List<AuditEntry> OnBeforeSaveChanges()
        {
            _context.ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            try
            {
                foreach (var entry in _context.ChangeTracker.Entries())
                {
                    if (entry.Entity is AuditDetails || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    {
                        continue;
                    }
                    string auditTableName = entry.Metadata.GetTableName();
                    // var auditableFields = _auditTableRepository.GetAuditableTableField(auditTableName);
                    var auditableTable = _context.AuditTableName.Where(x => x.TableName == auditTableName).ToList();
                    if (auditableTable.Count == 0)
                    {
                        return null;
                    }

                    var auditableFields = _context.AuditFieldName.Where(x => x.TableName == auditTableName).ToList();
                    if (auditableFields.Count == 0)
                    {
                        return null;
                    }

                    var auditEntry = new AuditEntry(entry, _context);
                    auditEntry.TableName = auditTableName;
                    auditEntry.Tenant = GetCurrentTenant();
                    auditEntries.Add(auditEntry);
                    var auditUserAdded = entry.Entity.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(AuditUserAdded)));
                    var auditUserModified = entry.Entity.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(AuditUserModified)));

                    // for the foreign key reference
                    //var references = entry.Entity.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(Reference)));

                    foreach (var property in entry.Properties)
                    {
                        if (!auditableFields.Any(x => x.FieldName == property.Metadata.Name))
                        {
                            continue;
                        }
                        if (property.IsTemporary)
                        {
                            // value will be generated by the database, get the value after saving
                            auditEntry.TemporaryProperties.Add(property);
                            continue;
                        }
                        string propertyName = property.Metadata.Name;
                        if (property.Metadata.IsPrimaryKey())
                        {
                            auditEntry.KeyValues[propertyName] = property.CurrentValue;
                            continue;
                        }
                        switch (entry.State)
                        {
                            case EntityState.Added:

                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.NewValues["EntityState"] = "Added";
                                auditEntry.EntityState = "Added";
                                auditEntry.AuditUser = GetCurrentUser().ToString();

                                // auditEntry.
                                break;

                            case EntityState.Deleted:
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.DeletingValues[propertyName] = property.CurrentValue;

                                auditEntry.OldValues["EntityState"] = "Deleted";
                                auditEntry.AuditUser = GetCurrentUser().ToString();
                                auditEntry.EntityState = "Deleted";

                                break;

                            case EntityState.Modified:
                                if (property.IsModified)
                                {
                                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                                    auditEntry.OldValues["EntityState"] = "Modified";
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                    auditEntry.NewValues["EntityState"] = "Modified";
                                    auditEntry.AuditUser = GetCurrentUser().ToString();

                                    auditEntry.EntityState = "Modified";
                                }
                                break;
                        }
                    }
                }
                // Save audit entities that have all the modifications
                foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
                {
                    _context.Audits.Add(auditEntry.ToAudit());
                }
            }
            catch (Exception)
            {

            }
            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        public Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            try
            {
                if (auditEntries == null || auditEntries.Count == 0)
                {
                    return Task.CompletedTask;
                }

                foreach (var auditEntry in auditEntries)
                {
                    // Get the final value of the temporary properties
                    foreach (var prop in auditEntry.TemporaryProperties)
                    {
                        if (prop.Metadata.IsPrimaryKey())
                        {
                            auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                        }
                        else
                        {
                            auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                        }
                    }
                    // Save the Audit entry
                    _context.Audits.Add(auditEntry.ToAudit());
                }

                return _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// Get Current Tenant Id
        /// </summary>
        /// <returns></returns>
        private string GetCurrentTenant()
        {
            string tenantId = "0";
            try
            {
                if (_accessor != null && _accessor.HttpContext.User != null)
                {
                    var claimsIdentity = (ClaimsIdentity)_accessor.HttpContext.User.Identity;
                    var tenantIdClaim = claimsIdentity.FindFirst(PCISEnum.TokenClaims.TenantId);
                    if (tenantIdClaim != null && !String.IsNullOrEmpty(tenantIdClaim.Value))
                    {
                        tenantId = tenantIdClaim.Value;
                    }
                }
            }
            catch (Exception)
            {

            }
            return tenantId;
        }

        /// <summary>
        /// Get Current User Id
        /// </summary>
        /// <returns></returns>
        private int GetCurrentUser()
        {
            int userId = 0;
            try
            {
                if (_accessor != null && _accessor.HttpContext.User != null)
                {
                    var claimsIdentity = (ClaimsIdentity)_accessor.HttpContext.User.Identity;
                    var userIDclaim = claimsIdentity.FindFirst(PCISEnum.TokenClaims.Culture);
                    if (userIDclaim != null && !String.IsNullOrEmpty(userIDclaim.Value))
                    {
                        userId = Convert.ToInt32(userIDclaim.Value);
                    }
                }
            }
            catch (Exception)
            {

            }

            return userId;

        }
    }

}
