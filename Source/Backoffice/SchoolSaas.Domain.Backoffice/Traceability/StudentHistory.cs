using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Traceability
{
    public class StudentHistory : BaseEntity<Guid>
    {
        public Guid StudentId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
        public string FieldChanges { get; set; }
        public Student Student { get; set; }
    }
}
