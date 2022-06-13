using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class LifeScenarioKeyTechnologyCorrelationDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "KeyTechnologyLifeScenarios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.CreateTable(        
            name: "KeyTechnologyLifeScenarios",
            columns: table => new 
            {  
                KeyTechnologyId = table.Column<long>(nullable: false),
                LifeScenarioId = table.Column<long>(nullable: false)
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
                        name: "FK_KeyTechnologyLifeScenarios_LifeScenarios_KeyTechnologyId",
                        column: x => x.KeyTechnologyId,
                        principalTable: "LifeScenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
            });
        }
    }
}
