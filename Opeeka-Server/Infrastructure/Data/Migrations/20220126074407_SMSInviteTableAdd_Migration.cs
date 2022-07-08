using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class SMSInviteTableAdd_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssessmentGUID",
                table: "AssessmentEmailLinkDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotifyReminderID",
                table: "Assessment",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SMSDetail",
                columns: table => new
                {
                    SMSDetailID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(nullable: true),
                    HelperID = table.Column<int>(nullable: true),
                    AgencyID = table.Column<long>(nullable: true),
                    PhoneNumber = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    SMSAttributes = table.Column<string>(nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    UpdateUserID = table.Column<int>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    URL = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    FKeyValue = table.Column<int>(nullable: true)
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
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSDetail");

            migrationBuilder.DropColumn(
                name: "AssessmentGUID",
                table: "AssessmentEmailLinkDetails");

            migrationBuilder.DropColumn(
                name: "NotifyReminderID",
                table: "Assessment");
        }
    }
}
