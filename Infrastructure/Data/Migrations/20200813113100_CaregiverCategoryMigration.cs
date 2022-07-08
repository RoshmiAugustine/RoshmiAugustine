﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class CaregiverCategoryMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaregiverCategory",
                table: "AssessmentResponse",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaregiverCategory",
                table: "AssessmentResponse");
        }
    }
}
