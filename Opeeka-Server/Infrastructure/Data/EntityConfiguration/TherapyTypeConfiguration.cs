// -----------------------------------------------------------------------
// <copyright file="TherapyTypeConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class TherapyTypeConfiguration : IEntityTypeConfiguration<TherapyType>
    {
        public void Configure(EntityTypeBuilder<TherapyType> builder)
        {
            builder.ToTable("TherapyType", "info");

            builder.Property(ci => ci.TherapyTypeID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate).IsRequired()
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
