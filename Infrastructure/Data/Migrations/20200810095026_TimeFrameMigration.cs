using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class TimeFrameMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeFrame",
                schema: "info",
                columns: table => new
                {
                    TimeFrameID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaysInService = table.Column<int>(nullable: false),
                    Month_Exact = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    Months_Int = table.Column<int>(nullable: false),
                    Qrts_Exact = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    Qrts_Int = table.Column<int>(nullable: false),
                    Qrt_Current = table.Column<int>(nullable: false),
                    Yrs_Exact = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    Years_Int = table.Column<int>(nullable: false),
                    Timeframe_Std = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeFrame", x => x.TimeFrameID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeFrame",
                schema: "info");
        }
    }
}
