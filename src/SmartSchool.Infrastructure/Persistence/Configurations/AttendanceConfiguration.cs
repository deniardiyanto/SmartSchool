using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Persistence.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.ToTable("attendances");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AttendanceType)
            .HasConversion<int>();

        builder.Property(x => x.Status)
            .HasConversion<int>();

        builder.Property(x => x.LateMinutes)
            .HasDefaultValue(0);

        builder.Property(x => x.PointDeduction)
            .HasDefaultValue(0);

        builder.Property(x => x.Notes)
            .HasMaxLength(300);

        builder.HasIndex(x => new
        {
            x.StudentId,
            x.AttendanceDate,
            x.AttendanceType
        });

        builder.HasOne(x => x.Student)
            .WithMany(x => x.Attendances)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.BarcodeCard)
            .WithMany(x => x.Attendances)
            .HasForeignKey(x => x.BarcodeCardId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AttendanceRule)
            .WithMany(x => x.Attendances)
            .HasForeignKey(x => x.AttendanceRuleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}