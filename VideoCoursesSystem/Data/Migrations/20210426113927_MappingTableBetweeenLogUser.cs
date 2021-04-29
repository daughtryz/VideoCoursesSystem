using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoCoursesSystem.Data.Migrations
{
    public partial class MappingTableBetweeenLogUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogsInformation_AspNetUsers_StudentId",
                table: "LogsInformation");

            migrationBuilder.DropIndex(
                name: "IX_LogsInformation_StudentId",
                table: "LogsInformation");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "LogsInformation");

            migrationBuilder.CreateTable(
                name: "UserLogsInformation",
                columns: table => new
                {
                    LogInformationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogsInformation", x => new { x.StudentId, x.LogInformationId });
                    table.ForeignKey(
                        name: "FK_UserLogsInformation_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLogsInformation_LogsInformation_LogInformationId",
                        column: x => x.LogInformationId,
                        principalTable: "LogsInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogsInformation_LogInformationId",
                table: "UserLogsInformation",
                column: "LogInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogsInformation");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "LogsInformation",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogsInformation_StudentId",
                table: "LogsInformation",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogsInformation_AspNetUsers_StudentId",
                table: "LogsInformation",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
