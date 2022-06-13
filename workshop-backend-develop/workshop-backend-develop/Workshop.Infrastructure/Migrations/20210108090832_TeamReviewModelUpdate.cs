using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Infrastructure.Migrations
{
    public partial class TeamReviewModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mark",
                table: "TeamReviews");

            migrationBuilder.AddColumn<int>(
                name: "GoalsAndTasks",
                table: "TeamReviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Impact",
                table: "TeamReviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Knowledge",
                table: "TeamReviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Presentation",
                table: "TeamReviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Result",
                table: "TeamReviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Solution",
                table: "TeamReviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Technical",
                table: "TeamReviews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalsAndTasks",
                table: "TeamReviews");

            migrationBuilder.DropColumn(
                name: "Impact",
                table: "TeamReviews");

            migrationBuilder.DropColumn(
                name: "Knowledge",
                table: "TeamReviews");

            migrationBuilder.DropColumn(
                name: "Presentation",
                table: "TeamReviews");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "TeamReviews");

            migrationBuilder.DropColumn(
                name: "Solution",
                table: "TeamReviews");

            migrationBuilder.DropColumn(
                name: "Technical",
                table: "TeamReviews");

            migrationBuilder.AddColumn<int>(
                name: "Mark",
                table: "TeamReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
