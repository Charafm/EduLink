using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Parent
{
    public class ParentAuditDTO
    {
        public DateTime ActionDate { get; set; }
        public ParentActionType ActionType { get; set; }
        public string Details { get; set; }
        public string PerformedBy { get; set; }
    }

}
