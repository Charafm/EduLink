using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Attendance
{
    public class AttendanceHistoryDTO
    {
        public Guid AttendanceId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceEnum Status { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
        public AttendanceChangeReasonEnum ChangeReason { get; set; }
    }

}
