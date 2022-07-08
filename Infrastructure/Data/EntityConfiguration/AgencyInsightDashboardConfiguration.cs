using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AgencyInsightDashboardConfiguration : IEntityTypeConfiguration<AgencyInsightDashboard>
    {
        public void Configure(EntityTypeBuilder<AgencyInsightDashboard> builder)
        {
            builder.ToTable("AgencyInsightDashboard");

            builder.Property(ci => ci.AgencyInsightDashboardId)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
             .HasDefaultValueSql("getdate()");

            builder.Property(ci => ci.Name).HasMaxLength(500);
            builder.Property(ci => ci.IconURL).HasMaxLength(500);
            builder.Property(ci => ci.ShortDescription).HasMaxLength(500);

            builder.HasOne(s => s.Agency)
              .WithMany()
              .HasForeignKey(d => d.AgencyId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
