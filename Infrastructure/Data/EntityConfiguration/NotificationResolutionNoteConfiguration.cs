// -----------------------------------------------------------------------
// <copyright file="NotificationResolutionNoteConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    /// <summary>
    /// Defines the <see cref="NotificationResolutionNoteConfiguration" />.
    /// </summary>
    public class NotificationResolutionNoteConfiguration : IEntityTypeConfiguration<NotificationResolutionNote>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{NotificationResolutionNote}"/>.</param>
        public void Configure(EntityTypeBuilder<NotificationResolutionNote> builder)
        {
            builder.ToTable("NotificationResolutionNote");

            builder.Property(ci => ci.NotificationResolutionNoteID)
                .ValueGeneratedOnAdd();

            builder.HasOne(ci => ci.NotificationLog)
                .WithMany()
                .HasForeignKey(f => f.NotificationLogID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.Note)
                .WithMany()
                .HasForeignKey(f => f.Note_NoteID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
