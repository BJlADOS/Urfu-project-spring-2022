using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class UserAuditorium : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Auditoriums_AuditoriumId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AuditoriumId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AuditoriumId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "TeamSlots",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditoriumId = table.Column<long>(nullable: false),
                    TeamId = table.Column<long>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamSlots_Auditoriums_AuditoriumId",
                        column: x => x.AuditoriumId,
                        principalTable: "Auditoriums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamSlots_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserAuditoriums",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    AuditoriumId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuditoriums", x => new { x.UserId, x.AuditoriumId });
                    table.ForeignKey(
                        name: "FK_UserAuditoriums_Auditoriums_AuditoriumId",
                        column: x => x.AuditoriumId,
                        principalTable: "Auditoriums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAuditoriums_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamSlots_AuditoriumId",
                table: "TeamSlots",
                column: "AuditoriumId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamSlots_TeamId",
                table: "TeamSlots",
                column: "TeamId",
                unique: true,
                filter: "[TeamId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuditoriums_AuditoriumId",
                table: "UserAuditoriums",
                column: "AuditoriumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamSlots");

            migrationBuilder.DropTable(
                name: "UserAuditoriums");

            migrationBuilder.AddColumn<long>(
                name: "AuditoriumId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuditoriumId",
                table: "Users",
                column: "AuditoriumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Auditoriums_AuditoriumId",
                table: "Users",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
