using Microsoft.EntityFrameworkCore.Migrations;

namespace DeadLinerWebApp.DAL.Migrations
{
    public partial class AddTaskMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resources = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                });


            migrationBuilder.CreateTable(
                name: "TasksHubs",
                columns: table => new
                {
                    HubId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    TasksHubsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksHubs", x => new { x.TaskId, x.HubId });
                    table.ForeignKey(
                        name: "FK_TasksHubs_Hubs_HubId",
                        column: x => x.HubId,
                        principalTable: "Hubs",
                        principalColumn: "HubId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TasksHubs_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });


            

            migrationBuilder.CreateTable(
                name: "UsersTasks",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    UsersTasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTasks", x => new { x.TaskId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
            name: "Invites",
            columns: table => new
            {
                HubId = table.Column<int>(type: "int", nullable: false),
                UserId = table.Column<int>(type: "int", nullable: false),
                InvitesId = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Invites", x => new { x.UserId, x.HubId, x.Status });
                table.ForeignKey(
                    name: "FK_Invites_Hubs_HubId",
                    column: x => x.HubId,
                    principalTable: "Hubs",
                    principalColumn: "HubId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Invites_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Cascade);
            });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TasksHubs");

            migrationBuilder.DropTable(
                name: "UsersTasks");

            migrationBuilder.DropTable(
                name: "Invites");

        }
    }
}
