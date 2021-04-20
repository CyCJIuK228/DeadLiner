using Microsoft.EntityFrameworkCore.Migrations;

namespace DeadLinerWebApp.DAL.Migrations
{
    public partial class AddRecoveryCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecoveryCode",
                columns: table => new
                {
                    RecoveryCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecoveryCode", x => x.RecoveryCodeId);
                    table.ForeignKey(
                        name: "FK_RecoveryCode_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TasksHubs",
                columns: new[] { "TasksHubsId", "HubId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "TasksHubs",
                columns: new[] { "TasksHubsId", "HubId" },
                values: new object[] { 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryCode_UserId",
                table: "RecoveryCode",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecoveryCode");

            migrationBuilder.DeleteData(
                table: "TasksHubs",
                keyColumn: "TasksHubsId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TasksHubs",
                keyColumn: "TasksHubsId",
                keyValue: 2);
        }
    }
}
