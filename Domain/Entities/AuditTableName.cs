// -----------------------------------------------------------------------
// <copyright file="AuditTableName.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class AuditTableName : BaseEntity
    {
        public string TableName { get; set; }
        public string Label { get; set; }
    }
}
