using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class SMSDetailColumnUpdate_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonSupportID",
                table: "SMSDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonSupportID",
                table: "EmailDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SMSDetail_PersonSupportID",
                table: "SMSDetail",
                column: "PersonSupportID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailDetail_PersonSupportID",
                table: "EmailDetail",
                column: "PersonSupportID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailDetail_PersonSupport_PersonSupportID",
                table: "EmailDetail",
                column: "PersonSupportID",
                principalTable: "PersonSupport",
                principalColumn: "PersonSupportID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SMSDetail_PersonSupport_PersonSupportID",
                table: "SMSDetail",
                column: "PersonSupportID",
                principalTable: "PersonSupport",
                principalColumn: "PersonSupportID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailDetail_PersonSupport_PersonSupportID",
                table: "EmailDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SMSDetail_PersonSupport_PersonSupportID",
                table: "SMSDetail");

            migrationBuilder.DropIndex(
                name: "IX_SMSDetail_PersonSupportID",
                table: "SMSDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmailDetail_PersonSupportID",
                table: "EmailDetail");

            migrationBuilder.DropColumn(
                name: "PersonSupportID",
                table: "SMSDetail");

            migrationBuilder.DropColumn(
                name: "PersonSupportID",
                table: "EmailDetail");
        }
    }
}
