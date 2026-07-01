using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Persistence.Configurations;

public class WhatsAppLogConfiguration : IEntityTypeConfiguration<WhatsAppLog>
{
    public void Configure(EntityTypeBuilder<WhatsAppLog> builder)
    {
        builder.ToTable("whatsapp_logs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.ProviderResponse)
            .HasMaxLength(500);

        builder.Property(x => x.Message)
            .HasMaxLength(1000)
            .IsRequired();

        builder.HasOne(x => x.Attendance)
            .WithMany(x => x.WhatsAppLogs)
            .HasForeignKey(x => x.AttendanceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}