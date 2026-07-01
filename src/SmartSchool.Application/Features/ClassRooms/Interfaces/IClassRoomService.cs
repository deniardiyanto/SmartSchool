using SmartSchool.Application.Features.ClassRooms.Contracts;

namespace SmartSchool.Application.Features.ClassRooms.Interfaces;

public interface IClassRoomService
{
    Task<PagedClassRoomResponse> GetPagedAsync(ClassRoomFilterRequest request);

    Task<ClassRoomDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(CreateClassRoomRequest request);

    Task UpdateAsync(Guid id, UpdateClassRoomRequest request);

    Task DeleteAsync(Guid id);
}