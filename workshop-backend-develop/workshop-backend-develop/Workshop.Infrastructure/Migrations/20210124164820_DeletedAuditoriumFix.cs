using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class DeletedAuditoriumFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Auditoriums_AuditoriumId",
                table: "Teams");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuditoriumId",
                table: "Users",
                column: "AuditoriumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Auditoriums_AuditoriumId",
                table: "Teams",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Auditoriums_AuditoriumId",
                table: "Users",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Auditoriums_AuditoriumId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Auditoriums_AuditoriumId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AuditoriumId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Auditoriums_AuditoriumId",
                table: "Teams",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id");
        }
    }
}
