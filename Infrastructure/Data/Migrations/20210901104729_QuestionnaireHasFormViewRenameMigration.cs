using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class QuestionnaireHasFormViewRenameMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "hasFormView",
                table: "Questionnaire",
                newName: "HasFormView");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasFormView",
                table: "Questionnaire",
                newName: "hasFormView");
        }
    }
}
