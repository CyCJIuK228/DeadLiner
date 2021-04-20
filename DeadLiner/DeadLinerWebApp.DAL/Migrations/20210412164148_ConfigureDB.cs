using Microsoft.EntityFrameworkCore.Migrations;

namespace DeadLinerWebApp.DAL.Migrations
{
    public partial class ConfigureDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TasksHubs_TasksHubsId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "TasksHubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersTasks",
                table: "UsersTasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TasksHubsId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TasksHubsId",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "TaskStatus",
                table: "UsersTasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HubId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersTasks",
                table: "UsersTasks",
                columns: new[] { "UserId", "TaskId", "TaskStatus" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 1,
                column: "HubId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 2,
                column: "HubId",
                value: 1);

            migrationBuilder.InsertData(
                table: "UsersTasks",
                columns: new[] { "TaskId", "TaskStatus", "UserId", "UsersTasksId" },
                values: new object[,]
                {
                    { 1, "New", 1, 1 },
                    { 2, "InProgress", 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_HubId",
                table: "Tasks",
                column: "HubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Hubs_HubId",
                table: "Tasks",
                column: "HubId",
                principalTable: "Hubs",
                principalColumn: "HubId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Hubs_HubId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersTasks",
                table: "UsersTasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_HubId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "UsersTasks",
                keyColumns: new[] { "TaskId", "TaskStatus", "UserId" },
                keyColumnTypes: new[] { "int", "nvarchar(450)", "int" },
                keyValues: new object[] { 1, "New", 1 });

            migrationBuilder.DeleteData(
                table: "UsersTasks",
                keyColumns: new[] { "TaskId", "TaskStatus", "UserId" },
                keyColumnTypes: new[] { "int", "nvarchar(450)", "int" },
                keyValues: new object[] { 2, "InProgress", 1 });

            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "UsersTasks");

            migrationBuilder.DropColumn(
                name: "HubId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "TasksHubsId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersTasks",
                table: "UsersTasks",
                columns: new[] { "UserId", "TaskId" });

            migrationBuilder.CreateTable(
                name: "TasksHubs",
                columns: table => new
                {
                    TasksHubsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksHubs", x => x.TasksHubsId);
                    table.ForeignKey(
                        name: "FK_TasksHubs_Hubs_HubId",
                        column: x => x.HubId,
                        principalTable: "Hubs",
                        principalColumn: "HubId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Tasks_TasksHubsId",
                table: "Tasks",
                column: "TasksHubsId");

            migrationBuilder.CreateIndex(
                name: "IX_TasksHubs_HubId",
                table: "TasksHubs",
                column: "HubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TasksHubs_TasksHubsId",
                table: "Tasks",
                column: "TasksHubsId",
                principalTable: "TasksHubs",
                principalColumn: "TasksHubsId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
