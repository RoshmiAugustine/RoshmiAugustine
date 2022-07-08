using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class Person_PersonSupport_UniversalID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniversalID",
                table: "PersonSupport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniversalID",
                table: "Person",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniversalID",
                table: "PersonSupport");

            migrationBuilder.DropColumn(
                name: "UniversalID",
                table: "Person");
        }
    }
}
