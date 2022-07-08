// -----------------------------------------------------------------------
// <copyright file="QuestionnaireDefaultResponseRuleConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class QuestionnaireDefaultResponseRuleConfiguration : IEntityTypeConfiguration<QuestionnaireDefaultResponseRule>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireDefaultResponseRule> builder)
        {
            builder.ToTable("QuestionnaireDefaultResponseRule");

            builder.Property(ci => ci.QuestionnaireDefaultResponseRuleID)
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
