using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class GenderKeychange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Gender_GenderID",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_GenderID",
                table: "Person");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Person_GenderID",
                table: "Person",
                column: "GenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Gender_GenderID",
                table: "Person",
                column: "GenderID",
                principalSchema: "info",
                principalTable: "Gender",
                principalColumn: "GenderID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
