using SchoolSaas.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Teacher
{
    public class CreateTeacherDTO
    {

        public Guid? BranchId { get; set; }
        //public Guid DepartmentId { get; set; }
        public string FirstNameFr { get; set; }
        public string FirstNameAr { get; set; }
        public string LastNameFr { get; set; }
        public string LastNameAr { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime HireDate { get; set; }
        public string? SpecializationFr { get; set; }
        public string? SpecializationAr { get; set; }
        public TeacherStatusEnum Status { get; set; }
    }
}
