using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSaas.Infrastructure.Backoffice.Migrations
{
    /// <inheritdoc />
    public partial class MajorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGradeMappings_GradeLevels_GradeLevelId1",
                table: "CourseGradeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Branches_BranchId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Branches_BranchId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherCourseAssignments_Courses_CourseId1",
                table: "TeacherCourseAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TeacherCourseAssignments_CourseId",
                table: "TeacherCourseAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TeacherCourseAssignments_CourseId1",
                table: "TeacherCourseAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Students_BranchId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Parents_BranchId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_CourseGradeMappings_GradeLevelId1",
                table: "CourseGradeMappings");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "TeacherCourseAssignments");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "GradeLevelId1",
                table: "CourseGradeMappings");

            migrationBuilder.AddColumn<string>(
                name: "RequestCode",
                table: "Enrollments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "Enrollments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EnrollmentRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentRequests_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EnrollmentRequests_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourseAssignments_CourseId",
                table: "TeacherCourseAssignments",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId1",
                table: "Enrollments",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentRequests_BranchId",
                table: "EnrollmentRequests",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentRequests_Created",
                table: "EnrollmentRequests",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentRequests_CreatedBy",
                table: "EnrollmentRequests",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentRequests_LastModifiedBy",
                table: "EnrollmentRequests",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentRequests_StudentId",
                table: "EnrollmentRequests",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId1",
                table: "Enrollments",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId1",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "EnrollmentRequests");

            migrationBuilder.DropIndex(
                name: "IX_TeacherCourseAssignments_CourseId",
                table: "TeacherCourseAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId1",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "RequestCode",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "Enrollments");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId1",
                table: "TeacherCourseAssignments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "Parents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GradeLevelId1",
                table: "CourseGradeMappings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourseAssignments_CourseId",
                table: "TeacherCourseAssignments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourseAssignments_CourseId1",
                table: "TeacherCourseAssignments",
                column: "CourseId1",
                unique: true,
                filter: "[CourseId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Students_BranchId",
                table: "Students",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_BranchId",
                table: "Parents",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGradeMappings_GradeLevelId1",
                table: "CourseGradeMappings",
                column: "GradeLevelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGradeMappings_GradeLevels_GradeLevelId1",
                table: "CourseGradeMappings",
                column: "GradeLevelId1",
                principalTable: "GradeLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Branches_BranchId",
                table: "Parents",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Branches_BranchId",
                table: "Students",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherCourseAssignments_Courses_CourseId1",
                table: "TeacherCourseAssignments",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
