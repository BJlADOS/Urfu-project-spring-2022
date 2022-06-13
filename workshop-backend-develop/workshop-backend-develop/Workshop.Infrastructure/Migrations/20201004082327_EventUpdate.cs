using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class EventUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "MaxTeamCount",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Events",
                nullable: false,
                defaultValue: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login_EventId",
                table: "Users",
                columns: new[] { "Login", "EventId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Login_EventId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "MaxTeamCount",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
