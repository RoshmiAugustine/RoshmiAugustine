// -----------------------------------------------------------------------
// <copyright file="PersonRaceEthnicityConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class PersonRaceEthnicityConfiguration : IEntityTypeConfiguration<PersonRaceEthnicity>
    {
        public void Configure(EntityTypeBuilder<PersonRaceEthnicity> builder)
        {
            builder.ToTable("PersonRaceEthnicity");

            builder.Property(ci => ci.PersonRaceEthnicityID)
                .ValueGeneratedOnAdd();

            builder.HasOne(d => d.Person)
                .WithMany()
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.RaceEthnicity)
                .WithMany()
                .HasForeignKey(d => d.RaceEthnicityID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
