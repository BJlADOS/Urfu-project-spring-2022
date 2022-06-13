using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class Events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "Users",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "ProfileFilled",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "Teams",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "Projects",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "LifeScenarios",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "KeyTechnologies",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "Auditoriums",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileFilled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "LifeScenarios");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "KeyTechnologies");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Auditoriums");
        }
    }
}
