using Microsoft.EntityFrameworkCore.Migrations;

namespace DeadLinerWebApp.DAL.Migrations
{
    public partial class ChangeTaskModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersTasks",
                table: "UsersTasks");

            migrationBuilder.AlterColumn<string>(
                name: "TaskStatus",
                table: "UsersTasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersTasks",
                table: "UsersTasks",
                columns: new[] { "UserId", "TaskId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersTasks",
                table: "UsersTasks");

            migrationBuilder.AlterColumn<string>(
                name: "TaskStatus",
                table: "UsersTasks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersTasks",
                table: "UsersTasks",
                columns: new[] { "UserId", "TaskId", "TaskStatus" });
        }
    }
}
