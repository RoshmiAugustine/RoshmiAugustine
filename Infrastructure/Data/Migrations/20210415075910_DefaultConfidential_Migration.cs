using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class DefaultConfidential_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DefaultOtherConfidentiality",
                table: "QuestionnaireItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DefaultPersonRequestedConfidentiality",
                table: "QuestionnaireItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DefaultRequiredConfidentiality",
                table: "QuestionnaireItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DefaultOtherConfidentiality",
                table: "Item",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DefaultPersonRequestedConfidentiality",
                table: "Item",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DefaultRequiredConfidentiality",
                table: "Item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultOtherConfidentiality",
                table: "QuestionnaireItem");

            migrationBuilder.DropColumn(
                name: "DefaultPersonRequestedConfidentiality",
                table: "QuestionnaireItem");

            migrationBuilder.DropColumn(
                name: "DefaultRequiredConfidentiality",
                table: "QuestionnaireItem");

            migrationBuilder.DropColumn(
                name: "DefaultOtherConfidentiality",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "DefaultPersonRequestedConfidentiality",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "DefaultRequiredConfidentiality",
                table: "Item");
        }
    }
}
