namespace SchoolSaas.Domain.Common.DataObjects.Attendance
{
    public class AttendanceSummaryDTO
    {
        public int TotalDays { get; set; }
        public int PresentDays { get; set; }
        public int LateDays { get; set; }
        public int AbsentDays { get; set; }
        public decimal AttendanceRate { get; set; }
    }
}
