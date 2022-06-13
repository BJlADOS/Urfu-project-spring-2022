using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class Linkstoresourcesrelatedtoproject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalLink",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PMSLink",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepositoryLink",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalLink",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PMSLink",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "RepositoryLink",
                table: "Teams");
        }
    }
}
