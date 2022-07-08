// -----------------------------------------------------------------------
// <copyright file="AgencyAddressConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AgencyAddressConfiguration : IEntityTypeConfiguration<AgencyAddress>
    {
        public void Configure(EntityTypeBuilder<AgencyAddress> builder)
        {
            builder.ToTable("AgencyAddress");

            builder.Property(ci => ci.AgencyAddressID)
                .ValueGeneratedOnAdd();

            builder.HasOne(s => s.Address)
               .WithMany()
               .HasForeignKey(d => d.AddressID);

            builder.HasOne(s => s.Agency)
               .WithMany()
               .HasForeignKey(d => d.AgencyID)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.UpdateUser)
               .WithMany()
               .HasForeignKey(d => d.UpdateUserID)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}