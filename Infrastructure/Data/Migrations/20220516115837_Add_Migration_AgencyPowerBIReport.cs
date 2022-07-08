using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class Add_Migration_AgencyPowerBIReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgencyPowerBIReport",
                columns: table => new
                {
                    AgencyPowerBIReportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyId = table.Column<long>(nullable: false),
                    InstrumentId = table.Column<int>(nullable: false),
                    ReportName = table.Column<string>(maxLength: 500, nullable: true),
                    ReportId = table.Column<Guid>(nullable: false),
                    Filters = table.Column<string>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyPowerBIReport", x => x.AgencyPowerBIReportId);
                    table.ForeignKey(
                        name: "FK_AgencyPowerBIReport_Agency_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgencyPowerBIReport_Instrument_InstrumentId",
                        column: x => x.InstrumentId,
                        principalSchema: "info",
                        principalTable: "Instrument",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgencyPowerBIReport_AgencyId",
                table: "AgencyPowerBIReport",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyPowerBIReport_InstrumentId",
                table: "AgencyPowerBIReport",
                column: "InstrumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgencyPowerBIReport");
        }
    }
}
