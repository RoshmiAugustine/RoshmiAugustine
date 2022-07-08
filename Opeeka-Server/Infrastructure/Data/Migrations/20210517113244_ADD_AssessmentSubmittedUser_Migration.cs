using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class ADD_AssessmentSubmittedUser_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HelperName",
                table: "NotificationLog",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmittedUserID",
                table: "Assessment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_SubmittedUserID",
                table: "Assessment",
                column: "SubmittedUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessment_User_SubmittedUserID",
                table: "Assessment",
                column: "SubmittedUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_User_SubmittedUserID",
                table: "Assessment");

            migrationBuilder.DropIndex(
                name: "IX_Assessment_SubmittedUserID",
                table: "Assessment");

            migrationBuilder.DropColumn(
                name: "HelperName",
                table: "NotificationLog");

            migrationBuilder.DropColumn(
                name: "SubmittedUserID",
                table: "Assessment");
        }
    }
}
