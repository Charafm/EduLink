namespace SchoolSaas.Domain.Common.DataObjects.Attendance
{
    public class AttendanceExcuseDTO
    {
        public Guid AttendanceId { get; set; }
        public string Explanation { get; set; }
        public string Language { get; set; } = "fr";
        public string? DocumentUrl { get; set; }
        public string SubmittedBy { get; set; }
    }
}
