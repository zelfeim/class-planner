using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClassroomAndLecturersCanHaveMultipleClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Classes_ClassroomId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_LecturerId",
                table: "Classes");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassroomId",
                table: "Classes",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_LecturerId",
                table: "Classes",
                column: "LecturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Classes_ClassroomId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_LecturerId",
                table: "Classes");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassroomId",
                table: "Classes",
                column: "ClassroomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_LecturerId",
                table: "Classes",
                column: "LecturerId",
                unique: true);
        }
    }
}
