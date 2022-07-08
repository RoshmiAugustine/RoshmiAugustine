using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class Add_column_EmailAlerts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAlertsHelpersManagers",
                table: "Questionnaire",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailRemindersHelpers",
                table: "Questionnaire",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailReminderAlerts",
                table: "Helper",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAlertsHelpersManagers",
                table: "Questionnaire");

            migrationBuilder.DropColumn(
                name: "IsEmailRemindersHelpers",
                table: "Questionnaire");

            migrationBuilder.DropColumn(
                name: "IsEmailReminderAlerts",
                table: "Helper");
        }
    }
}
