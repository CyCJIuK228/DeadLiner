using Microsoft.EntityFrameworkCore.Migrations;

namespace DeadLinerWebApp.DAL.Migrations
{
    public partial class AddDescriptionToHub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Hubs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Hubs");

            migrationBuilder.UpdateData(
                table: "Hubs",
                keyColumn: "HubId",
                keyValue: 1,
                columns: new[] { "Code", "Name" },
                values: new object[] { "testCodeHub", "testNameHub" });
        }
    }
}
