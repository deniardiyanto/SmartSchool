// using Microsoft.EntityFrameworkCore;
// using SmartSchool.Application.Common.Interfaces;
// using SmartSchool.Application.Common.Models;
// using SmartSchool.Application.Features.Attendances.Scan.Contracts;
// using SmartSchool.Application.Features.Attendances.Scan.Interfaces;
// using SmartSchool.Domain.Entities;
// using SmartSchool.Domain.Enums;
// using SmartSchool.Infrastructure.Persistence.Context;

// namespace SmartSchool.Infrastructure.Services.Attend;

// public class AttendanceScannerService : IAttendanceScannerService
// {
//     private readonly SmartSchoolDbContext _context;
//     private readonly IDateTimeProvider _dateTimeProvider;
//     private readonly ICurrentUserService _currentUser;

//     public AttendanceScannerService(
//         SmartSchoolDbContext context,
//         IDateTimeProvider dateTimeProvider,
//         ICurrentUserService currentUser)
//     {
//         _context = context;
//         _dateTimeProvider = dateTimeProvider;
//         _currentUser = currentUser;
//     }

//     public async Task<ApiResponse<ScanAttendanceResponse>> ScanAsync(
//         ScanAttendanceRequest request)
//     {
//         var now = _dateTimeProvider.UtcNow;

//         var today = DateOnly.FromDateTime(now);

//         //-------------------------------------------------------
//         // Cari barcode
//         //-------------------------------------------------------

//         var barcode = await _context.BarcodeCards
//             .Include(x => x.Student)
//                 .ThenInclude(x => x.ClassRoom)
//             .FirstOrDefaultAsync(x =>
//                 x.BarcodeValue == request.BarcodeValue &&
//                 !x.IsDeleted);

//         if (barcode == null)
//         {
//             return ApiResponse<ScanAttendanceResponse>.Fail(
//                 "Barcode tidak ditemukan.");
//         }

//         //-------------------------------------------------------
//         // Barcode aktif
//         //-------------------------------------------------------

//         if (!barcode.IsActive)
//         {
//             return ApiResponse<ScanAttendanceResponse>.Fail(
//                 "Barcode sudah tidak aktif.");
//         }

//         //-------------------------------------------------------
//         // Expired
//         //-------------------------------------------------------

//         if (barcode.ExpiredDate.HasValue &&
//             barcode.ExpiredDate.Value < now)
//         {
//             return ApiResponse<ScanAttendanceResponse>.Fail(
//                 "Barcode sudah expired.");
//         }

//         //-------------------------------------------------------
//         // Student aktif
//         //-------------------------------------------------------

//         var student = barcode.Student;

//         if (!student.IsActive)
//         {
//             return ApiResponse<ScanAttendanceResponse>.Fail(
//                 "Student sudah tidak aktif.");
//         }

//         //-------------------------------------------------------
//         // Attendance hari ini
//         //-------------------------------------------------------

//         var attendance = await _context.Attendances
//             .FirstOrDefaultAsync(x =>
//                 x.StudentId == student.Id &&
//                 x.AttendanceDate == today &&
//                 !x.IsDeleted);

//         //-------------------------------------------------------
//         // CHECK IN
//         //-------------------------------------------------------

//         if (attendance == null)
//         {
//             attendance = new Attendance
//             {
//                 Id = Guid.NewGuid(),

//                 StudentId = student.Id,

//                 BarcodeCardId = barcode.Id,

//                 AttendanceDate = today,

//                 CheckInTime = now,

//                 Status = AttendanceStatus.Present,

//                 CreatedAt = now,

//                 CreatedBy = _currentUser.UserId
//             };

//             _context.Attendances.Add(attendance);

//             await _context.SaveChangesAsync();

//             await CreateAttendancePointAsync(
//                 attendance,
//                 10,
//                 "Present");

//             return ApiResponse<ScanAttendanceResponse>.Ok(
//                 new ScanAttendanceResponse
//                 {
//                     AttendanceId = attendance.Id,
//                     StudentId = student.Id,
//                     StudentName = student.FullName,
//                     ClassRoomName = student.ClassRoom.Name,
//                     BarcodeValue = barcode.BarcodeValue,
//                     ScanType = "CheckIn",
//                     ScanTime = now,
//                     Status = attendance.Status.ToString()
//                 },
//                 "Check-in berhasil.");
//         }

//         //-------------------------------------------------------
//         // CHECK OUT
//         //-------------------------------------------------------

//         if (attendance.CheckOutTime == null)
//         {
//             attendance.CheckOutTime = now;

//             attendance.UpdatedAt = now;

//             attendance.UpdatedBy = _currentUser.UserId;

//             await _context.SaveChangesAsync();

//             return ApiResponse<ScanAttendanceResponse>.Ok(
//                 new ScanAttendanceResponse
//                 {
//                     AttendanceId = attendance.Id,
//                     StudentId = student.Id,
//                     StudentName = student.FullName,
//                     ClassRoomName = student.ClassRoom.Name,
//                     BarcodeValue = barcode.BarcodeValue,
//                     ScanType = "CheckOut",
//                     ScanTime = now,
//                     Status = attendance.Status.ToString()
//                 },
//                 "Check-out berhasil.");
//         }

//         //-------------------------------------------------------
//         // Sudah checkout
//         //-------------------------------------------------------

//         return ApiResponse<ScanAttendanceResponse>.Fail(
//             "Attendance hari ini sudah selesai.");
//     }

//     /// <summary>
//     /// Membuat attendance point otomatis setelah check-in.
//     /// </summary>
//     private async Task CreateAttendancePointAsync(
//         Attendance attendance,
//         int point,
//         string reason)
//     {
//         var exists = await _context.AttendancePoints
//             .AnyAsync(x =>
//                 x.AttendanceId == attendance.Id &&
//                 !x.IsDeleted);

//         if (exists)
//             return;

//         var entity = new AttendancePoint
//         {
//             Id = Guid.NewGuid(),

//             AttendanceId = attendance.Id,

//             StudentId = attendance.StudentId,

//             Point = point,

//             Reason = reason,

//             Description = "Generated automatically from barcode scan.",

//             PointDate = _dateTimeProvider.UtcNow,

//             CreatedAt = _dateTimeProvider.UtcNow,

//             CreatedBy = _currentUser.UserId
//         };

//         _context.AttendancePoints.Add(entity);

//         await _context.SaveChangesAsync();
//     }
// }
using Microsoft.EntityFrameworkCore;
using SmartSchool.Application.Common.Interfaces;
using SmartSchool.Application.Common.Models;
using SmartSchool.Application.Features.Attendances.Scan.Contracts;
using SmartSchool.Application.Features.Attendances.Scan.Interfaces;
using SmartSchool.Domain.Entities;
using SmartSchool.Domain.Enums;
using SmartSchool.Infrastructure.Persistence.Context;

namespace SmartSchool.Infrastructure.Services.Attend;

public class AttendanceScannerService : IAttendanceScannerService
{
    private readonly SmartSchoolDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICurrentUserService _currentUser;

    public AttendanceScannerService(
        SmartSchoolDbContext context,
        IDateTimeProvider dateTimeProvider,
        ICurrentUserService currentUser)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
        _currentUser = currentUser;
    }

    public async Task<ApiResponse<ScanAttendanceResponse>> ScanAsync(
        ScanAttendanceRequest request)
    {
        var now = _dateTimeProvider.UtcNow;

        var today = DateOnly.FromDateTime(now);

        var rule = await GetAttendanceRuleAsync();

        //-------------------------------------------------------
        // Cari Barcode
        //-------------------------------------------------------

        var barcode = await _context.BarcodeCards
            .Include(x => x.Student)
                .ThenInclude(x => x.ClassRoom)
            .FirstOrDefaultAsync(x =>
                x.BarcodeValue == request.BarcodeValue &&
                !x.IsDeleted);

        if (barcode == null)
        {
            return ApiResponse<ScanAttendanceResponse>.Fail(
                "Barcode tidak ditemukan.");
        }

        //-------------------------------------------------------
        // Barcode aktif
        //-------------------------------------------------------

        if (!barcode.IsActive)
        {
            return ApiResponse<ScanAttendanceResponse>.Fail(
                "Barcode sudah tidak aktif.");
        }

        //-------------------------------------------------------
        // Barcode expired
        //-------------------------------------------------------

        if (barcode.ExpiredDate.HasValue &&
            barcode.ExpiredDate.Value < now)
        {
            return ApiResponse<ScanAttendanceResponse>.Fail(
                "Barcode sudah expired.");
        }

        //-------------------------------------------------------
        // Student aktif
        //-------------------------------------------------------

        var student = barcode.Student;

        if (!student.IsActive)
        {
            return ApiResponse<ScanAttendanceResponse>.Fail(
                "Student sudah tidak aktif.");
        }

        //-------------------------------------------------------
        // Attendance hari ini
        //-------------------------------------------------------

        var attendance = await _context.Attendances
            .FirstOrDefaultAsync(x =>
                x.StudentId == student.Id &&
                x.AttendanceDate == today &&
                !x.IsDeleted);

        //-------------------------------------------------------
        // CHECK IN
        //-------------------------------------------------------

        if (attendance == null)
        {
            var status = GetAttendanceStatus(rule, now);
            var result = CalculateAttendanceResult(now, rule);

            attendance = new Attendance
            {
                Id = Guid.NewGuid(),

                StudentId = student.Id,

                BarcodeCardId = barcode.Id,

                AttendanceDate = today,

                CheckInTime = now,

                Status = result.status,

                CreatedAt = now,

                CreatedBy = _currentUser.UserId
            };

            _context.Attendances.Add(attendance);

            await _context.SaveChangesAsync();

            await CreateAttendancePointAsync(
                attendance,
                rule);

            return ApiResponse<ScanAttendanceResponse>.Ok(
                new ScanAttendanceResponse
                {
                    AttendanceId = attendance.Id,
                    StudentId = student.Id,
                    StudentName = student.FullName,
                    ClassRoomName = student.ClassRoom.Name,
                    BarcodeValue = barcode.BarcodeValue,
                    ScanType = "CheckIn",
                    ScanTime = now,
                    Status = attendance.Status.ToString(),
                    Point = result.point,
                    Reason = result.reason
                },
                "Check-in berhasil.");
        }

        //-------------------------------------------------------
        // CHECK OUT
        //-------------------------------------------------------

        if (attendance.CheckOutTime == null)
        {
            attendance.CheckOutTime = now;

            attendance.UpdatedAt = now;

            attendance.UpdatedBy = _currentUser.UserId;

            await _context.SaveChangesAsync();

            return ApiResponse<ScanAttendanceResponse>.Ok(
                new ScanAttendanceResponse
                {
                    AttendanceId = attendance.Id,
                    StudentId = student.Id,
                    StudentName = student.FullName,
                    ClassRoomName = student.ClassRoom.Name,
                    BarcodeValue = barcode.BarcodeValue,
                    ScanType = "CheckOut",
                    ScanTime = now,
                    Status = attendance.Status.ToString()
                },
                "Check-out berhasil.");
        }

        //-------------------------------------------------------
        // Sudah checkout
        //-------------------------------------------------------

        return ApiResponse<ScanAttendanceResponse>.Fail(
            "Attendance hari ini sudah selesai.");
    }

    /// <summary>
    /// Ambil attendance rule aktif.
    /// </summary>
    private async Task<AttendanceRule> GetAttendanceRuleAsync()
    {
        var rule = await _context.AttendanceRules
            .FirstOrDefaultAsync(x =>
                x.IsActive &&
                !x.IsDeleted);

        if (rule == null)
            throw new Exception(
                "Attendance Rule belum dikonfigurasi.");

        return rule;
    }

    /// <summary>
    /// Menentukan status attendance berdasarkan rule.
    /// </summary>
    private AttendanceStatus GetAttendanceStatus(
        AttendanceRule rule,
        DateTime now)
    {
        var currentTime = TimeOnly.FromDateTime(now);

        if (currentTime <= rule.CheckInEnd)
            return AttendanceStatus.Present;

        return AttendanceStatus.Late;
    }

    /// <summary>
    /// Membuat attendance point otomatis.
    /// </summary>
    private async Task CreateAttendancePointAsync(
        Attendance attendance,
        AttendanceRule rule)
    {
        var exists = await _context.AttendancePoints
            .AnyAsync(x =>
                x.AttendanceId == attendance.Id &&
                !x.IsDeleted);

        if (exists)
            return;

        int point;
        string reason;
        string description;

        switch (attendance.Status)
        {
            case AttendanceStatus.Present:
                point = rule.PresentPoint;
                reason = "Present";
                description = "Student checked in on time.";
                break;

            case AttendanceStatus.Late:
                point = rule.LatePoint;
                reason = "Late";
                description = "Student checked in after allowed time.";
                break;

            default:
                point = 0;
                reason = attendance.Status.ToString();
                description = "Attendance point generated automatically.";
                break;
        }

        var entity = new AttendancePoint
        {
            Id = Guid.NewGuid(),

            AttendanceId = attendance.Id,

            StudentId = attendance.StudentId,

            Point = point,

            Reason = reason,

            Description = description,

            PointDate = _dateTimeProvider.UtcNow,

            CreatedAt = _dateTimeProvider.UtcNow,

            CreatedBy = _currentUser.UserId
        };

        _context.AttendancePoints.Add(entity);

        await _context.SaveChangesAsync();
    }
    private (AttendanceStatus status, int point, string reason)
    CalculateAttendanceResult(
        DateTime now,
        AttendanceRule rule)
    {
        var currentTime = TimeOnly.FromDateTime(now);

        if (currentTime <= rule.CheckInEnd)
        {
            return
            (
                AttendanceStatus.Present,
                rule.PresentPoint,
                "Present"
            );
        }

        return
        (
            AttendanceStatus.Late,
            rule.LatePoint,
            "Late"
        );
    }
}