using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class NotesTableAddVoiceTypeFKID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddedByVoiceTypeID",
                table: "Note",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VoiceTypeFKID",
                table: "Note",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedByVoiceTypeID",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "VoiceTypeFKID",
                table: "Note");
        }
    }
}
