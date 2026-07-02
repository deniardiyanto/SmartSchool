using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartSchool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendanceRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendances_attendance_rules_AttendanceRuleId",
                table: "attendances");

            migrationBuilder.DropIndex(
                name: "IX_attendances_AttendanceRuleId",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "AttendanceRuleId",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "AllowMultipleScan",
                table: "attendance_rules");

            migrationBuilder.DropColumn(
                name: "AlphaTime",
                table: "attendance_rules");

            migrationBuilder.DropColumn(
                name: "EnableWhatsapp",
                table: "attendance_rules");

            migrationBuilder.DropColumn(
                name: "LateTime",
                table: "attendance_rules");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "attendance_rules");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "attendance_rules");

            migrationBuilder.AlterColumn<int>(
                name: "LatePoint",
                table: "attendance_rules",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: -5);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "attendance_rules",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<int>(
                name: "AbsentPoint",
                table: "attendance_rules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "CheckInEnd",
                table: "attendance_rules",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "CheckInStart",
                table: "attendance_rules",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "CheckOutEnd",
                table: "attendance_rules",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "CheckOutStart",
                table: "attendance_rules",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "EarlyCheckoutPoint",
                table: "attendance_rules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LateToleranceMinutes",
                table: "attendance_rules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PresentPoint",
                table: "attendance_rules",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsentPoint",
                table: "attendance_rules");

            migrationBuilder.DropColumn(
                name: "CheckInEnd",
                table: "attendance_rules");

            migrationBuilder.DropColumn(
                name: "CheckInStart",
                table: "attendance_rules");

            migrationBuilder.DropColumn(
                name: "CheckOutEnd",
                table: "attendance_rules");

            migrationBuilder.DropColumn(
                name: "CheckOutStart",
                table: "attendance_rules");

            migrationBuilder.DropColumn(
                name: "EarlyCheckoutPoint",
                table: "attendance_rules");

            migrationBuilder.DropColumn(
                name: "LateToleranceMinutes",
                table: "attendance_rules");

            migrationBuilder.DropColumn(
                name: "PresentPoint",
                table: "attendance_rules");

            migrationBuilder.AddColumn<Guid>(
                name: "AttendanceRuleId",
                table: "attendances",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LatePoint",
                table: "attendance_rules",
                type: "integer",
                nullable: false,
                defaultValue: -5,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "attendance_rules",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowMultipleScan",
                table: "attendance_rules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "AlphaTime",
                table: "attendance_rules",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "EnableWhatsapp",
                table: "attendance_rules",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "LateTime",
                table: "attendance_rules",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "attendance_rules",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "attendance_rules",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_attendances_AttendanceRuleId",
                table: "attendances",
                column: "AttendanceRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_attendance_rules_AttendanceRuleId",
                table: "attendances",
                column: "AttendanceRuleId",
                principalTable: "attendance_rules",
                principalColumn: "Id");
        }
    }
}
