using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class QuestionnaireCloningChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClonedQuestionnaireNotifyRiskRuleID",
                table: "QuestionnaireNotifyRiskRule",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClonedQuestionnaireItemId",
                table: "QuestionnaireItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClonedQuestionnaireNotifyRiskRuleID",
                table: "QuestionnaireNotifyRiskRule");

            migrationBuilder.DropColumn(
                name: "ClonedQuestionnaireItemId",
                table: "QuestionnaireItem");
        }
    }
}
