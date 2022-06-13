using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class CompetencyCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCompetencies_Competencies_CompetencyId",
                table: "ProjectCompetencies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompetencies_Competencies_CompetencyId",
                table: "UserCompetencies");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCompetencies_Competencies_CompetencyId",
                table: "ProjectCompetencies",
                column: "CompetencyId",
                principalTable: "Competencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompetencies_Competencies_CompetencyId",
                table: "UserCompetencies",
                column: "CompetencyId",
                principalTable: "Competencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCompetencies_Competencies_CompetencyId",
                table: "ProjectCompetencies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompetencies_Competencies_CompetencyId",
                table: "UserCompetencies");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCompetencies_Competencies_CompetencyId",
                table: "ProjectCompetencies",
                column: "CompetencyId",
                principalTable: "Competencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompetencies_Competencies_CompetencyId",
                table: "UserCompetencies",
                column: "CompetencyId",
                principalTable: "Competencies",
                principalColumn: "Id");
        }
    }
}
