using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Persistence.Configurations;

public class AttendancePointConfiguration : IEntityTypeConfiguration<AttendancePoint>
{
    public void Configure(EntityTypeBuilder<AttendancePoint> builder)
    {
        builder.ToTable("attendance_points");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Reason)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne(x => x.Student)
            .WithMany(x => x.AttendancePoints)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Attendance)
            .WithMany(x => x.AttendancePoints)
            .HasForeignKey(x => x.AttendanceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}