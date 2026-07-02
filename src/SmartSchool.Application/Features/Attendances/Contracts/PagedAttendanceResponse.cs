namespace SmartSchool.Application.Features.Attendances.Contracts;

public class PagedAttendanceResponse
{
    public IReadOnlyCollection<AttendanceDto> Items { get; set; }
        = new List<AttendanceDto>();

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }

    public int TotalPages { get; set; }
}