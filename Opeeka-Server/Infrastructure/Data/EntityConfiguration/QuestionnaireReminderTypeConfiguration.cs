// -----------------------------------------------------------------------
// <copyright file="QuestionnaireReminderTypeConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class QuestionnaireReminderTypeConfiguration : IEntityTypeConfiguration<QuestionnaireReminderType>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireReminderType> builder)
        {
            builder.ToTable("QuestionnaireReminderType", "info");

            builder.Property(ci => ci.QuestionnaireReminderTypeID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
             .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.NotificationLevel)
                   .WithMany()
                   .HasForeignKey(d => d.NotificationLevelID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                   .WithMany()
                   .HasForeignKey(d => d.UpdateUserID)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
