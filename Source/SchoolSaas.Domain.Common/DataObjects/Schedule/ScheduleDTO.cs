namespace SchoolSaas.Domain.Common.DataObjects.Schedule
{
    public class ScheduleDTO
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid GradeSectionId { get; set; }
        public Guid ClassroomId { get; set; }
        public Guid TeacherId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
