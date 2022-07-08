using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class BackgroundProcessLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLogAdded",
                table: "NotifyReminder",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BackgroundProcessLog",
                columns: table => new
                {
                    BackgroundProcessLogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessName = table.Column<string>(nullable: true),
                    LastProcessedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackgroundProcessLog", x => x.BackgroundProcessLogID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackgroundProcessLog");

            migrationBuilder.DropColumn(
                name: "IsLogAdded",
                table: "NotifyReminder");
        }
    }
}
