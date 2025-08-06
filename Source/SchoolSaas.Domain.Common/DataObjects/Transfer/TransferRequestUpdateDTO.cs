using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Domain.Common.DataObjects.Transfer
{
    public class TransferRequestUpdateDTO
    {
        public Guid? ToBranchId { get; set; }
        public Guid? ToSchoolId { get; set; }
        public string Reason { get; set; }
        public List<DocumentDTO> Documents { get; set; } = new();
        public string UpdatedBy { get; set; } 
    }
}
