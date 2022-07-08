// -----------------------------------------------------------------------
// <copyright file="ConfigConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data
{
    public class ConfigConfiguration : IEntityTypeConfiguration<Configuration>
    {
        public void Configure(EntityTypeBuilder<Configuration> builder)
        {

            builder.ToTable("Configuration", "info");
            builder.HasKey(ci => ci.ConfigurationID);

            builder.Property(ci => ci.ConfigurationID)
                .ValueGeneratedOnAdd();
        }
    }
}
