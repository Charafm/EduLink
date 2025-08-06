namespace SchoolSaas.Domain.Common.DataObjects.Transfer
{
    public class TransferHistoryFilterDTO
    {
        public Guid TransferRequestId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
