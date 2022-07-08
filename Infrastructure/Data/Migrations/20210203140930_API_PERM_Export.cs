using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class API_PERM_Export : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExportTemplate",
                columns: table => new
                {
                    ExportTemplateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TemplateSourceText = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportTemplate", x => x.ExportTemplateID);
                });

             

            migrationBuilder.CreateTable(
                name: "ExportTemplateAgency",
                columns: table => new
                {
                    ExportTemplateAgencyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportTemplateID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportTemplateAgency", x => x.ExportTemplateAgencyID);
                    table.ForeignKey(
                        name: "FK_ExportTemplateAgency_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExportTemplateAgency_ExportTemplate_ExportTemplateID",
                        column: x => x.ExportTemplateID,
                        principalTable: "ExportTemplate",
                        principalColumn: "ExportTemplateID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportTemplateAgency_AgencyID",
                table: "ExportTemplateAgency",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_ExportTemplateAgency_ExportTemplateID",
                table: "ExportTemplateAgency",
                column: "ExportTemplateID");
 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportTemplateAgency");

            migrationBuilder.DropTable(
                name: "ExportTemplate");
        }
    }
}
