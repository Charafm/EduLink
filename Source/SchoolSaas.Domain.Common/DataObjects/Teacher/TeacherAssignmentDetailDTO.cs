namespace SchoolSaas.Domain.Common.DataObjects.Teacher
{
    public class TeacherAssignmentDetailDTO
    {
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; }
        public Guid AcademicYearId { get; set; }
    }
}
