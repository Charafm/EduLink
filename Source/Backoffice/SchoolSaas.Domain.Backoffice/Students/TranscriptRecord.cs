using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Students
{
    public class TranscriptRecord : BaseEntity<Guid>
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public double Grade { get; set; }
        public string Term { get; set; }
        public string AcademicYear { get; set; }
        public Course Course { get; set; }
    }
}
