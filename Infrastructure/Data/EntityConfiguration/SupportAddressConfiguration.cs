// -----------------------------------------------------------------------
// <copyright file="SupportAddressConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class SupportAddressConfiguration : IEntityTypeConfiguration<SupportAddress>
    {
        public void Configure(EntityTypeBuilder<SupportAddress> builder)
        {
            builder.ToTable("SupportAddress");
            builder.Property(ci => ci.SupportAddressID)
                 .ValueGeneratedOnAdd();

            builder.HasOne(d => d.Address)
                   .WithMany()
                   .HasForeignKey(d => d.AddressID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Support)
                   .WithMany()
                   .HasForeignKey(d => d.SupportID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
