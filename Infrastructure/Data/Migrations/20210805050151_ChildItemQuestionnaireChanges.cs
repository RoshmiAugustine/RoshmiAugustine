using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class ChildItemQuestionnaireChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentItemID",
                table: "QuestionnaireSkipLogicRuleAction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRuleAction_ParentItemID",
                table: "QuestionnaireSkipLogicRuleAction",
                column: "ParentItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionnaireSkipLogicRuleAction_Item_ParentItemID",
                table: "QuestionnaireSkipLogicRuleAction",
                column: "ParentItemID",
                principalTable: "Item",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionnaireSkipLogicRuleAction_Item_ParentItemID",
                table: "QuestionnaireSkipLogicRuleAction");

            migrationBuilder.DropIndex(
                name: "IX_QuestionnaireSkipLogicRuleAction_ParentItemID",
                table: "QuestionnaireSkipLogicRuleAction");

            migrationBuilder.DropColumn(
                name: "ParentItemID",
                table: "QuestionnaireSkipLogicRuleAction");
        }
    }
}
