using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditoriums",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoriums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competencies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    CompetencyType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyTechnologies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    LifeScenarioId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyTechnologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LifeScenarios",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeScenarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key_Id = table.Column<Guid>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    Expires = table.Column<DateTime>(nullable: false),
                    LastConnection = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    TeamStatus = table.Column<int>(nullable: false),
                    AuditoriumId = table.Column<long>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    TeamCompleteDate = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    Curator = table.Column<string>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    Contacts = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    TeamCapacity = table.Column<int>(nullable: false),
                    LifeScenarioId = table.Column<long>(nullable: false),
                    KeyTechnologyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_KeyTechnologies_KeyTechnologyId",
                        column: x => x.KeyTechnologyId,
                        principalTable: "KeyTechnologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_LifeScenarios_LifeScenarioId",
                        column: x => x.LifeScenarioId,
                        principalTable: "LifeScenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    About = table.Column<string>(nullable: true),
                    AcademicGroup = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    TeamId = table.Column<long>(nullable: false),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectCompetencies",
                columns: table => new
                {
                    ProjectId = table.Column<long>(nullable: false),
                    CompetencyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCompetencies", x => new { x.CompetencyId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectCompetencies_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectCompetencies_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCompetencies",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    CompetencyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompetencies", x => new { x.CompetencyId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserCompetencies_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserCompetencies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoriums");

            migrationBuilder.DropTable(
                name: "KeyTechnologyLifeScenarios");

            migrationBuilder.DropTable(
                name: "ProjectCompetencies");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "UserCompetencies");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Competencies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "KeyTechnologies");

            migrationBuilder.DropTable(
                name: "LifeScenarios");

            migrationBuilder.DropTable(
                name: "Teams");

        }
    }
}
