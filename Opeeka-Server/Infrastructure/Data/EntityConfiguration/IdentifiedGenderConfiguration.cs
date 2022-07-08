// -----------------------------------------------------------------------
// <copyright file="IdentifiedGenderConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class IdentifiedGenderConfiguration : IEntityTypeConfiguration<IdentifiedGender>
    {
        public void Configure(EntityTypeBuilder<IdentifiedGender> builder)
        {
            builder.ToTable("IdentifiedGender", "info");

            builder.Property(ci => ci.IdentifiedGenderID)
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
