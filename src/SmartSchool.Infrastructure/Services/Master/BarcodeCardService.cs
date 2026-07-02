using Microsoft.EntityFrameworkCore;
using SmartSchool.Application.Features.BarcodeCards.Contracts;
using SmartSchool.Application.Features.BarcodeCards.Interfaces;
using SmartSchool.Infrastructure.Persistence.Context;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Services.Master;

public class BarcodeCardService : IBarcodeCardService
{
    private readonly SmartSchoolDbContext _context;

    public BarcodeCardService(SmartSchoolDbContext context)
    {
        _context = context;
    }

    public async Task<PagedBarcodeCardResponse> GetPagedAsync(
        BarcodeCardFilterRequest request)
    {
        var query = _context.BarcodeCards
            .Include(x => x.Student)
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var keyword = request.Search.Trim().ToLower();

            query = query.Where(x =>
                x.CardNumber.ToLower().Contains(keyword) ||
                x.BarcodeValue.ToLower().Contains(keyword) ||
                x.Student.FullName.ToLower().Contains(keyword));
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(x =>
                x.IsActive == request.IsActive.Value);
        }

        var totalRecords = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.CardNumber)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedBarcodeCardResponse
        {
            Items = items
                .Select(BarcodeCardDto.FromEntity)
                .ToList(),

            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(
                totalRecords / (double)request.PageSize)
        };
    }

    public async Task<BarcodeCardDto> GetByIdAsync(Guid id)
    {
        var entity = await _context.BarcodeCards
            .Include(x => x.Student)
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                !x.IsDeleted);

        if (entity == null)
            throw new KeyNotFoundException("Barcode card not found.");

        return BarcodeCardDto.FromEntity(entity);
    }

    public async Task<Guid> CreateAsync(
        CreateBarcodeCardRequest request)
    {
        var studentExists = await _context.Students
            .AnyAsync(x =>
                x.Id == request.StudentId &&
                !x.IsDeleted);

        if (!studentExists)
            throw new Exception("Student not found.");

        var hasCard = await _context.BarcodeCards
            .AnyAsync(x =>
                x.StudentId == request.StudentId &&
                !x.IsDeleted);

        if (hasCard)
            throw new Exception("Student already has barcode card.");

        var cardNumberExists = await _context.BarcodeCards
            .AnyAsync(x =>
                x.CardNumber == request.CardNumber &&
                !x.IsDeleted);

        if (cardNumberExists)
            throw new Exception("Card number already exists.");

        var barcodeExists = await _context.BarcodeCards
            .AnyAsync(x =>
                x.BarcodeValue == request.BarcodeValue &&
                !x.IsDeleted);

        if (barcodeExists)
            throw new Exception("Barcode already exists.");

        var entity = new BarcodeCard
        {
            Id = Guid.NewGuid(),

            StudentId = request.StudentId,

            CardNumber = request.CardNumber,

            BarcodeValue = request.BarcodeValue,

            IssuedDate = DateTime.SpecifyKind(
                request.IssuedDate,
                DateTimeKind.Utc),

            ExpiredDate = request.ExpiredDate.HasValue
                ? DateTime.SpecifyKind(
                    request.ExpiredDate.Value,
                    DateTimeKind.Utc)
                : null,

            IsActive = true
        };

        _context.BarcodeCards.Add(entity);

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task UpdateAsync(
        Guid id,
        UpdateBarcodeCardRequest request)
    {
        var entity = await _context.BarcodeCards
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                !x.IsDeleted);

        if (entity == null)
            throw new KeyNotFoundException("Barcode card not found.");

        var cardExists = await _context.BarcodeCards
            .AnyAsync(x =>
                x.CardNumber == request.CardNumber &&
                x.Id != id &&
                !x.IsDeleted);

        if (cardExists)
            throw new Exception("Card number already exists.");

        var barcodeExists = await _context.BarcodeCards
            .AnyAsync(x =>
                x.BarcodeValue == request.BarcodeValue &&
                x.Id != id &&
                !x.IsDeleted);

        if (barcodeExists)
            throw new Exception("Barcode already exists.");

        entity.CardNumber = request.CardNumber;

        entity.BarcodeValue = request.BarcodeValue;

        entity.IssuedDate = DateTime.SpecifyKind(
            request.IssuedDate,
            DateTimeKind.Utc);

        entity.ExpiredDate = request.ExpiredDate.HasValue
            ? DateTime.SpecifyKind(
                request.ExpiredDate.Value,
                DateTimeKind.Utc)
            : null;

        entity.IsActive = request.IsActive;

        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.BarcodeCards
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                !x.IsDeleted);

        if (entity == null)
            throw new KeyNotFoundException("Barcode card not found.");

        entity.IsDeleted = true;
        entity.IsActive = false;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
}