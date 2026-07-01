using Microsoft.EntityFrameworkCore;
using SmartSchool.Application.Features.ClassRooms.Contracts;
using SmartSchool.Application.Features.ClassRooms.Interfaces;
using SmartSchool.Domain.Entities;
using SmartSchool.Infrastructure.Persistence.Context;
using SmartSchool.Application.Common.Exceptions;

namespace SmartSchool.Infrastructure.Services.Master;

public class ClassRoomService : IClassRoomService
{
    private readonly SmartSchoolDbContext _context;

    public ClassRoomService(SmartSchoolDbContext context)
    {
        _context = context;
    }

    public async Task<PagedClassRoomResponse> GetPagedAsync(ClassRoomFilterRequest request)
    {
        var query = _context.ClassRooms
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Name, $"%{request.Search}%") ||
                EF.Functions.ILike(x.Code, $"%{request.Search}%"));
        }

        if (request.Grade.HasValue)
        {
            query = query.Where(x => x.Grade == request.Grade);
        }

        if (!string.IsNullOrWhiteSpace(request.AcademicYear))
        {
            query = query.Where(x => x.AcademicYear == request.AcademicYear);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(x => x.IsActive == request.IsActive);
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Grade)
            .ThenBy(x => x.Name)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ClassRoomDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Grade = x.Grade,
                AcademicYear = x.AcademicYear,
                Description = x.Description,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToListAsync();

        return new PagedClassRoomResponse
        {
            Items = items,
            TotalRecords = total,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<ClassRoomDto?> GetByIdAsync(Guid id)
    {
        return await _context.ClassRooms
            .AsNoTracking()
            .Where(x => x.Id == id && !x.IsDeleted)
            .Select(x => new ClassRoomDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Grade = x.Grade,
                AcademicYear = x.AcademicYear,
                Description = x.Description,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Guid> CreateAsync(CreateClassRoomRequest request)
    {
        var exists = await _context.ClassRooms
            .AnyAsync(x => x.Code == request.Code && !x.IsDeleted);

        if (exists)
            throw new ConflictException("Class room code already exists.");

        var entity = new ClassRoom
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            Grade = request.Grade,
            AcademicYear = request.AcademicYear,
            Description = request.Description,
            IsActive = true
        };

        _context.ClassRooms.Add(entity);

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateClassRoomRequest request)
    {
        var entity = await _context.ClassRooms
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
           throw new NotFoundException("Class room not found.");

        var duplicate = await _context.ClassRooms.AnyAsync(x =>
            x.Id != id &&
            x.Code == request.Code &&
            !x.IsDeleted);

        if (duplicate)
            throw new ConflictException("Class room code already exists.");

        entity.Code = request.Code;
        entity.Name = request.Name;
        entity.Grade = request.Grade;
        entity.AcademicYear = request.AcademicYear;
        entity.Description = request.Description;
        entity.IsActive = request.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.ClassRooms
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
           throw new NotFoundException("Class room not found.");

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
}