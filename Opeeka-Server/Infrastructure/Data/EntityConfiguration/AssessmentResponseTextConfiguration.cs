// -----------------------------------------------------------------------
// <copyright file="AssessmentResponseTextConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class AssessmentResponseTextConfiguration : IEntityTypeConfiguration<AssessmentResponseText>
    {
        public void Configure(EntityTypeBuilder<AssessmentResponseText> builder)
        {
            builder.ToTable("AssessmentResponseText");

            builder.Property(ci => ci.AssessmentResponseTextID)
               .ValueGeneratedOnAdd();

            builder.Property(e => e.ResponseText).IsUnicode(false);

            builder.HasOne(d => d.AssessmentResponse)
                   .WithMany()
                   .HasForeignKey(d => d.AssessmentResponseID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
