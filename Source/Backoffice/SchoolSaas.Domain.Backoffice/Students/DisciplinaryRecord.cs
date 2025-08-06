using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Students
{
    public class DisciplinaryRecord : BaseEntity<Guid>
    {
        public Guid StudentId { get; set; }
        public string Description { get; set; }
        public DateTime IncidentDate { get; set; }
        public bool Resolved { get; set; }
        public Student Student { get; set; }
    }
}
