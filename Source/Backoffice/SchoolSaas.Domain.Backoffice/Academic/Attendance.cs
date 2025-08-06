using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Academic
{
    public class Attendance : BaseEntity<Guid>
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }

        public DateTime Date { get; set; }
        public AttendanceEnum Status { get; set; } 
        public string? NotesFr { get; set; }
        public string? NotesAr { get; set; }
        public string? NotesEn { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }

}
