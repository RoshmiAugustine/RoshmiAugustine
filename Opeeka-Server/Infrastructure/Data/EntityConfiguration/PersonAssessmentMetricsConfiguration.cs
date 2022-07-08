using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class PersonAssessmentMetricsConfiguration : IEntityTypeConfiguration<PersonAssessmentMetrics>
    {
        public void Configure(EntityTypeBuilder<PersonAssessmentMetrics> builder)
        {
            builder.ToTable("PersonAssessmentMetrics", "dbo");

            builder.Property(ci => ci.PersonAssessmentMetricsID)
                 .ValueGeneratedOnAdd();

            builder.HasOne(d => d.Assessment)
                .WithMany()
                .HasForeignKey(d => d.AssessmentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}
