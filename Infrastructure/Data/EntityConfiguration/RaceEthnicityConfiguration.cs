// -----------------------------------------------------------------------
// <copyright file="RaceEthnicityConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    /// <summary>
    /// Defines the <see cref="RaceEthnicityConfiguration" />.
    /// </summary>
    public class RaceEthnicityConfiguration : IEntityTypeConfiguration<RaceEthnicity>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{RaceEthnicity}"/>.</param>
        public void Configure(EntityTypeBuilder<RaceEthnicity> builder)
        {
            builder.ToTable("RaceEthnicity", "info");

            builder.Property(ci => ci.RaceEthnicityID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
            .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.UpdateUser)
                  .WithMany()
                  .HasForeignKey(d => d.UpdateUserID)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Agency)
                  .WithMany()
                  .HasForeignKey(d => d.AgencyID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}