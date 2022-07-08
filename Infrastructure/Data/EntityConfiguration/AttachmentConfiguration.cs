// -----------------------------------------------------------------------
// <copyright file="AttachmentConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("Attachment", "info");

            builder.HasKey(ci => ci.AttachmentId);

            builder.Property(ci => ci.AttachmentId)
                .ValueGeneratedOnAdd();
        }
    }
}