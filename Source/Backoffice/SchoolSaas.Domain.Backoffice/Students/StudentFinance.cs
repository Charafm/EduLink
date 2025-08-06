using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Students
{
    public class StudentFinance: BaseEntity<Guid>
    {
        public Guid StudentId { get; set; }
        public PaymentStatusEnum Status { get; set; }
        public double? DueAmount { get; set; }
        public string? Comment { get; set; }
    }

   
}
