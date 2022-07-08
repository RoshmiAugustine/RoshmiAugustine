// -----------------------------------------------------------------------
// <copyright file="HelperTitleConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class HelperTitleConfiguration : IEntityTypeConfiguration<HelperTitle>
    {
        public void Configure(EntityTypeBuilder<HelperTitle> builder)
        {
            builder.ToTable("HelperTitle", "info");

            builder.Property(ci => ci.HelperTitleID)
                 .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Abbrev)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(ci => ci.UpdateDate)
                 .HasDefaultValueSql("getdate()");

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
