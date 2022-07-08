// -----------------------------------------------------------------------
// <copyright file="ActionLevelConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ActionLevelConfiguration : IEntityTypeConfiguration<ActionLevel>
    {
        public void Configure(EntityTypeBuilder<ActionLevel> builder)
        {
            builder.ToTable("ActionLevel", "info");

            builder.HasKey(ci => ci.ActionLevelID);

            builder.Property(ci => ci.ActionLevelID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name)
                .IsRequired();
        }
    }
}