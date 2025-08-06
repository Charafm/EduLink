using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Common
{
    public class Document
    {
        public string Id { get; set; }
        public string Uri { get; set; } = null!;
        public byte[] Data { get; set; } = null!;
        public string MimeType { get; set; } = null!;

        public DocumentTypeEnum Type { get; set; }
        public DocumentSpec Spec { get; set; } = DocumentSpec.None;
        public DocumentStatusTypeEnum Status { get; set; }
        public string Comment { get; set; } = null!;
    }
}
