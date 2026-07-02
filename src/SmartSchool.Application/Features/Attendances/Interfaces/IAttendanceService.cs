using SmartSchool.Application.Features.Attendances.Contracts;

namespace SmartSchool.Application.Features.Attendances.Interfaces;

public interface IAttendanceService
{
    Task<PagedAttendanceResponse> GetPagedAsync(
        AttendanceFilterRequest request);

    Task<AttendanceDto> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(
        CreateAttendanceRequest request);

    Task UpdateAsync(
        Guid id,
        UpdateAttendanceRequest request);

    Task DeleteAsync(Guid id);
}