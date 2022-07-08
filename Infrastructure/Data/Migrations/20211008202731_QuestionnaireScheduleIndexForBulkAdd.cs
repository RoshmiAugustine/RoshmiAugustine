using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class QuestionnaireScheduleIndexForBulkAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonQuestionnaireScheduleIndex",
                table: "PersonQuestionnaireSchedule",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonQuestionnaireScheduleIndex",
                table: "PersonQuestionnaireSchedule");
        }
    }
}
