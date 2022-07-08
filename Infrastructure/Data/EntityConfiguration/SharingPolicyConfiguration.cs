// -----------------------------------------------------------------------
// <copyright file="SharingPolicyConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class SharingPolicyConfiguration : IEntityTypeConfiguration<SharingPolicy>
    {
        public void Configure(EntityTypeBuilder<SharingPolicy> builder)
        {
            builder.ToTable("SharingPolicy", "info");

            builder.HasKey(ci => ci.SharingPolicyID);

            builder.Property(ci => ci.SharingPolicyID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.AccessName)
                .IsRequired();
        }
    }
}