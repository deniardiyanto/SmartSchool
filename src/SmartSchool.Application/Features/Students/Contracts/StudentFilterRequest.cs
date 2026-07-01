namespace SmartSchool.Application.Features.Students.Contracts;

public class StudentFilterRequest
{
    public string? Keyword { get; set; }

    public Guid? ClassRoomId { get; set; }

    public Guid? GuardianId { get; set; }

    public bool? IsActive { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}