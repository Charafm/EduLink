using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Students
{
    public class Student : BaseEntity<Guid>
    {
        
        public string UserId { get; set; } 
       
        public string FirstNameFr { get; set; }
        public string FirstNameAr { get; set; }
        public string LastNameFr { get; set; }
        public string LastNameAr { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
  
        public string Status { get; set; } 
        public StudentDetail? Detail { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public ICollection<EnrollmentRequest> EnrollmentRequests { get; set; } = new List<EnrollmentRequest>();

    }
}
