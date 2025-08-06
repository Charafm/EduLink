using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Traceability
{
    public class AttendanceHistory : BaseEntity<Guid>
    {
        public Guid AttendanceId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceEnum Status { get; set; } 

        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
        public AttendanceChangeReasonEnum ChangeReason { get; set; }


        public string? PreviousNotesFr { get; set; }
        public string? PreviousNotesAr { get; set; }
        public string? PreviousNotesEn { get; set; }

        public Student Student { get; set; }
        public Attendance Attendances { get; set; }

    }
}
