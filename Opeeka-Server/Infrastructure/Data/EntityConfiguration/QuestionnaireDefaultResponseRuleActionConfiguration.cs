// -----------------------------------------------------------------------
// <copyright file="QuestionnaireDefaultResponseRuleActionConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class QuestionnaireDefaultResponseRuleActionConfiguration : IEntityTypeConfiguration<QuestionnaireDefaultResponseRuleAction>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireDefaultResponseRuleAction> builder)
        {
            builder.ToTable("QuestionnaireDefaultResponseRuleAction");

            builder.Property(ci => ci.QuestionnaireDefaultResponseRuleActionID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.QuestionnaireItem)
                   .WithMany()
                   .HasForeignKey(d => d.QuestionnaireItemID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.QuestionnaireDefaultResponseRule)
                   .WithMany()
                   .HasForeignKey(d => d.QuestionnaireDefaultResponseRuleID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.DefaultResponse)
                 .WithMany()
                 .HasForeignKey(d => d.DefaultResponseID)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                   .WithMany()
                   .HasForeignKey(d => d.UpdateUserID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}