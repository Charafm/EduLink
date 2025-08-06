using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class EnrollmentStatusHistoryDTO
    {
        public Guid EnrollmentId { get; set; }
        public EnrollmentStatusEnum OldStatus { get; set; }
        public EnrollmentStatusEnum NewStatus { get; set; }
        public string ChangedBy { get; set; } // Identity reference (e.g., UserId)
        public DateTime ChangedAt { get; set; }
        public EnrollmentChangeReasonEnum ChangeReason { get; set; }
    }
}
