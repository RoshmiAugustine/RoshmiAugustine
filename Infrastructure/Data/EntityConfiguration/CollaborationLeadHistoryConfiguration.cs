// -----------------------------------------------------------------------
// <copyright file="CollaborationLeadHistoryConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CollaborationLeadHistoryConfiguration : IEntityTypeConfiguration<CollaborationLeadHistory>
    {
        public void Configure(EntityTypeBuilder<CollaborationLeadHistory> builder)
        {
            builder.ToTable("CollaborationLeadHistory");

            builder.Property(ci => ci.CollaborationLeadHistoryID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.IsRemoved).IsRequired();


            builder.HasOne(s => s.Collaboration)
              .WithMany()
              .HasForeignKey(d => d.CollaborationID)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
