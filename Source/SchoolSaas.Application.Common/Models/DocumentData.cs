using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Application.Common.Models
{
    public class DocumentData
    {
        public Guid Id { get; set; }
        public Guid RefId { get; set; }
        public string? Uri { get; set; }
        public string? MimeType { get; set; }
        public DocumentTypeEnum Type { get; set; }
        public DocumentSpec Spec { get; set; }
        public byte[]? Data { get; set; }

        public DocumentData() { }
        public DocumentData(AbstractDocument document)
        {
            Id = document.Id;
        }
    }
}
