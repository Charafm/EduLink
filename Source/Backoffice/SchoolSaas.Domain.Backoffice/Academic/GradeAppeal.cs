using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Academic
{
    public class GradeAppeal : BaseEntity<Guid>
    {
        public Guid GradeId { get; set; }
        public string Reason { get; set; }
        public AppealStatusEnum Status { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime SubmittedAt { get; set; }
        public Grade Grade { get; set; }
    }
}
