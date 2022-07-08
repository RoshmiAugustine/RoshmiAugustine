using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class AgencyInsightTable_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgencyInsightDashboard",
                columns: table => new
                {
                    AgencyInsightDashboardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DashboardId = table.Column<int>(nullable: false),
                    AgencyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Filters = table.Column<string>(nullable: true),
                    CustomFilters = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(maxLength: 500, nullable: true),
                    LongDescription = table.Column<string>(nullable: true),
                    IconURL = table.Column<string>(maxLength: 500, nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyInsightDashboard", x => x.AgencyInsightDashboardId);
                    table.ForeignKey(
                        name: "FK_AgencyInsightDashboard_Agency_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgencyInsightDashboard_AgencyId",
                table: "AgencyInsightDashboard",
                column: "AgencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgencyInsightDashboard");
        }
    }
}
