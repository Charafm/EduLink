using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class EnrollmentDocumentVerificationDTO
    {
        // New verification status (e.g., Verified, Rejected)
        public VerificationStatusEnum VerificationStatus { get; set; }
        // Optional comment or feedback regarding the verification decision
        public string VerificationComment { get; set; }
    }

}
