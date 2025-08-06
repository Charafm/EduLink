namespace SchoolSaas.Domain.Common.DataObjects.Schedule
{
    public class ScheduleItemDTO
    {
        public Guid Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid ClassroomId { get; set; }
        public string ClassroomName { get; set; }
    }
}
