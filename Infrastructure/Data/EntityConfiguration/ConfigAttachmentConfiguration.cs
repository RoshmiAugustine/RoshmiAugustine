// -----------------------------------------------------------------------
// <copyright file="ConfigAttachmentConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data
{
    public class ConfigAttachmentConfiguration : IEntityTypeConfiguration<ConfigurationAttachment>
    {
        public void Configure(EntityTypeBuilder<ConfigurationAttachment> builder)
        {
            builder.ToTable("ConfigurationAttachment", "info");

            builder.HasKey(ci => ci.ConfigurationAttachmentID);

            builder.Property(ci => ci.ConfigurationAttachmentID)
                .ValueGeneratedOnAdd();
        }
    }
}