using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class TeamReviewModelCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamReviews",
                columns: table => new
                {
                    TeamId = table.Column<long>(nullable: false),
                    ExpertId = table.Column<long>(nullable: false),
                    Mark = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamReviews", x => new { x.ExpertId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamReviews_Users_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamReviews_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamReviews_TeamId",
                table: "TeamReviews",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamReviews");
        }
    }
}
