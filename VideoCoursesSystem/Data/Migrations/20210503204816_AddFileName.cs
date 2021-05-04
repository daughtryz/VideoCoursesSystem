using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoCoursesSystem.Data.Migrations
{
    public partial class AddFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Exercises");
        }
    }
}
