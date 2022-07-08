using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class Add_AssessmentAttachmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssessmentResponseAttachment",
                columns: table => new
                {
                    AssessmentResponseAttachmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentResponseID = table.Column<int>(nullable: false),
                    BlobURL = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AddedByVoiceTypeID = table.Column<int>(nullable: true),
                    VoiceTypeFKID = table.Column<long>(nullable: true),
                    AssessmentResponseFileGUID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentResponseAttachment", x => x.AssessmentResponseAttachmentID);
                    table.ForeignKey(
                        name: "FK_AssessmentResponseAttachment_AssessmentResponse_AssessmentResponseID",
                        column: x => x.AssessmentResponseID,
                        principalTable: "AssessmentResponse",
                        principalColumn: "AssessmentResponseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentResponseAttachment_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponseAttachment_AssessmentResponseID",
                table: "AssessmentResponseAttachment",
                column: "AssessmentResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponseAttachment_UpdateUserID",
                table: "AssessmentResponseAttachment",
                column: "UpdateUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentResponseAttachment");
        }
    }
}
