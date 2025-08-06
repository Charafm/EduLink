namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class StudentFilterDTO
    {
        public Guid? BranchId { get; set; }
        public string Status { get; set; }
        public DateTime? EnrollmentStart { get; set; }
        public DateTime? EnrollmentEnd { get; set; }
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
