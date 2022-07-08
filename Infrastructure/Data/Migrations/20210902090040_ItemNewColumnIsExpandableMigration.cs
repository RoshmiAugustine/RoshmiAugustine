using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class ItemNewColumnIsExpandableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExpandable",
                table: "Item",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpandable",
                table: "Item");
        }
    }
}
