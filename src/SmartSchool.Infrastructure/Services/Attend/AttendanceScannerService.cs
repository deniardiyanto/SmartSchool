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

        //--------------------------------------------------
        // Cari Barcode
        //--------------------------------------------------

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

        //--------------------------------------------------
        // Barcode aktif
        //--------------------------------------------------

        if (!barcode.IsActive)
        {
            return ApiResponse<ScanAttendanceResponse>.Fail(
                "Barcode sudah tidak aktif.");
        }

        //--------------------------------------------------
        // Expired
        //--------------------------------------------------

        if (barcode.ExpiredDate.HasValue &&
            barcode.ExpiredDate.Value < now)
        {
            return ApiResponse<ScanAttendanceResponse>.Fail(
                "Barcode sudah expired.");
        }

        //--------------------------------------------------
        // Student aktif
        //--------------------------------------------------

        var student = barcode.Student;

        if (!student.IsActive)
        {
            return ApiResponse<ScanAttendanceResponse>.Fail(
                "Student sudah tidak aktif.");
        }

        //--------------------------------------------------
        // Attendance hari ini
        //--------------------------------------------------

        var attendance = await _context.Attendances
            .FirstOrDefaultAsync(x =>
                x.StudentId == student.Id &&
                x.AttendanceDate == today &&
                !x.IsDeleted);

        //--------------------------------------------------
        // Belum ada attendance = CHECK IN
        //--------------------------------------------------

        if (attendance == null)
        {
            attendance = new Domain.Entities.Attendance
            {
                Id = Guid.NewGuid(),

                StudentId = student.Id,

                BarcodeCardId = barcode.Id,

                AttendanceDate = today,

                CheckInTime = now,

                Status = AttendanceStatus.Present,

                CreatedAt = now,

                CreatedBy = _currentUser.UserId
            };

            _context.Attendances.Add(attendance);

            await _context.SaveChangesAsync();

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
        Status = attendance.Status.ToString()
    },
    "Check-in berhasil.");
        }

        //--------------------------------------------------
        // Belum checkout = CHECK OUT
        //--------------------------------------------------

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

        //--------------------------------------------------
        // Sudah checkout
        //--------------------------------------------------

        return ApiResponse<ScanAttendanceResponse>.Fail(
            "Attendance hari ini sudah selesai.");
    }
}