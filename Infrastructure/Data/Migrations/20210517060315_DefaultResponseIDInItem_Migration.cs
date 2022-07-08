using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class DefaultResponseIDInItem_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultResponseID",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_DefaultResponseID",
                table: "Item",
                column: "DefaultResponseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Response_DefaultResponseID",
                table: "Item",
                column: "DefaultResponseID",
                principalTable: "Response",
                principalColumn: "ResponseID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Response_DefaultResponseID",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_DefaultResponseID",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "DefaultResponseID",
                table: "Item");
        }
    }
}
