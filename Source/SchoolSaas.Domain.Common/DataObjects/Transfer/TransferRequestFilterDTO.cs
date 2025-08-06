using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Transfer
{
    public class TransferRequestFilterDTO
    {
        public Guid? StudentId { get; set; }
        public Guid? FromSchoolId { get; set; }
        public TransferRequestStatus? Status { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
