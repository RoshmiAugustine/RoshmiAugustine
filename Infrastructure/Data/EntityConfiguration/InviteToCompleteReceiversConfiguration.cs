using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class InviteToCompleteReceiversConfiguration : IEntityTypeConfiguration<InviteToCompleteReceiver>
    {
        public void Configure(EntityTypeBuilder<InviteToCompleteReceiver> builder)
        {
            builder.ToTable("InviteToCompleteReceiver", "info");

            builder.Property(ci => ci.InviteToCompleteReceiverID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name).IsRequired().HasMaxLength(15);

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");
        }
    }
}
