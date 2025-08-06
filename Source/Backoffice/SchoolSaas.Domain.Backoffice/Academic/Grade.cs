using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Academic
{
    public class Grade : BaseEntity<Guid>
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public Guid SemesterId { get; set; }
        public Guid TeacherId { get; set; }
        public double Score { get; set; }
        public GradeTypeEnum GradeType { get; set; } 
        public string? TeacherCommentFr { get; set; }
        public string? TeacherCommentAr { get; set; }
        public string? TeacherCommentEn { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public Semester Semester { get; set; }
    }

}
