using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class Add_CountryId_Address_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Address",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryId",
                table: "Address",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Country_CountryId",
                table: "Address",
                column: "CountryId",
                principalSchema: "info",
                principalTable: "Country",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Country_CountryId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_CountryId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Address");
        }
    }
}
