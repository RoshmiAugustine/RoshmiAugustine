// -----------------------------------------------------------------------
// <copyright file="QuestionnaireItemConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class QuestionnaireItemConfiguration : IEntityTypeConfiguration<QuestionnaireItem>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireItem> builder)
        {
            builder.ToTable("QuestionnaireItem");

            builder.Property(ci => ci.QuestionnaireItemID)
                .ValueGeneratedOnAdd();
            builder.Property(ci => ci.QuestionnaireItemIndex)
               .ValueGeneratedOnAdd()
               .HasDefaultValueSql("NEWID ( )");

            builder.HasIndex(ci => ci.QuestionnaireItemIndex)
                .IsClustered(false);

            builder.Property(ci => ci.IsOptional).IsRequired().HasDefaultValue(false);
            builder.Property(ci => ci.CanOverrideLowerResponseBehavior).IsRequired().HasDefaultValue(true);
            builder.Property(ci => ci.CanOverrideMedianResponseBehavior).IsRequired().HasDefaultValue(true);

            builder.Property(ci => ci.CanOverrideUpperResponseBehavior).IsRequired().HasDefaultValue(true);

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.Category)
                   .WithMany()
                   .HasForeignKey(d => d.CategoryID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Item)
                   .WithMany()
                   .HasForeignKey(d => d.ItemID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.LowerItemResponseBehavior)
                   .WithMany()
                   .HasForeignKey(d => d.LowerItemResponseBehaviorID);

            builder.HasOne(d => d.MedianItemResponseBehavior)
                   .WithMany()
                   .HasForeignKey(d => d.MedianItemResponseBehaviorID);

            builder.HasOne(d => d.Questionnaire)
                   .WithMany()
                   .HasForeignKey(d => d.QuestionnaireID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpperItemResponseBehavior)
                  .WithMany()
                  .HasForeignKey(d => d.UpperItemResponseBehaviorID);
        }
    }
}
