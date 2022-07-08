// -----------------------------------------------------------------------
// <copyright file="CategoryConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category", "info");

            builder.Property(ci => ci.CategoryID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.UpdateUser)
              .WithMany()
              .HasForeignKey(d => d.UpdateUserID)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
