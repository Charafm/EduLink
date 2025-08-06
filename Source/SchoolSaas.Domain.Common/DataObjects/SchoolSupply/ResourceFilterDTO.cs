using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.SchoolSupply
{
    public class ResourceFilterDTO
    {
        public Guid? GradeLevelId { get; set; }
        public ResourceType? ResourceType { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
