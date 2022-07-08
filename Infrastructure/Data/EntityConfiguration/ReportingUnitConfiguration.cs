// -----------------------------------------------------------------------
// <copyright file="ReportingUnitConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ReportingUnitConfiguration : IEntityTypeConfiguration<ReportingUnit>
    {
        public void Configure(EntityTypeBuilder<ReportingUnit> builder)
        {
            builder.ToTable("ReportingUnit");

            builder.HasKey(ci => ci.ReportingUnitID);

            builder.Property(ci => ci.ReportingUnitID)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.ReportingUnitIndex)
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("newid()");

            builder.HasIndex(b => b.ReportingUnitIndex)
            .IsClustered(false);

            builder.Property(ci => ci.ReportingUnitID)
                .IsRequired();

            builder.Property(ci => ci.Name).IsRequired();

            builder.Property(ci => ci.Abbrev);

            builder.Property(ci => ci.Description);

            builder.Property(ci => ci.ParentAgencyID).IsRequired();

            builder.Property(ci => ci.IsRemoved).IsRequired();

            builder.Property(ci => ci.UpdateUserID).IsRequired();

            builder.Property(ci => ci.UpdateDate).IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.Property(ci => ci.StartDate)
                .IsRequired();

            builder.Property(ci => ci.EndDate);

            builder.HasOne(d => d.UpdateUser)
                  .WithMany()
                  .HasForeignKey(d => d.UpdateUserID)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ParentAgency)
                  .WithMany()
                  .HasForeignKey(d => d.ParentAgencyID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
