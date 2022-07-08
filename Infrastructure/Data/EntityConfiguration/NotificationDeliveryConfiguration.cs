// -----------------------------------------------------------------------
// <copyright file="NotificationDeliveryConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class NotificationDeliveryConfiguration : IEntityTypeConfiguration<NotificationDelivery>
    {
        public void Configure(EntityTypeBuilder<NotificationDelivery> builder)
        {
            builder.ToTable("NotificationDelivery");

            builder.Property(ci => ci.NotificationDeliveryID)
                .ValueGeneratedOnAdd();

            builder.HasOne(ci => ci.NotificationLog)
               .WithMany()
               .HasForeignKey(f => f.NotificationLogID)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.NotificationTemplate)
               .WithMany()
               .HasForeignKey(f => f.NotificationTemplateID)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
