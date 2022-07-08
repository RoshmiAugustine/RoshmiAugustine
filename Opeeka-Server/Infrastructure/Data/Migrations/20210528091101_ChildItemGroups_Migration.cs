using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class ChildItemGroups_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DisplayChildItem",
                table: "Response",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ParentItemID",
                table: "Item",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionnaireItemID",
                table: "AssessmentResponse",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GroupNumber",
                table: "AssessmentResponse",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemID",
                table: "AssessmentResponse",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ParentItemID",
                table: "Item",
                column: "ParentItemID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponse_ItemID",
                table: "AssessmentResponse",
                column: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentResponse_Item_ItemID",
                table: "AssessmentResponse",
                column: "ItemID",
                principalTable: "Item",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Item_ParentItemID",
                table: "Item",
                column: "ParentItemID",
                principalTable: "Item",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentResponse_Item_ItemID",
                table: "AssessmentResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Item_ParentItemID",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ParentItemID",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentResponse_ItemID",
                table: "AssessmentResponse");

            migrationBuilder.DropColumn(
                name: "DisplayChildItem",
                table: "Response");

            migrationBuilder.DropColumn(
                name: "ParentItemID",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "GroupNumber",
                table: "AssessmentResponse");

            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "AssessmentResponse");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionnaireItemID",
                table: "AssessmentResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
