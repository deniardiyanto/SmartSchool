using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.WhatsApp.Interfaces;

public interface IAttendanceMessageBuilder
{
    string BuildCheckInMessage(
        Student student,
        Attendance attendance,
        int point);

    string BuildCheckOutMessage(
        Student student,
        Attendance attendance);
}