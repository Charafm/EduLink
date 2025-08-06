using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSaas.Infrastructure.Backoffice.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseGradeMappings_GradeLevelId1",
                table: "CourseGradeMappings");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGradeMappings_GradeLevelId1",
                table: "CourseGradeMappings",
                column: "GradeLevelId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseGradeMappings_GradeLevelId1",
                table: "CourseGradeMappings");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGradeMappings_GradeLevelId1",
                table: "CourseGradeMappings",
                column: "GradeLevelId1",
                unique: true,
                filter: "[GradeLevelId1] IS NOT NULL");
        }
    }
}
