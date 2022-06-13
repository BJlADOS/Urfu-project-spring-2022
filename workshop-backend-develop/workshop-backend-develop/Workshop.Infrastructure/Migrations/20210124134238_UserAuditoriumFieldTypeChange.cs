using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class UserAuditoriumFieldTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auditorium",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "AuditoriumId",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditoriumId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Auditorium",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
