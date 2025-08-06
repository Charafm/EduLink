using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Transfer
{
    public class TransferStatusUpdateDTO
    {
        public TransferRequestStatus NewStatus { get; set; }
        public string AdminComment { get; set; }
        public string Reason { get; set; }
        public string ChangedBy { get; set; }
        public TransferRequestChangeReasonEnum ChangeReason { get; set; }
    }

}
