// -----------------------------------------------------------------------
// <copyright file="AgencySharingHistoryConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;


namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AgencySharingHistoryConfiguration : IEntityTypeConfiguration<AgencySharingHistory>
    {
        public void Configure(EntityTypeBuilder<AgencySharingHistory> builder)
        {
            builder.ToTable("AgencySharingHistory");

            builder.Property(ci => ci.AgencySharingHistoryID)
                .ValueGeneratedOnAdd();

        }
    }
}
