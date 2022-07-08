// -----------------------------------------------------------------------
// <copyright file="ConfigParameterConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data
{
    public class ConfigParameterConfiguration : IEntityTypeConfiguration<ConfigurationParameter>
    {
        public void Configure(EntityTypeBuilder<ConfigurationParameter> builder)
        {

            builder.ToTable("ConfigurationParameter", "info");

            builder.HasKey(ci => ci.ConfigurationParameterID);
            builder.Property(ci => ci.ConfigurationParameterID)
                .ValueGeneratedOnAdd();
            builder.Property(ci => ci.Name);
            builder.Property(ci => ci.Description);
            builder.Property(ci => ci.IsActive);
            builder.Property(ci => ci.CanModify);
            builder.Property(ci => ci.Deprecated);
        }
    }
}
