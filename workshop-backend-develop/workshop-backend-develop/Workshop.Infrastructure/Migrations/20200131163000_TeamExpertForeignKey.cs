using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class TeamExpertForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditoriumId",
                table: "Teams");

            migrationBuilder.AddColumn<long>(
                name: "ExpertId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ExpertId",
                table: "Teams",
                column: "ExpertId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_ExpertId",
                table: "Teams",
                column: "ExpertId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_ExpertId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ExpertId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ExpertId",
                table: "Teams");

            migrationBuilder.AddColumn<long>(
                name: "AuditoriumId",
                table: "Teams",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
