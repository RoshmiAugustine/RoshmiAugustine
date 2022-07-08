// -----------------------------------------------------------------------
// <copyright file="HelperAddressConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class HelperAddressConfiguration : IEntityTypeConfiguration<HelperAddress>
    {
        public void Configure(EntityTypeBuilder<HelperAddress> builder)
        {
            builder.ToTable("HelperAddress");

            builder.Property(ci => ci.HelperAddressID)
                .ValueGeneratedOnAdd();

            //builder.HasKey(e => new { e.AddressID, e.HelperID });

            builder.Property(ci => ci.HelperAddressIndex)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID ( )");

            builder.HasIndex(ci => ci.HelperAddressIndex)
                .IsClustered(false);

            builder.HasOne(s => s.Address)
                .WithMany()
                .HasForeignKey(d => d.AddressID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Helper)
                .WithMany()
                .HasForeignKey(d => d.HelperID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
