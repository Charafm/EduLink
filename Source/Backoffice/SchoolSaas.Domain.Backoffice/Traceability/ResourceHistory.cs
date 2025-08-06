using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Traceability
{
    public class ResourceHistory : BaseEntity<Guid>
    {
        public Guid ResourceId { get; set; }
        public ResourceType ResourceType { get; set; }
        public ResourceActionType ActionType { get; set; }
        public string OldValues { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
