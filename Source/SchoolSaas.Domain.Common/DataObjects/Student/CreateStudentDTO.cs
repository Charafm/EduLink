using SchoolSaas.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class CreateStudentDTO
    {
       
       public Guid ParentId { get; set; }
        public string FirstNameFr { get; set; }
        public string FirstNameAr { get; set; }
        public string LastNameFr { get; set; }
        public string LastNameAr { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
       
        public string Status { get; set; }
       
        public string? EmergencyContact { get; set; }
       
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
