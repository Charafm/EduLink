namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class AssociatedStudentStatusDTO
    {
        public Guid StudentId { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
    }
}
