// -----------------------------------------------------------------------
// <copyright file="CollaborationTagConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CollaborationTagConfiguration : IEntityTypeConfiguration<CollaborationTag>
    {
        public void Configure(EntityTypeBuilder<CollaborationTag> builder)
        {
            builder.ToTable("CollaborationTag");

            builder.Property(ci => ci.CollaborationTagID)
                .ValueGeneratedOnAdd();

            builder.HasOne(s => s.Collaboration)
              .WithMany()
              .HasForeignKey(d => d.CollaborationID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.CollaborationTagType)
              .WithMany()
              .HasForeignKey(d => d.CollaborationTagTypeID)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
