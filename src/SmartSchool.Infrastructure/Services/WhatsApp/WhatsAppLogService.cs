using Microsoft.EntityFrameworkCore;
using SmartSchool.Application.Features.WhatsApp.Contracts;
using SmartSchool.Application.Features.WhatsApp.Interfaces;
using SmartSchool.Infrastructure.Persistence.Context;

namespace SmartSchool.Infrastructure.Services.WhatsApp;

public class WhatsAppLogService : IWhatsAppLogService
{
    private readonly SmartSchoolDbContext _context;

    public WhatsAppLogService(
        SmartSchoolDbContext context)
    {
        _context = context;
    }

    public async Task<PagedWhatsAppLogResponse> GetPagedAsync(
        WhatsAppLogFilterRequest request)
    {
        var query = _context.WhatsAppLogs
            .Include(x => x.Attendance)
                .ThenInclude(x => x.Student)
                    .ThenInclude(x => x.ClassRoom)
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        //-----------------------------------------------------
        // Student Name
        //-----------------------------------------------------

        if (!string.IsNullOrWhiteSpace(request.StudentName))
        {
            var keyword = request.StudentName.Trim().ToLower();

            query = query.Where(x =>
                x.Attendance.Student.FullName
                    .ToLower()
                    .Contains(keyword));
        }

        //-----------------------------------------------------
        // Phone Number
        //-----------------------------------------------------

        if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            var keyword = request.PhoneNumber.Trim();

            query = query.Where(x =>
                x.PhoneNumber.Contains(keyword));
        }

        //-----------------------------------------------------
        // Status
        //-----------------------------------------------------

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            var status = request.Status.Trim().ToLower();

            query = query.Where(x =>
                x.Status.ToLower() == status);
        }

        //-----------------------------------------------------
        // From Date
        //-----------------------------------------------------

        if (request.FromDate.HasValue)
        {
            var from = request.FromDate.Value.ToDateTime(
                TimeOnly.MinValue);

            query = query.Where(x =>
                x.SentAt >= from);
        }

        //-----------------------------------------------------
        // To Date
        //-----------------------------------------------------

        if (request.ToDate.HasValue)
        {
            var to = request.ToDate.Value.ToDateTime(
                TimeOnly.MaxValue);

            query = query.Where(x =>
                x.SentAt <= to);
        }

        //-----------------------------------------------------
        // Total
        //-----------------------------------------------------

        var totalRecords =
            await query.CountAsync();

        //-----------------------------------------------------
        // Paging
        //-----------------------------------------------------

        var items = await query
            .OrderByDescending(x => x.SentAt)
            .Skip((request.PageNumber - 1)
                    * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedWhatsAppLogResponse
        {
            Items = items
                .Select(WhatsAppLogDto.FromEntity)
                .ToList(),

            PageNumber = request.PageNumber,

            PageSize = request.PageSize,

            TotalRecords = totalRecords,

            TotalPages = (int)Math.Ceiling(
                totalRecords /
                (double)request.PageSize)
        };
    }

    public async Task<WhatsAppLogDto> GetByIdAsync(
        Guid id)
    {
        var entity = await _context.WhatsAppLogs
            .Include(x => x.Attendance)
                .ThenInclude(x => x.Student)
                    .ThenInclude(x => x.ClassRoom)
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                !x.IsDeleted);

        if (entity == null)
            throw new KeyNotFoundException(
                "WhatsApp log not found.");

        return WhatsAppLogDto.FromEntity(entity);
    }
}