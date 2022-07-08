using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ReminderInviteToCompleteConfiguration : IEntityTypeConfiguration<ReminderInviteToComplete>
    {
        public void Configure(EntityTypeBuilder<ReminderInviteToComplete> builder)
        {
            builder.ToTable("ReminderInviteToComplete");

            builder.Property(ci => ci.ReminderInviteToCompleteID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Status)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(ci => ci.Email)
                .HasMaxLength(255);
            builder.Property(ci => ci.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");
            builder.Property(ci => ci.CreatedDate)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.Person)
                .WithMany()
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Helper)
                .WithMany()
                .HasForeignKey(d => d.HelperID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Assessment)
                .WithMany()
                .HasForeignKey(d => d.AssessmentID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
