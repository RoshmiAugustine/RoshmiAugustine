using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class RecurrenceEndTypeConfiguration : IEntityTypeConfiguration<RecurrenceEndType>
    {
        public void Configure(EntityTypeBuilder<RecurrenceEndType> builder)
        {
            builder.ToTable("RecurrenceEndType", "info");

            builder.Property(ci => ci.RecurrenceEndTypeID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name).IsRequired().HasMaxLength(20);

            builder.Property(ci => ci.DisplayLabel).IsRequired().HasMaxLength(15);

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");
        }
    }

    public class RecurrencePatternConfiguration : IEntityTypeConfiguration<RecurrencePattern>
    {
        public void Configure(EntityTypeBuilder<RecurrencePattern> builder)
        {
            builder.ToTable("RecurrencePattern", "info");

            builder.Property(ci => ci.RecurrencePatternID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name).IsRequired().HasMaxLength(20);

            builder.Property(ci => ci.GroupName).IsRequired().HasMaxLength(15);

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");
        }
    }

    public class RecurrenceOrdinalConfiguration : IEntityTypeConfiguration<RecurrenceOrdinal>
    {
        public void Configure(EntityTypeBuilder<RecurrenceOrdinal> builder)
        {
            builder.ToTable("RecurrenceOrdinal", "info");

            builder.Property(ci => ci.RecurrenceOrdinalID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name).IsRequired().HasMaxLength(15);

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");
        }
    }

    public class RecurrenceDayConfiguration : IEntityTypeConfiguration<RecurrenceDay>
    {
        public void Configure(EntityTypeBuilder<RecurrenceDay> builder)
        {
            builder.ToTable("RecurrenceDay", "info");

            builder.Property(ci => ci.RecurrenceDayID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name).IsRequired().HasMaxLength(15);

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");
        }
    }

    public class RecurrenceMonthConfiguration : IEntityTypeConfiguration<RecurrenceMonth>
    {
        public void Configure(EntityTypeBuilder<RecurrenceMonth> builder)
        {
            builder.ToTable("RecurrenceMonth", "info");

            builder.Property(ci => ci.RecurrenceMonthID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name).IsRequired().HasMaxLength(15);

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");
        }
    }

    public class OffsetTypeConfiguration : IEntityTypeConfiguration<OffsetType>
    {
        public void Configure(EntityTypeBuilder<OffsetType> builder)
        {
            builder.ToTable("OffsetType", "info");

            builder.Property(ci => ci.OffsetTypeID)
                .HasColumnType("char(1)")
                .HasMaxLength(1)
                .HasDefaultValue('d');

            builder.Property(ci => ci.Name).IsRequired().HasMaxLength(15);

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");
        }
    }

    public class TimeZonesConfiguration : IEntityTypeConfiguration<TimeZones>
    {
        public void Configure(EntityTypeBuilder<TimeZones> builder)
        {
            builder.ToTable("TimeZones", "info");

            builder.Property(ci => ci.TimeZonesID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name).IsRequired().HasMaxLength(100);

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");
        }
    }
}
