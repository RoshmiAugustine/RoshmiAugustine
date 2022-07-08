// -----------------------------------------------------------------------
// <copyright file="CategoryFocusConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CategoryFocusConfiguration : IEntityTypeConfiguration<CategoryFocus>
    {
        public void Configure(EntityTypeBuilder<CategoryFocus> builder)
        {
            builder.ToTable("CategoryFocus", "info");

            builder.HasKey(ci => ci.CategoryFocusID);

            builder.Property(ci => ci.CategoryFocusID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name)
                .IsRequired();

            builder.Property(ci => ci.Abbrev);

            builder.Property(ci => ci.Description);

            builder.Property(ci => ci.ListOrder).IsRequired();

            builder.Property(ci => ci.IsRemoved)
               .IsRequired();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.Property(ci => ci.UpdateUserID)
                .IsRequired();

            builder.HasOne(d => d.UpdateUser)
                  .WithMany()
                  .HasForeignKey(d => d.UpdateUserID)
                  .OnDelete(DeleteBehavior.Restrict);

        }
    }
}