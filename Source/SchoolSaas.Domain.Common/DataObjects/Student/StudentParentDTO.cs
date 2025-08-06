using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class StudentParentDTO
    {
        public Guid ParentId { get; set; }
        public string FullName { get; set; }
        public StudentParentRelationshipEnum Relationship { get; set; }
    }
}
