using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class QuestionnaireSkipLogicRuleAction_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChildItemID",
                table: "QuestionnaireSkipLogicRuleAction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRuleAction_ChildItemID",
                table: "QuestionnaireSkipLogicRuleAction",
                column: "ChildItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionnaireSkipLogicRuleAction_Item_ChildItemID",
                table: "QuestionnaireSkipLogicRuleAction",
                column: "ChildItemID",
                principalTable: "Item",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionnaireSkipLogicRuleAction_Item_ChildItemID",
                table: "QuestionnaireSkipLogicRuleAction");

            migrationBuilder.DropIndex(
                name: "IX_QuestionnaireSkipLogicRuleAction_ChildItemID",
                table: "QuestionnaireSkipLogicRuleAction");

            migrationBuilder.DropColumn(
                name: "ChildItemID",
                table: "QuestionnaireSkipLogicRuleAction");
        }
    }
}
