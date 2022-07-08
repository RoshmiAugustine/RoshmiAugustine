using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class AddColumn_powerbiRDLReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filters",
                table: "AgencyPowerBIReport");

            migrationBuilder.AddColumn<string>(
                name: "FiltersOrParameters",
                table: "AgencyPowerBIReport",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRDLReport",
                table: "AgencyPowerBIReport",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FiltersOrParameters",
                table: "AgencyPowerBIReport");

            migrationBuilder.DropColumn(
                name: "IsRDLReport",
                table: "AgencyPowerBIReport");

            migrationBuilder.AddColumn<string>(
                name: "Filters",
                table: "AgencyPowerBIReport",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
