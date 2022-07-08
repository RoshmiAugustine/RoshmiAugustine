// -----------------------------------------------------------------------
// <copyright file="ItemConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Item");

            builder.Property(ci => ci.ItemID)
                .ValueGeneratedOnAdd();
            builder.Property(ci => ci.ItemIndex)
               .ValueGeneratedOnAdd()
               .HasDefaultValueSql("NEWID ( )");

            builder.HasIndex(ci => ci.ItemIndex)
                .IsClustered(false);

            builder.Property(ci => ci.UseRequiredConfidentiality)
                .IsRequired()
                .HasDefaultValue(false);
            builder.Property(ci => ci.UsePersonRequestedConfidentiality)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(ci => ci.UseOtherConfidentiality)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(ci => ci.IsExpandable)
                .HasDefaultValue(true);

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.ItemResponseType)
                .WithMany()
                .HasForeignKey(d => d.ItemResponseTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ResponseValueType)
                .WithMany()
                .HasForeignKey(d => d.ResponseValueTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.DefaultResponse)
                .WithMany()
                .HasForeignKey(d => d.DefaultResponseID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.ResponseRequired)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(ci => ci.ShowNotes)
                .IsRequired()
                .HasDefaultValue(true);
            builder.Property(ci => ci.GridLayoutInFormView)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
