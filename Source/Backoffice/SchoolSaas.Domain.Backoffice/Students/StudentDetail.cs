using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Students
{
    public class StudentDetail : BaseEntity<Guid>
    {
        public Guid StudentId { get; set; }

        public string? MedicalInfo { get; set; } 
        public string? EmergencyContact { get; set; }
        public string? PreviousSchool { get; set; }
        public string? AdditionalNotes { get; set; }
        public string? Email { get; set; }
        public string? Phone {  get; set; }
        public Student Student { get; set; }
    }
}