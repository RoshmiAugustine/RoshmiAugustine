using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class ReminderInviteToCompleteAdd_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailDetail_PersonSupport_PersonSupportID",
                table: "EmailDetail");

            migrationBuilder.DropTable(
                name: "SMSDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmailDetail_PersonSupportID",
                table: "EmailDetail");

            migrationBuilder.DropColumn(
                name: "PersonSupportID",
                table: "EmailDetail");

            migrationBuilder.CreateTable(
                name: "ReminderInviteToComplete",
                columns: table => new
                {
                    ReminderInviteToCompleteID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotifyReminderID = table.Column<int>(nullable: false),
                    AssessmentID = table.Column<int>(nullable: false),
                    InviteToCompleteReceiverID = table.Column<int>(nullable: false),
                    PersonID = table.Column<long>(nullable: true),
                    HelperID = table.Column<int>(nullable: true),
                    PersonSupportID = table.Column<int>(nullable: true),
                    Attributes = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    AssessmentURL = table.Column<string>(nullable: true),
                    TypeOfInviteSend = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    UpdateUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderInviteToComplete", x => x.ReminderInviteToCompleteID);
                    table.ForeignKey(
                        name: "FK_ReminderInviteToComplete_Assessment_AssessmentID",
                        column: x => x.AssessmentID,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReminderInviteToComplete_Helper_HelperID",
                        column: x => x.HelperID,
                        principalTable: "Helper",
                        principalColumn: "HelperID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReminderInviteToComplete_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReminderInviteToComplete_PersonSupport_PersonSupportID",
                        column: x => x.PersonSupportID,
                        principalTable: "PersonSupport",
                        principalColumn: "PersonSupportID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReminderInviteToComplete_AssessmentID",
                table: "ReminderInviteToComplete",
                column: "AssessmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ReminderInviteToComplete_HelperID",
                table: "ReminderInviteToComplete",
                column: "HelperID");

            migrationBuilder.CreateIndex(
                name: "IX_ReminderInviteToComplete_PersonID",
                table: "ReminderInviteToComplete",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_ReminderInviteToComplete_PersonSupportID",
                table: "ReminderInviteToComplete",
                column: "PersonSupportID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReminderInviteToComplete");

            migrationBuilder.AddColumn<int>(
                name: "PersonSupportID",
                table: "EmailDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SMSDetail",
                columns: table => new
                {
                    SMSDetailID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKeyValue = table.Column<int>(type: "int", nullable: true),
                    HelperID = table.Column<int>(type: "int", nullable: true),
                    PersonID = table.Column<long>(type: "bigint", nullable: true),
                    PersonSupportID = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    SMSAttributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSDetail", x => x.SMSDetailID);
                    table.ForeignKey(
                        name: "FK_SMSDetail_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMSDetail_Helper_HelperID",
                        column: x => x.HelperID,
                        principalTable: "Helper",
                        principalColumn: "HelperID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMSDetail_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMSDetail_PersonSupport_PersonSupportID",
                        column: x => x.PersonSupportID,
                        principalTable: "PersonSupport",
                        principalColumn: "PersonSupportID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailDetail_PersonSupportID",
                table: "EmailDetail",
                column: "PersonSupportID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSDetail_AgencyID",
                table: "SMSDetail",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSDetail_HelperID",
                table: "SMSDetail",
                column: "HelperID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSDetail_PersonID",
                table: "SMSDetail",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSDetail_PersonSupportID",
                table: "SMSDetail",
                column: "PersonSupportID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailDetail_PersonSupport_PersonSupportID",
                table: "EmailDetail",
                column: "PersonSupportID",
                principalTable: "PersonSupport",
                principalColumn: "PersonSupportID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
