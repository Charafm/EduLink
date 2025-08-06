using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Transfer
{
    public class TransferRequestDTO
    {
        public Guid StudentId { get; set; }
        public Guid FromBranchId { get; set; }
        public Guid ToBranchId { get; set; }
        public Guid? FromSchoolId { get; set; }
        public Guid? ToSchoolId { get; set; }
        public TransferRequestStatus Status { get; set; }
        public List<DocumentDTO>? Documents { get; set; }
        public string? Reason { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string? AdminComment { get; set; }
    }

}
