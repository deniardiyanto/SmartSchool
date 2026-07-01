using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Persistence.Configurations;

public class SchoolSettingConfiguration : IEntityTypeConfiguration<SchoolSetting>
{
    public void Configure(EntityTypeBuilder<SchoolSetting> builder)
    {
        builder.ToTable("school_settings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SchoolName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.SchoolAddress)
            .HasMaxLength(255);

        builder.Property(x => x.SchoolPhone)
            .HasMaxLength(20);

        builder.Property(x => x.PrincipalName)
            .HasMaxLength(100);
    }
}