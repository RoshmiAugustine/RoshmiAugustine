// -----------------------------------------------------------------------
// <copyright file="AuditConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AuditDetailsConfiguration : IEntityTypeConfiguration<AuditDetails>
    {
        public void Configure(EntityTypeBuilder<AuditDetails> builder)
        {
            builder.ToTable("AuditDetails");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.DateTime)
                .HasDefaultValueSql("getdate()");

            builder.Property(ci => ci.TableName)
                .IsRequired();

        }
    }
}