using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class ProjectProposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectProposals",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<long>(nullable: false),
                    AuthorId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    Contacts = table.Column<string>(nullable: true),
                    Curator = table.Column<string>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    TeamCapacity = table.Column<int>(nullable: false),
                    MaxTeamCount = table.Column<int>(nullable: false),
                    LifeScenarioName = table.Column<string>(nullable: true),
                    KeyTechnologyName = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProposals", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectProposals");
        }
    }
}
