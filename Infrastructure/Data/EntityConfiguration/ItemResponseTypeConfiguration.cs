// -----------------------------------------------------------------------
// <copyright file="ItemResponseTypeConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class ItemResponseTypeConfiguration : IEntityTypeConfiguration<ItemResponseType>
    {
        public void Configure(EntityTypeBuilder<ItemResponseType> builder)
        {
            builder.ToTable("ItemResponseType", "info");

            builder.Property(ci => ci.ItemResponseTypeID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.UpdateUser)
               .WithMany()
               .HasForeignKey(d => d.UpdateUserID)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}