// -----------------------------------------------------------------------
// <copyright file="UserProfileConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfile");

            builder.Property(ci => ci.UserProfileID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UserID)
                .IsRequired();

            builder.Property(ci => ci.ImageFileID)
               .IsRequired();
        }
    }
}
