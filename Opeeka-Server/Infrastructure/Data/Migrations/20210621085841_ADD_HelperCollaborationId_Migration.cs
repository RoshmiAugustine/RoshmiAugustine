using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class ADD_HelperCollaborationId_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollaborationID",
                table: "PersonHelper",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonHelper_CollaborationID",
                table: "PersonHelper",
                column: "CollaborationID");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonHelper_Collaboration_CollaborationID",
                table: "PersonHelper",
                column: "CollaborationID",
                principalTable: "Collaboration",
                principalColumn: "CollaborationID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonHelper_Collaboration_CollaborationID",
                table: "PersonHelper");

            migrationBuilder.DropIndex(
                name: "IX_PersonHelper_CollaborationID",
                table: "PersonHelper");

            migrationBuilder.DropColumn(
                name: "CollaborationID",
                table: "PersonHelper");
        }
    }
}
