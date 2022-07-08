using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class PersonAssessmentMetrics_Entity_ADD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonAssessmentMetrics",
                schema: "dbo",
                columns: table => new
                {
                    PersonAssessmentMetricsID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(nullable: false),
                    InstrumentID = table.Column<int>(nullable: false),
                    PersonQuestionnaireID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    NeedsEver = table.Column<int>(nullable: false),
                    NeedsIdentified = table.Column<int>(nullable: false),
                    NeedsAddressed = table.Column<int>(nullable: false),
                    NeedsAddressing = table.Column<int>(nullable: false),
                    NeedsImproved = table.Column<int>(nullable: false),
                    StrengthsEver = table.Column<int>(nullable: false),
                    StrengthsIdentified = table.Column<int>(nullable: false),
                    StrengthsBuilt = table.Column<int>(nullable: false),
                    StrengthsBuilding = table.Column<int>(nullable: false),
                    StrengthsImproved = table.Column<int>(nullable: false),
                    AssessmentID = table.Column<int>(nullable: false),
                    updateDate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAssessmentMetrics", x => x.PersonAssessmentMetricsID);
                    table.ForeignKey(
                        name: "FK_PersonAssessmentMetrics_Assessment_AssessmentID",
                        column: x => x.AssessmentID,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonAssessmentMetrics_AssessmentID",
                schema: "dbo",
                table: "PersonAssessmentMetrics",
                column: "AssessmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonAssessmentMetrics",
                schema: "dbo");
        }
    }
}
