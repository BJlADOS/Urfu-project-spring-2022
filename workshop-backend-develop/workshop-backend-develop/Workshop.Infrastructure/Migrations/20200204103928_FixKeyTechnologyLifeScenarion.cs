using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class FixKeyTechnologyLifeScenarion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyTechnologyLifeScenarios_LifeScenarios_KeyTechnologyId",
                table: "KeyTechnologyLifeScenarios");

            migrationBuilder.DropColumn(
                name: "LifeScenarioId",
                table: "KeyTechnologies");

            migrationBuilder.CreateIndex(
                name: "IX_KeyTechnologyLifeScenarios_LifeScenarioId",
                table: "KeyTechnologyLifeScenarios",
                column: "LifeScenarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyTechnologyLifeScenarios_LifeScenarios_LifeScenarioId",
                table: "KeyTechnologyLifeScenarios",
                column: "LifeScenarioId",
                principalTable: "LifeScenarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyTechnologyLifeScenarios_LifeScenarios_LifeScenarioId",
                table: "KeyTechnologyLifeScenarios");

            migrationBuilder.DropIndex(
                name: "IX_KeyTechnologyLifeScenarios_LifeScenarioId",
                table: "KeyTechnologyLifeScenarios");

            migrationBuilder.AddColumn<long>(
                name: "LifeScenarioId",
                table: "KeyTechnologies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyTechnologyLifeScenarios_LifeScenarios_KeyTechnologyId",
                table: "KeyTechnologyLifeScenarios",
                column: "KeyTechnologyId",
                principalTable: "LifeScenarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
