// -----------------------------------------------------------------------
// <copyright file="NotificationTemplateConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;


namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
    {
        public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
        {
            builder.ToTable("NotificationTemplate");

            builder.Property(ci => ci.NotificationTemplateID)
                .ValueGeneratedOnAdd();

            builder.HasOne(ci => ci.NotificationLevel)
                .WithMany()
                .HasForeignKey(f => f.NotificationLevelID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.NotificationMode)
                .WithMany()
                .HasForeignKey(f => f.NotificationModeID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
