using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Resources
{
    public class GradeClassroomAssignment : BaseEntity<Guid>
    {
        public Guid GradeSectionId { get; set; }
        public Guid ClassroomId { get; set; }
        public Guid AcademicYearId { get; set; }

        public bool IsFixed { get; set; } 

        public GradeSection GradeSection { get; set; }
        public Classroom Classroom { get; set; }
    }
}
