using SchoolSaas.Domain.Backoffice.Staff;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Academic
{
    public class TeacherCourseAssignment : BaseEntity<Guid>
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public Guid AcademicYearId { get; set; }

        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
        public AcademicYear AcademicYear { get; set; }
    }

}
