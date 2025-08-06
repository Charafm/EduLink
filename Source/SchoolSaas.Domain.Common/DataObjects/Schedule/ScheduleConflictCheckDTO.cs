namespace SchoolSaas.Domain.Common.DataObjects.Schedule
{
    public class ScheduleConflictCheckDTO
    {
        public Guid ClassroomId { get; set; }
        public Guid TeacherId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Guid? ExcludeScheduleId { get; set; }
    }
}
