// -----------------------------------------------------------------------
// <copyright file="NotifyReminderConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    /// <summary>
    /// Defines the <see cref="NotifyReminderConfiguration" />.
    /// </summary>
    public class BackgroundProcessLogConfiguration : IEntityTypeConfiguration<BackgroundProcessLog>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{NotifyReminder}"/>.</param>
        public void Configure(EntityTypeBuilder<BackgroundProcessLog> builder)
        {
            builder.ToTable("BackgroundProcessLog");

            builder.Property(ci => ci.BackgroundProcessLogID)
                .ValueGeneratedOnAdd();
        }
    }
}
