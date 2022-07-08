using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class PersonSupport_SMS_Script_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailPermission",
                table: "PersonSupport",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TextPermission",
                table: "PersonSupport",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailPermission",
                table: "PersonSupport");

            migrationBuilder.DropColumn(
                name: "TextPermission",
                table: "PersonSupport");
        }
    }
}
