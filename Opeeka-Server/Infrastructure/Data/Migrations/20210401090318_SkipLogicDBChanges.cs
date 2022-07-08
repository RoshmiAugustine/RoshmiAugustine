using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class SkipLogicDBChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasSkipLogic",
                table: "Questionnaire",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "QuestionnaireSkipLogicRule",
                columns: table => new
                {
                    QuestionnaireSkipLogicRuleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    ClonedQuestionnaireSkipLogicRuleID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireSkipLogicRule", x => x.QuestionnaireSkipLogicRuleID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSkipLogicRule_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSkipLogicRule_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActionLevel",
                schema: "info",
                columns: table => new
                {
                    ActionLevelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLevel", x => x.ActionLevelID);
                });

            migrationBuilder.CreateTable(
                name: "ActionType",
                schema: "info",
                columns: table => new
                {
                    ActionTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionType", x => x.ActionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireSkipLogicRuleCondition",
                columns: table => new
                {
                    QuestionnaireSkipLogicRuleConditionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireItemID = table.Column<int>(nullable: false),
                    ComparisonOperator = table.Column<string>(nullable: true),
                    ComparisonValue = table.Column<int>(nullable: false),
                    QuestionnaireSkipLogicRuleID = table.Column<int>(nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    JoiningOperator = table.Column<string>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireSkipLogicRuleCondition", x => x.QuestionnaireSkipLogicRuleConditionID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSkipLogicRuleCondition_QuestionnaireItem_QuestionnaireItemID",
                        column: x => x.QuestionnaireItemID,
                        principalTable: "QuestionnaireItem",
                        principalColumn: "QuestionnaireItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSkipLogicRuleCondition_QuestionnaireSkipLogicRule_QuestionnaireSkipLogicRuleID",
                        column: x => x.QuestionnaireSkipLogicRuleID,
                        principalTable: "QuestionnaireSkipLogicRule",
                        principalColumn: "QuestionnaireSkipLogicRuleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSkipLogicRuleCondition_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireSkipLogicRuleAction",
                columns: table => new
                {
                    QuestionnaireSkipLogicRuleActionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireSkipLogicRuleID = table.Column<int>(nullable: false),
                    ActionLevelID = table.Column<int>(nullable: false),
                    QuestionnaireItemID = table.Column<int>(nullable: true),
                    CategoryID = table.Column<int>(nullable: true),
                    ActionTypeID = table.Column<int>(nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireSkipLogicRuleAction", x => x.QuestionnaireSkipLogicRuleActionID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSkipLogicRuleAction_ActionLevel_ActionLevelID",
                        column: x => x.ActionLevelID,
                        principalSchema: "info",
                        principalTable: "ActionLevel",
                        principalColumn: "ActionLevelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSkipLogicRuleAction_ActionType_ActionTypeID",
                        column: x => x.ActionTypeID,
                        principalSchema: "info",
                        principalTable: "ActionType",
                        principalColumn: "ActionTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSkipLogicRuleAction_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalSchema: "info",
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSkipLogicRuleAction_QuestionnaireItem_QuestionnaireItemID",
                        column: x => x.QuestionnaireItemID,
                        principalTable: "QuestionnaireItem",
                        principalColumn: "QuestionnaireItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSkipLogicRuleAction_QuestionnaireSkipLogicRule_QuestionnaireSkipLogicRuleID",
                        column: x => x.QuestionnaireSkipLogicRuleID,
                        principalTable: "QuestionnaireSkipLogicRule",
                        principalColumn: "QuestionnaireSkipLogicRuleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireSkipLogicRuleAction_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRule_QuestionnaireID",
                table: "QuestionnaireSkipLogicRule",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRule_UpdateUserID",
                table: "QuestionnaireSkipLogicRule",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRuleAction_ActionLevelID",
                table: "QuestionnaireSkipLogicRuleAction",
                column: "ActionLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRuleAction_ActionTypeID",
                table: "QuestionnaireSkipLogicRuleAction",
                column: "ActionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRuleAction_CategoryID",
                table: "QuestionnaireSkipLogicRuleAction",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRuleAction_QuestionnaireItemID",
                table: "QuestionnaireSkipLogicRuleAction",
                column: "QuestionnaireItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRuleAction_QuestionnaireSkipLogicRuleID",
                table: "QuestionnaireSkipLogicRuleAction",
                column: "QuestionnaireSkipLogicRuleID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRuleAction_UpdateUserID",
                table: "QuestionnaireSkipLogicRuleAction",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRuleCondition_QuestionnaireItemID",
                table: "QuestionnaireSkipLogicRuleCondition",
                column: "QuestionnaireItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRuleCondition_QuestionnaireSkipLogicRuleID",
                table: "QuestionnaireSkipLogicRuleCondition",
                column: "QuestionnaireSkipLogicRuleID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireSkipLogicRuleCondition_UpdateUserID",
                table: "QuestionnaireSkipLogicRuleCondition",
                column: "UpdateUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionnaireSkipLogicRuleAction");

            migrationBuilder.DropTable(
                name: "QuestionnaireSkipLogicRuleCondition");

            migrationBuilder.DropTable(
                name: "ActionLevel",
                schema: "info");

            migrationBuilder.DropTable(
                name: "ActionType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "QuestionnaireSkipLogicRule");

            migrationBuilder.DropColumn(
                name: "HasSkipLogic",
                table: "Questionnaire");
        }
    }
}
