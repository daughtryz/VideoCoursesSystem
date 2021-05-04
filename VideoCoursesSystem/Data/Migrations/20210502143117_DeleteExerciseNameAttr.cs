using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoCoursesSystem.Data.Migrations
{
    public partial class DeleteExerciseNameAttr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Exercises");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
