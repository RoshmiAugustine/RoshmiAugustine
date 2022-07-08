// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskRuleConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class QuestionnaireSkipLogicRuleConfiguration : IEntityTypeConfiguration<QuestionnaireSkipLogicRule>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireSkipLogicRule> builder)
        {
            builder.ToTable("QuestionnaireSkipLogicRule");

            builder.Property(ci => ci.QuestionnaireSkipLogicRuleID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.Questionnaire)
                  .WithMany()
                  .HasForeignKey(d => d.QuestionnaireID)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                   .WithMany()
                   .HasForeignKey(d => d.UpdateUserID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
