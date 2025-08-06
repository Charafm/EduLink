namespace SchoolSaas.Domain.Common.DataObjects.Teacher
{
    public class BulkTeacherAssignmentDTO
    {
        public List<TeacherAssignmentDTO> Assignments { get; set; } = new();
    }
}
