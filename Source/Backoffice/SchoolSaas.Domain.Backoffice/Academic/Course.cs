using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Academic
{
    public class Course : BaseEntity<Guid>
    {
        public string TitleFr { get; set; }
        public string? TitleAr { get; set; }
        public string? TitleEn { get; set; }
        public string? Code { get; set; } 
        public string? Description { get; set; }
        public CourseGradeMapping GradeMapping { get; set; }
        public TeacherCourseAssignment TeacherCourseAssignments { get; set; }
    }
}
