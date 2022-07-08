// -----------------------------------------------------------------------
// <copyright file="HelperContactConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;
namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ItemResponseBehaviorConfiguration : IEntityTypeConfiguration<ItemResponseBehavior>
    {
        public void Configure(EntityTypeBuilder<ItemResponseBehavior> builder)
        {
            builder.ToTable("ItemResponseBehavior", "info");

            builder.Property(ci => ci.ItemResponseBehaviorID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.UpdateUser)
                 .WithMany()
                 .HasForeignKey(d => d.UpdateUserID)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ItemResponseType)
               .WithMany()
               .HasForeignKey(d => d.ItemResponseTypeID)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
