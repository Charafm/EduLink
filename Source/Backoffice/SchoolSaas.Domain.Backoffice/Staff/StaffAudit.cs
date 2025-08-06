using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Staff
{
    public class StaffAudit : BaseEntity<Guid>
    {
        public Guid StaffId { get; set; }
        public StaffActionType ActionType { get; set; }
        public DateTime ActionDate { get; set; }
        public string Details { get; set; }
        public string PerformedBy { get; set; }
        public Staff Staff { get; set; }
    }
}
