namespace SchoolSaas.Domain.Common.DataObjects.Course
{
    public class CourseSectionConstraints
    {
        public Guid CourseId { get; set; }
        public Guid GradeSectionId { get; set; }
        public int RequiredCapacity { get; set; }
        public List<DayOfWeek> PreferredDays { get; set; } = new();
        public TimeOnly PreferredStartTime { get; set; }
        public TimeOnly PreferredEndTime { get; set; }
    }
}
