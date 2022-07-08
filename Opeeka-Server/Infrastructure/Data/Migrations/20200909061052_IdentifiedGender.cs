using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class IdentifiedGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentifiedGender",
                schema: "info",
                columns: table => new
                {
                    IdentifiedGenderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentifiedGender", x => x.IdentifiedGenderID);
                    table.ForeignKey(
                        name: "FK_IdentifiedGender_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentifiedGender_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentifiedGender_AgencyID",
                schema: "info",
                table: "IdentifiedGender",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_IdentifiedGender_UpdateUserID",
                schema: "info",
                table: "IdentifiedGender",
                column: "UpdateUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentifiedGender",
                schema: "info");
        }
    }
}
