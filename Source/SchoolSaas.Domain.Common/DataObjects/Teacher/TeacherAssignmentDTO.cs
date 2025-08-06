namespace SchoolSaas.Domain.Common.DataObjects.Teacher
{
    public class TeacherAssignmentDTO
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public Guid AcademicYearId { get; set; }
        public string TeacherName { get; set; }
        public string CourseTitle { get; set; }
        public string AcademicYear { get; set; }
    }
}
