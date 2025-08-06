using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Grade
{
    public class GradeAppealDTO
    {
        public Guid GradeId { get; set; }
        public string Reason { get; set; }
        public AppealStatusEnum Status { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
