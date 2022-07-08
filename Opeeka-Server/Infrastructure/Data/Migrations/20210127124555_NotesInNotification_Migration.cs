using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class NotesInNotification_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssessmentNoteID",
                table: "NotificationLog",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NoteUpdateDate",
                table: "Assessment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NoteUpdateUserID",
                table: "Assessment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssessmentNoteID",
                table: "NotificationLog");

            migrationBuilder.DropColumn(
                name: "NoteUpdateDate",
                table: "Assessment");

            migrationBuilder.DropColumn(
                name: "NoteUpdateUserID",
                table: "Assessment");
        }
    }
}
