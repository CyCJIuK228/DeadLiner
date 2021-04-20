using Microsoft.EntityFrameworkCore.Migrations;

namespace DeadLinerWebApp.DAL.Migrations
{
    public partial class SeedCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecoveryCode_Users_UserId",
                table: "RecoveryCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecoveryCode",
                table: "RecoveryCode");

            migrationBuilder.RenameTable(
                name: "RecoveryCode",
                newName: "RecoveryCodes");

            migrationBuilder.RenameIndex(
                name: "IX_RecoveryCode_UserId",
                table: "RecoveryCodes",
                newName: "IX_RecoveryCodes_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RecoveryCodes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecoveryCodes",
                table: "RecoveryCodes",
                column: "RecoveryCodeId");

            migrationBuilder.InsertData(
                table: "RecoveryCodes",
                columns: new[] { "RecoveryCodeId", "Code", "UserId" },
                values: new object[] { 1, "test", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_RecoveryCodes_Users_UserId",
                table: "RecoveryCodes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecoveryCodes_Users_UserId",
                table: "RecoveryCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecoveryCodes",
                table: "RecoveryCodes");

            migrationBuilder.DeleteData(
                table: "RecoveryCodes",
                keyColumn: "RecoveryCodeId",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "RecoveryCodes",
                newName: "RecoveryCode");

            migrationBuilder.RenameIndex(
                name: "IX_RecoveryCodes_UserId",
                table: "RecoveryCode",
                newName: "IX_RecoveryCode_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RecoveryCode",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecoveryCode",
                table: "RecoveryCode",
                column: "RecoveryCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecoveryCode_Users_UserId",
                table: "RecoveryCode",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
