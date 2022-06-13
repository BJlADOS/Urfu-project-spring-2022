using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class AddApplicationEssence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name:"ApplicationEssence",
                columns:table => new
                {
                    Id = table.Column<long>(nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(nullable: false),
                    TeamleadId = table.Column<long>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    EventId = table.Column<long>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    RoleName = table.Column<string>(nullable: false, defaultValue: "Не назначено")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventID",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TeamleadId",
                        column: x => x.TeamleadId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);

                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationEssence_UserId",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationEssence_ProjectId",
                table: "Projects",
                column: "Id");


            migrationBuilder.CreateIndex(
                name: "IX_ApplicationEssence_TeamleadId",
                table: "Users",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ApplicationEssence");
        }
    }
}
