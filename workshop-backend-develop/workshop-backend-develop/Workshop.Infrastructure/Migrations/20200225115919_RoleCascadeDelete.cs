using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class RoleCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Projects_ProjectId",
                table: "Roles");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Projects_ProjectId",
                table: "Roles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Projects_ProjectId",
                table: "Roles");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Projects_ProjectId",
                table: "Roles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
