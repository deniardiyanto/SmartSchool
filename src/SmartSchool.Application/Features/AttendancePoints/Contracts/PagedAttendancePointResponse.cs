namespace SmartSchool.Application.Features.AttendancePoints.Contracts;

public class PagedAttendancePointResponse
{
    public IReadOnlyList<AttendancePointDto> Items { get; set; }
        = new List<AttendancePointDto>();

    public int TotalRecords { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }
}