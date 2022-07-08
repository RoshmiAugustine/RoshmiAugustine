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
    public class QuestionnaireDefaultResponseRuleConditionConfiguration : IEntityTypeConfiguration<QuestionnaireDefaultResponseRuleCondition>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireDefaultResponseRuleCondition> builder)
        {
            builder.ToTable("QuestionnaireDefaultResponseRuleCondition");

            builder.Property(ci => ci.QuestionnaireDefaultResponseRuleConditionID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.Questionnaire)
                   .WithMany()
                   .HasForeignKey(d => d.QuestionnaireID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.QuestionnaireItem)
                   .WithMany()
                   .HasForeignKey(d => d.QuestionnaireItemID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.QuestionnaireDefaultResponseRule)
                   .WithMany()
                   .HasForeignKey(d => d.QuestionnaireDefaultResponseRuleID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                   .WithMany()
                   .HasForeignKey(d => d.UpdateUserID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.ComparisonValue).HasColumnType("decimal(16,2)");
        }
    }
}