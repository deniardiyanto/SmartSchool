using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Persistence.Configurations;

public class AttendanceRuleConfiguration : IEntityTypeConfiguration<AttendanceRule>
{
    public void Configure(EntityTypeBuilder<AttendanceRule> builder)
    {
        builder.ToTable("attendance_rules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.RuleName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(300);

        builder.Property(x => x.LatePoint)
            .HasDefaultValue(-5);

        builder.Property(x => x.EnableWhatsapp)
            .HasDefaultValue(true);

        builder.Property(x => x.AllowMultipleScan)
            .HasDefaultValue(false);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);
    }
}