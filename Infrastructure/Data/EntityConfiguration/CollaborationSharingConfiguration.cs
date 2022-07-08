// -----------------------------------------------------------------------
// <copyright file="CollaborationSharingConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CollaborationSharingConfiguration : IEntityTypeConfiguration<CollaborationSharing>
    {
        public void Configure(EntityTypeBuilder<CollaborationSharing> builder)
        {
            builder.ToTable("CollaborationSharing");

            builder.Property(ci => ci.CollaborationSharingID)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.CollaborationSharingIndex)
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("newid()");

            builder.HasIndex(b => b.CollaborationSharingIndex)
            .IsClustered(false);

            builder.HasOne(s => s.Agency)
                .WithMany()
                .HasForeignKey(d => d.AgencyID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Collaboration)
              .WithMany()
              .HasForeignKey(d => d.CollaborationID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.ReportingUnit)
              .WithMany()
              .HasForeignKey(d => d.ReportingUnitID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.IsSharing).IsRequired();
        }
    }
}
