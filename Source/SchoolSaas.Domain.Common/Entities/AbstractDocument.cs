using SchoolSaas.Domain.Common.Enums;


namespace SchoolSaas.Domain.Common.Entities
{
    public abstract class AbstractDocument : BaseEntity<Guid>
    {
        public string? Uri { get; set; } = null!;
        public byte[]? Data { get; set; } = null!;

        public string MimeType { get; set; } = null!;
        public bool? IsGenerated { get; set; }

        public Guid? ParentId { get; set; }

        public DocumentTypeEnum Type { get; set; }
        public DocumentSpec Spec { get; set; } = DocumentSpec.None;
        public DocumentStatusTypeEnum Status { get; set; }
        public string? Comment { get; set; } = null!;
        public string? DocumentCode { get; set; }

    }
}