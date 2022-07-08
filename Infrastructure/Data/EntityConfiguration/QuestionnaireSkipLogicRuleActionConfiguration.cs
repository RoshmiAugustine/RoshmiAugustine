// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskRuleConditionConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class QuestionnaireSkipLogicRuleActionConfiguration : IEntityTypeConfiguration<QuestionnaireSkipLogicRuleAction>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireSkipLogicRuleAction> builder)
        {
            builder.ToTable("QuestionnaireSkipLogicRuleAction");

            builder.Property(ci => ci.QuestionnaireSkipLogicRuleActionID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.QuestionnaireItem)
                   .WithMany()
                   .HasForeignKey(d => d.QuestionnaireItemID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Category)
                   .WithMany()
                   .HasForeignKey(d => d.CategoryID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Item)
                   .WithMany()
                   .HasForeignKey(d => d.ChildItemID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ItemForParent)
                   .WithMany()
                   .HasForeignKey(d => d.ParentItemID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.QuestionnaireSkipLogicRule)
                   .WithMany()
                   .HasForeignKey(d => d.QuestionnaireSkipLogicRuleID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ActionType)
                   .WithMany()
                   .HasForeignKey(d => d.ActionTypeID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ActionLevel)
                   .WithMany()
                   .HasForeignKey(d => d.ActionLevelID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                   .WithMany()
                   .HasForeignKey(d => d.UpdateUserID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}