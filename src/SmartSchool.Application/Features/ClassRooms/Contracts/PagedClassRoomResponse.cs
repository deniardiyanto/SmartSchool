namespace SmartSchool.Application.Features.ClassRooms.Contracts;

public class PagedClassRoomResponse
{
    public IReadOnlyList<ClassRoomDto> Items { get; set; }
        = new List<ClassRoomDto>();

    public int TotalRecords { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalPages =>
        (int)Math.Ceiling((double)TotalRecords / PageSize);
}