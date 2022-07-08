// -----------------------------------------------------------------------
// <copyright file="PersonNoteConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;


namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class VoiceTypeConfiguration : IEntityTypeConfiguration<VoiceType>
    {
        public void Configure(EntityTypeBuilder<VoiceType> builder)
        {
            builder.ToTable("VoiceType", "info");

            builder.Property(ci => ci.VoiceTypeID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
              .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.UpdateUser)
                  .WithMany()
                  .HasForeignKey(d => d.UpdateUserID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
