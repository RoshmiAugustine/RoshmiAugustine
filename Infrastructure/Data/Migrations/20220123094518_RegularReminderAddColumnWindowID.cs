using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class RegularReminderAddColumnWindowID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionnaireWindowID",
                table: "QuestionnaireRegularReminderRecurrence",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireRegularReminderRecurrence_QuestionnaireWindowID",
                table: "QuestionnaireRegularReminderRecurrence",
                column: "QuestionnaireWindowID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionnaireRegularReminderRecurrence_QuestionnaireWindow_QuestionnaireWindowID",
                table: "QuestionnaireRegularReminderRecurrence",
                column: "QuestionnaireWindowID",
                principalTable: "QuestionnaireWindow",
                principalColumn: "QuestionnaireWindowID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionnaireRegularReminderRecurrence_QuestionnaireWindow_QuestionnaireWindowID",
                table: "QuestionnaireRegularReminderRecurrence");

            migrationBuilder.DropIndex(
                name: "IX_QuestionnaireRegularReminderRecurrence_QuestionnaireWindowID",
                table: "QuestionnaireRegularReminderRecurrence");

            migrationBuilder.DropColumn(
                name: "QuestionnaireWindowID",
                table: "QuestionnaireRegularReminderRecurrence");
        }
    }
}
