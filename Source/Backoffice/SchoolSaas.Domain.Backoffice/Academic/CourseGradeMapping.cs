using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Academic
{
    public class CourseGradeMapping : BaseEntity<Guid>
    {
        public Guid CourseId { get; set; }
        public Guid GradeLevelId { get; set; }

        //need to think about it
        public bool? IsElective { get; set; }
        public int? Sequence { get; set; }

        public Course Course { get; set; }
        public GradeLevel GradeLevel { get; set; }
    }

}
