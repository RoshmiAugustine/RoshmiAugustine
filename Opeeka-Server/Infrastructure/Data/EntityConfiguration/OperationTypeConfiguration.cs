// -----------------------------------------------------------------------
// <copyright file="OperationTypeConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class OperationTypeConfiguration : IEntityTypeConfiguration<OperationType>
    {
        public void Configure(EntityTypeBuilder<OperationType> builder)
        {

            builder.ToTable("OperationType", "info");

            builder.Property(ci => ci.OperationTypeID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.UpdateUser)
               .WithMany()
               .HasForeignKey(d => d.UpdateUserID)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}