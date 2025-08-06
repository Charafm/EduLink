using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class EnrollmentDTO
    {
        public Guid StudentId { get; set; }
        public Guid BranchId { get; set; }
        public EnrollmentStatusEnum Status { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string? AdminComment { get; set; }
    }
}
