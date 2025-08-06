using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSaas.Infrastructure.Backoffice.Migrations
{
    /// <inheritdoc />
    public partial class Intimigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartYear = table.Column<int>(type: "int", nullable: false),
                    EndYear = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_AcademicYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleFr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TitleEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AuthorNameFr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AuthorNameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AuthorNameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchNameFr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BranchNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BranchNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressFr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AddressAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrincipalNameFr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrincipalNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleFr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleFr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EducationalStage = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_GradeLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationBodies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
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
                    table.PrimaryKey("PK_NotificationBodies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolSupplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameFr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DescriptionFr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_SchoolSupplies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    RoleNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.CheckConstraint("CK_Semester_EndDate_After_StartDate", "[EndDate] > [StartDate]");
                    table.ForeignKey(
                        name: "FK_Semesters_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                    table.CheckConstraint("CK_Vacation_EndDate_After_StartDate", "[EndDate] > [StartDate]");
                    table.ForeignKey(
                        name: "FK_Vacations_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstNameFr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstNameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastNameFr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastNameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
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
                    table.PrimaryKey("PK_Parents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parents_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstNameFr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstNameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastNameFr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastNameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartmentFr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DepartmentAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JobTitleFr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JobTitleAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staffs_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstNameFr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstNameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastNameFr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastNameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstNameFr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstNameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastNameFr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastNameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecializationFr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SpecializationAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RoomTitleFr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RoomTitleAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RoomType = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classrooms_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentBodies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
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
                    table.PrimaryKey("PK_CommentBodies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentBodies_Comments_Id",
                        column: x => x.Id,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseGradeMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsElective = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_CourseGradeMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseGradeMappings_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseGradeMappings_GradeLevels_GradeLevelId",
                        column: x => x.GradeLevelId,
                        principalTable: "GradeLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GradeSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionNameFr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SectionNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SectionNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_GradeSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeSections_GradeLevels_GradeLevelId",
                        column: x => x.GradeLevelId,
                        principalTable: "GradeLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageBodies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
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
                    table.PrimaryKey("PK_MessageBodies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageBodies_Messages_Id",
                        column: x => x.Id,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gradeResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplyQuantity = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_gradeResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gradeResources_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_gradeResources_GradeLevels_GradeLevelId",
                        column: x => x.GradeLevelId,
                        principalTable: "GradeLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_gradeResources_SchoolSupplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "SchoolSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Target = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    NotificationBodyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationBodies_NotificationBodyId",
                        column: x => x.NotificationBodyId,
                        principalTable: "NotificationBodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_UserDto_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ParentDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressFr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AddressAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AdditionalContactInfo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
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
                    table.PrimaryKey("PK_ParentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentDetails_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    NotesFr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NotesAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NotesEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendances_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminComment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    GradeType = table.Column<int>(type: "int", nullable: false),
                    TeacherCommentFr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TeacherCommentAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TeacherCommentEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalInfo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EmergencyContact = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PreviousSchool = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AdditionalNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_StudentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentDetails_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentParents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelationshipType = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_StudentParents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentParents_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentParents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromSchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToSchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminComment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_TransferRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferRequests_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherCourseAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_TeacherCourseAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherCourseAssignments_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherCourseAssignments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherCourseAssignments_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GradeClassroomAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeSectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassroomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_GradeClassroomAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeClassroomAssignments_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GradeClassroomAssignments_GradeSections_GradeSectionId",
                        column: x => x.GradeSectionId,
                        principalTable: "GradeSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassroomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeSectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeekFr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DayOfWeekAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DayOfWeekEn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
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
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.CheckConstraint("CK_Schedule_EndTime_After_StartTime", "[EndTime] > [StartTime]");
                    table.ForeignKey(
                        name: "FK_Schedules_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_GradeSections_GradeSectionId",
                        column: x => x.GradeSectionId,
                        principalTable: "GradeSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplyAssignmentHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    GradeResource = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GradeLevel = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SchoolSupply = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradeResourceEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GradeLevelEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SchoolSupplyEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_SupplyAssignmentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplyAssignmentHistories_GradeLevels_GradeLevelEntityId",
                        column: x => x.GradeLevelEntityId,
                        principalTable: "GradeLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupplyAssignmentHistories_SchoolSupplies_SchoolSupplyEntityId",
                        column: x => x.SchoolSupplyEntityId,
                        principalTable: "SchoolSupplies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupplyAssignmentHistories_gradeResources_GradeResourceEntityId",
                        column: x => x.GradeResourceEntityId,
                        principalTable: "gradeResources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttendanceHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeReason = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AttendanceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceHistories_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceHistories_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_EnrollmentDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentDocuments_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentStatusHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldStatus = table.Column<int>(type: "int", nullable: false),
                    NewStatus = table.Column<int>(type: "int", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeReason = table.Column<int>(type: "int", maxLength: 500, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_EnrollmentStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentStatusHistories_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GradeHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldScore = table.Column<double>(type: "float", nullable: false),
                    NewScore = table.Column<double>(type: "float", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_GradeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeHistory_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferRequestHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransferRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldStatus = table.Column<int>(type: "int", nullable: false),
                    NewStatus = table.Column<int>(type: "int", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeReason = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_TransferRequestHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferRequestHistories_TransferRequests_TransferRequestId",
                        column: x => x.TransferRequestId,
                        principalTable: "TransferRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYears_Created",
                table: "AcademicYears",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYears_CreatedBy",
                table: "AcademicYears",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYears_LastModifiedBy",
                table: "AcademicYears",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceHistories_AttendanceId",
                table: "AttendanceHistories",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceHistories_Created",
                table: "AttendanceHistories",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceHistories_CreatedBy",
                table: "AttendanceHistories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceHistories_LastModifiedBy",
                table: "AttendanceHistories",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceHistories_StudentId",
                table: "AttendanceHistories",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_CourseId",
                table: "Attendances",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_Created",
                table: "Attendances",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_CreatedBy",
                table: "Attendances",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_LastModifiedBy",
                table: "Attendances",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Created",
                table: "Books",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedBy",
                table: "Books",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LastModifiedBy",
                table: "Books",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_Created",
                table: "Branches",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CreatedBy",
                table: "Branches",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_LastModifiedBy",
                table: "Branches",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_Created",
                table: "Buildings",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_CreatedBy",
                table: "Buildings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_LastModifiedBy",
                table: "Buildings",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_BuildingId",
                table: "Classrooms",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_Created",
                table: "Classrooms",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_CreatedBy",
                table: "Classrooms",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_LastModifiedBy",
                table: "Classrooms",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CommentBodies_Created",
                table: "CommentBodies",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_CommentBodies_CreatedBy",
                table: "CommentBodies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CommentBodies_LastModifiedBy",
                table: "CommentBodies",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Created",
                table: "Comments",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedBy",
                table: "Comments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_LastModifiedBy",
                table: "Comments",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGradeMappings_CourseId",
                table: "CourseGradeMappings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGradeMappings_Created",
                table: "CourseGradeMappings",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGradeMappings_CreatedBy",
                table: "CourseGradeMappings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGradeMappings_GradeLevelId",
                table: "CourseGradeMappings",
                column: "GradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGradeMappings_LastModifiedBy",
                table: "CourseGradeMappings",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Created",
                table: "Courses",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CreatedBy",
                table: "Courses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LastModifiedBy",
                table: "Courses",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentDocuments_Created",
                table: "EnrollmentDocuments",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentDocuments_CreatedBy",
                table: "EnrollmentDocuments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentDocuments_EnrollmentId",
                table: "EnrollmentDocuments",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentDocuments_LastModifiedBy",
                table: "EnrollmentDocuments",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_BranchId",
                table: "Enrollments",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_Created",
                table: "Enrollments",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CreatedBy",
                table: "Enrollments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_LastModifiedBy",
                table: "Enrollments",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentStatusHistories_Created",
                table: "EnrollmentStatusHistories",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentStatusHistories_CreatedBy",
                table: "EnrollmentStatusHistories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentStatusHistories_EnrollmentId",
                table: "EnrollmentStatusHistories",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentStatusHistories_LastModifiedBy",
                table: "EnrollmentStatusHistories",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GradeClassroomAssignments_ClassroomId",
                table: "GradeClassroomAssignments",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeClassroomAssignments_Created",
                table: "GradeClassroomAssignments",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_GradeClassroomAssignments_CreatedBy",
                table: "GradeClassroomAssignments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GradeClassroomAssignments_GradeSectionId",
                table: "GradeClassroomAssignments",
                column: "GradeSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeClassroomAssignments_LastModifiedBy",
                table: "GradeClassroomAssignments",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GradeHistory_Created",
                table: "GradeHistory",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_GradeHistory_CreatedBy",
                table: "GradeHistory",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GradeHistory_GradeId",
                table: "GradeHistory",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeHistory_LastModifiedBy",
                table: "GradeHistory",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GradeLevels_Created",
                table: "GradeLevels",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_GradeLevels_CreatedBy",
                table: "GradeLevels",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GradeLevels_LastModifiedBy",
                table: "GradeLevels",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_gradeResources_BookId",
                table: "gradeResources",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_gradeResources_Created",
                table: "gradeResources",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_gradeResources_CreatedBy",
                table: "gradeResources",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_gradeResources_GradeLevelId",
                table: "gradeResources",
                column: "GradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_gradeResources_LastModifiedBy",
                table: "gradeResources",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_gradeResources_SupplyId",
                table: "gradeResources",
                column: "SupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CourseId",
                table: "Grades",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Created",
                table: "Grades",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CreatedBy",
                table: "Grades",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_LastModifiedBy",
                table: "Grades",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_SemesterId",
                table: "Grades",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSections_Created",
                table: "GradeSections",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSections_CreatedBy",
                table: "GradeSections",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSections_GradeLevelId",
                table: "GradeSections",
                column: "GradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSections_LastModifiedBy",
                table: "GradeSections",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBodies_Created",
                table: "MessageBodies",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBodies_CreatedBy",
                table: "MessageBodies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBodies_LastModifiedBy",
                table: "MessageBodies",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Created",
                table: "Messages",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CreatedBy",
                table: "Messages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_LastModifiedBy",
                table: "Messages",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationBodies_Created",
                table: "NotificationBodies",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationBodies_CreatedBy",
                table: "NotificationBodies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationBodies_LastModifiedBy",
                table: "NotificationBodies",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Created",
                table: "Notifications",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedBy",
                table: "Notifications",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_LastModifiedBy",
                table: "Notifications",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationBodyId",
                table: "Notifications",
                column: "NotificationBodyId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentDetails_Created",
                table: "ParentDetails",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_ParentDetails_CreatedBy",
                table: "ParentDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ParentDetails_LastModifiedBy",
                table: "ParentDetails",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ParentDetails_ParentId",
                table: "ParentDetails",
                column: "ParentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_BranchId",
                table: "Parents",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_Created",
                table: "Parents",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_CreatedBy",
                table: "Parents",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_LastModifiedBy",
                table: "Parents",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ClassroomId",
                table: "Schedules",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CourseId",
                table: "Schedules",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Created",
                table: "Schedules",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CreatedBy",
                table: "Schedules",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_GradeSectionId",
                table: "Schedules",
                column: "GradeSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_LastModifiedBy",
                table: "Schedules",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSupplies_Created",
                table: "SchoolSupplies",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSupplies_CreatedBy",
                table: "SchoolSupplies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSupplies_LastModifiedBy",
                table: "SchoolSupplies",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_AcademicYearId",
                table: "Semesters",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_Created",
                table: "Semesters",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_CreatedBy",
                table: "Semesters",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_LastModifiedBy",
                table: "Semesters",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_BranchId",
                table: "Staffs",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_Created",
                table: "Staffs",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_CreatedBy",
                table: "Staffs",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_LastModifiedBy",
                table: "Staffs",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDetails_Created",
                table: "StudentDetails",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDetails_CreatedBy",
                table: "StudentDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDetails_LastModifiedBy",
                table: "StudentDetails",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDetails_StudentId",
                table: "StudentDetails",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentParents_Created",
                table: "StudentParents",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_StudentParents_CreatedBy",
                table: "StudentParents",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentParents_LastModifiedBy",
                table: "StudentParents",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentParents_ParentId",
                table: "StudentParents",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentParents_StudentId",
                table: "StudentParents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_BranchId",
                table: "Students",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Created",
                table: "Students",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CreatedBy",
                table: "Students",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Students_LastModifiedBy",
                table: "Students",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyAssignmentHistories_Created",
                table: "SupplyAssignmentHistories",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyAssignmentHistories_CreatedBy",
                table: "SupplyAssignmentHistories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyAssignmentHistories_GradeLevelEntityId",
                table: "SupplyAssignmentHistories",
                column: "GradeLevelEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyAssignmentHistories_GradeResourceEntityId",
                table: "SupplyAssignmentHistories",
                column: "GradeResourceEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyAssignmentHistories_LastModifiedBy",
                table: "SupplyAssignmentHistories",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyAssignmentHistories_SchoolSupplyEntityId",
                table: "SupplyAssignmentHistories",
                column: "SchoolSupplyEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourseAssignments_AcademicYearId",
                table: "TeacherCourseAssignments",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourseAssignments_CourseId",
                table: "TeacherCourseAssignments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourseAssignments_Created",
                table: "TeacherCourseAssignments",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourseAssignments_CreatedBy",
                table: "TeacherCourseAssignments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourseAssignments_LastModifiedBy",
                table: "TeacherCourseAssignments",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourseAssignments_TeacherId",
                table: "TeacherCourseAssignments",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_BranchId",
                table: "Teachers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_Created",
                table: "Teachers",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_CreatedBy",
                table: "Teachers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_LastModifiedBy",
                table: "Teachers",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequestHistories_Created",
                table: "TransferRequestHistories",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequestHistories_CreatedBy",
                table: "TransferRequestHistories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequestHistories_LastModifiedBy",
                table: "TransferRequestHistories",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequestHistories_TransferRequestId",
                table: "TransferRequestHistories",
                column: "TransferRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequests_Created",
                table: "TransferRequests",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequests_CreatedBy",
                table: "TransferRequests",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequests_LastModifiedBy",
                table: "TransferRequests",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequests_StudentId",
                table: "TransferRequests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_AcademicYearId",
                table: "Vacations",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_Created",
                table: "Vacations",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_CreatedBy",
                table: "Vacations",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_LastModifiedBy",
                table: "Vacations",
                column: "LastModifiedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceHistories");

            migrationBuilder.DropTable(
                name: "CommentBodies");

            migrationBuilder.DropTable(
                name: "CourseGradeMappings");

            migrationBuilder.DropTable(
                name: "EnrollmentDocuments");

            migrationBuilder.DropTable(
                name: "EnrollmentStatusHistories");

            migrationBuilder.DropTable(
                name: "GradeClassroomAssignments");

            migrationBuilder.DropTable(
                name: "GradeHistory");

            migrationBuilder.DropTable(
                name: "MessageBodies");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ParentDetails");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "StudentDetails");

            migrationBuilder.DropTable(
                name: "StudentParents");

            migrationBuilder.DropTable(
                name: "SupplyAssignmentHistories");

            migrationBuilder.DropTable(
                name: "TeacherCourseAssignments");

            migrationBuilder.DropTable(
                name: "TransferRequestHistories");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "NotificationBodies");

            migrationBuilder.DropTable(
                name: "UserDto");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "GradeSections");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "gradeResources");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "TransferRequests");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "GradeLevels");

            migrationBuilder.DropTable(
                name: "SchoolSupplies");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "AcademicYears");

            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
