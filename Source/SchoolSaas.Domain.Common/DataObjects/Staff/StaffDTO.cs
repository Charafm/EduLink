using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Staff
{
    public class StaffDTO
    {
        public Guid Id { get; set; }
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
    }
}
