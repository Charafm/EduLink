using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Staff
{
    public class Staff : BaseEntity<Guid>
    {
        public string UserId { get; set; }
        public Guid BranchId { get; set; }

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

        public Branch Branch { get; set; }
    }
}
