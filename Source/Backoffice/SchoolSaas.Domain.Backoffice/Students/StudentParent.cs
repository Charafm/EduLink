using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Students
{
    public class StudentParent : BaseEntity<Guid>
    {
        public Guid StudentId { get; set; }
        public Guid ParentId { get; set; }

        public StudentParentRelationshipEnum RelationshipType { get; set; } // Mother, Father, Guardian

        public Student Student { get; set; }
        public Parent Parent { get; set; }
    }
}
