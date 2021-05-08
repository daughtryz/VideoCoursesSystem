using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoCoursesSystem.Data.Migrations
{
    public partial class AddEditWiki : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EditWikiCount",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditWikiCount",
                table: "Exercises");
        }
    }
}
