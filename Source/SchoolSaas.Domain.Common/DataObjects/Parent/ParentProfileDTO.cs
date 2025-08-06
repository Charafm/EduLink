using SchoolSaas.Domain.Common.DataObjects.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Parent
{
    public class ParentProfileDTO
    {
        public Guid ParentId { get; set; }
        public string FullNameFr { get; set; }
        public string FullNameAr { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Occupation { get; set; }
        public int AssociatedStudentsCount { get; set; }
        public List<StudentDTO> Children { get; set; }
    }
}
