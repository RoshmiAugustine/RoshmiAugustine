using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class Import_AddQuestionID_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionnaireID",
                table: "FileImport",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileImport_QuestionnaireID",
                table: "FileImport",
                column: "QuestionnaireID");

            migrationBuilder.AddForeignKey(
                name: "FK_FileImport_Questionnaire_QuestionnaireID",
                table: "FileImport",
                column: "QuestionnaireID",
                principalTable: "Questionnaire",
                principalColumn: "QuestionnaireID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileImport_Questionnaire_QuestionnaireID",
                table: "FileImport");

            migrationBuilder.DropIndex(
                name: "IX_FileImport_QuestionnaireID",
                table: "FileImport");

            migrationBuilder.DropColumn(
                name: "QuestionnaireID",
                table: "FileImport");
        }
    }
}
