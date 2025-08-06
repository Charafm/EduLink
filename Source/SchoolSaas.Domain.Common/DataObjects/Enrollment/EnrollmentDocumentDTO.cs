using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class EnrollmentDocumentDTO
    {
        public Guid EnrollmentId { get; set; }
        public string FilePath { get; set; }
        public EnrollmentDocumentTypeEnum DocumentType { get; set; } 
        public DateTime UploadedAt { get; set; }
        public string? Remarks { get; set; }

    }
    public class EnrollmentDocumentUploadDTO
    {
        public Guid EnrollmentId { get; set; }
        public DocumentTypeEnum DocumentType { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
    }

}
