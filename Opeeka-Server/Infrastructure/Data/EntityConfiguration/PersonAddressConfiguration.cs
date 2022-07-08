// -----------------------------------------------------------------------
// <copyright file="PersonAddressConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class PersonAddressConfiguration : IEntityTypeConfiguration<PersonAddress>
    {
        public void Configure(EntityTypeBuilder<PersonAddress> builder)
        {
            builder.ToTable("PersonAddress");

            builder.Property(ci => ci.PersonAddressID)
                .ValueGeneratedOnAdd();

            builder.HasOne(s => s.Address)
                .WithMany()
                .HasForeignKey(d => d.AddressID);

            builder.HasOne(s => s.Person)
                .WithMany()
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
