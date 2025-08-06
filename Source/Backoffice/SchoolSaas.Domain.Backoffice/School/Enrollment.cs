using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.School
{
    public class Enrollment : BaseEntity<Guid>
    {
        public string RequestCode { get; set; } = GenerateRequestCode();

        public static string GenerateRequestCode()
        {
            // 1) prefix
            const string prefix = "ENL";

            // 2) date segment
            var date = DateTime.UtcNow.ToString("yyyyMMdd");

            // 3) random 6-char alphanumeric (from a GUID)
            var rand = Guid.NewGuid()
                          .ToString("N")        // e.g. "d4e7f02c41e14b2a9a8c9e8e6b7f8ab0"
                          .Substring(0, 6)      // e.g. "d4e7f0"
                          .ToUpperInvariant();  // e.g. "D4E7F0"

            return $"{prefix}-{date}-{rand}";
        }

        public Guid StudentId { get; set; }

       
        public Guid BranchId { get; set; }

       
        public EnrollmentStatusEnum Status { get; set; }

        
        public DateTime SubmittedAt { get; set; }

        
        public string? AdminComment { get; set; }

        
        public Student Student { get; set; }
        public Branch Branch { get; set; }
    }

}
