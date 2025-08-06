using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.School
{
    public class TransferDocument : BaseEntity<Guid>
    {
        public Guid TransferId { get; set; }

        // File path or URL where the document is stored, 
        public string FilePath { get; set; }


        public DocumentTypeEnum DocumentType { get; set; }


        public DateTime UploadedAt { get; set; }


        public string? Remarks { get; set; }


        public TransferRequest TransferRequest { get; set; }
    }
}
