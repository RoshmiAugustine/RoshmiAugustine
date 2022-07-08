// -----------------------------------------------------------------------
// <copyright file="QuestionnaireConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class QuestionnaireConfiguration : IEntityTypeConfiguration<Questionnaire>
    {
        public void Configure(EntityTypeBuilder<Questionnaire> builder)
        {
            builder.ToTable("Questionnaire");

            builder.Property(ci => ci.QuestionnaireID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.RequiredConfidentialityLanguage).HasDefaultValue("Confidential").IsRequired();

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");
            builder.Property(ci => ci.InviteToCompleteReceiverIds);

            builder.HasOne(d => d.Agency)
                    .WithMany()
                    .HasForeignKey(d => d.AgencyID)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Instrument)
                    .WithMany()
                    .HasForeignKey(d => d.InstrumentID)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ParentQuestionnaire)
                   .WithMany()
                   .HasForeignKey(d => d.ParentQuestionnaireID);

            builder.HasOne(d => d.UpdateUser)
                    .WithMany()
                    .HasForeignKey(d => d.UpdateUserID)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }    
}
