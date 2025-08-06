using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Staff
{
    public class StaffAuditDTO
    {
        public DateTime ActionDate { get; set; }
        public StaffActionType ActionType { get; set; }
        public string Details { get; set; }
        public string PerformedBy { get; set; }
    }
}
