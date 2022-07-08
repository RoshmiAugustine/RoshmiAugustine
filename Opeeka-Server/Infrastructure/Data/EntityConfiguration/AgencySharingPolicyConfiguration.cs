// -----------------------------------------------------------------------
// <copyright file="AgencySharingPolicyConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AgencySharingPolicyConfiguration : IEntityTypeConfiguration<AgencySharingPolicy>
    {
        public void Configure(EntityTypeBuilder<AgencySharingPolicy> builder)
        {
            builder.ToTable("AgencySharingPolicy");

            builder.HasKey(ci => ci.AgencySharingPolicyID);

            builder.Property(ci => ci.AgencySharingPolicyID)
                .ValueGeneratedOnAdd();
        }
    }
}
