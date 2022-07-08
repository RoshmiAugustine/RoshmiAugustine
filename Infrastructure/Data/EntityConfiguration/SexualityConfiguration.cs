// -----------------------------------------------------------------------
// <copyright file="SexualityConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    /// <summary>
    /// Defines the <see cref="SexualityConfiguration" />.
    /// </summary>
    public class SexualityConfiguration : IEntityTypeConfiguration<Sexuality>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{Sexuality}"/>.</param>
        public void Configure(EntityTypeBuilder<Sexuality> builder)
        {
            builder.ToTable("Sexuality", "info");

            builder.Property(ci => ci.SexualityID)
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