using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class AssessmentEmailOtpEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssessmentEmailOtp",
                columns: table => new
                {
                    AssessmentEmailOtpID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentEmailLinkDetailsID = table.Column<int>(nullable: false),
                    Otp = table.Column<string>(nullable: true),
                    ExpiryTime = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("AssessmentEmailOtpID", x => x.AssessmentEmailOtpID);
                    table.ForeignKey(
                        name: "FK_AssessmentEmailOtp_AssessmentEmailLinkDetails_AssessmentEmailLinkDetailsID",
                        column: x => x.AssessmentEmailLinkDetailsID,
                        principalTable: "AssessmentEmailLinkDetails",
                        principalColumn: "AssessmentEmailLinkDetailsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentEmailOtp_AssessmentEmailLinkDetailsID",
                table: "AssessmentEmailOtp",
                column: "AssessmentEmailLinkDetailsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentEmailOtp");
        }
    }
}
