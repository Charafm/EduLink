using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Staff
{
    public class StaffFilterDTO
    {
        public Guid ?BranchId { get; set; }
       
        public string SearchTerm { get; set; }
        public StaffRole? Role { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
