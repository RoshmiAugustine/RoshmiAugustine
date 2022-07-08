using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class columnNameChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlobURL",
                table: "AssessmentResponseAttachment");

            migrationBuilder.AddColumn<string>(
                name: "FileURL",
                table: "AssessmentResponseAttachment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileURL",
                table: "AssessmentResponseAttachment");

            migrationBuilder.AddColumn<string>(
                name: "BlobURL",
                table: "AssessmentResponseAttachment",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
