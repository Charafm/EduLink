using SchoolSaas.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Staff
{
    public class CreateStaffDTO
    {
  
        public string FirstNameFr { get; set; }
        public string FirstNameAr { get; set; }
        public string LastNameFr { get; set; }
        public string LastNameAr { get; set; }
        public string? DepartmentFr { get; set; }
        public string? DepartmentAr { get; set; }
        public StaffRole Role { get; set; }
        public string? JobTitleFr { get; set; }
        public string? JobTitleAr { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
