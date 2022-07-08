// -----------------------------------------------------------------------
// <copyright file="CollaborationAgencyAddressConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CollaborationAgencyAddressConfiguration : IEntityTypeConfiguration<CollaborationAgencyAddress>
    {
        public void Configure(EntityTypeBuilder<CollaborationAgencyAddress> builder)
        {
            builder.ToTable("CollaborationAgencyAddress");

            builder.HasKey(e => new { e.CollaborationID, e.AddressID });

            builder.HasOne(d => d.Address)
                    .WithMany()
                    .HasForeignKey(d => d.AddressID)
                    .OnDelete(DeleteBehavior.Restrict); ;

            builder.HasOne(d => d.Collaboration)
                .WithMany()
                .HasForeignKey(d => d.CollaborationID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
