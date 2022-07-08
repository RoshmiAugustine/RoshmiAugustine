// -----------------------------------------------------------------------
// <copyright file="InstrumentAgencyConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class InstrumentAgencyConfiguration : IEntityTypeConfiguration<InstrumentAgency>
    {
        public void Configure(EntityTypeBuilder<InstrumentAgency> builder)
        {
            builder.ToTable("InstrumentAgency");

            builder.Property(ci => ci.InstrumentAgencyID)
                 .ValueGeneratedOnAdd();

            builder.HasOne(d => d.Agency)
                    .WithMany()
                    .HasForeignKey(d => d.AgencyID)
                    .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(d => d.Instrument)
                    .WithMany()
                    .HasForeignKey(d => d.InstrumentID)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
