// -----------------------------------------------------------------------
// <copyright file="AuditFieldName.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class AuditFieldName : BaseEntity
    {
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public string Label { get; set; }
    }
}
