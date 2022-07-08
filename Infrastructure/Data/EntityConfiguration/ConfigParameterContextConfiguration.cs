// -----------------------------------------------------------------------
// <copyright file="ConfigParameterContextConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data
{
    public class ConfigParameterContextConfiguration : IEntityTypeConfiguration<ConfigurationParameterContext>
    {
        public void Configure(EntityTypeBuilder<ConfigurationParameterContext> builder)
        {

            builder.ToTable("ConfigurationParameterContext", "info");
            builder.HasKey(ci => ci.ConfigurationParameterContextID);
            builder.Property(ci => ci.ConfigurationParameterContextID)
                .ValueGeneratedOnAdd();
        }
    }
}
