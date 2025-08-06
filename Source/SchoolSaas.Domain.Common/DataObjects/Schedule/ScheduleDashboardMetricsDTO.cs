namespace SchoolSaas.Domain.Common.DataObjects.Schedule
{
    public class ScheduleDashboardMetricsDTO
    {
        public int TotalClassesScheduled { get; set; }
        public int TotalConflicts { get; set; }
        public double AverageDailyClasses { get; set; }
        // add any other metrics you need, e.g. ClassroomUtilizationPercent
    }
}
