using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Persistence.Configurations;

public class BarcodeCardConfiguration : IEntityTypeConfiguration<BarcodeCard>
{
    public void Configure(EntityTypeBuilder<BarcodeCard> builder)
    {
        builder.ToTable("barcode_cards");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CardNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.CardNumber)
            .IsUnique();

        builder.Property(x => x.BarcodeValue)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.BarcodeValue)
            .IsUnique();

        builder.Property(x => x.PrintedCount)
            .HasDefaultValue(0);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(x => x.Student)
            .WithOne(x => x.BarcodeCard)
            .HasForeignKey<BarcodeCard>(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Attendances)
            .WithOne(x => x.BarcodeCard)
            .HasForeignKey(x => x.BarcodeCardId)
            .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(x => x.StudentId).IsUnique();


    }
}