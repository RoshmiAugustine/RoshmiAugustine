// -----------------------------------------------------------------------
// <copyright file="ConfigContextConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data
{
    public class ConfigContextConfiguration : IEntityTypeConfiguration<ConfigurationContext>
    {
        public void Configure(EntityTypeBuilder<ConfigurationContext> builder)
        {

            builder.ToTable("ConfigurationContext", "info");
            builder.HasKey(ci => ci.ConfigurationContextID);

            builder.Property(ci => ci.ConfigurationContextID)
                .ValueGeneratedOnAdd();
        }
    }
}
