// -----------------------------------------------------------------------
// <copyright file="GenderConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    /// <summary>
    /// Defines the <see cref="GenderConfiguration" />.
    /// </summary>
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{Gender}"/>.</param>
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Gender", "info");

            builder.Property(ci => ci.GenderID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
            .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.Agency)
                .WithMany()
                .HasForeignKey(d => d.AgencyID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}