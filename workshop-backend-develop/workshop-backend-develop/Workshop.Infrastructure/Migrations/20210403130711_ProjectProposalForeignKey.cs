using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class ProjectProposalForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposals_AuthorId",
                table: "ProjectProposals",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposals_Users_AuthorId",
                table: "ProjectProposals",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposals_Users_AuthorId",
                table: "ProjectProposals");

            migrationBuilder.DropIndex(
                name: "IX_ProjectProposals_AuthorId",
                table: "ProjectProposals");
        }
    }
}
