using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Attendance
{
    public class AttendanceDTO
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }

        public DateTime Date { get; set; }
        public AttendanceEnum Status { get; set; }
        public string? NotesFr { get; set; }
        public string? NotesAr { get; set; }
        public string? NotesEn { get; set; }
    }
}
 