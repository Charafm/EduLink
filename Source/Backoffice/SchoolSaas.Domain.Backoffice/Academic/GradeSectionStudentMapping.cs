using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Academic
{
    public class GradeSectionStudentMapping : BaseEntity<Guid>
    {
        public Guid StudentId { get; set; }
        public Guid GradeSectionId { get; set; }

        public Student Student { get; set; }
        public GradeSection GradeSection { get; set; }
    }
}
