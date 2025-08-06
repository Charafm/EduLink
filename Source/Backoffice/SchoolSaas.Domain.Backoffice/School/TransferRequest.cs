using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.School
{
    public class TransferRequest : BaseEntity<Guid>
    {

        public Guid StudentId { get; set; }

        // Origin branch/school ID (from current enrollment)
        public Guid FromBranchId { get; set; }
        public Guid? FromSchoolId { get; set; }

        // Target branch/school ID (desired destination)
        public Guid ToBranchId { get; set; }
        public Guid? ToSchoolId { get; set; }
        public TransferRequestReasonEnum ReasonEnum { get; set; }
        public string? Reason { get; set; }
        public TransferRequestStatus Status { get; set; }
        public List<TransferDocument>? Documents { get; set; }
        
        public DateTime SubmittedAt { get; set; }


        // Administrator comments or feedback regarding the transfer
     
        public string? AdminComment { get; set; }
        public Student Student { get; set; }
    }

}
