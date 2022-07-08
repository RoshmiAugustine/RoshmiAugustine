using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class InviteToCompleteTableRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionnaireInviteToCompleteReceiver");

            migrationBuilder.AddColumn<int>(
                name: "QuestionnaireID",
                table: "ReminderInviteToComplete",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InviteToCompleteReceiverIds",
                table: "Questionnaire",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionnaireID",
                table: "ReminderInviteToComplete");

            migrationBuilder.DropColumn(
                name: "InviteToCompleteReceiverIds",
                table: "Questionnaire");

            migrationBuilder.CreateTable(
                name: "QuestionnaireInviteToCompleteReceiver",
                columns: table => new
                {
                    QuestionnaireInviteToCompleteReceiverID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InviteToCompleteReceiverID = table.Column<int>(type: "int", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    QuestionnaireID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireInviteToCompleteReceiver", x => x.QuestionnaireInviteToCompleteReceiverID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireInviteToCompleteReceiver_InviteToCompleteReceiver_InviteToCompleteReceiverID",
                        column: x => x.InviteToCompleteReceiverID,
                        principalSchema: "info",
                        principalTable: "InviteToCompleteReceiver",
                        principalColumn: "InviteToCompleteReceiverID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireInviteToCompleteReceiver_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireInviteToCompleteReceiver_InviteToCompleteReceiverID",
                table: "QuestionnaireInviteToCompleteReceiver",
                column: "InviteToCompleteReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireInviteToCompleteReceiver_QuestionnaireID",
                table: "QuestionnaireInviteToCompleteReceiver",
                column: "QuestionnaireID");
        }
    }
}
