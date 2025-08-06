using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class StudentDTO
    {
        public Guid? Id { get; set; }
        public string UserId { get; set; }
      
        public string FirstNameFr { get; set; }
        public string FirstNameAr { get; set; }
        public string LastNameFr { get; set; }
        public string LastNameAr { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
     
        public string Status { get; set; }

     

    }
    public class ParsedStudentDto : StudentDTO
    {
        public bool isEnrolled { get; set; }
    }
}
