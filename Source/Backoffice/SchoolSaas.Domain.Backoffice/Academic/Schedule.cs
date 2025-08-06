using SchoolSaas.Domain.Backoffice.Resources;
using SchoolSaas.Domain.Backoffice.Staff;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Academic
{
    public class Schedule : BaseEntity<Guid>
    {
        public Guid CourseId { get; set; }
        public Guid ClassroomId { get; set; }
        public Guid GradeSectionId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid TeacherId { get; set; }

        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
        public GradeSection GradeSection { get; set; }
        public Classroom Classroom { get; set; }
    }

}
