using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class EnrollmentStatusUpdateDTO
    {
        public EnrollmentStatusEnum NewStatus { get; set; }
        public string? AdminComment { get; set; }
        public EnrollmentChangeReasonEnum ChangeReason { get; set; }
    }
}
