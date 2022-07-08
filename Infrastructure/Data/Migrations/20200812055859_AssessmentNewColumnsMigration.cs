using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class AssessmentNewColumnsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EventDate",
                table: "Assessment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventNotes",
                table: "Assessment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDate",
                table: "Assessment");

            migrationBuilder.DropColumn(
                name: "EventNotes",
                table: "Assessment");
        }
    }
}
