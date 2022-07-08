// -----------------------------------------------------------------------
// <copyright file="AuditFieldNameConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AuditFieldNameConfiguration : IEntityTypeConfiguration<AuditFieldName>
    {
        public void Configure(EntityTypeBuilder<AuditFieldName> builder)
        {
            builder.ToTable("AuditFieldName");

            builder.HasKey(ci => ci.TableName);
            builder.HasKey(ci => ci.FieldName);


            builder.Property(ci => ci.TableName)
              .IsRequired();

            builder.Property(ci => ci.FieldName)
                .IsRequired();

            builder.Property(ci => ci.Label)
                .IsRequired();
        }
    }
}