using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class EmailDetail_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailDetail",
                columns: table => new
                {
                    EmailDetailID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(nullable: true),
                    HelperID = table.Column<int>(nullable: true),
                    AgencyID = table.Column<long>(nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    EmailAttributes = table.Column<string>(nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    UpdateUserID = table.Column<int>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailDetail", x => x.EmailDetailID);
                    table.ForeignKey(
                        name: "FK_EmailDetail_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailDetail_Helper_HelperID",
                        column: x => x.HelperID,
                        principalTable: "Helper",
                        principalColumn: "HelperID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailDetail_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailDetail_AgencyID",
                table: "EmailDetail",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailDetail_HelperID",
                table: "EmailDetail",
                column: "HelperID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailDetail_PersonID",
                table: "EmailDetail",
                column: "PersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailDetail");
        }
    }
}
