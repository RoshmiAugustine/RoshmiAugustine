// -----------------------------------------------------------------------
// <copyright file="CollaborationSharingPolicyConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CollaborationSharingPolicyConfiguration : IEntityTypeConfiguration<CollaborationSharingPolicy>
    {
        public void Configure(EntityTypeBuilder<CollaborationSharingPolicy> builder)
        {
            builder.ToTable("CollaborationSharingPolicy");

            builder.Property(ci => ci.CollaborationSharingPolicyID)
                .ValueGeneratedOnAdd();
        }
    }
}
