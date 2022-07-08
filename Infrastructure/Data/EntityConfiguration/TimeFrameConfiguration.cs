// -----------------------------------------------------------------------
// <copyright file="TimeFrameConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;


namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class TimeFrameConfiguration : IEntityTypeConfiguration<TimeFrame>
    {
        public void Configure(EntityTypeBuilder<TimeFrame> builder)
        {
            builder.ToTable("TimeFrame", "info");

            builder.Property(o => o.TimeFrameID)
                .ValueGeneratedOnAdd();

            builder.Property(o => o.Month_Exact)
                .HasColumnType("decimal(18,10)")
                .IsRequired(true);

            builder.Property(o => o.Qrts_Exact)
                .HasColumnType("decimal(18,10)")
                .IsRequired(true);

            builder.Property(o => o.Yrs_Exact)
                .HasColumnType("decimal(18,10)")
                .IsRequired(true);
        }
    }
}
