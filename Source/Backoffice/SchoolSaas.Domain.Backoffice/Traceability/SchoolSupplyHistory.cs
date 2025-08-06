using SchoolSaas.Domain.Backoffice.Resources;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Traceability
{
    public class SchoolSupplyHistory : BaseEntity<Guid>
    {
        public Guid SupplyId { get; set; }
        public ResourceActionType ActionType { get; set; }
        public string OldValues { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
        public SchoolSupply Supply { get; set; }
    }
}
