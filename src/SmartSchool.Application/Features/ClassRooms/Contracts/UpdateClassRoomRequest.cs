namespace SmartSchool.Application.Features.ClassRooms.Contracts;

public class UpdateClassRoomRequest
{
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int Grade { get; set; }

    public string AcademicYear { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}