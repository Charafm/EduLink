namespace SchoolSaas.Domain.Common.DataObjects.Attendance
{
    public class BulkAttendanceDTO
    {
        public List<AttendanceDTO> Records { get; set; } = new();
    }
}
