// -----------------------------------------------------------------------
// <copyright file="AgencySharingConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AgencySharingConfiguration : IEntityTypeConfiguration<AgencySharing>
    {
        public void Configure(EntityTypeBuilder<AgencySharing> builder)
        {
            builder.ToTable("AgencySharing");

            builder.HasKey(ci => ci.AgencySharingID);

            builder.Property(ci => ci.AgencySharingID)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.AgencySharingIndex)
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("newid()");

            builder.HasIndex(b => b.AgencySharingIndex)
            .IsClustered(false);

            builder.Property(ci => ci.ReportingUnitID)
                .IsRequired();

            builder.Property(ci => ci.AgencyID)
                .IsRequired();

            builder.Property(ci => ci.AgencySharingPolicyID);

            builder.Property(ci => ci.HistoricalView)
                .IsRequired();

            builder.Property(ci => ci.StartDate);

            builder.Property(ci => ci.EndDate);

            builder.Property(ci => ci.IsSharing).IsRequired();
        }
    }
}
