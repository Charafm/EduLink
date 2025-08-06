using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Common
{
    public class DocumentDTO
    {
        public DocumentTypeEnum DocumentType { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
        public VerificationStatusEnum VerificationStatus { get; set; }
    }
}
