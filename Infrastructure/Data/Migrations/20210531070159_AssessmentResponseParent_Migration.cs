using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class AssessmentResponseParent_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentAssessmentResponseID",
                table: "AssessmentResponse",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponse_ParentAssessmentResponseID",
                table: "AssessmentResponse",
                column: "ParentAssessmentResponseID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentResponse_AssessmentResponse_ParentAssessmentResponseID",
                table: "AssessmentResponse",
                column: "ParentAssessmentResponseID",
                principalTable: "AssessmentResponse",
                principalColumn: "AssessmentResponseID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentResponse_AssessmentResponse_ParentAssessmentResponseID",
                table: "AssessmentResponse");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentResponse_ParentAssessmentResponseID",
                table: "AssessmentResponse");

            migrationBuilder.DropColumn(
                name: "ParentAssessmentResponseID",
                table: "AssessmentResponse");
        }
    }
}
