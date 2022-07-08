// -----------------------------------------------------------------------
// <copyright file="CollaborationSharingHistoryConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CollaborationSharingHistoryConfiguration : IEntityTypeConfiguration<CollaborationSharingHistory>
    {
        public void Configure(EntityTypeBuilder<CollaborationSharingHistory> builder)
        {
            builder.ToTable("CollaborationSharingHistory");

            builder.Property(ci => ci.CollaborationSharingHistoryID)
                .ValueGeneratedOnAdd();

        }
    }
}
