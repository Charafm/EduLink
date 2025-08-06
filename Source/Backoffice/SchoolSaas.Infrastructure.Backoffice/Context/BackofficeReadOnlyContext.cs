using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Common.Context;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Backoffice.Resources;
using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Backoffice.Staff;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Backoffice.Traceability;


namespace SchoolSaas.Infrastructure.Backoffice.Context
{
    public class BackofficeReadOnlyContext : AbstractReadOnlyContext<BackofficeReadOnlyContext>, IBackofficeReadOnlyContext
    {
        public BackofficeReadOnlyContext(ITenantAccessor tenantAccessor, ICurrentUserService currentUserService,
            ICacheService cacheService, DbContextOptions<BackofficeReadOnlyContext> options)
            : base(tenantAccessor, currentUserService, cacheService, options)
        {
        }
        public DbSet<GradeSectionStudentMapping> GradeSectionStudentMappings => Set<GradeSectionStudentMapping>();
        public DbSet<AttendanceHistory> AttendanceHistories => Set<AttendanceHistory>();
        public DbSet<EnrollmentStatusHistory> EnrollmentStatusHistories => Set<EnrollmentStatusHistory>();
        public DbSet<GradeHistory> GradeHistory => Set<GradeHistory>();
        public DbSet<SupplyAssignmentHistory> SupplyAssignmentHistories => Set<SupplyAssignmentHistory>();
        public DbSet<TransferRequestHistory> TransferRequestHistories => Set<TransferRequestHistory>();
        public DbSet<Parent> Parents => Set<Parent>();
        public DbSet<ParentAudit> ParentAudits => Set<ParentAudit>();
        public DbSet<ParentDetail> ParentDetails => Set<ParentDetail>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<TranscriptRecord> TranscriptRecords { get; set; }
        public DbSet<StudentFinance> StudentFinances => Set<StudentFinance>();
        public DbSet<StudentDetail> StudentDetails => Set<StudentDetail>();
        public DbSet<StudentParent> StudentParents => Set<StudentParent>();
        public DbSet<Staff> Staffs => Set<Staff>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<EnrollmentRequest> EnrollmentRequests => Set<EnrollmentRequest>();
        public DbSet<EnrollmentDocument> EnrollmentDocuments => Set<EnrollmentDocument>();
        public DbSet<TransferRequest> TransferRequests => Set<TransferRequest>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Building> Buildings => Set<Building>();
        public DbSet<Classroom> Classrooms => Set<Classroom>();
        public DbSet<GradeClassroomAssignment> GradeClassroomAssignments => Set<GradeClassroomAssignment>();
        public DbSet<GradeResource> GradeResources => Set<GradeResource>();
        public DbSet<SchoolSupply> SchoolSupplies => Set<SchoolSupply>();
        public DbSet<AcademicYear> AcademicYears => Set<AcademicYear>();
        public DbSet<Attendance> Attendances => Set<Attendance>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<CourseGradeMapping> CourseGradeMappings => Set<CourseGradeMapping>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<GradeLevel> GradeLevels => Set<GradeLevel>();
        public DbSet<GradeSection> GradeSections => Set<GradeSection>();
        public DbSet<ResourceHistory> ResourceHistories => Set<ResourceHistory>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<Semester> Semesters => Set<Semester>();
        public DbSet<TeacherCourseAssignment> TeacherCourseAssignments => Set<TeacherCourseAssignment>();
        public DbSet<Vacation> Vacations => Set<Vacation>();
        public DbSet<GradeAppeal> GradeAppeals => Set<GradeAppeal>();
        public DbSet<StudentHistory> StudentHistories => Set<StudentHistory>();
        public DbSet<SchoolSupplyHistory> SchoolSupplyHistories => Set<SchoolSupplyHistory>();
        public DbSet<DisciplinaryRecord> DisciplinaryRecords => Set<DisciplinaryRecord>();
        public DbSet<TransferDocument> TransferDocuments => Set<TransferDocument>();
        public DbSet<StaffAudit> StaffAudits => Set<StaffAudit>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<MessageBody> MessageBodies => Set<MessageBody>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<CommentBody> CommentBodies => Set<CommentBody>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<NotificationBody> NotificationBodies => Set<NotificationBody>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
         
            base.OnModelCreating(builder);

        }

    }
}
