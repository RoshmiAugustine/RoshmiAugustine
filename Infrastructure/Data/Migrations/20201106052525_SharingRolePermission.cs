using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class SharingRolePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SharingRolePermission",
                schema: "info",
                columns: table => new
                {
                    SharingRolePermissionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemRolePermissionID = table.Column<int>(nullable: false),
                    AgencySharingPolicyID = table.Column<int>(nullable: false),
                    CollaborationSharingPolicyID = table.Column<int>(nullable: false),
                    AllowInactiveAccess = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharingRolePermission", x => x.SharingRolePermissionID);
                    table.ForeignKey(
                        name: "FK_SharingRolePermission_AgencySharingPolicy_AgencySharingPolicyID",
                        column: x => x.AgencySharingPolicyID,
                        principalTable: "AgencySharingPolicy",
                        principalColumn: "AgencySharingPolicyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharingRolePermission_CollaborationSharingPolicy_CollaborationSharingPolicyID",
                        column: x => x.CollaborationSharingPolicyID,
                        principalTable: "CollaborationSharingPolicy",
                        principalColumn: "CollaborationSharingPolicyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharingRolePermission_SystemRolePermission_SystemRolePermissionID",
                        column: x => x.SystemRolePermissionID,
                        principalSchema: "info",
                        principalTable: "SystemRolePermission",
                        principalColumn: "SystemRolePermissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharingRolePermission_AgencySharingPolicyID",
                schema: "info",
                table: "SharingRolePermission",
                column: "AgencySharingPolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_SharingRolePermission_CollaborationSharingPolicyID",
                schema: "info",
                table: "SharingRolePermission",
                column: "CollaborationSharingPolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_SharingRolePermission_SystemRolePermissionID",
                schema: "info",
                table: "SharingRolePermission",
                column: "SystemRolePermissionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SharingRolePermission",
                schema: "info");
        }
    }
}
