using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoCoursesSystem.Data.Migrations
{
    public partial class FileAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Exercises",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Exercises");
        }
    }
}
