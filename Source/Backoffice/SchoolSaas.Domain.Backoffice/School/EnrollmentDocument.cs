using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.School
{
    public class EnrollmentDocument : BaseEntity<Guid>
    {
        public Guid EnrollmentId { get; set; }

        // File path or URL where the document is stored, 
        public string FilePath { get; set; }

        
        public DocumentTypeEnum DocumentType { get; set; }
        public VerificationStatusEnum VerificationStatus { get; set; } = VerificationStatusEnum.Pending;
        
        public DateTime UploadedAt { get; set; }


        public string? VerificationComment { get; set; }

       
        public Enrollment Enrollment { get; set; }
    }
  
}
