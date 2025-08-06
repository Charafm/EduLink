using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class EnrollmentFilterDTO
    {
    
        public Guid? BranchId { get; set; }
        public EnrollmentStatusEnum? Status { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public string searchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

}
