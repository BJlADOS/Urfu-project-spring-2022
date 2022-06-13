using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class MarkAndComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mark",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "Teams");
        }
    }
}
