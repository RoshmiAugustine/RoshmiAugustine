// -----------------------------------------------------------------------
// <copyright file="QuestionnaireItemHistoryConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;


namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class QuestionnaireItemHistoryConfiguration : IEntityTypeConfiguration<QuestionnaireItemHistory>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireItemHistory> builder)
        {
            builder.ToTable("QuestionnaireItemHistory");


            builder.HasOne(s => s.QuestionnaireItem)
                .WithMany()
                .HasForeignKey(d => d.QuestionnaireItemID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
