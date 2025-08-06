using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Backoffice.Resources;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Traceability
{
    public class SupplyAssignmentHistory : BaseEntity<Guid>
    {
        public DateTime ChangeDate { get; set; }
        public Guid AssignmentId { get; set; }
        public required string UserId { get; set; }
        public SupplyAssignmentActionEnum Action { get; set; }
        public Guid? GradeResource { get; set; }
        public Guid? GradeLevel { get; set; }
        public Guid? SchoolSupply { get; set; }
        public string? Comment { get; set; }
        public GradeResource? GradeResourceEntity { get; set; }
        public GradeLevel? GradeLevelEntity { get; set; }
        public SchoolSupply? SchoolSupplyEntity { get; set; }
    }

}
