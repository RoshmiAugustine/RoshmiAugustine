using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class ReminderReccurenceChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "info",
                table: "TimeZones",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "info",
                table: "RecurrencePattern",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "info",
                table: "RecurrenceEndType",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "RecurrenceOrdinalIDs",
                table: "QuestionnaireRegularReminderRecurrence",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "RecurrenceMonthIDs",
                table: "QuestionnaireRegularReminderRecurrence",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "RecurrenceDayNameIDs",
                table: "QuestionnaireRegularReminderRecurrence",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<int>(
                name: "UniqueCounter",
                table: "PersonQuestionnaireSchedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "WindowCloseDate",
                table: "PersonQuestionnaireSchedule",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WindowOpenDate",
                table: "PersonQuestionnaireSchedule",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueCounter",
                table: "PersonQuestionnaireSchedule");

            migrationBuilder.DropColumn(
                name: "WindowCloseDate",
                table: "PersonQuestionnaireSchedule");

            migrationBuilder.DropColumn(
                name: "WindowOpenDate",
                table: "PersonQuestionnaireSchedule");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "info",
                table: "TimeZones",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "info",
                table: "RecurrencePattern",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "info",
                table: "RecurrenceEndType",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "RecurrenceOrdinalIDs",
                table: "QuestionnaireRegularReminderRecurrence",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "RecurrenceMonthIDs",
                table: "QuestionnaireRegularReminderRecurrence",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "RecurrenceDayNameIDs",
                table: "QuestionnaireRegularReminderRecurrence",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }
    }
}
