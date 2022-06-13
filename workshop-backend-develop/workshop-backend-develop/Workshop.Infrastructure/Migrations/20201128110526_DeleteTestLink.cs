using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class DeleteTestLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestLink",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestLink",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
