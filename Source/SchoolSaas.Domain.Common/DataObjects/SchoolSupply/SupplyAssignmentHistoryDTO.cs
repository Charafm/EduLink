using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.SchoolSupply
{
    public class SupplyAssignmentHistoryDTO
    {
        public DateTime ChangeDate { get; set; }
        public string UserId { get; set; }
        public SupplyAssignmentActionEnum Action { get; set; }
        public Guid? GradeResource { get; set; }
        public Guid? GradeLevel { get; set; }
        public Guid? SchoolSupply { get; set; }
    }
}
