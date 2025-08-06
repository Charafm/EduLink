using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class StudentProfileDTO
    {
        public Guid StudentId { get; set; }
        public string FullNameFr { get; set; }
        public string FullNameAr { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string GradeSectionFr { get; set; }
        public string GrafeSectionAr { get; set; }
        public string GradeLevelFr { get; set; }
        public string GradeLevelAr { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
    }
}
