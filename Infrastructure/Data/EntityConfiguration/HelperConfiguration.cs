// -----------------------------------------------------------------------
// <copyright file="HelperConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class HelperConfiguration : IEntityTypeConfiguration<Helper>
    {
        public void Configure(EntityTypeBuilder<Helper> builder)
        {
            builder.ToTable("Helper");

            builder.Property(ci => ci.HelperID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.HelperIndex)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID ( )");

            builder.HasIndex(ci => ci.HelperIndex)
                .IsClustered(false);

            builder.Property(ci => ci.FirstName)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(ci => ci.MiddleName)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(ci => ci.LastName)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(ci => ci.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false); ;

            builder.Property(ci => ci.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.Property(ci => ci.Phone2)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(s => s.Agency)
                .WithMany()
                .HasForeignKey(d => d.AgencyID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.SupervisorHelper)
                .WithMany()
                .HasForeignKey(d => d.SupervisorHelperID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Reviewer)
                .WithMany()
                .HasForeignKey(d => d.ReviewerID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.HelperTitle)
                .WithMany()
                .HasForeignKey(d => d.HelperTitleID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(d => d.StartDate)
                .IsRequired();

            builder.Property(d => d.EndDate);
        }
    }
}
