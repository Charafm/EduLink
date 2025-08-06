using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class BulkEnrollmentStatusUpdateDTO
    {
        public List<Guid> EnrollmentIds { get; set; } = new();
        public EnrollmentStatusEnum NewStatus { get; set; }
        public EnrollmentChangeReasonEnum ChangeReason { get; set; }
    }
}
