using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Teacher
{
    public class FilterTeacherDTO
    {
        public Guid? BranchId { get; set; }
        public string? Specialization { get; set; }
        public TeacherStatusEnum? Status { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public bool IsAscending { get; set; } = true;
    }

}
