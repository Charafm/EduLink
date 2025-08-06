using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Academic
{
    public class AcademicYear : BaseEntity<Guid>
    {
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string Description { get; set; } 
    }
}
