using Microsoft.EntityFrameworkCore;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Backoffice.Traceability;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Backoffice.Staff;
using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Backoffice.Resources;
using SchoolSaas.Domain.Backoffice.Academic;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IBackofficeReadOnlyContext : IReadOnlyContext
    {
        DbSet<TranscriptRecord> TranscriptRecords { get; }  
        DbSet<Message> Messages { get; }
        DbSet<MessageBody> MessageBodies { get; }
        DbSet<Comment> Comments { get; }
        DbSet<CommentBody> CommentBodies { get; }
        DbSet<GradeAppeal> GradeAppeals { get; }
        DbSet<Notification> Notifications { get; }
        DbSet<NotificationBody> NotificationBodies { get; }
       DbSet<AttendanceHistory> AttendanceHistories { get; }
        DbSet<EnrollmentStatusHistory> EnrollmentStatusHistories { get; }
        DbSet<GradeHistory> GradeHistory { get; }
        DbSet<SupplyAssignmentHistory> SupplyAssignmentHistories { get; }
        DbSet<TransferRequestHistory> TransferRequestHistories { get; }
        DbSet<Parent> Parents { get; }
        DbSet<ParentDetail> ParentDetails { get; }
        DbSet<ParentAudit> ParentAudits { get; }
        DbSet<EnrollmentRequest> EnrollmentRequests { get; }
        DbSet<Student> Students { get; }
        DbSet<StudentDetail> StudentDetails { get; }
        DbSet<StudentParent> StudentParents { get; }
        DbSet<Staff> Staffs { get; }
        DbSet<Teacher> Teachers { get; }
        DbSet<Branch> Branches { get; }
        DbSet<Enrollment> Enrollments { get; }
        DbSet<EnrollmentDocument> EnrollmentDocuments { get; }
        DbSet<TransferRequest> TransferRequests { get; }
        DbSet<Book> Books { get; }
        DbSet<Building> Buildings { get; }
        DbSet<Classroom> Classrooms { get; }
        DbSet<GradeClassroomAssignment> GradeClassroomAssignments { get; }
        DbSet<GradeResource> GradeResources { get; }
        DbSet<SchoolSupply> SchoolSupplies { get; }
        DbSet<AcademicYear> AcademicYears { get; }
        DbSet<Attendance> Attendances { get; }
        DbSet<Course> Courses { get; }
        DbSet<CourseGradeMapping> CourseGradeMappings { get; }
        DbSet<Grade> Grades { get; }
        DbSet<GradeLevel> GradeLevels { get; }
        DbSet<GradeSection> GradeSections { get; }
        DbSet<Schedule> Schedules { get; }
        DbSet<Semester> Semesters { get; }
        DbSet<TeacherCourseAssignment> TeacherCourseAssignments { get; }
        DbSet<Vacation> Vacations { get; }
        DbSet<StudentHistory> StudentHistories {  get; }
        DbSet<StudentFinance> StudentFinances { get; }
        DbSet<TransferDocument> TransferDocuments { get; }
        DbSet<DisciplinaryRecord> DisciplinaryRecords { get; }
        DbSet<StaffAudit> StaffAudits { get; }
        DbSet<ResourceHistory> ResourceHistories { get; }
        DbSet<GradeSectionStudentMapping> GradeSectionStudentMappings { get; }
        DbSet<SchoolSupplyHistory> SchoolSupplyHistories { get; }
    }
}
