using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("students");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.NIS)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => x.NIS)
            .IsUnique();

        builder.Property(x => x.NISN)
            .HasMaxLength(20);

        builder.HasIndex(x => x.NISN)
            .IsUnique();

        builder.Property(x => x.FullName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.BirthPlace)
            .HasMaxLength(100);

        builder.Property(x => x.Address)
            .HasMaxLength(255);

        builder.Property(x => x.PhotoUrl)
            .HasMaxLength(255);

        builder.Property(x => x.Gender)
            .HasConversion<int>();

        builder.Property(x => x.Status)
            .HasConversion<int>();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(x => x.ClassRoom)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.ClassRoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Guardian)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.GuardianId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.BarcodeCard)
            .WithOne(x => x.Student)
            .HasForeignKey<BarcodeCard>(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}