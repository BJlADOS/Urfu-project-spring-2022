using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class RemoveTeamNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Teams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "KeyTechnologyLifeScenarios",
                columns: table => new
                {
                    KeyTechnologyId = table.Column<long>(type: "bigint", nullable: false),
                    LifeScenarioId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyTechnologyLifeScenarios", x => new { x.KeyTechnologyId, x.LifeScenarioId });
                    table.ForeignKey(
                        name: "FK_KeyTechnologyLifeScenarios_KeyTechnologies_KeyTechnologyId",
                        column: x => x.KeyTechnologyId,
                        principalTable: "KeyTechnologies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KeyTechnologyLifeScenarios_LifeScenarios_LifeScenarioId",
                        column: x => x.LifeScenarioId,
                        principalTable: "LifeScenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyTechnologyLifeScenarios_LifeScenarioId",
                table: "KeyTechnologyLifeScenarios",
                column: "LifeScenarioId");
        }
    }
}
