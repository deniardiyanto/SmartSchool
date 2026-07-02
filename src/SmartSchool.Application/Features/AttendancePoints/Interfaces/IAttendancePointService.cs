using SmartSchool.Application.Features.AttendancePoints.Contracts;

namespace SmartSchool.Application.Features.AttendancePoints.Interfaces;

public interface IAttendancePointService
{
    Task<PagedAttendancePointResponse> GetPagedAsync(
        AttendancePointFilterRequest request);

    Task<AttendancePointDto> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(
        CreateAttendancePointRequest request);

    Task UpdateAsync(
        Guid id,
        UpdateAttendancePointRequest request);

    Task DeleteAsync(Guid id);
}