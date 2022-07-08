using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class FileImportConfiguration : IEntityTypeConfiguration<FileImport>
    {
        public void Configure(EntityTypeBuilder<FileImport> builder)
        {
            builder.ToTable("FileImport");

            builder.Property(ci => ci.FileImportID).ValueGeneratedOnAdd();

            builder.Property(ci => ci.FileJsonData);
            builder.Property(ci => ci.IsProcessed);
            builder.Property(ci => ci.CreatedDate).HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.Agency)
                .WithMany()
                .HasForeignKey(d => d.AgencyID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ImportType)
              .WithMany()
              .HasForeignKey(d => d.ImportTypeID)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
