using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class RowVersionColumn_ADD_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "info",
                table: "Instrument",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "info",
                table: "Category",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Response",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "QuestionnaireItem",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Questionnaire",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "info",
                table: "Instrument");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "info",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Response");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "QuestionnaireItem");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Questionnaire");
        }
    }
}
