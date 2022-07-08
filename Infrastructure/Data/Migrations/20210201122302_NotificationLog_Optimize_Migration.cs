using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class NotificationLog_Optimize_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssessmentID",
                table: "NotificationLog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "NotificationLog",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionnaireID",
                table: "NotificationLog",
                nullable: true);           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssessmentID",
                table: "NotificationLog");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "NotificationLog");

            migrationBuilder.DropColumn(
                name: "QuestionnaireID",
                table: "NotificationLog");
        }
    }
}
