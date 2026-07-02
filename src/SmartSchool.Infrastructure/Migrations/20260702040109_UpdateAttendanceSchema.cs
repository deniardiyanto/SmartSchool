using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartSchool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttendanceSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendances_attendance_rules_AttendanceRuleId",
                table: "attendances");

            migrationBuilder.DropIndex(
                name: "IX_attendances_StudentId_AttendanceDate_AttendanceType",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "AttendanceType",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "LateMinutes",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "PointDeduction",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "ScanTime",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "ScannedBy",
                table: "attendances");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "attendances",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<Guid>(
                name: "BarcodeCardId",
                table: "attendances",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "AttendanceRuleId",
                table: "attendances",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "AttendanceDate",
                table: "attendances",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInTime",
                table: "attendances",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutTime",
                table: "attendances",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_attendances_StudentId_AttendanceDate",
                table: "attendances",
                columns: new[] { "StudentId", "AttendanceDate" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_attendance_rules_AttendanceRuleId",
                table: "attendances",
                column: "AttendanceRuleId",
                principalTable: "attendance_rules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendances_attendance_rules_AttendanceRuleId",
                table: "attendances");

            migrationBuilder.DropIndex(
                name: "IX_attendances_StudentId_AttendanceDate",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "CheckInTime",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "CheckOutTime",
                table: "attendances");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "attendances",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "BarcodeCardId",
                table: "attendances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AttendanceRuleId",
                table: "attendances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AttendanceDate",
                table: "attendances",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "AttendanceType",
                table: "attendances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LateMinutes",
                table: "attendances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PointDeduction",
                table: "attendances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScanTime",
                table: "attendances",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ScannedBy",
                table: "attendances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_attendances_StudentId_AttendanceDate_AttendanceType",
                table: "attendances",
                columns: new[] { "StudentId", "AttendanceDate", "AttendanceType" });

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_attendance_rules_AttendanceRuleId",
                table: "attendances",
                column: "AttendanceRuleId",
                principalTable: "attendance_rules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
