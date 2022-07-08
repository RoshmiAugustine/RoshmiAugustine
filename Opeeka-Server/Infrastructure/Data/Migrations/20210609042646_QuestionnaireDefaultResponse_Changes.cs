using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class QuestionnaireDefaultResponse_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasDefaultResponseRule",
                table: "Questionnaire",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "QuestionnaireDefaultResponseRule",
                columns: table => new
                {
                    QuestionnaireDefaultResponseRuleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    ClonedQuestionnaireDefaultResponseRuleID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireDefaultResponseRule", x => x.QuestionnaireDefaultResponseRuleID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireDefaultResponseRule_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireDefaultResponseRule_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireDefaultResponseRuleAction",
                columns: table => new
                {
                    QuestionnaireDefaultResponseRuleActionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireDefaultResponseRuleID = table.Column<int>(nullable: false),
                    QuestionnaireItemID = table.Column<int>(nullable: true),
                    DefaultResponseID = table.Column<int>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireDefaultResponseRuleAction", x => x.QuestionnaireDefaultResponseRuleActionID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireDefaultResponseRuleAction_Response_DefaultResponseID",
                        column: x => x.DefaultResponseID,
                        principalTable: "Response",
                        principalColumn: "ResponseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireDefaultResponseRuleAction_QuestionnaireDefaultResponseRule_QuestionnaireDefaultResponseRuleID",
                        column: x => x.QuestionnaireDefaultResponseRuleID,
                        principalTable: "QuestionnaireDefaultResponseRule",
                        principalColumn: "QuestionnaireDefaultResponseRuleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireDefaultResponseRuleAction_QuestionnaireItem_QuestionnaireItemID",
                        column: x => x.QuestionnaireItemID,
                        principalTable: "QuestionnaireItem",
                        principalColumn: "QuestionnaireItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireDefaultResponseRuleAction_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireDefaultResponseRuleCondition",
                columns: table => new
                {
                    QuestionnaireDefaultResponseRuleConditionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    QuestionnaireItemID = table.Column<int>(nullable: false),
                    ComparisonOperator = table.Column<string>(nullable: true),
                    ComparisonValue = table.Column<int>(nullable: false),
                    QuestionnaireDefaultResponseRuleID = table.Column<int>(nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    JoiningOperator = table.Column<string>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireDefaultResponseRuleCondition", x => x.QuestionnaireDefaultResponseRuleConditionID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireDefaultResponseRuleCondition_QuestionnaireDefaultResponseRule_QuestionnaireDefaultResponseRuleID",
                        column: x => x.QuestionnaireDefaultResponseRuleID,
                        principalTable: "QuestionnaireDefaultResponseRule",
                        principalColumn: "QuestionnaireDefaultResponseRuleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireDefaultResponseRuleCondition_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireDefaultResponseRuleCondition_QuestionnaireItem_QuestionnaireItemID",
                        column: x => x.QuestionnaireItemID,
                        principalTable: "QuestionnaireItem",
                        principalColumn: "QuestionnaireItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireDefaultResponseRuleCondition_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireDefaultResponseRule_QuestionnaireID",
                table: "QuestionnaireDefaultResponseRule",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireDefaultResponseRule_UpdateUserID",
                table: "QuestionnaireDefaultResponseRule",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireDefaultResponseRuleAction_DefaultResponseID",
                table: "QuestionnaireDefaultResponseRuleAction",
                column: "DefaultResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireDefaultResponseRuleAction_QuestionnaireDefaultResponseRuleID",
                table: "QuestionnaireDefaultResponseRuleAction",
                column: "QuestionnaireDefaultResponseRuleID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireDefaultResponseRuleAction_QuestionnaireItemID",
                table: "QuestionnaireDefaultResponseRuleAction",
                column: "QuestionnaireItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireDefaultResponseRuleAction_UpdateUserID",
                table: "QuestionnaireDefaultResponseRuleAction",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireDefaultResponseRuleCondition_QuestionnaireDefaultResponseRuleID",
                table: "QuestionnaireDefaultResponseRuleCondition",
                column: "QuestionnaireDefaultResponseRuleID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireDefaultResponseRuleCondition_QuestionnaireID",
                table: "QuestionnaireDefaultResponseRuleCondition",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireDefaultResponseRuleCondition_QuestionnaireItemID",
                table: "QuestionnaireDefaultResponseRuleCondition",
                column: "QuestionnaireItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireDefaultResponseRuleCondition_UpdateUserID",
                table: "QuestionnaireDefaultResponseRuleCondition",
                column: "UpdateUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionnaireDefaultResponseRuleAction");

            migrationBuilder.DropTable(
                name: "QuestionnaireDefaultResponseRuleCondition");

            migrationBuilder.DropTable(
                name: "QuestionnaireDefaultResponseRule");

            migrationBuilder.DropColumn(
                name: "HasDefaultResponseRule",
                table: "Questionnaire");
        }
    }
}
