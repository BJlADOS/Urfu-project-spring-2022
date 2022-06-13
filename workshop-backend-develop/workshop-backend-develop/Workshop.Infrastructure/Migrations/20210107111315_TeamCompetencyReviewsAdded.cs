using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class TeamCompetencyReviewsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamCompetencyReviews",
                columns: table => new
                {
                    TeamId = table.Column<long>(nullable: false),
                    ExpertId = table.Column<long>(nullable: false),
                    CompetencyId = table.Column<long>(nullable: false),
                    Mark = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCompetencyReviews", x => new { x.ExpertId, x.TeamId, x.CompetencyId });
                    table.ForeignKey(
                        name: "FK_TeamCompetencyReviews_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamCompetencyReviews_Users_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamCompetencyReviews_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamCompetencyReviews_CompetencyId",
                table: "TeamCompetencyReviews",
                column: "CompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCompetencyReviews_TeamId",
                table: "TeamCompetencyReviews",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamCompetencyReviews");
        }
    }
}
