using SchoolSaas.Domain.Common.DataObjects.Course;

namespace SchoolSaas.Domain.Common.DataObjects.Schedule
{
    public class ScheduleConstraintsDTO
    {
        public List<CourseSectionConstraints> CourseSections { get; set; } = new();
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
