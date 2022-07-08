using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class EmailAssessmentEntityChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentEmailLinkDetails_Helper_HelperID",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentEmailLinkDetails_PersonSupport_PersonSupportID",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.DropColumn(
                name: "PersonSupportEmail",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.AlterColumn<int>(
                name: "PersonSupportID",
                table: "AssessmentEmailLinkDetails",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HelperID",
                table: "AssessmentEmailLinkDetails",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PersonOrSupportEmail",
                table: "AssessmentEmailLinkDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VoiceTypeID",
                table: "AssessmentEmailLinkDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentEmailLinkDetails_VoiceTypeID",
                table: "AssessmentEmailLinkDetails",
                column: "VoiceTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentEmailLinkDetails_Helper_HelperID",
                table: "AssessmentEmailLinkDetails",
                column: "HelperID",
                principalTable: "Helper",
                principalColumn: "HelperID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentEmailLinkDetails_PersonSupport_PersonSupportID",
                table: "AssessmentEmailLinkDetails",
                column: "PersonSupportID",
                principalTable: "PersonSupport",
                principalColumn: "PersonSupportID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentEmailLinkDetails_VoiceType_VoiceTypeID",
                table: "AssessmentEmailLinkDetails",
                column: "VoiceTypeID",
                principalSchema: "info",
                principalTable: "VoiceType",
                principalColumn: "VoiceTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentEmailLinkDetails_Helper_HelperID",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentEmailLinkDetails_PersonSupport_PersonSupportID",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentEmailLinkDetails_VoiceType_VoiceTypeID",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentEmailLinkDetails_VoiceTypeID",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.DropColumn(
                name: "PersonOrSupportEmail",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.DropColumn(
                name: "VoiceTypeID",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.AlterColumn<int>(
                name: "PersonSupportID",
                table: "AssessmentEmailLinkDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HelperID",
                table: "AssessmentEmailLinkDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonSupportEmail",
                table: "AssessmentEmailLinkDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentEmailLinkDetails_Helper_HelperID",
                table: "AssessmentEmailLinkDetails",
                column: "HelperID",
                principalTable: "Helper",
                principalColumn: "HelperID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentEmailLinkDetails_PersonSupport_PersonSupportID",
                table: "AssessmentEmailLinkDetails",
                column: "PersonSupportID",
                principalTable: "PersonSupport",
                principalColumn: "PersonSupportID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
