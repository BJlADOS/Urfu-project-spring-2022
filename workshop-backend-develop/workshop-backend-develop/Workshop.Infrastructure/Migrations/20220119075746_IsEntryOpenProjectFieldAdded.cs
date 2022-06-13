using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class IsEntryOpenProjectFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEntryOpen",
                table: "Projects",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEntryOpen",
                table: "Projects");
        }
    }
}
