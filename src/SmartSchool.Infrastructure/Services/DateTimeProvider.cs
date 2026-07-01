using SmartSchool.Application.Common.Interfaces;

namespace SmartSchool.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}