using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Common.Context;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Backoffice.Traceability;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Backoffice.Staff;
using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Backoffice.Resources;
using SchoolSaas.Domain.Backoffice.Academic;
using System.Reflection.Emit;

namespace SchoolSaas.Infrastructure.Backoffice.Context
{
    public class BackofficeContext : AbstractContext<BackofficeContext>, IBackofficeContext
    {
        public BackofficeContext(ITenantAccessor tenantAccessor, ICurrentUserService currentUserService, IDateTime dateTime, IStorage storage,
            ICacheService cacheService, DbContextOptions<BackofficeContext> options)
            : base(tenantAccessor, currentUserService, dateTime, storage, cacheService, options)
        {
        }

        protected override IContextInitializer CreateContextInitializer(ModelBuilder builder)
        {


            return new BackofficeContextInitializer(builder);
        }
        public DbSet<Student> Students => Set<Student>(); // Here is the student Class or Entity
        public DbSet<AttendanceHistory> AttendanceHistories =>Set<AttendanceHistory>();
        public DbSet<EnrollmentStatusHistory> EnrollmentStatusHistories =>Set<EnrollmentStatusHistory>();
        public DbSet<GradeHistory> GradeHistory =>Set<GradeHistory>();
        public DbSet<SupplyAssignmentHistory> SupplyAssignmentHistories =>Set<SupplyAssignmentHistory>();
        public DbSet<TransferRequestHistory> TransferRequestHistories =>Set<TransferRequestHistory>();
        public DbSet<Parent> Parents =>Set<Parent>();
        public DbSet<ParentDetail > ParentDetails =>Set<ParentDetail>();
        public DbSet<EnrollmentRequest> EnrollmentRequests =>Set<EnrollmentRequest>();
        public DbSet<StudentDetail> StudentDetails =>Set<StudentDetail>();
        public DbSet<StudentParent> StudentParents =>Set<StudentParent>();
        public DbSet<Staff> Staffs =>Set<Staff>();
        public DbSet<Teacher> Teachers =>Set<Teacher>();
        public DbSet<Branch> Branches =>Set<Branch>();
        public DbSet<Enrollment> Enrollments =>Set<Enrollment>();
        public DbSet<EnrollmentDocument> EnrollmentDocuments    =>Set<EnrollmentDocument>();
        public DbSet<TransferRequest> TransferRequests =>Set<TransferRequest>();
        public DbSet<Book> Books =>Set<Book>();
        public DbSet<SchoolSupplyHistory> SchoolSupplyHistories => Set<SchoolSupplyHistory>();
        public DbSet<ResourceHistory> ResourceHistories => Set<ResourceHistory>();
        public DbSet<Building> Buildings =>Set<Building>();
        public DbSet<Classroom> Classrooms =>Set<Classroom>();
        public DbSet<GradeSectionStudentMapping> GradeSectionStudentMappings => Set<GradeSectionStudentMapping>();
        public DbSet<GradeClassroomAssignment> GradeClassroomAssignments =>Set<GradeClassroomAssignment>();
        public DbSet<GradeResource> GradeResources => Set<GradeResource>();
        public DbSet<SchoolSupply> SchoolSupplies =>Set<SchoolSupply>();
        public DbSet<AcademicYear> AcademicYears =>Set<AcademicYear>();
        public DbSet<Attendance> Attendances =>Set<Attendance>();
        public DbSet<Course > Courses =>Set<Course>();
        public DbSet<CourseGradeMapping> CourseGradeMappings =>Set<CourseGradeMapping>();
        public DbSet<Grade> Grades =>Set<Grade>();
        public DbSet<TranscriptRecord> TranscriptRecords => Set<TranscriptRecord>();
        public DbSet<GradeLevel> GradeLevels => Set<GradeLevel>();
        public DbSet<GradeSection> GradeSections => Set<GradeSection>();
        public DbSet<Schedule> Schedules =>Set<Schedule>();
        public DbSet<Semester> Semesters =>Set<Semester>();
        public DbSet<TeacherCourseAssignment> TeacherCourseAssignments =>Set<TeacherCourseAssignment>();
        public DbSet<Vacation> Vacations =>Set<Vacation>();
        public DbSet<GradeAppeal> GradeAppeals => Set<GradeAppeal>();
        public DbSet<TransferDocument> TransferDocuments => Set<TransferDocument>();
        public DbSet<StudentHistory> StudentHistories => Set<StudentHistory>();
        public DbSet<StaffAudit> StaffAudits => Set<StaffAudit>();
        public DbSet<ParentAudit> ParentAudits => Set<ParentAudit>();
        public DbSet<StudentFinance> StudentFinances => Set<StudentFinance>();
        public DbSet<DisciplinaryRecord> DisciplinaryRecords => Set<DisciplinaryRecord>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<MessageBody> MessageBodies => Set<MessageBody>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<CommentBody> CommentBodies => Set<CommentBody>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<NotificationBody> NotificationBodies => Set<NotificationBody>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            base.OnModelCreating(builder);

            builder.Entity<Branch>(entity =>
            {
               // entity.HasKey(b => b.Id);
                entity.Property(b => b.BranchNameFr).IsRequired().HasMaxLength(100);
                entity.Property(b => b.BranchNameAr).IsRequired().HasMaxLength(100);
                entity.Property(b => b.BranchNameEn).HasMaxLength(100);
                entity.Property(b => b.AddressFr).HasMaxLength(200).IsRequired();
                entity.Property(b => b.AddressAr).HasMaxLength(200).IsRequired();
                entity.Property(b => b.Phone).HasMaxLength(20);
                entity.Property(b => b.PrincipalNameFr).HasMaxLength(100).IsRequired();
                entity.Property(b => b.PrincipalNameAr).HasMaxLength(100).IsRequired();
                entity.Property(b => b.CityId).HasMaxLength(100).IsRequired();
            });

            // -------------------------
            // User & Personnel
            // -------------------------
            // Note: UserId is stored as string (non-FK)
            builder.Entity<Teacher>(entity =>
            {
               // entity.HasKey(t => t.Id);

                entity.Property(t => t.FirstNameFr).IsRequired().HasMaxLength(50);
                entity.Property(t => t.LastNameFr).IsRequired().HasMaxLength(50);

                entity.Property(t => t.FirstNameAr).IsRequired().HasMaxLength(50);
                entity.Property(t => t.LastNameAr).IsRequired().HasMaxLength(50);

                entity.Property(t => t.UserId).IsRequired();

                entity.Property(t => t.Email).HasMaxLength(100);
                entity.Property(t => t.Phone);
                entity.Property(t => t.SpecializationFr).HasMaxLength(100);
                entity.Property(t => t.SpecializationAr).HasMaxLength(100);

                entity.Property(t => t.HireDate);
                entity.Property(t => t.Status);

                // Relationship with Branch
                entity.HasOne(t => t.Branch)
                      .WithMany() // a branch can have many teachers
                      .HasForeignKey(t => t.BranchId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Staff>(entity =>
            {
               // entity.HasKey(s => s.Id);
                entity.Property(s => s.UserId).IsRequired();

                entity.Property(s => s.FirstNameFr).IsRequired().HasMaxLength(50);
                entity.Property(s => s.LastNameFr).IsRequired().HasMaxLength(50);

                entity.Property(s => s.FirstNameAr).IsRequired().HasMaxLength(50);
                entity.Property(s => s.LastNameAr).IsRequired().HasMaxLength(50);

                entity.Property(s => s.DepartmentFr).HasMaxLength(50);
                entity.Property(s => s.DepartmentAr).HasMaxLength(50);

                entity.Property(s => s.JobTitleFr).HasMaxLength(50);
                entity.Property(s => s.JobTitleAr).HasMaxLength(50);

                entity.Property(s => s.Email).HasMaxLength(50);
                entity.Property(s => s.Phone).HasMaxLength(50);

                entity.HasOne(s => s.Branch)
                      .WithMany() // a branch can have many staff members
                      .HasForeignKey(s => s.BranchId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Parent>(entity =>
            {
               // entity.HasKey(p => p.Id); // Correct primary key

                entity.Property(p => p.UserId).IsRequired();

                entity.Property(p => p.FirstNameFr).IsRequired().HasMaxLength(50);
                entity.Property(p => p.FirstNameAr).IsRequired().HasMaxLength(50);
                entity.Property(p => p.LastNameFr).IsRequired().HasMaxLength(50);
                entity.Property(p => p.LastNameAr).IsRequired().HasMaxLength(50);

                entity.Property(p => p.Email).HasMaxLength(100);
                entity.Property(p => p.Phone).HasMaxLength(20);

               
            });

            builder.Entity<ParentDetail>(entity =>
            {
               // entity.HasKey(pd => pd.Id); // From BaseEntity<Guid>

                entity.Property(pd => pd.AddressFr).IsRequired().HasMaxLength(200);
                entity.Property(pd => pd.AddressAr).HasMaxLength(200);
                entity.Property(pd => pd.Occupation).HasMaxLength(100);
                entity.Property(pd => pd.AdditionalContactInfo).HasMaxLength(200);

                entity.HasOne(pd => pd.Parent)
                      .WithOne(p => p.ParentDetail)
                      .HasForeignKey<ParentDetail>(pd => pd.ParentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<Student>(entity =>
            {
               // entity.HasKey(s => s.Id); // from BaseEntity<Guid>

                entity.Property(s => s.UserId).IsRequired();

                entity.Property(s => s.FirstNameFr).IsRequired().HasMaxLength(50);
                entity.Property(s => s.FirstNameAr).IsRequired().HasMaxLength(50);
                entity.Property(s => s.LastNameFr).IsRequired().HasMaxLength(50);
                entity.Property(s => s.LastNameAr).IsRequired().HasMaxLength(50);

                entity.Property(s => s.DateOfBirth).IsRequired();
                entity.Property(s => s.Gender).IsRequired();

             
                entity.Property(s => s.Status).IsRequired().HasMaxLength(50);

               

                entity.HasOne(s => s.Detail)
                      .WithOne(d => d.Student)
                      .HasForeignKey<StudentDetail>(d => d.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<StudentDetail>(entity =>
            {
               // entity.HasKey(sd => sd.Id); // from BaseEntity<Guid>

                entity.Property(sd => sd.MedicalInfo).HasMaxLength(500);
                entity.Property(sd => sd.EmergencyContact).HasMaxLength(100);
                entity.Property(sd => sd.PreviousSchool).HasMaxLength(100);
                entity.Property(sd => sd.AdditionalNotes).HasMaxLength(500);

                entity.HasOne(sd => sd.Student)
                      .WithOne(s => s.Detail)
                      .HasForeignKey<StudentDetail>(sd => sd.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // Many-to-many join between Students and Parents
            builder.Entity<StudentParent>(entity =>
            {
               // entity.HasKey(sp => sp.Id); // from BaseEntity<Guid>

                entity.Property(sp => sp.RelationshipType)
                      .IsRequired();

                entity.HasOne(sp => sp.Student)
                      .WithMany() // optionally, you can create a collection in Student like ICollection<StudentParent>
                      .HasForeignKey(sp => sp.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(sp => sp.Parent)
                      .WithMany() // optionally, a collection in Parent like ICollection<StudentParent>
                      .HasForeignKey(sp => sp.ParentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // -------------------------
            // Academic Structure
            // -------------------------
            builder.Entity<GradeLevel>(entity =>
            {
               // entity.HasKey(gl => gl.Id);

                entity.Property(gl => gl.TitleFr)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(gl => gl.TitleAr)
                      .HasMaxLength(100);

                entity.Property(gl => gl.TitleEn)
                      .HasMaxLength(100);

                entity.Property(gl => gl.Description)
                      .HasMaxLength(250);

                entity.Property(gl => gl.EducationalStage)
                      .IsRequired(); // enum - stored as int by default
            });


            builder.Entity<GradeSection>(entity =>
            {
               // entity.HasKey(gs => gs.Id);

                entity.Property(gs => gs.SectionNameFr)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(gs => gs.SectionNameAr)
                      .HasMaxLength(100);

                entity.Property(gs => gs.SectionNameEn)
                      .HasMaxLength(100);

                entity.Property(gs => gs.MaxCapacity)
                      .IsRequired();

                entity.HasOne(gs => gs.GradeLevel)
                      .WithMany() // One GradeLevel → Many GradeSections
                      .HasForeignKey(gs => gs.GradeLevelId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Building>(entity =>
            {
               // entity.HasKey(b => b.Id); // Ensure 'Id' is defined in your Building entity
                                          // Other property configurations...
            });

            builder.Entity<Classroom>(entity =>
            {
               // entity.HasKey(c => c.Id);

                entity.Property(c => c.RoomNumber)
                      .HasMaxLength(20);

                entity.Property(c => c.RoomTitleFr)
                      .HasMaxLength(100);

                entity.Property(c => c.RoomTitleAr)
                      .HasMaxLength(100);

                entity.Property(c => c.RoomType)
                      .IsRequired();

                entity.Property(c => c.Capacity)
                      .IsRequired();

                entity.HasOne(c => c.Building)
                      .WithMany() // one building → many classrooms
                      .HasForeignKey(c => c.BuildingId)
                      .OnDelete(DeleteBehavior.Restrict); // nullable FK, so Restrict is safer
            });


            builder.Entity<GradeClassroomAssignment>(entity =>
            {
               // entity.HasKey(gca => gca.Id);

                entity.Property(gca => gca.IsFixed)
                      .IsRequired();

                entity.HasOne(gca => gca.GradeSection)
                      .WithMany() // One section can appear in many assignments
                      .HasForeignKey(gca => gca.GradeSectionId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(gca => gca.Classroom)
                      .WithMany() // One classroom can be assigned to many sections
                      .HasForeignKey(gca => gca.ClassroomId)
                      .OnDelete(DeleteBehavior.Restrict);

                // AcademicYear FK, assuming it's a separate entity
                entity.Property(gca => gca.AcademicYearId)
                      .IsRequired(); // relationship not defined in model — add if needed later
            });


            // -------------------------
            // Course Management
            // -------------------------
            builder.Entity<Course>(entity =>
            {
               // entity.HasKey(c => c.Id);

                entity.Property(c => c.TitleFr)
                      .IsRequired()
                      .HasMaxLength(100); // Optional: Add limit for consistency

                entity.Property(c => c.TitleAr)
                      .HasMaxLength(100);

                entity.Property(c => c.TitleEn)
                      .HasMaxLength(100);

                entity.Property(c => c.Code)
                      .HasMaxLength(50);

                entity.Property(c => c.Description)
                      .HasMaxLength(500); // Optional: adjust as per design
            });


            builder.Entity<CourseGradeMapping>()
                .HasOne(cgm => cgm.Course)
                .WithMany() // Or .WithMany(c => c.CourseGradeMappings) if navigation is defined
                .HasForeignKey(cgm => cgm.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<CourseGradeMapping>()
                .HasOne(cgm => cgm.GradeLevel)
                .WithMany(gl => gl.CourseGradeMappings)
                .HasForeignKey(cgm => cgm.GradeLevelId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<TeacherCourseAssignment>(entity =>
            {
               // entity.HasKey(tca => tca.Id);

                // Relationships
                entity.HasOne(tca => tca.Teacher)
                      .WithMany() // You can specify the navigation property on Teacher if applicable
                      .HasForeignKey(tca => tca.TeacherId)
                      .OnDelete(DeleteBehavior.Restrict); // Or set it to DeleteBehavior.Restrict if needed

                entity.HasOne(tca => tca.Course)
                        .WithOne(c => c.TeacherCourseAssignments)
                        .HasForeignKey<TeacherCourseAssignment>(tca => tca.CourseId)
                        .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(tca => tca.AcademicYear)
                      .WithMany() // You can specify the navigation property on AcademicYear if applicable
                      .HasForeignKey(tca => tca.AcademicYearId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Additional configuration for properties if needed
                entity.Property(tca => tca.TeacherId)
                      .IsRequired();

                entity.Property(tca => tca.CourseId)
                      .IsRequired();

                entity.Property(tca => tca.AcademicYearId)
                      .IsRequired();
            });


            // -------------------------
            // Scheduling and Time
            // -------------------------
            builder.Entity<AcademicYear>(entity =>
            {
               // entity.HasKey(ay => ay.Id);

                // Properties
                entity.Property(ay => ay.StartYear)
                      .IsRequired();

                entity.Property(ay => ay.EndYear)
                      .IsRequired();

                entity.Property(ay => ay.Description)
                      .HasMaxLength(500); // Adjust as necessary

                // You could add additional validation or length restrictions as needed
            });


            builder.Entity<Semester>(entity =>
            {
               // entity.HasKey(s => s.Id);

                // Properties
                entity.Property(s => s.AcademicYearId)
                      .IsRequired();

                entity.Property(s => s.Name)
                      .IsRequired();

                entity.Property(s => s.StartDate)
                      .IsRequired();

                entity.Property(s => s.EndDate)
                      .IsRequired();

                // Relationships
                entity.HasOne(s => s.AcademicYear)
                      .WithMany() // Specify the navigation property on AcademicYear if applicable
                      .HasForeignKey(s => s.AcademicYearId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Optional: If you want to ensure that the EndDate is after the StartDate
                entity.HasCheckConstraint("CK_Semester_EndDate_After_StartDate", "[EndDate] > [StartDate]");
            });

            builder.Entity<Schedule>(entity =>
            {
               // entity.HasKey(s => s.Id);

                // Properties
                entity.Property(s => s.CourseId)
                      .IsRequired();

                entity.Property(s => s.ClassroomId)
                      .IsRequired();

                entity.Property(s => s.GradeSectionId)
                      .IsRequired();

                entity.Property(s => s.DayOfWeek)
                      .IsRequired();

                

                entity.Property(s => s.StartTime)
                      .IsRequired();

                entity.Property(s => s.EndTime)
                      .IsRequired();

                // Relationships
                entity.HasOne(s => s.Course)
                      .WithMany() // Specify the navigation property on Course if applicable
                      .HasForeignKey(s => s.CourseId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.GradeSection)
                      .WithMany() // Specify the navigation property on GradeSection if applicable
                      .HasForeignKey(s => s.GradeSectionId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Classroom)
                      .WithMany() // Specify the navigation property on Classroom if applicable
                      .HasForeignKey(s => s.ClassroomId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Optional: You may want to add validation for StartTime and EndTime (e.g., EndTime > StartTime)
                entity.HasCheckConstraint("CK_Schedule_EndTime_After_StartTime", "[EndTime] > [StartTime]");
            });


            builder.Entity<Vacation>(entity =>
            {
               // entity.HasKey(v => v.Id);

                // Properties
                entity.Property(v => v.AcademicYearId)
                      .IsRequired();

                entity.Property(v => v.Name)
                      .IsRequired();

                entity.Property(v => v.StartDate)
                      .IsRequired();

                entity.Property(v => v.EndDate)
                      .IsRequired();

                entity.Property(v => v.Duration)
                      .IsRequired();

                // Relationships
                entity.HasOne(v => v.AcademicYear)
                      .WithMany() // You can specify the navigation property on AcademicYear if applicable
                      .HasForeignKey(v => v.AcademicYearId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Optional: You can add validation to ensure that EndDate is after StartDate
                entity.HasCheckConstraint("CK_Vacation_EndDate_After_StartDate", "[EndDate] > [StartDate]");
            });


            // -------------------------
            // Gradebook & Assessments
            // -------------------------
            builder.Entity<Grade>(entity =>
            {
               // entity.HasKey(g => g.Id);

                // Properties
                entity.Property(g => g.StudentId)
                      .IsRequired();

                entity.Property(g => g.CourseId)
                      .IsRequired();

                entity.Property(g => g.SemesterId)
                      .IsRequired();

                entity.Property(g => g.Score)
                      .IsRequired();

                entity.Property(g => g.GradeType)
                      .IsRequired();

                entity.Property(g => g.TeacherCommentFr)
                      .HasMaxLength(500); // Adjust length as needed

                entity.Property(g => g.TeacherCommentAr)
                      .HasMaxLength(500);

                entity.Property(g => g.TeacherCommentEn)
                      .HasMaxLength(500);

                // Relationships
                entity.HasOne(g => g.Student)
                      .WithMany() // You can specify the navigation property on Student if applicable
                      .HasForeignKey(g => g.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(g => g.Course)
                      .WithMany() // You can specify the navigation property on Course if applicable
                      .HasForeignKey(g => g.CourseId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(g => g.Semester)
                      .WithMany() // You can specify the navigation property on Semester if applicable
                      .HasForeignKey(g => g.SemesterId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<GradeHistory>(entity =>
            {
               // entity.HasKey(gh => gh.Id);

                // Properties
                entity.Property(gh => gh.GradeId)
                      .IsRequired();

                entity.Property(gh => gh.OldScore)
                      .IsRequired();

                entity.Property(gh => gh.NewScore)
                      .IsRequired();

                entity.Property(gh => gh.ModifiedBy)
                      .IsRequired()
                      .HasMaxLength(100); // Adjust as needed for the length of the username or modifier

                entity.Property(gh => gh.ModifiedAt)
                      .IsRequired();

                // Relationships
                entity.HasOne(gh => gh.Grade)
                      .WithMany() // You can specify the navigation property on Grade if applicable
                      .HasForeignKey(gh => gh.GradeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // -------------------------
            // Attendance
            // -------------------------
            builder.Entity<Attendance>(entity =>
            {
               // entity.HasKey(a => a.Id);

                // Properties
                entity.Property(a => a.StudentId)
                      .IsRequired();

                entity.Property(a => a.CourseId)
                      .IsRequired();

                entity.Property(a => a.Date)
                      .IsRequired();

                entity.Property(a => a.Status)
                      .IsRequired();

                entity.Property(a => a.NotesFr)
                      .HasMaxLength(500); // Adjust as needed

                entity.Property(a => a.NotesAr)
                      .HasMaxLength(500);

                entity.Property(a => a.NotesEn)
                      .HasMaxLength(500);

                // Relationships
                entity.HasOne(a => a.Student)
                      .WithMany() // You can specify the navigation property on Student if applicable
                      .HasForeignKey(a => a.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Course)
                      .WithMany() // You can specify the navigation property on Course if applicable
                      .HasForeignKey(a => a.CourseId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<AttendanceHistory>(entity =>
            {
               // entity.HasKey(ah => ah.Id);

                // Properties
                entity.Property(ah => ah.AttendanceId)
                      .IsRequired();

                entity.Property(ah => ah.StudentId)
                      .IsRequired();

                entity.Property(ah => ah.Date)
                      .IsRequired();

                entity.Property(ah => ah.Status)
                      .IsRequired();

                entity.Property(ah => ah.ChangedBy)
                      .IsRequired()
                      .HasMaxLength(100); // Adjust as needed for the length of the username or modifier

                entity.Property(ah => ah.ChangedAt)
                      .IsRequired();

                entity.Property(ah => ah.ChangeReason)
                      .IsRequired();

                // Relationships
                entity.HasOne(ah => ah.Student)
                    .WithMany()
                    .HasForeignKey(ah => ah.StudentId)
                    .OnDelete(DeleteBehavior.Restrict); // Changed from Restrict

                entity.HasOne(ah => ah.Attendances)
                      .WithMany()
                      .HasForeignKey(ah => ah.AttendanceId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // -------------------------
            // Online Enrollment & Transfers
            // -------------------------
            builder.Entity<Enrollment>(entity =>
            {
               // entity.HasKey(e => e.Id);

                // Properties
                entity.Property(e => e.StudentId)
                      .IsRequired();

                entity.Property(e => e.BranchId)
                      .IsRequired();

                entity.Property(e => e.Status)
                      .IsRequired();

                entity.Property(e => e.SubmittedAt)
                      .IsRequired();

                entity.Property(e => e.AdminComment)
                      .HasMaxLength(500); // Adjust length as needed for the admin comment

                // Relationships
                entity.HasOne(e => e.Student)
                      .WithMany() // You can specify the navigation property on Student if applicable
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Optional: Add relationship with Branch if there's a Branch entity
                entity.HasOne(e => e.Branch)
                      .WithMany() // Specify the navigation property on Branch if applicable
                      .HasForeignKey(e => e.BranchId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<EnrollmentDocument>(entity =>
            {
               // entity.HasKey(ed => ed.Id);

                // Properties
                entity.Property(ed => ed.EnrollmentId)
                      .IsRequired();

                entity.Property(ed => ed.FilePath)
                      .IsRequired()
                      .HasMaxLength(500); // Adjust based on the expected length for file path or URL

                entity.Property(ed => ed.DocumentType)
                      .IsRequired();

                entity.Property(ed => ed.UploadedAt)
                      .IsRequired();

                entity.Property(ed => ed.VerificationComment)
                      .HasMaxLength(500); // Adjust length as needed for remarks

                // Relationships
                entity.HasOne(ed => ed.Enrollment)
                      .WithMany() // You can specify the navigation property on Enrollment if applicable
                      .HasForeignKey(ed => ed.EnrollmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<EnrollmentStatusHistory>(entity =>
            {
               // entity.HasKey(esh => esh.Id);

                // Properties
                entity.Property(esh => esh.EnrollmentId)
                      .IsRequired();

                entity.Property(esh => esh.OldStatus)
                      .IsRequired();

                entity.Property(esh => esh.NewStatus)
                      .IsRequired();

                entity.Property(esh => esh.ChangedBy)
                      .IsRequired()
                      .HasMaxLength(100); // Adjust the length based on your system's user identifier

                entity.Property(esh => esh.ChangedAt)
                      .IsRequired();

                entity.Property(esh => esh.ChangeReason)
                      .HasMaxLength(500); // Adjust length as needed for the change reason

                // Relationships
                entity.HasOne(esh => esh.Enrollment)
                      .WithMany() // You can specify the navigation property on Enrollment if applicable
                      .HasForeignKey(esh => esh.EnrollmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<TransferRequest>(entity =>
            {
               // entity.HasKey(tr => tr.Id);

                // Properties
                entity.Property(tr => tr.StudentId)
                      .IsRequired();

                entity.Property(tr => tr.FromBranchId)
                      .IsRequired();

                entity.Property(tr => tr.FromSchoolId)
                      .IsRequired(false); // Nullable, hence no 'IsRequired'

                entity.Property(tr => tr.ToBranchId)
                      .IsRequired();

                entity.Property(tr => tr.ToSchoolId)
                      .IsRequired();

                entity.Property(tr => tr.Status)
                      .IsRequired();

                entity.Property(tr => tr.SubmittedAt)
                      .IsRequired();

                entity.Property(tr => tr.AdminComment)
                      .HasMaxLength(500); // Adjust as needed for admin comments

                // Relationships
                entity.HasOne(tr => tr.Student)
                      .WithMany() // You can specify the navigation property on Student if applicable
                      .HasForeignKey(tr => tr.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Optional: Add relationships with Branch and School entities if they exist
                // entity.HasOne(tr => tr.FromBranch)
                //       .WithMany() 
                //       .HasForeignKey(tr => tr.FromBranchId)
                //       .OnDelete(DeleteBehavior.Restrict);

                // entity.HasOne(tr => tr.ToBranch)
                //       .WithMany() 
                //       .HasForeignKey(tr => tr.ToBranchId)
                //       .OnDelete(DeleteBehavior.Restrict);

                // If there's a relationship with School entity, you can add those similarly
                // entity.HasOne(tr => tr.FromSchool)
                //       .WithMany()
                //       .HasForeignKey(tr => tr.FromSchoolId)
                //       .OnDelete(DeleteBehavior.Restrict);

                // entity.HasOne(tr => tr.ToSchool)
                //       .WithMany()
                //       .HasForeignKey(tr => tr.ToSchoolId)
                //       .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<TransferRequestHistory>(entity =>
            {
               // entity.HasKey(esh => esh.Id);

                // Properties
                entity.Property(esh => esh.TransferRequestId)
                      .IsRequired();

                entity.Property(esh => esh.OldStatus)
                      .IsRequired();

                entity.Property(esh => esh.NewStatus)
                      .IsRequired();

                entity.Property(esh => esh.ChangedBy)
                      .IsRequired()
                      .HasMaxLength(100); // Adjust based on your system's user identifier

                entity.Property(esh => esh.ChangedAt)
                      .IsRequired();

                entity.Property(esh => esh.ChangeReason)
                      .IsRequired(); // Assuming ChangeReason is required
                entity.Property(esh => esh.Reason)
                      .HasMaxLength(500); // Adjust based on your system's user identifier

                // Relationships
                entity.HasOne(esh => esh.TransferRequest)
                      .WithMany() // You can specify the navigation property on Enrollment if applicable
                      .HasForeignKey(esh => esh.TransferRequestId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // -------------------------
            // Resources & Supplies
            // -------------------------
            builder.Entity<Book>(entity =>
            {
               // entity.HasKey(b => b.Id);

                // Properties
                entity.Property(b => b.TitleFr)
                      .IsRequired()
                      .HasMaxLength(200); // Adjust max length as needed

                entity.Property(b => b.TitleAr)
                      .HasMaxLength(200);

                entity.Property(b => b.TitleEn)
                      .HasMaxLength(200);

                entity.Property(b => b.AuthorNameFr)
                      .IsRequired()
                      .HasMaxLength(200); // Adjust max length as needed

                entity.Property(b => b.AuthorNameAr)
                      .HasMaxLength(200);

                entity.Property(b => b.AuthorNameEn)
                      .HasMaxLength(200);

                entity.Property(b => b.ISBN)
                      .HasMaxLength(13); // ISBN is typically 13 characters long

                entity.Property(b => b.Subject)
                      .IsRequired()
                      .HasMaxLength(100); // Adjust max length as needed
            });


            builder.Entity<SchoolSupply>(entity =>
            {
               // entity.HasKey(ss => ss.Id);

                // Properties
                entity.Property(ss => ss.NameFr)
                      .IsRequired()
                      .HasMaxLength(200); // Adjust max length as needed

                entity.Property(ss => ss.NameAr)
                      .HasMaxLength(200);

                entity.Property(ss => ss.NameEn)
                      .HasMaxLength(200);

                entity.Property(ss => ss.DescriptionFr)
                      .HasMaxLength(500); // Adjust max length as needed

                entity.Property(ss => ss.DescriptionAr)
                      .HasMaxLength(500);

                entity.Property(ss => ss.DescriptionEn)
                      .HasMaxLength(500);
            });


            builder.Entity<GradeResource>(entity =>
            {
               // entity.HasKey(gr => gr.Id);

                // Properties
                entity.Property(gr => gr.GradeLevelId)
                      .IsRequired();

                entity.Property(gr => gr.SupplyQuantity)
                      .IsRequired(false); // Nullable, adjust if needed

                // Relationships
                entity.HasOne(gr => gr.GradeLevel)
                      .WithMany() // You can specify the navigation property on GradeLevel if applicable
                      .HasForeignKey(gr => gr.GradeLevelId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(gr => gr.Book)
                      .WithMany() // You can specify the navigation property on Book if applicable
                      .HasForeignKey(gr => gr.BookId)
                      .OnDelete(DeleteBehavior.SetNull); // Assuming Book is optional

                entity.HasOne(gr => gr.Supply)
                      .WithMany() // You can specify the navigation property on SchoolSupply if applicable
                      .HasForeignKey(gr => gr.SupplyId)
                      .OnDelete(DeleteBehavior.SetNull); // Assuming Supply is optional
            });

            // -------------------------
            // Communication (Messages, Notifications, Comments)
            // (Assuming these are already configured)
            // -------------------------
            builder.Entity<Notification>(entity =>
            {
               // entity.HasKey(n => n.Id);

                // Properties
                entity.Property(n => n.Target)
                      .IsRequired();

                entity.Property(n => n.IsRead)
                      .IsRequired();
                entity.Property(n => n.IsSeen)
                     .IsRequired();
                entity.Property(n => n.NotificationBodyId)
                     .IsRequired();
                entity.Property(n => n.UserId)
                     .IsRequired(false);
                //entity.Property(n => n.User)
                //     .IsRequired();
                // Relationships
                entity.HasOne(n => n.NotificationBody)
                      .WithMany() // You can specify the navigation property on GradeLevel if applicable
                      .HasForeignKey(n => n.NotificationBodyId)
                      .OnDelete(DeleteBehavior.Restrict);

            });
            // NotificationBody
            builder.Entity<NotificationBody>(entity =>
            {
               // entity.HasKey(nb => nb.Id);

                entity.Property(nb => nb.Title)
                      .IsRequired()
                      .HasMaxLength(200); // Adjust as needed

                entity.Property(nb => nb.Description)
                      .IsRequired()
                      .HasMaxLength(1000); // Adjust as needed

                // Optional: If you want to configure the inverse navigation (if defined in your model)
                entity.HasMany(nb => nb.Notifications)
                      .WithOne(n => n.NotificationBody)
                      .HasForeignKey(n => n.NotificationBodyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Message & MessageBody
            //builder.Entity<Message>(entity =>
            //{
            //   // entity.HasKey(m => m.Id);

            //    entity.Property(m => m.SenderId)
            //          .IsRequired();

            //    // TargetId is optional, so no extra configuration is needed

            //    // Configure one-to-one relationship using a shared primary key
            //    entity.HasOne(m => m.Body)
            //          .WithOne()
            //          .HasForeignKey<MessageBody>(mb => mb.Id)
            //          .OnDelete(DeleteBehavior.Restrict);
            //});

            //builder.Entity<MessageBody>(entity =>
            //{
            //   // entity.HasKey(mb => mb.Id);

            //    entity.Property(mb => mb.Content)
            //          .IsRequired()
            //          .HasMaxLength(2000); // Adjust as needed
            //});

            // Comment & CommentBody
            //builder.Entity<Comment>(entity =>
            //{
            //   // entity.HasKey(c => c.Id);

            //    //entity.Property(c => c.Body.FirstOrDefault().SenderId)
            //    //      .IsRequired();

            //    // TargetEntityId is optional, so no extra configuration is needed

            //    // Configure one-to-one relationship using a shared primary key
            //    entity.HasOne(c => c.Body)
            //          .WithOne()
            //          .HasForeignKey<CommentBody>(cb => cb.Id)
            //          .OnDelete(DeleteBehavior.Restrict);
            //});

            //builder.Entity<CommentBody>(entity =>
            //{
            //   // entity.HasKey(cb => cb.Id);

            //    entity.Property(cb => cb.Content)
            //          .IsRequired()
            //          .HasMaxLength(2000); // Adjust as needed
            //});

            // You can add additional configuration if needed.

            // -------------------------
            // Indexes and Constraints
            // -------------------------
            // Example: Indexes on frequently queried fields:
       
            builder.Entity<Enrollment>().HasIndex(e => e.BranchId);
            builder.Entity<Teacher>().HasIndex(t => t.BranchId);
        }
    }
}