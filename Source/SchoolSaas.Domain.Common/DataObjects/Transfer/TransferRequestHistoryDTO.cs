using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Transfer
{
    public class TransferRequestHistoryDTO
    {
        public Guid TransferRequestId { get; set; }
        public TransferRequestStatus OldStatus { get; set; }
        public TransferRequestStatus NewStatus { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
        public TransferRequestChangeReasonEnum ChangeReason { get; set; }
        public string Reason { get; set; }
    }
}
