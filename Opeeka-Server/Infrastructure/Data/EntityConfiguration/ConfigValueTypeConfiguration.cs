// -----------------------------------------------------------------------
// <copyright file="ConfigValueTypeConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data
{
    public class ConfigValueTypeConfiguration : IEntityTypeConfiguration<ConfigurationValueType>
    {
        public void Configure(EntityTypeBuilder<ConfigurationValueType> builder)
        {
            builder.ToTable("ConfigurationValueType", "info");
            builder.HasKey(ci => ci.ConfigurationValueTypeID);
            builder.Property(ci => ci.ConfigurationValueTypeID)
                .ValueGeneratedOnAdd();
        }
    }
}
