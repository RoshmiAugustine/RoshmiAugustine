using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class SharingPolicyLookupsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaborationSharingPolicy_CollaborationSharing_CollaborationSharingID",
                table: "CollaborationSharingPolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaborationSharingPolicy_SharingPolicy_SharingPolicyID",
                table: "CollaborationSharingPolicy");

            migrationBuilder.DropIndex(
                name: "IX_CollaborationSharingPolicy_CollaborationSharingID",
                table: "CollaborationSharingPolicy");

            migrationBuilder.DropIndex(
                name: "IX_CollaborationSharingPolicy_SharingPolicyID",
                table: "CollaborationSharingPolicy");

            migrationBuilder.DropColumn(
                name: "CollaborationSharingID",
                table: "CollaborationSharingPolicy");

            migrationBuilder.DropColumn(
                name: "SharingPolicyID",
                table: "CollaborationSharingPolicy");

            migrationBuilder.DropColumn(
                name: "AgencySharingID",
                table: "AgencySharingPolicy");

            migrationBuilder.DropColumn(
                name: "SharingPolicyID",
                table: "AgencySharingPolicy");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "CollaborationSharingPolicy",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CollaborationSharingPolicy",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CollaborationSharingPolicy",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CollaborationSharingPolicy",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "CollaborationSharingPolicy",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "AgencySharingPolicy",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AgencySharingPolicy",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AgencySharingPolicy",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AgencySharingPolicy",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "CollaborationSharingPolicy");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CollaborationSharingPolicy");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CollaborationSharingPolicy");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CollaborationSharingPolicy");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "CollaborationSharingPolicy");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "AgencySharingPolicy");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AgencySharingPolicy");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AgencySharingPolicy");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AgencySharingPolicy");

            migrationBuilder.AddColumn<int>(
                name: "CollaborationSharingID",
                table: "CollaborationSharingPolicy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SharingPolicyID",
                table: "CollaborationSharingPolicy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AgencySharingID",
                table: "AgencySharingPolicy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SharingPolicyID",
                table: "AgencySharingPolicy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationSharingPolicy_CollaborationSharingID",
                table: "CollaborationSharingPolicy",
                column: "CollaborationSharingID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationSharingPolicy_SharingPolicyID",
                table: "CollaborationSharingPolicy",
                column: "SharingPolicyID");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaborationSharingPolicy_CollaborationSharing_CollaborationSharingID",
                table: "CollaborationSharingPolicy",
                column: "CollaborationSharingID",
                principalTable: "CollaborationSharing",
                principalColumn: "CollaborationSharingID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaborationSharingPolicy_SharingPolicy_SharingPolicyID",
                table: "CollaborationSharingPolicy",
                column: "SharingPolicyID",
                principalSchema: "info",
                principalTable: "SharingPolicy",
                principalColumn: "SharingPolicyID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
