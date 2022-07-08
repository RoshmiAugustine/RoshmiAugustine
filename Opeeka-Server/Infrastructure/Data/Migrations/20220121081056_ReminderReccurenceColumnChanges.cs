using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class ReminderReccurenceColumnChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OffsetTypeID",
                schema: "info",
                table: "OffsetType",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "d",
                oldClrType: typeof(string),
                oldMaxLength: 1,
                oldDefaultValue: "d");

            migrationBuilder.AlterColumn<string>(
                name: "OpenOffsetTypeID",
                table: "QuestionnaireWindow",
                type: "char(1)",
                maxLength: 1,
                nullable: true,
                defaultValue: "d",
                oldClrType: typeof(string),
                oldMaxLength: 1,
                oldNullable: true,
                oldDefaultValue: "d");

            migrationBuilder.AlterColumn<string>(
                name: "CloseOffsetTypeID",
                table: "QuestionnaireWindow",
                type: "char(1)",
                maxLength: 1,
                nullable: true,
                defaultValue: "d",
                oldClrType: typeof(string),
                oldMaxLength: 1,
                oldNullable: true,
                oldDefaultValue: "d");

            migrationBuilder.AlterColumn<string>(
                name: "ReminderOffsetTypeID",
                table: "QuestionnaireReminderRule",
                type: "char(1)",
                maxLength: 1,
                nullable: true,
                defaultValue: "d",
                oldClrType: typeof(string),
                oldMaxLength: 1,
                oldNullable: true,
                oldDefaultValue: "d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OffsetTypeID",
                schema: "info",
                table: "OffsetType",
                maxLength: 1,
                nullable: false,
                defaultValue: "d",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldDefaultValue: "d");

            migrationBuilder.AlterColumn<string>(
                name: "OpenOffsetTypeID",
                table: "QuestionnaireWindow",
                maxLength: 1,
                nullable: true,
                defaultValue: "d",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldNullable: true,
                oldDefaultValue: "d");

            migrationBuilder.AlterColumn<string>(
                name: "CloseOffsetTypeID",
                table: "QuestionnaireWindow",
                maxLength: 1,
                nullable: true,
                defaultValue: "d",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldNullable: true,
                oldDefaultValue: "d");

            migrationBuilder.AlterColumn<string>(
                name: "ReminderOffsetTypeID",
                table: "QuestionnaireReminderRule",
                maxLength: 1,
                nullable: true,
                defaultValue: "d",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldNullable: true,
                oldDefaultValue: "d");
        }
    }
}
