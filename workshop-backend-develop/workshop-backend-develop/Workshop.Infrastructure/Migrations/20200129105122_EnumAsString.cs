using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class EnumAsString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserCompetencyType",
                table: "UserCompetencies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TeamStatus",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CompetencyType",
                table: "Competencies",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCompetencyType",
                table: "UserCompetencies");

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "TeamStatus",
                table: "Teams",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "CompetencyType",
                table: "Competencies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
