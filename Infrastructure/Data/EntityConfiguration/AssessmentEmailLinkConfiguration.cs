// -----------------------------------------------------------------------
// <copyright file="AssessmentEmailLinkConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class AssessmentEmailLinkConfiguration : IEntityTypeConfiguration<AssessmentEmailLinkDetails>
    {
        public void Configure(EntityTypeBuilder<AssessmentEmailLinkDetails> builder)
        {
            builder.ToTable("AssessmentEmailLinkDetails");

            builder.HasKey(e => e.AssessmentEmailLinkDetailsID)
                    .HasName("AssessmentEmailLinkDetailsID");

            builder.Property(ci => ci.AssessmentEmailLinkDetailsID)
               .ValueGeneratedOnAdd();

            builder.Property(b => b.EmailLinkDetailsIndex)
           .ValueGeneratedOnAdd()
           .HasDefaultValueSql("newid()");

            builder.Property(ci => ci.UpdateDate)
             .HasDefaultValueSql("getdate()");

            builder.Property(ci => ci.PhoneNumber).HasDefaultValue(null);
        }
    }
}
