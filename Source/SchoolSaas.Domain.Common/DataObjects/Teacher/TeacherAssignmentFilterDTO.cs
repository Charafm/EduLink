namespace SchoolSaas.Domain.Common.DataObjects.Teacher
{
    public class TeacherAssignmentFilterDTO
    {
        public Guid? TeacherId { get; set; }
        public Guid? AcademicYearId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
