using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class RegularReminderRecurrenceDBChanges_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CloseOffsetTypeID",
                table: "QuestionnaireWindow",
                maxLength: 1,
                nullable: true,
                defaultValue: "d");

            migrationBuilder.AddColumn<string>(
                name: "OpenOffsetTypeID",
                table: "QuestionnaireWindow",
                maxLength: 1,
                nullable: true,
                defaultValue: "d");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "QuestionnaireWindow",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<string>(
                name: "ReminderOffsetTypeID",
                table: "QuestionnaireReminderRule",
                maxLength: 1,
                nullable: true,
                defaultValue: "d");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "QuestionnaireReminderRule",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailInviteToCompleteReminders",
                table: "Questionnaire",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "InviteToCompleteSentAt",
                table: "NotifyReminder",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InviteToCompleteReceiver",
                schema: "info",
                columns: table => new
                {
                    InviteToCompleteReceiverID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    Abbrev = table.Column<string>(maxLength: 15, nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InviteToCompleteReceiver", x => x.InviteToCompleteReceiverID);
                });

            migrationBuilder.CreateTable(
                name: "OffsetType",
                schema: "info",
                columns: table => new
                {
                    OffsetTypeID = table.Column<string>(maxLength: 1, nullable: false),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffsetType", x => x.OffsetTypeID);
                });

            migrationBuilder.CreateTable(
                name: "RecurrenceDay",
                schema: "info",
                columns: table => new
                {
                    RecurrenceDayID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    IsWeekDay = table.Column<bool>(nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrenceDay", x => x.RecurrenceDayID);
                });

            migrationBuilder.CreateTable(
                name: "RecurrenceEndType",
                schema: "info",
                columns: table => new
                {
                    RecurrenceEndTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    DisplayLabel = table.Column<string>(maxLength: 15, nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrenceEndType", x => x.RecurrenceEndTypeID);
                });

            migrationBuilder.CreateTable(
                name: "RecurrenceMonth",
                schema: "info",
                columns: table => new
                {
                    RecurrenceMonthID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    NoOfDays = table.Column<int>(nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrenceMonth", x => x.RecurrenceMonthID);
                });

            migrationBuilder.CreateTable(
                name: "RecurrenceOrdinal",
                schema: "info",
                columns: table => new
                {
                    RecurrenceOrdinalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrenceOrdinal", x => x.RecurrenceOrdinalID);
                });

            migrationBuilder.CreateTable(
                name: "RecurrencePattern",
                schema: "info",
                columns: table => new
                {
                    RecurrencePatternID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(maxLength: 15, nullable: false),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrencePattern", x => x.RecurrencePatternID);
                });

            migrationBuilder.CreateTable(
                name: "TimeZones",
                schema: "info",
                columns: table => new
                {
                    TimeZonesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    Abbrev = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeZones", x => x.TimeZonesID);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireInviteToCompleteReceiver",
                columns: table => new
                {
                    QuestionnaireInviteToCompleteReceiverID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    InviteToCompleteReceiverID = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "QuestionnaireRegularReminderRecurrence",
                columns: table => new
                {
                    QuestionnaireRegularReminderRecurrenceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    RecurrencePatternID = table.Column<int>(nullable: false),
                    RecurrenceInterval = table.Column<int>(nullable: false),
                    RecurrenceOrdinalIDs = table.Column<string>(maxLength: 15, nullable: false),
                    RecurrenceDayNameIDs = table.Column<string>(maxLength: 15, nullable: false),
                    RecurrenceDayNoOfMonth = table.Column<int>(nullable: true),
                    RecurrenceMonthIDs = table.Column<string>(maxLength: 15, nullable: false),
                    RecurrenceRangeStartDate = table.Column<DateTime>(nullable: false),
                    RecurrenceRangeEndTypeID = table.Column<int>(nullable: false),
                    RecurrenceRangeEndDate = table.Column<DateTime>(nullable: true),
                    RecurrenceRangeEndInNumber = table.Column<int>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireRegularReminderRecurrence", x => x.QuestionnaireRegularReminderRecurrenceID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireRegularReminderRecurrence_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireRegularReminderRecurrence_RecurrencePattern_RecurrencePatternID",
                        column: x => x.RecurrencePatternID,
                        principalSchema: "info",
                        principalTable: "RecurrencePattern",
                        principalColumn: "RecurrencePatternID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireRegularReminderRecurrence_RecurrenceEndType_RecurrenceRangeEndTypeID",
                        column: x => x.RecurrenceRangeEndTypeID,
                        principalSchema: "info",
                        principalTable: "RecurrenceEndType",
                        principalColumn: "RecurrenceEndTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireRegularReminderTimeRule",
                columns: table => new
                {
                    QuestionnaireRegularReminderTimeRuleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    Time = table.Column<TimeSpan>(nullable: false),
                    TimeZonesID = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireRegularReminderTimeRule", x => x.QuestionnaireRegularReminderTimeRuleID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireRegularReminderTimeRule_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireRegularReminderTimeRule_TimeZones_TimeZonesID",
                        column: x => x.TimeZonesID,
                        principalSchema: "info",
                        principalTable: "TimeZones",
                        principalColumn: "TimeZonesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireWindow_CloseOffsetTypeID",
                table: "QuestionnaireWindow",
                column: "CloseOffsetTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireWindow_OpenOffsetTypeID",
                table: "QuestionnaireWindow",
                column: "OpenOffsetTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireReminderRule_ReminderOffsetTypeID",
                table: "QuestionnaireReminderRule",
                column: "ReminderOffsetTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireInviteToCompleteReceiver_InviteToCompleteReceiverID",
                table: "QuestionnaireInviteToCompleteReceiver",
                column: "InviteToCompleteReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireInviteToCompleteReceiver_QuestionnaireID",
                table: "QuestionnaireInviteToCompleteReceiver",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireRegularReminderRecurrence_QuestionnaireID",
                table: "QuestionnaireRegularReminderRecurrence",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireRegularReminderRecurrence_RecurrencePatternID",
                table: "QuestionnaireRegularReminderRecurrence",
                column: "RecurrencePatternID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireRegularReminderRecurrence_RecurrenceRangeEndTypeID",
                table: "QuestionnaireRegularReminderRecurrence",
                column: "RecurrenceRangeEndTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireRegularReminderTimeRule_QuestionnaireID",
                table: "QuestionnaireRegularReminderTimeRule",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireRegularReminderTimeRule_TimeZonesID",
                table: "QuestionnaireRegularReminderTimeRule",
                column: "TimeZonesID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionnaireReminderRule_OffsetType_ReminderOffsetTypeID",
                table: "QuestionnaireReminderRule",
                column: "ReminderOffsetTypeID",
                principalSchema: "info",
                principalTable: "OffsetType",
                principalColumn: "OffsetTypeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionnaireWindow_OffsetType_CloseOffsetTypeID",
                table: "QuestionnaireWindow",
                column: "CloseOffsetTypeID",
                principalSchema: "info",
                principalTable: "OffsetType",
                principalColumn: "OffsetTypeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionnaireWindow_OffsetType_OpenOffsetTypeID",
                table: "QuestionnaireWindow",
                column: "OpenOffsetTypeID",
                principalSchema: "info",
                principalTable: "OffsetType",
                principalColumn: "OffsetTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionnaireReminderRule_OffsetType_ReminderOffsetTypeID",
                table: "QuestionnaireReminderRule");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionnaireWindow_OffsetType_CloseOffsetTypeID",
                table: "QuestionnaireWindow");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionnaireWindow_OffsetType_OpenOffsetTypeID",
                table: "QuestionnaireWindow");

            migrationBuilder.DropTable(
                name: "QuestionnaireInviteToCompleteReceiver");

            migrationBuilder.DropTable(
                name: "QuestionnaireRegularReminderRecurrence");

            migrationBuilder.DropTable(
                name: "QuestionnaireRegularReminderTimeRule");

            migrationBuilder.DropTable(
                name: "OffsetType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "RecurrenceDay",
                schema: "info");

            migrationBuilder.DropTable(
                name: "RecurrenceMonth",
                schema: "info");

            migrationBuilder.DropTable(
                name: "RecurrenceOrdinal",
                schema: "info");

            migrationBuilder.DropTable(
                name: "InviteToCompleteReceiver",
                schema: "info");

            migrationBuilder.DropTable(
                name: "RecurrencePattern",
                schema: "info");

            migrationBuilder.DropTable(
                name: "RecurrenceEndType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "TimeZones",
                schema: "info");

            migrationBuilder.DropIndex(
                name: "IX_QuestionnaireWindow_CloseOffsetTypeID",
                table: "QuestionnaireWindow");

            migrationBuilder.DropIndex(
                name: "IX_QuestionnaireWindow_OpenOffsetTypeID",
                table: "QuestionnaireWindow");

            migrationBuilder.DropIndex(
                name: "IX_QuestionnaireReminderRule_ReminderOffsetTypeID",
                table: "QuestionnaireReminderRule");

            migrationBuilder.DropColumn(
                name: "CloseOffsetTypeID",
                table: "QuestionnaireWindow");

            migrationBuilder.DropColumn(
                name: "OpenOffsetTypeID",
                table: "QuestionnaireWindow");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "QuestionnaireWindow");

            migrationBuilder.DropColumn(
                name: "ReminderOffsetTypeID",
                table: "QuestionnaireReminderRule");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "QuestionnaireReminderRule");

            migrationBuilder.DropColumn(
                name: "IsEmailInviteToCompleteReminders",
                table: "Questionnaire");

            migrationBuilder.DropColumn(
                name: "InviteToCompleteSentAt",
                table: "NotifyReminder");
        }
    }
}
