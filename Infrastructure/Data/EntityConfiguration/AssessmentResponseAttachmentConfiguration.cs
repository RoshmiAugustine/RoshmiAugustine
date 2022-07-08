using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AssessmentResponseAttachmentConfiguration : IEntityTypeConfiguration<AssessmentResponseAttachment>
    {
        public void Configure(EntityTypeBuilder<AssessmentResponseAttachment> builder)
        {

            builder.ToTable("AssessmentResponseAttachment");

            builder.Property(ci => ci.AssessmentResponseAttachmentID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
             .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.AssessmentResponse)
                .WithMany()
                .HasForeignKey(d => d.AssessmentResponseID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
