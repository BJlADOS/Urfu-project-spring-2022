using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class AuditoriumRestore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AuditoriumId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Auditoriums",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoriums", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_AuditoriumId",
                table: "Teams",
                column: "AuditoriumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Auditoriums_AuditoriumId",
                table: "Teams",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Auditoriums_AuditoriumId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Auditoriums");

            migrationBuilder.DropIndex(
                name: "IX_Teams_AuditoriumId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "AuditoriumId",
                table: "Teams");
        }
    }
}
