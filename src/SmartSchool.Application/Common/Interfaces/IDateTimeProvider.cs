namespace SmartSchool.Application.Common.Interfaces;

public interface IDateTimeProvider
{
    DateTime Now { get; }

    DateTime UtcNow { get; }
}