using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class AgencyPowerBIReportConfigurations : IEntityTypeConfiguration<AgencyPowerBIReport>
    {

        public void Configure(EntityTypeBuilder<AgencyPowerBIReport> builder)
        {

            builder.ToTable("AgencyPowerBIReport");

            builder.Property(ci => ci.AgencyPowerBIReportId)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
             .HasDefaultValueSql("getdate()");

            builder.Property(ci => ci.ReportName).HasMaxLength(500);

            builder.HasOne(s => s.Agency)
              .WithMany()
              .HasForeignKey(d => d.AgencyId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Instrument)
              .WithMany()
              .HasForeignKey(d => d.InstrumentId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
