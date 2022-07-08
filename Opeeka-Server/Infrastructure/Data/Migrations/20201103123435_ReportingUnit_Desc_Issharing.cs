using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class ReportingUnit_Desc_Issharing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ReportingUnit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSharing",
                table: "ReportingUnit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSharing",
                table: "AgencySharing",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ReportingUnit");

            migrationBuilder.DropColumn(
                name: "IsSharing",
                table: "ReportingUnit");

            migrationBuilder.DropColumn(
                name: "IsSharing",
                table: "AgencySharing");
        }
    }
}
