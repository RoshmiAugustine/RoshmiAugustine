using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class InviteToCompleteAbbrevRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionnaireRegularReminderRecurrence_QuestionnaireWindow_QuestionnaireWindowID",
                table: "QuestionnaireRegularReminderRecurrence");

            migrationBuilder.DropColumn(
                name: "Abbrev",
                schema: "info",
                table: "InviteToCompleteReceiver");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionnaireRegularReminderRecurrence_QuestionnaireWindow_QuestionnaireWindowID",
                table: "QuestionnaireRegularReminderRecurrence",
                column: "QuestionnaireWindowID",
                principalTable: "QuestionnaireWindow",
                principalColumn: "QuestionnaireWindowID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionnaireRegularReminderRecurrence_QuestionnaireWindow_QuestionnaireWindowID",
                table: "QuestionnaireRegularReminderRecurrence");

            migrationBuilder.AddColumn<string>(
                name: "Abbrev",
                schema: "info",
                table: "InviteToCompleteReceiver",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionnaireRegularReminderRecurrence_QuestionnaireWindow_QuestionnaireWindowID",
                table: "QuestionnaireRegularReminderRecurrence",
                column: "QuestionnaireWindowID",
                principalTable: "QuestionnaireWindow",
                principalColumn: "QuestionnaireWindowID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
