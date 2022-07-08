using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class EmailDetail_SchemaChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "EmailDetail",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "EmailDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "EmailDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "EmailDetail");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "EmailDetail");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "EmailDetail");
        }
    }
}
