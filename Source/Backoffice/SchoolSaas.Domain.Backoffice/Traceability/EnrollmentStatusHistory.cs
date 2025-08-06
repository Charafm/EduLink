using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Traceability
{
    public class EnrollmentStatusHistory : BaseEntity<Guid>
    {
        public Guid EnrollmentId { get; set; }

        public EnrollmentStatusEnum OldStatus { get; set; }
        public EnrollmentStatusEnum NewStatus { get; set; }

  
        public required string ChangedBy { get; set; }

        public DateTime ChangedAt { get; set; }

        // Optional: Reason for the status change
        public EnrollmentChangeReasonEnum ChangeReason { get; set; }
        public string Reason { get; set; }
        public Enrollment Enrollment { get; set; }
    }

}
