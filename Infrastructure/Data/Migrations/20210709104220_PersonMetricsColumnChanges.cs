using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class PersonMetricsColumnChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updateDate",
                schema: "dbo",
                table: "PersonAssessmentMetrics",
                newName: "UpdateDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                schema: "dbo",
                table: "PersonAssessmentMetrics",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                schema: "dbo",
                table: "PersonAssessmentMetrics",
                newName: "updateDate");

            migrationBuilder.AlterColumn<int>(
                name: "updateDate",
                schema: "dbo",
                table: "PersonAssessmentMetrics",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
