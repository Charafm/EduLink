using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Academic
{
    public class GradeLevel : BaseEntity<Guid>
    {
        public string TitleFr { get; set; }
        public string? TitleAr { get; set; }
        public string? TitleEn { get; set; }
        public string? Description { get; set; }
        public EducationalStageEnum EducationalStage { get; set; }
        public virtual ICollection<CourseGradeMapping> CourseGradeMappings { get; set; }

    }
}
