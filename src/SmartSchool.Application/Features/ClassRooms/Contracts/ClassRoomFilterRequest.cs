namespace SmartSchool.Application.Features.ClassRooms.Contracts;

public class ClassRoomFilterRequest
{
    public string? Search { get; set; }

    public int? Grade { get; set; }

    public string? AcademicYear { get; set; }

    public bool? IsActive { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}