// -----------------------------------------------------------------------
// <copyright file="ColorPaletteConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;


namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class ColorPaletteConfiguration : IEntityTypeConfiguration<ColorPalette>
    {
        public void Configure(EntityTypeBuilder<ColorPalette> builder)
        {
            builder.ToTable("ColorPalette", "info");

            builder.HasKey(ci => ci.ColorPaletteID);

            builder.Property(ci => ci.ColorPaletteID)
                 .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name)
                 .IsRequired();

            builder.Property(ci => ci.RGB).IsRequired();

            builder.Property(ci => ci.ListOrder)
                 .IsRequired();

            builder.Property(ci => ci.IsRemoved)
                 .IsRequired();

            builder.Property(ci => ci.UpdateDate)
            .HasDefaultValueSql("getdate()");

            builder.Property(ci => ci.UpdateUserID)
                .IsRequired();

            builder.HasOne(s => s.UpdateUser)
             .WithMany()
             .HasForeignKey(d => d.UpdateUserID)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
