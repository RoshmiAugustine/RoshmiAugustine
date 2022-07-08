using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class History_Agency_Collaboration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgencySharingHistory_Agency_ReportingUnitAgencyID",
                table: "AgencySharingHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaborationSharingHistory_CollaborationSharing_ReportingUnitCollaborationID",
                table: "CollaborationSharingHistory");

            migrationBuilder.DropIndex(
                name: "IX_CollaborationSharingHistory_ReportingUnitCollaborationID",
                table: "CollaborationSharingHistory");

            migrationBuilder.DropIndex(
                name: "IX_AgencySharingHistory_ReportingUnitAgencyID",
                table: "AgencySharingHistory");

            migrationBuilder.DropColumn(
                name: "ReportingUnitCollaborationID",
                table: "CollaborationSharingHistory");

            migrationBuilder.DropColumn(
                name: "ReportingUnitAgencyID",
                table: "AgencySharingHistory");

            migrationBuilder.AddColumn<int>(
                name: "CollaborationSharingID",
                table: "CollaborationSharingHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AgencySharingID",
                table: "AgencySharingHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationSharingHistory_CollaborationSharingID",
                table: "CollaborationSharingHistory",
                column: "CollaborationSharingID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencySharingHistory_AgencySharingID",
                table: "AgencySharingHistory",
                column: "AgencySharingID");

            migrationBuilder.AddForeignKey(
                name: "FK_AgencySharingHistory_AgencySharing_AgencySharingID",
                table: "AgencySharingHistory",
                column: "AgencySharingID",
                principalTable: "AgencySharing",
                principalColumn: "AgencySharingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaborationSharingHistory_CollaborationSharing_CollaborationSharingID",
                table: "CollaborationSharingHistory",
                column: "CollaborationSharingID",
                principalTable: "CollaborationSharing",
                principalColumn: "CollaborationSharingID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgencySharingHistory_AgencySharing_AgencySharingID",
                table: "AgencySharingHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaborationSharingHistory_CollaborationSharing_CollaborationSharingID",
                table: "CollaborationSharingHistory");

            migrationBuilder.DropIndex(
                name: "IX_CollaborationSharingHistory_CollaborationSharingID",
                table: "CollaborationSharingHistory");

            migrationBuilder.DropIndex(
                name: "IX_AgencySharingHistory_AgencySharingID",
                table: "AgencySharingHistory");

            migrationBuilder.DropColumn(
                name: "CollaborationSharingID",
                table: "CollaborationSharingHistory");

            migrationBuilder.DropColumn(
                name: "AgencySharingID",
                table: "AgencySharingHistory");

            migrationBuilder.AddColumn<int>(
                name: "ReportingUnitCollaborationID",
                table: "CollaborationSharingHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ReportingUnitAgencyID",
                table: "AgencySharingHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationSharingHistory_ReportingUnitCollaborationID",
                table: "CollaborationSharingHistory",
                column: "ReportingUnitCollaborationID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencySharingHistory_ReportingUnitAgencyID",
                table: "AgencySharingHistory",
                column: "ReportingUnitAgencyID");

            migrationBuilder.AddForeignKey(
                name: "FK_AgencySharingHistory_Agency_ReportingUnitAgencyID",
                table: "AgencySharingHistory",
                column: "ReportingUnitAgencyID",
                principalTable: "Agency",
                principalColumn: "AgencyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaborationSharingHistory_CollaborationSharing_ReportingUnitCollaborationID",
                table: "CollaborationSharingHistory",
                column: "ReportingUnitCollaborationID",
                principalTable: "CollaborationSharing",
                principalColumn: "CollaborationSharingID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
