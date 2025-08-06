using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Application.Common.Models
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public DocumentTypeEnum Type { get; set; }
        public string? DocumentCode { get; set; }
    }
}
