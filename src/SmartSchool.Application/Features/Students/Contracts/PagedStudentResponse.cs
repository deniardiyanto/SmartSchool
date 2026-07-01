namespace SmartSchool.Application.Features.Students.Contracts;

public class PagedStudentResponse
{
    public IReadOnlyList<StudentDto> Items { get; set; }
        = new List<StudentDto>();

    public int TotalRecords { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}