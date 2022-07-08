// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireMetricsConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class PersonQuestionnaireMetricsConfiguration : IEntityTypeConfiguration<PersonQuestionnaireMetrics>
    {
        public void Configure(EntityTypeBuilder<PersonQuestionnaireMetrics> builder)
        {
            builder.ToTable("PersonQuestionnaireMetrics", "dbo");

            builder.Property(ci => ci.PersonQuestionnaireMetricsID)
                 .ValueGeneratedOnAdd();
        }
    }
}
