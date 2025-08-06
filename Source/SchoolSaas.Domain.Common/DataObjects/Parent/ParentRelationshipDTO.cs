using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Parent
{
    public class ParentRelationshipDTO
    {
        public Guid ParentId { get; set; }
        public List<Guid> StudentIds { get; set; } = new();
        public StudentParentRelationshipEnum RelationshipType { get; set; }
    }
}
