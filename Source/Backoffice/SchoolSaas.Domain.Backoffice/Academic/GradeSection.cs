using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Academic
{
    public class GradeSection : BaseEntity<Guid>
    {
        public Guid GradeLevelId { get; set; }
        public string SectionNameFr { get; set; }
        public string? SectionNameAr { get; set; }
        public string? SectionNameEn { get; set; }
        public int MaxCapacity { get; set; }

        public GradeLevel GradeLevel { get; set; }
    }
}
