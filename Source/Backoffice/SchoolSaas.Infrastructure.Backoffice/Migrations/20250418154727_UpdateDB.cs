using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSaas.Infrastructure.Backoffice.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentBodies_Comments_Id",
                table: "CommentBodies");

            migrationBuilder.DropForeignKey(
                name: "FK_gradeResources_Books_BookId",
                table: "gradeResources");

            migrationBuilder.DropForeignKey(
                name: "FK_gradeResources_GradeLevels_GradeLevelId",
                table: "gradeResources");

            migrationBuilder.DropForeignKey(
                name: "FK_gradeResources_SchoolSupplies_SupplyId",
                table: "gradeResources");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageBodies_Messages_Id",
                table: "MessageBodies");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplyAssignmentHistories_gradeResources_GradeResourceEntityId",
                table: "SupplyAssignmentHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gradeResources",
                table: "gradeResources");

            migrationBuilder.DropColumn(
                name: "DayOfWeekAr",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DayOfWeekEn",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DayOfWeekFr",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "gradeResources",
                newName: "GradeResources");

            migrationBuilder.RenameIndex(
                name: "IX_gradeResources_SupplyId",
                table: "GradeResources",
                newName: "IX_GradeResources_SupplyId");

            migrationBuilder.RenameIndex(
                name: "IX_gradeResources_LastModifiedBy",
                table: "GradeResources",
                newName: "IX_GradeResources_LastModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_gradeResources_GradeLevelId",
                table: "GradeResources",
                newName: "IX_GradeResources_GradeLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_gradeResources_CreatedBy",
                table: "GradeResources",
                newName: "IX_GradeResources_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_gradeResources_Created",
                table: "GradeResources",
                newName: "IX_GradeResources_Created");

            migrationBuilder.RenameIndex(
                name: "IX_gradeResources_BookId",
                table: "GradeResources",
                newName: "IX_GradeResources_BookId");

            migrationBuilder.RenameColumn(
                name: "Remarks",
                table: "EnrollmentDocuments",
                newName: "VerificationComment");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "TransferRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReasonEnum",
                table: "TransferRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Teachers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId1",
                table: "TeacherCourseAssignments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssignmentId",
                table: "SupplyAssignmentHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Schedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Parents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Parents",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Parents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Parents",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "AddressAr",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressFr",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CIN",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsIdentityVerified",
                table: "Parents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationDate",
                table: "Parents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TargetId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BodyId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "MessageBodies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "MessageBodies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Grades",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "NewComments",
                table: "GradeHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldComments",
                table: "GradeHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "EnrollmentStatusHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VerificationStatus",
                table: "EnrollmentDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId1",
                table: "CourseGradeMappings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GradeLevelId1",
                table: "CourseGradeMappings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "CourseGradeMappings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "CommentBodies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "CommentBodies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PreviousNotesAr",
                table: "AttendanceHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousNotesEn",
                table: "AttendanceHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousNotesFr",
                table: "AttendanceHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradeResources",
                table: "GradeResources",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CommentCommentBody",
                columns: table => new
                {
                    BodyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentCommentBody", x => new { x.BodyId, x.CommentsId });
                    table.ForeignKey(
                        name: "FK_CommentCommentBody_CommentBodies_BodyId",
                        column: x => x.BodyId,
                        principalTable: "CommentBodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentCommentBody_Comments_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinaryRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncidentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Resolved = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_DisciplinaryRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplinaryRecords_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradeAppeals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubmittedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_GradeAppeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeAppeals_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradeSectionStudentMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeSectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_GradeSectionStudentMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeSectionStudentMappings_GradeSections_GradeSectionId",
                        column: x => x.GradeSectionId,
                        principalTable: "GradeSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeSectionStudentMappings_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParentAudits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_ParentAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentAudits_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceType = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_ResourceHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolSupplyHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_SchoolSupplyHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolSupplyHistories_SchoolSupplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "SchoolSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffAudits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_StaffAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffAudits_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentFinances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DueAmount = table.Column<double>(type: "float", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_StudentFinances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FieldChanges = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_StudentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentHistories_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranscriptRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grade = table.Column<double>(type: "float", nullable: false),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_TranscriptRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranscriptRecords_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_TransferDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferDocuments_TransferRequests_TransferRequestId",
                        column: x => x.TransferRequestId,
                        principalTable: "TransferRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourseAssignments_CourseId1",
                table: "TeacherCourseAssignments",
                column: "CourseId1",
                unique: true,
                filter: "[CourseId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TeacherId",
                table: "Schedules",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_BodyId",
                table: "Messages",
                column: "BodyId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGradeMappings_CourseId1",
                table: "CourseGradeMappings",
                column: "CourseId1",
                unique: true,
                filter: "[CourseId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGradeMappings_GradeLevelId1",
                table: "CourseGradeMappings",
                column: "GradeLevelId1",
                unique: true,
                filter: "[GradeLevelId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CommentCommentBody_CommentsId",
                table: "CommentCommentBody",
                column: "CommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaryRecords_Created",
                table: "DisciplinaryRecords",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaryRecords_CreatedBy",
                table: "DisciplinaryRecords",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaryRecords_LastModifiedBy",
                table: "DisciplinaryRecords",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaryRecords_StudentId",
                table: "DisciplinaryRecords",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeAppeals_Created",
                table: "GradeAppeals",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_GradeAppeals_CreatedBy",
                table: "GradeAppeals",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GradeAppeals_GradeId",
                table: "GradeAppeals",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeAppeals_LastModifiedBy",
                table: "GradeAppeals",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSectionStudentMappings_Created",
                table: "GradeSectionStudentMappings",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSectionStudentMappings_CreatedBy",
                table: "GradeSectionStudentMappings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSectionStudentMappings_GradeSectionId",
                table: "GradeSectionStudentMappings",
                column: "GradeSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSectionStudentMappings_LastModifiedBy",
                table: "GradeSectionStudentMappings",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSectionStudentMappings_StudentId",
                table: "GradeSectionStudentMappings",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentAudits_Created",
                table: "ParentAudits",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_ParentAudits_CreatedBy",
                table: "ParentAudits",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ParentAudits_LastModifiedBy",
                table: "ParentAudits",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ParentAudits_ParentId",
                table: "ParentAudits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceHistories_Created",
                table: "ResourceHistories",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceHistories_CreatedBy",
                table: "ResourceHistories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceHistories_LastModifiedBy",
                table: "ResourceHistories",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSupplyHistories_Created",
                table: "SchoolSupplyHistories",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSupplyHistories_CreatedBy",
                table: "SchoolSupplyHistories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSupplyHistories_LastModifiedBy",
                table: "SchoolSupplyHistories",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSupplyHistories_SupplyId",
                table: "SchoolSupplyHistories",
                column: "SupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAudits_Created",
                table: "StaffAudits",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAudits_CreatedBy",
                table: "StaffAudits",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAudits_LastModifiedBy",
                table: "StaffAudits",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAudits_StaffId",
                table: "StaffAudits",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFinances_Created",
                table: "StudentFinances",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFinances_CreatedBy",
                table: "StudentFinances",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFinances_LastModifiedBy",
                table: "StudentFinances",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHistories_Created",
                table: "StudentHistories",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHistories_CreatedBy",
                table: "StudentHistories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHistories_LastModifiedBy",
                table: "StudentHistories",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHistories_StudentId",
                table: "StudentHistories",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptRecords_CourseId",
                table: "TranscriptRecords",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptRecords_Created",
                table: "TranscriptRecords",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptRecords_CreatedBy",
                table: "TranscriptRecords",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptRecords_LastModifiedBy",
                table: "TranscriptRecords",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDocuments_Created",
                table: "TransferDocuments",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDocuments_CreatedBy",
                table: "TransferDocuments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDocuments_LastModifiedBy",
                table: "TransferDocuments",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDocuments_TransferRequestId",
                table: "TransferDocuments",
                column: "TransferRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGradeMappings_Courses_CourseId1",
                table: "CourseGradeMappings",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGradeMappings_GradeLevels_GradeLevelId1",
                table: "CourseGradeMappings",
                column: "GradeLevelId1",
                principalTable: "GradeLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeResources_Books_BookId",
                table: "GradeResources",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeResources_GradeLevels_GradeLevelId",
                table: "GradeResources",
                column: "GradeLevelId",
                principalTable: "GradeLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeResources_SchoolSupplies_SupplyId",
                table: "GradeResources",
                column: "SupplyId",
                principalTable: "SchoolSupplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageBodies_BodyId",
                table: "Messages",
                column: "BodyId",
                principalTable: "MessageBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Teachers_TeacherId",
                table: "Schedules",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyAssignmentHistories_GradeResources_GradeResourceEntityId",
                table: "SupplyAssignmentHistories",
                column: "GradeResourceEntityId",
                principalTable: "GradeResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherCourseAssignments_Courses_CourseId1",
                table: "TeacherCourseAssignments",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGradeMappings_Courses_CourseId1",
                table: "CourseGradeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseGradeMappings_GradeLevels_GradeLevelId1",
                table: "CourseGradeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeResources_Books_BookId",
                table: "GradeResources");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeResources_GradeLevels_GradeLevelId",
                table: "GradeResources");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeResources_SchoolSupplies_SupplyId",
                table: "GradeResources");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageBodies_BodyId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Teachers_TeacherId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplyAssignmentHistories_GradeResources_GradeResourceEntityId",
                table: "SupplyAssignmentHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherCourseAssignments_Courses_CourseId1",
                table: "TeacherCourseAssignments");

            migrationBuilder.DropTable(
                name: "CommentCommentBody");

            migrationBuilder.DropTable(
                name: "DisciplinaryRecords");

            migrationBuilder.DropTable(
                name: "GradeAppeals");

            migrationBuilder.DropTable(
                name: "GradeSectionStudentMappings");

            migrationBuilder.DropTable(
                name: "ParentAudits");

            migrationBuilder.DropTable(
                name: "ResourceHistories");

            migrationBuilder.DropTable(
                name: "SchoolSupplyHistories");

            migrationBuilder.DropTable(
                name: "StaffAudits");

            migrationBuilder.DropTable(
                name: "StudentFinances");

            migrationBuilder.DropTable(
                name: "StudentHistories");

            migrationBuilder.DropTable(
                name: "TranscriptRecords");

            migrationBuilder.DropTable(
                name: "TransferDocuments");

            migrationBuilder.DropIndex(
                name: "IX_TeacherCourseAssignments_CourseId1",
                table: "TeacherCourseAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_TeacherId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Messages_BodyId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GradeResources",
                table: "GradeResources");

            migrationBuilder.DropIndex(
                name: "IX_CourseGradeMappings_CourseId1",
                table: "CourseGradeMappings");

            migrationBuilder.DropIndex(
                name: "IX_CourseGradeMappings_GradeLevelId1",
                table: "CourseGradeMappings");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "TransferRequests");

            migrationBuilder.DropColumn(
                name: "ReasonEnum",
                table: "TransferRequests");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "TeacherCourseAssignments");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "SupplyAssignmentHistories");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "AddressAr",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "AddressFr",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "CIN",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "IsIdentityVerified",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "VerificationDate",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "BodyId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "MessageBodies");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "NewComments",
                table: "GradeHistory");

            migrationBuilder.DropColumn(
                name: "OldComments",
                table: "GradeHistory");

            migrationBuilder.DropColumn(
                name: "VerificationStatus",
                table: "EnrollmentDocuments");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "CourseGradeMappings");

            migrationBuilder.DropColumn(
                name: "GradeLevelId1",
                table: "CourseGradeMappings");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "CourseGradeMappings");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "CommentBodies");

            migrationBuilder.DropColumn(
                name: "PreviousNotesAr",
                table: "AttendanceHistories");

            migrationBuilder.DropColumn(
                name: "PreviousNotesEn",
                table: "AttendanceHistories");

            migrationBuilder.DropColumn(
                name: "PreviousNotesFr",
                table: "AttendanceHistories");

            migrationBuilder.RenameTable(
                name: "GradeResources",
                newName: "gradeResources");

            migrationBuilder.RenameIndex(
                name: "IX_GradeResources_SupplyId",
                table: "gradeResources",
                newName: "IX_gradeResources_SupplyId");

            migrationBuilder.RenameIndex(
                name: "IX_GradeResources_LastModifiedBy",
                table: "gradeResources",
                newName: "IX_gradeResources_LastModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_GradeResources_GradeLevelId",
                table: "gradeResources",
                newName: "IX_gradeResources_GradeLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_GradeResources_CreatedBy",
                table: "gradeResources",
                newName: "IX_gradeResources_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_GradeResources_Created",
                table: "gradeResources",
                newName: "IX_gradeResources_Created");

            migrationBuilder.RenameIndex(
                name: "IX_GradeResources_BookId",
                table: "gradeResources",
                newName: "IX_gradeResources_BookId");

            migrationBuilder.RenameColumn(
                name: "VerificationComment",
                table: "EnrollmentDocuments",
                newName: "Remarks");

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Teachers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DayOfWeekAr",
                table: "Schedules",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DayOfWeekEn",
                table: "Schedules",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DayOfWeekFr",
                table: "Schedules",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Parents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Parents",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Parents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Parents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TargetId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "MessageBodies",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "EnrollmentStatusHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "CommentBodies",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gradeResources",
                table: "gradeResources",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentBodies_Comments_Id",
                table: "CommentBodies",
                column: "Id",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_gradeResources_Books_BookId",
                table: "gradeResources",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_gradeResources_GradeLevels_GradeLevelId",
                table: "gradeResources",
                column: "GradeLevelId",
                principalTable: "GradeLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_gradeResources_SchoolSupplies_SupplyId",
                table: "gradeResources",
                column: "SupplyId",
                principalTable: "SchoolSupplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageBodies_Messages_Id",
                table: "MessageBodies",
                column: "Id",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyAssignmentHistories_gradeResources_GradeResourceEntityId",
                table: "SupplyAssignmentHistories",
                column: "GradeResourceEntityId",
                principalTable: "gradeResources",
                principalColumn: "Id");
        }
    }
}
