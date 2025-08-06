using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Parent
{
    public class ParentLinkDTO
    {
        public Guid ParentId { get; set; }
        public StudentParentRelationshipEnum RelationshipType { get; set; }
    }
}
