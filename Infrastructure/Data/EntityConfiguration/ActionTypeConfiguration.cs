// -----------------------------------------------------------------------
// <copyright file="ActionTypeConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ActionTypeConfiguration : IEntityTypeConfiguration<ActionType>
    {
        public void Configure(EntityTypeBuilder<ActionType> builder)
        {
            builder.ToTable("ActionType", "info");

            builder.HasKey(ci => ci.ActionTypeID);

            builder.Property(ci => ci.ActionTypeID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name)
                .IsRequired();
        }
    }
}