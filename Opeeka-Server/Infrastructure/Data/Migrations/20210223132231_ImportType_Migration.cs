using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class ImportType_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportType",
                table: "FileImport");

            migrationBuilder.AddColumn<int>(
                name: "ImportTypeID",
                table: "FileImport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ImportType",
                schema: "info",
                columns: table => new
                {
                    ImportTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    TemplateJson = table.Column<string>(nullable: true),
                    TemplateURL = table.Column<string>(maxLength: 250, nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportType", x => x.ImportTypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileImport_ImportTypeID",
                table: "FileImport",
                column: "ImportTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_FileImport_ImportType_ImportTypeID",
                table: "FileImport",
                column: "ImportTypeID",
                principalSchema: "info",
                principalTable: "ImportType",
                principalColumn: "ImportTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileImport_ImportType_ImportTypeID",
                table: "FileImport");

            migrationBuilder.DropTable(
                name: "ImportType",
                schema: "info");

            migrationBuilder.DropIndex(
                name: "IX_FileImport_ImportTypeID",
                table: "FileImport");

            migrationBuilder.DropColumn(
                name: "ImportTypeID",
                table: "FileImport");

            migrationBuilder.AddColumn<string>(
                name: "ImportType",
                table: "FileImport",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
