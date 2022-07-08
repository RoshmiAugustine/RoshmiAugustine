// -----------------------------------------------------------------------
// <copyright file="AuditTableNameConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AuditTableNameConfiguration : IEntityTypeConfiguration<AuditTableName>
    {
        public void Configure(EntityTypeBuilder<AuditTableName> builder)
        {
            builder.ToTable("AuditTableName");

            builder.HasKey(ci => ci.TableName);


            builder.Property(ci => ci.TableName)
              .IsRequired();

            builder.Property(ci => ci.Label)
                .IsRequired();
        }
    }
}