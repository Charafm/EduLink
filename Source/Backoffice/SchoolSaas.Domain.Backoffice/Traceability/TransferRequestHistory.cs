using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Traceability
{
    public class TransferRequestHistory : BaseEntity<Guid>
    {
        public Guid TransferRequestId { get; set; }

        public TransferRequestStatus OldStatus { get; set; }
        public TransferRequestStatus NewStatus { get; set; }

        public required string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }

        // Optional: Explanation or reason for change
        public TransferRequestChangeReasonEnum ChangeReason { get; set; }
        public string? Reason { get; set; }
        public TransferRequest TransferRequest { get; set; }
    }

}
