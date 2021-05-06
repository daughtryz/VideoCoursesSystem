using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoCoursesSystem.Data.Migrations
{
    public partial class AddGradeToExercise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GradeId",
                table: "Exercises",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_GradeId",
                table: "Exercises",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Grades_GradeId",
                table: "Exercises",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Grades_GradeId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_GradeId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Exercises");
        }
    }
}
