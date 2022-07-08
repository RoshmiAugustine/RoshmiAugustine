using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class ReminderTimeDBChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "QuestionnaireRegularReminderTimeRule");

            migrationBuilder.AddColumn<string>(
                name: "AMorPM",
                table: "QuestionnaireRegularReminderTimeRule",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hour",
                table: "QuestionnaireRegularReminderTimeRule",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Minute",
                table: "QuestionnaireRegularReminderTimeRule",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InviteToCompleteMailStatus",
                table: "NotifyReminder",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AMorPM",
                table: "QuestionnaireRegularReminderTimeRule");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "QuestionnaireRegularReminderTimeRule");

            migrationBuilder.DropColumn(
                name: "Minute",
                table: "QuestionnaireRegularReminderTimeRule");

            migrationBuilder.DropColumn(
                name: "InviteToCompleteMailStatus",
                table: "NotifyReminder");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "QuestionnaireRegularReminderTimeRule",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);
        }
    }
}
