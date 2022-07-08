using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class EmailLinkColumnUpdate_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssessmentGUID",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "AssessmentEmailLinkGUID",
                table: "AssessmentEmailLinkDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "AssessmentEmailLinkDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssessmentEmailLinkGUID",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "AssessmentGUID",
                table: "AssessmentEmailLinkDetails",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
