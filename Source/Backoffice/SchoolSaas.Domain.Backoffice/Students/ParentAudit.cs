using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Students
{
    public class ParentAudit : BaseEntity<Guid>
    {
        public Guid ParentId { get; set; }
        public ParentActionType ActionType { get; set; }
        public DateTime ActionDate { get; set; }
        public string Details { get; set; }
        public string PerformedBy { get; set; }
        public Parent Parent { get; set; }
    }
}
