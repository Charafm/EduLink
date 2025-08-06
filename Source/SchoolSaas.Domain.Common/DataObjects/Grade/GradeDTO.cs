using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Grade
{
    public class GradeDTO
    {
        public Guid Id { get; set; } 
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public Guid SemesterId { get; set; }
        public double Score { get; set; }
        public GradeTypeEnum GradeType { get; set; } // Exam,Assignment, Quiz, OralTest
        public string? TeacherCommentFr { get; set; }
        public string? TeacherCommentAr { get; set; }
        public string? TeacherCommentEn { get; set; }
    }
}
