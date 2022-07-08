using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class SMSConsentStopColumn_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SMSConsentStoppedON",
                table: "PersonSupport",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SMSConsentStoppedON",
                table: "Person",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SMSConsentStoppedON",
                table: "PersonSupport");

            migrationBuilder.DropColumn(
                name: "SMSConsentStoppedON",
                table: "Person");
        }
    }
}
