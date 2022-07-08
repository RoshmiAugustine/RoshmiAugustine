// -----------------------------------------------------------------------
// <copyright file="FileConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable("File");

            builder.Property(ci => ci.FileID).ValueGeneratedOnAdd();

            builder.Property(ci => ci.AzureID);

            builder.Property(ci => ci.AgencyID);

            builder.Property(ci => ci.FileName).HasMaxLength(1000);

            builder.Property(ci => ci.Path).HasMaxLength(1000);

            builder.Property(ci => ci.IsTemp);

        }
    }
}
